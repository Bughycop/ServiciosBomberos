namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class SalidasViewModel : BaseViewModel
    {
        #region Atributos
        private readonly ApiService apiService;
        private ObservableCollection<Salida> salidas;
        private bool isRefreshing;
        #endregion

        #region Propiedades
        public ObservableCollection<Salida> Salidas
        {
            get => this.salidas;
            set => this.SetValue(ref this.salidas, value);
        }

        public bool IsRefreshing 
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }
        #endregion

        #region Constructores
        public SalidasViewModel()
        {
            this.apiService = new ApiService();
            this.LoadSalidas();
        }


        #endregion

        #region Metodos
        private async void LoadSalidas()
        {
            this.IsRefreshing = true;

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.GetListAsync<Salida>(
                url,
                "/api",
                "/Salidas",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    response.Message,
                    "Accept");
                return;
            }

            var mySalidas = (List<Salida>)response.Result;
            this.Salidas = new ObservableCollection<Salida>(mySalidas);
        }
        #endregion

        #region Comandos
        public ICommand RefreshCommand 
        {
            get 
            {
                return new RelayCommand(LoadSalidas);
            }
        }
        #endregion
    }
}
