namespace ServiciosBomberos.UIForms.ViewModels
{
    using Common.Models;
    using ServiciosBomberos.Common.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class TiposViewModel : BaseViewModel
    {
        #region Atributos
        private readonly ApiService apiService;
        private List<Tipo> myTipos;
        private ObservableCollection<TipoItemViewModel> tipos;
        private bool isRefreshing;
        #endregion
        #region Propiedades
        public ObservableCollection<TipoItemViewModel> Tipos
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

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.GetListAsync<Tipo>(
                url,
                "/api",
                "/Tipos",
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

            this.myTipos = (List<Tipo>)response.Result;
            this.RefreshTiposList();
            this.IsRefreshing = false;

        }

        public void AddTipoToList(Tipo tipo)
        {
            this.myTipos.Add(tipo);
            this.RefreshTiposList();
        }

        public void UpdateTipoInList(Tipo tipo)
        {
            var previousTipo = this.myTipos.Where(t => t.Id == tipo.Id).FirstOrDefault();
            if (previousTipo!=null)
            {
                this.myTipos.Remove(previousTipo);
            }
            this.myTipos.Add(tipo);
            this.RefreshTiposList();
        }

        public void DeleteTipoinList(int tipoId)
        {
            var previousTipo = this.myTipos.Where(t => t.Id == tipoId).FirstOrDefault();
            if (previousTipo!=null)
            {
                this.myTipos.Remove(previousTipo);
            }
            this.RefreshTiposList();
        }

        private void RefreshTiposList()
        {
            this.Tipos = new ObservableCollection<TipoItemViewModel>(
                this.myTipos.Select(t => new TipoItemViewModel
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    Prioridad = t.Prioridad,
                    User = t.User
                })
                .OrderBy(t => t.Nombre)
                .ToList());
        }
        #endregion
    }
}