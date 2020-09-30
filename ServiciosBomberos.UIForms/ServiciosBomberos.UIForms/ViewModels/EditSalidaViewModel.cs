namespace ServiciosBomberos.UIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using ServiciosBomberos.Common.Services;
    using ServiciosBomberos.UIForms.Helpers;
    using Xamarin.Forms;

    public class EditSalidaViewModel : BaseViewModel
    {
        #region Atributos
        private readonly ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        private ObservableCollection<Bombero> bomberos;
        private ObservableCollection<Tipo> tipos;
        private Tipo tipoCombo;
        private Bombero bomberoCombo1;
        private Bombero bomberoCombo2;
        private List<Bombero> myBomberos;
        private List<Tipo> myTipos;
        #endregion

        #region Propiedades
        public Salida Salida { get; set; }
        public TimeSpan EditHoraSalida { get; set; }
        public TimeSpan EditHoraRegreso { get; set; }
        public TimeSpan TiempoServicio => this.EditHoraRegreso - this.EditHoraSalida;
        public Bombero BomberoCombo1
        {
            get => this.bomberoCombo1;
            set => this.SetValue(ref this.bomberoCombo1, value);
        }

        public Bombero BomberoCombo2
        {
            get => this.bomberoCombo2;
            set => this.SetValue(ref this.bomberoCombo2, value);
        }
        public Tipo TipoCombo
        {
            get => this.tipoCombo;
            set => this.SetValue(ref this.tipoCombo, value);
        }
        public ObservableCollection<Bombero> Bomberos
        {
            get => this.bomberos;
            set => this.SetValue(ref this.bomberos, value);
        }
        public ObservableCollection<Tipo> Tipos
        {
            get => this.tipos;
            set => this.SetValue(ref this.tipos, value);
        }
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
        #endregion

        #region Comandos
        public ICommand SaveCommand => new RelayCommand(this.Save);

        #endregion
        #region Constructores
        public EditSalidaViewModel(Salida salida)
        {
            this.Salida = salida;
            this.apiService = new ApiService();
            this.isEnabled = true;
            this.EditHoraSalida = salida.HoraSalida.TimeOfDay;
            this.EditHoraRegreso = salida.HoraRegreso.TimeOfDay;
            this.LoadBomberos();
            this.LoadTipos();
        }
        #endregion

        #region Metodos
        private async void Save()
        {
            if (bomberoCombo1 == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.RequiredFiremanLbl,
                    Languages.AcceptLbl);
                return;
            }

            if (EditHoraSalida == TimeSpan.Zero)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.RequiredInitHourLbl,
                    Languages.AcceptLbl);
                return;
            }

            if (EditHoraRegreso == TimeSpan.Zero)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.RequiredFinishHourLbl,
                    Languages.AcceptLbl);
                return;
            }

            if (TipoCombo == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    Languages.ServiceTypeLbl,
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

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.PutAsync(
                url,
                "/api",
                "/Salidas",
                this.Salida.Id,
                this.Salida,
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

            var modifiedSalida = (Salida)response.Result;
            MainViewModel.GetInstance().Salidas.UpdateSalidaInList(modifiedSalida);
            await App.Navigator.PopAsync();
        }


        private async void LoadBomberos()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    connection.Message,
                    Languages.AcceptLbl);
                return;
            }

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.GetListAsync<Bombero>(
                url,
                "/api",
                "/Bomberos",
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

            this.myBomberos = (List<Bombero>)response.Result;
            this.Bomberos = new ObservableCollection<Bombero>(myBomberos);
            this.SetBombero1();
            this.SetBombero2();
        }


        private void SetBombero1()
        {
            var bombero1 = this.myBomberos.Where(b => b.Value == Salida.Bombero1).FirstOrDefault();
            if (bombero1 != null)
            {
                this.BomberoCombo1 = bombero1;
                return;
            }
        }
        private void SetBombero2()
        {
            var bombero2 = this.myBomberos.Where(b => b.Value == Salida.Bombero2).FirstOrDefault();
            if (bombero2 != null)
            {
                this.BomberoCombo2 = bombero2;
                return;
            }
        }

        private async void LoadTipos()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorLbl,
                    connection.Message,
                    Languages.AcceptLbl);
                return;
            }

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.GetListAsync<Tipo>(
                url,
                "/api",
                "/Tipos",
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

            this.myTipos = (List<Tipo>)response.Result;
            this.Tipos = new ObservableCollection<Tipo>(myTipos);
            this.SetTipo();
        }

        private void SetTipo()
        {
            var tipo = this.myTipos.Where(t => t.Nombre == Salida.TipoSalida).FirstOrDefault();
            if (tipo != null)
            {
                this.TipoCombo = tipo;
                return;
            }
        }
        #endregion
    }
}
