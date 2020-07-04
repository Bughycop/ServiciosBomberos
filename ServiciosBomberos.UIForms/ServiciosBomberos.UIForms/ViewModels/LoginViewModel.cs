namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Helpers;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using Views;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Atributos
        private readonly ApiService apiService;
        private bool iSRunning;
        private bool isEnabled;
        #endregion

        #region Propiedades
        public bool IsRemember { get; set; }

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
            //this.Email = "bughybombero@gmail.com";
            //this.Password = "123456";
            this.IsEnabled = true;
            this.IsRemember = true;
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
            this.IsEnabled = false;

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

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR", "Correo o Contraseña incorrecto", "Accept");
                return;
            }

            var token = (TokenResponse)response.Result;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.UserEmail = this.Email;
            mainViewModel.USerPassword = this.Password;
            mainViewModel.Token = token;
            mainViewModel.Tipos = new TiposViewModel();

            Settings.IsRemember = this.IsRemember;
            Settings.UserEmail = this.Email;
            Settings.UserPassword = this.Password;
            Settings.Token = JsonConvert.SerializeObject(token);

            Application.Current.MainPage = new MasterPage();
        }

        #endregion
    }
}
