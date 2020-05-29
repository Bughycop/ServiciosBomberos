namespace ServiciosBomberos.UIForms.ViewModels
{
    using Common.Models;
    using ServiciosBomberos.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class TiposViewModel : BaseViewModel
    {
        #region Atributos
        private readonly ApiService apiService;
        private ObservableCollection<Tipo> tipos;
        private bool isRefreshing;
        #endregion
        #region Propiedades
        public ObservableCollection<Tipo> Tipos
        {
            get => this.tipos;
            set => this.SetValue(ref this.tipos, value);
        }

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }
        #endregion
        #region Constructores
        public TiposViewModel()
        {
            this.apiService = new ApiService();
            this.LoadTipos();
        }

        #endregion
        #region Metodos
        private async void LoadTipos()
        {
            this.IsRefreshing = true;

            var response = await this.apiService.GetListAsync<Tipo>(
                "https://serviciosbomberos.azurewebsites.net",
                "/api",
                "/Tipos");

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    response.Message,
                    "Accept");
                return;
            }

            var myTipos = (List<Tipo>)response.Result;
            this.Tipos = new ObservableCollection<Tipo>(myTipos);
        }
        #endregion
    }
}