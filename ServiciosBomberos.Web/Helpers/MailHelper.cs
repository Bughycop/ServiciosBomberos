namespace ServiciosBomberos.Web.Helpers
{
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Configuration;
    using MimeKit;

    public class MailHelper : IMailHelper
    {
        #region Atributos
        private readonly IConfiguration configuration;

        #endregion

        #region Constructores
        public MailHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion

        #region Metodos
        public void SendMail(string to, string subject, string body)
        {
            var from = this.configuration["Mail:From"];
            var smtp = this.configuration["Mail:Smtp"];
            var port = this.configuration["Mail:Port"];
            var password = this.configuration["Mail:Password"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(string.Empty,from));
            message.To.Add(new MailboxAddress(string.Empty, to));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();

            using(var client = new SmtpClient())
            {
                client.CheckCertificateRevocation = false;
                client.Connect(smtp, int.Parse(port));
                client.Authenticate(from, password);
                client.Send(message);
                client.Disconnect(true);
            }
        } 
        #endregion
    }
}
