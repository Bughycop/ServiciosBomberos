namespace ServiciosBomberos.UIForms.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Security.Cryptography;
    using Common.Models;
    using ServiciosBomberos.Common.Services;

    public class SalidasViewModel
    {
        #region Atributos
        private ApiService apiService;
        #endregion
        #region Propiedades
        public ObservableCollection<Salida> MyProperty { get; set; }
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
            var response = await this.apiService.GetListAsync<Salida>(
                "https://serviciosbomberos.azurewebsites.net",
                "/api",
                "/Salidas");
        }
        #endregion

    }
}
