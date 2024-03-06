namespace Web.Areas.Admin.ViewModels.Message
{
    public class MessageDetailsVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime RecieveTime { get; set; }
    }
}
