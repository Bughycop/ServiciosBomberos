namespace ServiciosBomberos.Web.Helpers
{
    public interface IMailHelper
    {
        #region Metodos
        void SendMail(string to, string subject, string body);
        #endregion
    }
}
