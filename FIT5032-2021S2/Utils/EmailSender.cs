using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032_2021S2.Utils
{
    public class EmailSender
    {
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("qingyunxu.au@outlook.com", "Teamo");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
    }
}