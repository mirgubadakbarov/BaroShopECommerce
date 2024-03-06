using Core.Constants;
using Core.Entities;
using Core.Utilities.FileService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Common;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Account;

namespace Web.Areas.Admin.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly ModelStateDictionary _modelState;
        public AccountService(SignInManager<User> signInManager,
          UserManager<User> userManager, IActionContextAccessor actionContextAccessor,
          IFileService fileService,
          IEmailService emailService
          )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _fileService = fileService;
            _emailService = emailService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> LoginAsync(AccountLoginVM model)
        {
            if (!_modelState.IsValid) return false;
            var userAdmin = await _userManager.FindByNameAsync(model.Username);
            if (userAdmin == null)
            {
                _modelState.AddModelError(string.Empty, "Username or password was wrong");
                return false;
            }
            if (!await _userManager.IsInRoleAsync(userAdmin, UserRoles.Admin.ToString()))
            {
                _modelState.AddModelError(string.Empty, "Username or password was wrong");
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (!result.Succeeded)
            {
                _modelState.AddModelError(string.Empty, "Username or password was wrong");
                return false;
            }
            return true;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> ComfirmEmailAsync(string userId, string token)
        {
            if (userId == null || token == null) return false;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return false;

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            return true;
        }

        public async Task<bool> ResetPasswordTokenAsync(string link, ForgotPasswordVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            string path = "wwwroot/templates/resetpasswordverify.html";
            string body = string.Empty;
            string subject = "Verify Password Reset";

            body = _fileService.ReadFile(path, body);

            body = body.Replace("{{link}}", link);

            body = body.Replace("{{fullname}}", user.Fullname);

            _emailService.Send(user.Email, subject, body);
            return true;
        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordVM resetPasswordVM)
        {
            if (!_modelState.IsValid)
            {
                _modelState.AddModelError(string.Empty, "Password is Required");
                return false;
            }


            var existUser = await _userManager.FindByIdAsync(resetPasswordVM.Id);
            if (existUser == null)
            {
                _modelState.AddModelError(string.Empty, "User cannot found");
                return false;
            }

            if (await _userManager.CheckPasswordAsync(existUser, resetPasswordVM.Password))
            {
                _modelState.AddModelError(string.Empty, "New password cant be same with old password");
                return false;
            }
            await _userManager.ResetPasswordAsync(existUser, resetPasswordVM.Token, resetPasswordVM.Password);
            return true;
        }

        public async Task<bool> ForgotPasswordFindUserAsync(ForgotPasswordVM forgotPasswordVM)
        {
            if (!_modelState.IsValid) return false;

            var userExist = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);
            if (userExist == null)
            {
                _modelState.AddModelError("Email", "User Not Found");
                return false;
            }

            var resetVM = new ResetPasswordVM
            {
                Id = userExist.Id,
            };



            string token = await _userManager.GeneratePasswordResetTokenAsync(userExist);

            forgotPasswordVM.Token = token;
            forgotPasswordVM.Id = userExist.Id;

            return true;

        }

    }
}
