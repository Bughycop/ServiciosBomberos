namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Helpers;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using ServiciosBomberos.UIForms.Helpers;
    using Xamarin.Forms;

    public class RememberPasswordViewModel : BaseViewModel
    {
        #region Atributos
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;
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

        public string Email { get; set; }
        #endregion

        #region Comandos
        public ICommand RecoverCommand => new RelayCommand(this.Recover);
        #endregion

        #region Constructores
        public RememberPasswordViewModel()
        {
            this.apiService = new ApiService();
            this.isEnabled = true;
        }
        #endregion

        #region Metodos
        private async void Recover()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.SuEmailLbl,
                    Languages.AcceptLbl);
                return;
            }

            if (!RegExHelper.IsValidEmail(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.ValidEmailLbl,
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

            this.isRunning = true;
            this.isEnabled = false;

            var request = new RecoverPasswordRequest
            {
                Email = this.Email
            };

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.RecoverPasswordAsync(
                url,
                "/api",
                "Account/RecoverPassword",
                request);

            this.isRunning = false;
            this.isEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    response.Message,
                    Languages.AcceptLbl);
            }

            await Application.Current.MainPage.DisplayAlert(
                Languages.OkLbl,
                response.Message,
                Languages.AcceptLbl);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion
    }
}
