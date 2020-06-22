namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using ServiciosBomberos.Common.Models;
    using ServiciosBomberos.Common.Services;
    using Views;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;
        private bool iSRunning;
        private bool isEnabled;
        #endregion

        #region Propiedades

        public bool IsRunning 
        {
            get => this.iSRunning;
            set => SetValue(ref this.iSRunning, value);
        }

        public bool IsEnabled 
        {
            get => this.isEnabled;
            set => SetValue(ref this.isEnabled, value);
        }
        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.Email = "bughycop@gmail.com";
            this.Password = "123456";
            this.isEnabled = true;
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

            this.IsRunning = true;
            this.isEnabled = false;

            var request = new TokenRequest
            {
                Password = this.Password,
                UserName = this.Email
            };

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.GetTokenAsync(
                url,
                "/Account",
                "/CreateToken",
                request);

            this.iSRunning = false;
            this.isEnabled = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR", "Correo o Contraseña incorrecto", "Accept");
                return;
            }

            var token = (TokenResponse)response.Result;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Tipos = new TiposViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new TiposPage());

            //await Application.Current.MainPage.DisplayAlert(
            //    "YUJUUUU!",
            //    "LO CONSEGUIMOS!",
            //    "Accept");

            //MainViewModel.GetInstance().Tipos = new TiposViewModel();
        }
        #endregion

    }
}
