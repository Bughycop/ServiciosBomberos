namespace ServiciosBomberos.UIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class AddSalidaViewModel : BaseViewModel
    {
        #region Atributos
        private bool isRunning;
        private bool isEnabled;
        private ObservableCollection<Bombero> bomberos;
        private ObservableCollection<Tipo> tipos;
        private Tipo tipoCombo;
        private Bombero bomberoCombo1;
        private Bombero bomberoCombo2;
        private readonly ApiService apiService;
        #endregion
        #region Propiedades

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

        public DateTime DiaSalida { get; set; }

        //public string Bombero1 { get; set; }

        public bool EsReten1 { get; set; }

        public string Bombero2 { get; set; }

        public bool EsReten2 { get; set; }

        public TimeSpan HoraSalida { get; set; }

        public TimeSpan HoraRegreso { get; set; }

        //public string TipoSalida { get; set; }

        public string Descripcion { get; set; }

        public ICommand SaveSalidaCommand => new RelayCommand(this.Save);

        #endregion

        #region Constructores
        public AddSalidaViewModel()
        {
            this.DiaSalida = DateTime.Now.Date;
            this.apiService = new ApiService();
            this.isEnabled = true;
            this.LoadBomberos();
            this.LoadTipos();
        }

        #endregion

        #region Metodos
        private async void Save()
        {

            if (BomberoCombo1 == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "El primer Bombero es obligatorio",
                    "Aeptar");
                return;
            }

            if (HoraSalida == TimeSpan.Zero)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Por favor indique hora de inicio",
                    "Aceptar");
                return;
            }

            if (HoraRegreso == TimeSpan.Zero)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Por favor indique hora de finalización",
                    "Aceptar");
                return;
            }

            if (TipoCombo == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Debe elegir un tipo de servicio",
                    "Aceptar");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var horaRegreso = new DateTime().Date;
            if (HoraRegreso < HoraSalida)
            {
                horaRegreso = this.DiaSalida.AddDays(1) + this.HoraRegreso;
            }
            else
            {
                horaRegreso = this.DiaSalida + this.HoraRegreso;
            }

            
            if (BomberoCombo2==null)
            {
                Bombero2 = string.Empty;
            }

            else
            {
                Bombero2 = BomberoCombo2.Value;
            }

            var salida = new Salida
            {
                DiaSalida = this.DiaSalida,
                Bombero1 = this.BomberoCombo1.Value,
                EsReten1 = this.EsReten1,
                Bombero2 = Bombero2,
                EsReten2 = this.EsReten2,
                HoraSalida = DiaSalida + this.HoraSalida,
                HoraRegreso = horaRegreso,
                TipoSalida = this.TipoCombo.Nombre,
                Descripcion = this.Descripcion,
                User = new User { Email = MainViewModel.GetInstance().UserEmail }
            };

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.PostAsync(
                url,
                "/api",
                "/Salidas",
                salida,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    response.Message,
                    "Aceptar");
                return;
            }

            var nuevaSalida = (Salida)response.Result;
            MainViewModel.GetInstance().Salidas.AdSalidaToList(nuevaSalida);

            this.IsRunning = false;
            this.isEnabled = true;
            await App.Navigator.PopAsync();
        }

        private async void LoadBomberos()
        {

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
                    "ERROR",
                    response.Message,
                    "Aceptar");
                return;
            }

            var myBomberos = (List<Bombero>)response.Result;
            this.Bomberos = new ObservableCollection<Bombero>(myBomberos);
        }
        private async void LoadTipos()
        {
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
                    "ERROR",
                    response.Message,
                    "Aceptar");
                return;
            }

            var myTipos = (List<Tipo>)response.Result;
            this.Tipos = new ObservableCollection<Tipo>(myTipos);
        }


        #endregion
    }
}
