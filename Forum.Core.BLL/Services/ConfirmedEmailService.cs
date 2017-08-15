using Forum.Core.BLL.Interfaces;
using System.Net.Mail;

namespace Forum.NewBLL.Services
{
    public class ConfirmedEmailService : IConfirmedEmailSender
    {
        const string SMTP_SERVER = "smtp.gmail.com";
        const int SMTP_PORT = 587;
        public void SendConfirmationMessage(MailMessage message, string pass)
        {
            SmtpClient smtp = new SmtpClient(SMTP_SERVER, SMTP_PORT);
            smtp.Credentials = new System.Net.NetworkCredential(message.From.Address, pass);
            smtp.EnableSsl = true;
            smtp.Send(message);
        }
    }
}
