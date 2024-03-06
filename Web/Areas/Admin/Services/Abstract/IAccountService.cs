using Web.Areas.Admin.ViewModels.Account;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(AccountLoginVM model);
        Task LogOutAsync();
        Task<bool> ComfirmEmailAsync(string userId, string token);
        Task<bool> ResetPasswordTokenAsync(string link, ForgotPasswordVM model);
        Task<bool> ResetPasswordAsync(ResetPasswordVM resetPasswordVM);
        Task<bool> ForgotPasswordFindUserAsync(ForgotPasswordVM forgotPasswordVM);
    }
}
