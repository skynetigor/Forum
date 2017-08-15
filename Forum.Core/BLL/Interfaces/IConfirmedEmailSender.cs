using System.Net.Mail;

namespace Forum.Core.BLL.Interfaces
{
    public interface IConfirmedEmailSender
    {
        void SendConfirmationMessage(MailMessage message, string pass);
    }
}
