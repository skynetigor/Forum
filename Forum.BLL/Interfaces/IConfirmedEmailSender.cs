using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Interfaces
{
    public interface IConfirmedEmailSender
    {
        void SendConfirmationMessage(MailMessage message, string pass);
    }
}
