using MailKit.Net.Smtp;
using MimeKit;

namespace AdminPanel.Models
{
    public class EmailService
    {

        public async Task SendEmailAsync(string email, string subject, string message)
        {

            var emailMsg = new MimeMessage();
            //Отправка сообщений от Администрации 
            emailMsg.From.Add(new MailboxAddress("Администрация Bilimkana AMerican", "wunata95@gmail.com"));
            //отправка сообщения на email пользователя, который регистрируется
            emailMsg.To.Add(new MailboxAddress("", email));
            emailMsg.Subject = subject;
            emailMsg.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using(var client = new SmtpClient())
            {
                //подключение по протоколу smtp, порт 465, поддержка ssl
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                //проходим аутентификацию в google аккаунте , пароль уникален, выдает сам gppgle
                await client.AuthenticateAsync("bekky.dzh@gmail.com", "wzzgqcobvpayjokm");
                await client.SendAsync(emailMsg);
                await client.DisconnectAsync(true);
            }
        }
    }
}
