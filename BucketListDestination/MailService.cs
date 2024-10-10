using MailKit.Net.Smtp;
using MimeKit;


namespace BucketListDestination
{
    public class MailService
    {
        private readonly string smtpServer = "sandbox.smtp.mailtrap.io";
        private readonly int smtpPort = 2525;
        private readonly string smtpUsername = "68b41fc119d90e"; // Mailtrap username
        private readonly string smtpPassword = "f9274f89766648"; // Mailtrap password
        private readonly string fromEmail = "do-not-reply@bucketlist.com"; // Sender's email
        private readonly string toEmail = "your-email@example.com"; // Receiver's email

        public void SendEmail(string subject, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("BucketList App", fromEmail));
            message.To.Add(new MailboxAddress("User", toEmail));
            message.Subject = subject;

            // Use HTML format for email body
            message.Body = new TextPart("html")
            {
                Text = $"<p>{messageBody}</p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    // Connect to Mailtrap's SMTP server using STARTTLS
                    client.Connect(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);

                    // Authenticate using your Mailtrap credentials
                    client.Authenticate(smtpUsername, smtpPassword);

                    // Send the email
                    client.Send(message);
                    Console.WriteLine("Email sent successfully through Mailtrap.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
                finally
                {
                    // Ensure the client is disconnected
                    client.Disconnect(true);
                }
            }
        }
    }
}




