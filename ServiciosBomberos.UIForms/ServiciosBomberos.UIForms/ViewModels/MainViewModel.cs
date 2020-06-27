namespace ServiciosBomberos.UIForms.ViewModels
{
    using Common.Models;
    using ServiciosBomberos.UIForms.Views;

    public class MainViewModel
    {
        #region Atributos
        private static MainViewModel instance;
        #endregion
        #region Propiedades
        public TokenResponse Token { get; set; }

        public LoginViewModel Login { get; set; }

        public SalidasViewModel Salidas { get; set; }

        public TiposViewModel Tipos { get; set; }

        #endregion

        #region Constructores
        public MainViewModel()
        {
            instance = this;
        }

        #endregion

        #region Metodos
        //Singleton
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }

        #endregion
    }
}
