namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Xamarin.Forms;

    public class LoginViewModel
    {
        #region Propiedades

        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.Email = "bughycop@gmail.com";
            this.Password = "123456";
        }

        #endregion

        #region Metodos
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "You must enter an Email...",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "You must enter an Password...",
                    "Accept");
                return;
            }

            //await Application.Current.MainPage.DisplayAlert(
            //    "YUJUUUU!",
            //    "LO CONSEGUIMOS!",
            //    "Accept");

            MainViewModel.GetInstance().Tipos = new TiposViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new TiposPage());
        }
        #endregion

    }
}
