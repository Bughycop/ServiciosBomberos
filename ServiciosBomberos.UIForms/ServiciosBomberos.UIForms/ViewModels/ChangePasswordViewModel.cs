namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Helpers;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using UIForms.Helpers;
    using Xamarin.Forms;

    public class ChangePasswordViewModel : BaseViewModel
    {
        #region Atributos
        private readonly ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
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

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
        #endregion

        #region Comandos
        public ICommand ChangePasswordCommand => new RelayCommand(this.ChangePassword);
        #endregion

        #region Constructores
        public ChangePasswordViewModel()
        {
            this.apiService = new ApiService();
            this.isEnabled = true;
        }
        #endregion

        #region Metodos

        private async void ChangePassword()
        {

            if (string.IsNullOrEmpty(CurrentPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.PasswordActualLbl,
                    Languages.AcceptLbl);
                return;
            }

            if (!MainViewModel.GetInstance().USerPassword.Equals(this.CurrentPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.PasswordIncorrectLbl,
                    Languages.AcceptLbl);
                return;
            }

            if (string.IsNullOrEmpty(this.NewPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.PasswordNewLbl,
                    Languages.AcceptLbl);
                return;
            }

            if (NewPassword.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.PasswordLengthLbl,
                    Languages.AcceptLbl);
                return;
            }

            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.PasswordConfirmLbl,
                    Languages.AcceptLbl);
                return;
            }

            if (!this.NewPassword.Equals(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.PasswordMatchLbl,
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

            var request = new ChangePasswordRequest
            {
                Email = MainViewModel.GetInstance().UserEmail,
                NewPassword = this.NewPassword,
                OldPassword = this.CurrentPassword
            };

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.ChangePasswordAsync(
                url,
                "/api",
                "/Account/ChangePassword",
                request,
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

            MainViewModel.GetInstance().USerPassword = this.NewPassword;
            Settings.UserPassword = this.NewPassword;

            await Application.Current.MainPage.DisplayAlert(
                Languages.OkLbl,
                response.Message,
                Languages.AcceptLbl);

            await App.Navigator.PopAsync();
        }

        #endregion
    }
}
