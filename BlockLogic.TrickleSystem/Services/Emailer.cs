using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace BlockLogic.TrickleSystem.Services
{
    public class Emailer
    {
        public string From { get; set; }
        public string Password { get; set; }
        public Backer Backer { get; set; }

        public Startup Startup { get; set; }

        public Milestone Milestone { get; set; }

        public Emailer()
        { }

        public Emailer(Backer Backer)
        {
            this.Backer = Backer;
        }

        public Emailer(string From, string Password, Backer Backer, Startup Startup, Milestone Milestone)
        {
            this.From = From;
            this.Password = Password;
            this.Backer = Backer;
            this.Startup = Startup;
            this.Milestone = Milestone;
        }

        public void MilestoneCompleted()
        {
            string subject = $"{Startup.Name} has completed a milestone!";
            string body = $"{Startup.Name} has completed a milestone! \nDescription: {Milestone.Description}";
            SendMail(subject, body);
        }

        public void MilestoneMissedDeadline()
        {
            string subject = $"{Startup.Name} has not completed a milestone in the promised time.";
            string body = $"{Startup.Name} has failed to complete the following milestone listed below. \nDescription: {Milestone.Description}";
            SendMail(subject, body);
        }
        public void SendMail(string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(From);
            mail.To.Add(Backer.Email);
            mail.Subject = subject;
            mail.Body = body;

            SmtpServer.Port = 587; //587, 465
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Credentials = new System.Net.NetworkCredential(mail.From.Address, Password);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
