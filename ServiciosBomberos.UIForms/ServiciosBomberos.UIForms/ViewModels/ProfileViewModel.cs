namespace ServiciosBomberos.UIForms.ViewModels
{
    using Common.Helpers;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class ProfileViewModel : BaseViewModel
    {
        #region Atributos
        private readonly ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        private User user;
        #endregion

        #region Propiedades
        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }
        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public User User
        {
            get => this.user;
            set => this.SetValue(ref this.user, value);
        }
        #endregion

        #region Comandos
        public ICommand SaveCommand => new RelayCommand(this.Save);

        public ICommand ModifyPasswordCommand => new RelayCommand(this.ModifyPassword);
        #endregion

        #region Constructores
        public ProfileViewModel()
        {
            apiService = new ApiService();
            this.user = MainViewModel.GetInstance().User;
            isEnabled = true;

        }
        #endregion

        #region Metodos
        private async void Save()
        {
            if (string.IsNullOrEmpty(this.User.Nombre))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Debe poner un Nombre",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.User.PrimerApellido))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Debe poner un Apellido",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.User.PhoneNumber))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Necesito su numero de Teléfono",
                    "Aceptar");
                return;
            }

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    connection.Message,
                    "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.PutAsync(
                url,
                "/api",
                "/Account",
                this.User,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    response.Message,
                    "Accept");
                return;
            }

            MainViewModel.GetInstance().User = this.User;
            Settings.User = JsonConvert.SerializeObject(this.User);

            await Application.Current.MainPage.DisplayAlert(
                "OK",
                "El Usuario ha sido modificado con éxito",
                "Aceptar");
            await App.Navigator.PopAsync();
        }

        private async void ModifyPassword()
        {
            MainViewModel.GetInstance().ChangePassword = new ChangePasswordViewModel();
            await App.Navigator.PushAsync(new ChangePasswordPage());
        }

        #endregion
    }
}
