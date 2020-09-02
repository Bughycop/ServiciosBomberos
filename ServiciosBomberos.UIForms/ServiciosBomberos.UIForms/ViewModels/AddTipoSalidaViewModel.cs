namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using ServiciosBomberos.UIForms.Helpers;
    using Xamarin.Forms;

    public class AddTipoSalidaViewModel : BaseViewModel
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

        public string Nombre { get; set; }

        public string Prioridad { get; set; }

        public ICommand SaveTipoCommand => new RelayCommand(this.Save);

        #endregion

        #region Constructor
        public AddTipoSalidaViewModel()
        {
            this.apiService = new ApiService();
            this.isEnabled = true;
        }
        #endregion

        #region Metodos
        private async void Save()
        {

            if (string.IsNullOrEmpty(this.Nombre))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.InsertNameLbl,
                    Languages.AcceptLbl);
                return;
            }

            if (string.IsNullOrEmpty(this.Prioridad))
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

            this.IsRunning = true;
            this.IsEnabled = false;

            var tipo = new Tipo
            {
                Nombre = this.Nombre,
                Prioridad = this.Prioridad,
                User = new User { Email = MainViewModel.GetInstance().UserEmail }
            };

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.PostAsync(
                url,
                "/api",
                "/Tipos",
                tipo,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl, 
                    response.Message, 
                    Languages.AcceptLbl);
                return;
            }

            var newTipo = (Tipo)response.Result;
            MainViewModel.GetInstance().Tipos.AddTipoToList(newTipo);

            this.IsRunning = false;
            this.IsEnabled = true;
            await App.Navigator.PopAsync();
        }

        #endregion
    }
}
