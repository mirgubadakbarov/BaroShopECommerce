using Core.Entities;
using Core.Utilities.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Org.BouncyCastle.Bcpg;
using Web.Services.Abstract;
using Web.ViewModels.Account;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;

        public AccountController(IAccountService accountService,
            UserManager<User> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }
        [OnlyAnonimous]
        public IActionResult Login()
        {
            return View();
        }


        [OnlyAnonimous]
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            var isSucceded = await _accountService.LoginAsync(model);
            if (!string.IsNullOrEmpty(model.ReturnUrl)) return Redirect(model.ReturnUrl);
            if (isSucceded) return RedirectToAction("index", "home");
            return View(model);
        }



        [OnlyAnonimous]
        public async Task<IActionResult> Register()
        {
            return View();
        }


        [OnlyAnonimous]
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            var isSucceded = await _accountService.RegisterAsync(model);
            if (isSucceded)
            {
                string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = model.UserId, model.Token }, Request.Scheme, Request.Host.ToString());
                var isConfirmed = await _accountService.ConfirmationTokenEmailAsync(link, model);
                if (isConfirmed) return RedirectToAction("VerifyEmail", "Account");
            }
            return View(model);
        }

        [HttpGet]
        [OnlyAnonimous]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var userIsExist = await _accountService.ForgotPasswordFindUserAsync(forgotPasswordVM);
            if (!userIsExist) return View(forgotPasswordVM);
            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = forgotPasswordVM.Id, forgotPasswordVM.Token },
             Request.Scheme);
            await _accountService.ResetPasswordTokenAsync(link, forgotPasswordVM);

            return RedirectToAction(nameof(VerifyEmail));
        }
        [HttpGet]
        [OnlyAnonimous]
        public IActionResult ResetPassword(string userId, string token) => View(new ResetPasswordVM { Token = token, Id = userId });


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            var isSucceded = await _accountService.ResetPasswordAsync(resetPasswordVM);
            if (isSucceded) return RedirectToAction(nameof(Login));
            return View(resetPasswordVM);
        }


        [HttpGet]
        public async Task<IActionResult> VerifyEmail()
        {
            return View();
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var isConfirmed = await _accountService.ConfirmEmailAsync(userId, token);
            if (isConfirmed) return RedirectToAction("index", "home");
            return BadRequest();
        }

        #region LogOut       
        public async Task<IActionResult> LogOut()
        {
            await _accountService.LogOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
    }


}
