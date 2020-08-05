namespace ServiciosBomberos.Common.Helpers
{
    using System;
    using System.Net.Mail;

    public static class RegExHelper
    {
        public static bool IsValidEmail(string emailAdress)
        {
            try
            {
                var mail = new MailAddress(emailAdress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
