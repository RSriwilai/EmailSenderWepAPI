using System.Threading.Tasks;

namespace EmailSender.Interface
{
    public interface IEmailSenderService
    {
        Task<string> SendEmailAsync(string recipientEmail, string subject, string mesasge);
    }
}
