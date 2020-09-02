namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Helpers;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Newtonsoft.Json;
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
            this.IsEnabled = true;

        }
        #endregion

        #region Metodos
        private async void Save()
        {
            if (string.IsNullOrEmpty(this.User.Nombre))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.RequiredFirstNameLbl,
                    Languages.AcceptLbl);
                return;
            }
            if (string.IsNullOrEmpty(this.User.PrimerApellido))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.RequiredSecondNameLbl,
                    Languages.AcceptLbl);
                return;
            }
            if (string.IsNullOrEmpty(this.User.PhoneNumber))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.RequiredTelephoneNumberLbl,
                    Languages.AcceptLbl);
                return;
            }

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    connection.Message,
                    Languages.AcceptLbl);
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
                    Languages.ErrorLbl,
                    response.Message,
                    Languages.AcceptLbl);
                return;
            }

            MainViewModel.GetInstance().User = this.User;
            Settings.User = JsonConvert.SerializeObject(this.User);

            await Application.Current.MainPage.DisplayAlert(
                Languages.OkLbl,
                Languages.UserChangedLbl,
                Languages.AcceptLbl);
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
