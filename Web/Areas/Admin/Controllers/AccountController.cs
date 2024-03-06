using Core.Entities;
using Core.Utilities.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Account;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]

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


        #region Login
        [OnlyAnonimous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            var isSucceded = await _accountService.LoginAsync(model);
            if (isSucceded) return RedirectToAction("index", "homemainslider");
            return View(model);
        }

        #endregion

        #region LogOut
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _accountService.LogOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var userIsExist = await _accountService.ForgotPasswordFindUserAsync(forgotPasswordVM);
            if (!userIsExist) return View(forgotPasswordVM);
            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = forgotPasswordVM.Id, forgotPasswordVM.Token },
             Request.Scheme, Request.Host.ToString());
            await _accountService.ResetPasswordTokenAsync(link, forgotPasswordVM);

            return RedirectToAction(nameof(VerifyEmail));
        }
        [HttpGet]
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



        #endregion

    }
}
