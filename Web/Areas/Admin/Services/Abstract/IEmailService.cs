namespace Web.Areas.Admin.Services.Abstract
{
    public interface IEmailService
    {
        void Send(string to, string subject, string body, string from = null);
    }
}
