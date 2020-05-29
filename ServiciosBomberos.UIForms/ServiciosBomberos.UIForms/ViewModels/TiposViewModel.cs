namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Common.Models;
    using ServiciosBomberos.Common.Services;
    using Xamarin.Forms;

    public class TiposViewModel : BaseViewModel
    {
        #region Atributos
        private readonly ApiService apiService;
        private ObservableCollection<Tipo> tipos;
        #endregion
        #region Propiedades
        public ObservableCollection<Tipo> Tipos 
        {
            get { return this.tipos; }
            set { this.SetValue(ref this.tipos, value); }
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
            var response = await this.apiService.GetListAsync<Tipo>(
                "https://serviciosbomberos.azurewebsites.net",
                "/api",
                "/Tipos");

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