namespace ServiciosBomberos.UIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class SalidasViewModel : BaseViewModel
    {
        #region Atributos
        private readonly ApiService apiService;
        private List<Salida> mySalidas;
        private ObservableCollection<SalidaItemViewModel> salidas;
        private bool isRefreshing;
        #endregion

        #region Propiedades
        public ObservableCollection<SalidaItemViewModel> Salidas
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
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    connection.Message,
                    "Aceptar");
                return;
            }

            this.IsRefreshing = true;

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.GetListAsync<Salida>(
                url,
                "/api",
                "/Salidas",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    response.Message,
                    "Accept");
                return;
            }

            this.mySalidas = (List<Salida>)response.Result;
            this.RefreshListaSalidas();
            this.IsRefreshing = false;
        }

        public void AdSalidaToList(Salida salida)
        {
            this.mySalidas.Add(salida);
            this.RefreshListaSalidas();
        }

        public void UpdateSalidaInList(Salida salida)
        {
            var previousSalida = this.mySalidas.Where(s => s.Id == salida.Id).FirstOrDefault();
            if (previousSalida!=null)
            {
                this.mySalidas.Remove(previousSalida);
            }
            this.mySalidas.Add(salida);
            this.RefreshListaSalidas();
        }

        private void DeleteSalidaInList(int salidaId)
        {
            var previousSalida = this.mySalidas.Where(s => s.Id == salidaId).FirstOrDefault();
            if (previousSalida != null)
            {
                this.mySalidas.Remove(previousSalida);
            }
            this.RefreshListaSalidas();

        }

        private void RefreshListaSalidas()
        {
            this.Salidas = new ObservableCollection<SalidaItemViewModel>(
                this.mySalidas.Select(s => new SalidaItemViewModel
                {
                    Id = s.Id,
                    DiaSalida = s.DiaSalida,
                    Bombero1 = s.Bombero1,
                    EsReten1 = s.EsReten1,
                    Bombero2 = s.Bombero2,
                    EsReten2 = s.EsReten2,
                    HoraSalida = s.HoraSalida,
                    HoraRegreso = s.HoraRegreso,
                    TipoSalida = s.TipoSalida,
                    Descripcion = s.Descripcion,
                    User = s.User
                }).OrderByDescending(s => s.Id).ToList());
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
