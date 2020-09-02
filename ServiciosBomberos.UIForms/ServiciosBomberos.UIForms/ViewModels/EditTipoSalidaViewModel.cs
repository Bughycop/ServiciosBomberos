namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Xamarin.Forms;

    public class EditTipoSalidaViewModel : BaseViewModel
    {
        #region Atributos
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;
        #endregion
        #region Propiedades
        public Tipo Tipo { get; set; }
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

        public ICommand SaveCommand => new RelayCommand(this.Save);

        public ICommand DeleteCommand => new RelayCommand(this.Delete);

        #endregion
        #region Constructores
        public EditTipoSalidaViewModel(Tipo tipo)
        {
            this.Tipo = tipo;
            this.apiService = new ApiService();
            this.isEnabled = true;
        }
        #endregion
        #region Metodos
        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Tipo.Nombre))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.ServiceTypeLbl,
                    Languages.AcceptLbl);
                return;
            }

            if (string.IsNullOrEmpty(this.Tipo.Prioridad))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.ChooseCategoryLbl,
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

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.PutAsync(
                url,
                "/api",
                "/Tipos",
                this.Tipo.Id,
                this.Tipo,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.isRunning = false;
            this.isEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    response.Message,
                    Languages.AcceptLbl);
                return;
            }

            var modifiedTipo = (Tipo)response.Result;
            MainViewModel.GetInstance().Tipos.UpdateTipoInList(modifiedTipo);
            await App.Navigator.PopAsync();
        }

        private async void Delete()
        {
            var confirm = await Application.Current.MainPage.DisplayAlert(
                Languages.ConfirmLbl,
                Languages.SureDeleteLbl,
                Languages.YesLbl,
                Languages.NoLbl);

            if (!confirm)
            {
                return;
            }

            this.isRunning = true;
            this.isEnabled = false;

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.DeleteAsync(
                url,
                "/api",
                "/Tipos",
                this.Tipo.Id,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.isRunning = false;
            this.isEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    response.Message,
                    Languages.AcceptLbl);
                return;
            }

            MainViewModel.GetInstance().Tipos.DeleteTipoinList(this.Tipo.Id);
            await App.Navigator.PopAsync();
        }

        #endregion
    }
}
