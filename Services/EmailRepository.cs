using System.Net;
using System.Net.Mail;

namespace DoAn_API.Services
{
    public class EmailRepository
    {

        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "taodechoigamehihi@gmail.com";
        private readonly string _smtpPassword = "xkze hiaa mtjh wmso";

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(_smtpUser);
                    message.To.Add(new MailAddress(to));
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true; // Set to false if you're sending plain text

                    using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPassword);
                        smtpClient.EnableSsl = true;
                        await smtpClient.SendMailAsync(message);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error
                throw new Exception($"Failed to send email: {ex.Message}", ex);
            }
        }


    }
}
