namespace ServiciosBomberos.UIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Common.Models;
    using Common.Services;
    using Helpers;
    using Xamarin.Forms;

    public class SalidasViewModel : BaseViewModel
    {
        #region Atributos
        private readonly ApiService apiService;
        private List<MonthsYear> mes;
        //private List<Salida> mySalidas;
        private ObservableCollection<SalidaItemViewModel> salidas;
        private bool isRefreshing;
        private int filterMonth;
        #endregion

        #region Propiedades
        public List<Salida> mySalidas { get; set; }
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
        public List<MonthsYear> Mes
        {
            get => this.mes;
            set => this.SetValue(ref this.mes, value);
        }
        public int FilterMonth
        {
            get => this.filterMonth;
            set
            {
                this.SetValue(ref this.filterMonth, value);
                this.LoadSalidas();
            }
        }
        #endregion

        #region Constructores
        public SalidasViewModel()
        {
            this.apiService = new ApiService();
            this.LoadSalidas();
            this.LoadMonths();
            //this.FilterMonth = -1;
        }

        #endregion

        #region Metodos
        private async void LoadSalidas()
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
                    Languages.ErrorLbl,
                    response.Message,
                    Languages.AcceptLbl);
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
            if (previousSalida != null)
            {
                this.mySalidas.Remove(previousSalida);
            }
            this.mySalidas.Add(salida);
            this.RefreshListaSalidas();
        }

        //private void DeleteSalidaInList(int salidaId)
        //{
        //    var previousSalida = this.mySalidas.Where(s => s.Id == salidaId).FirstOrDefault();
        //    if (previousSalida != null)
        //    {
        //        this.mySalidas.Remove(previousSalida);
        //    }
        //    this.RefreshListaSalidas();

        //}

        private void RefreshListaSalidas()
        {
            if (this.FilterMonth!=-1 & this.filterMonth!=0)
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
                       }).Where(
                           s => s.DiaSalida.Month == filterMonth & s.DiaSalida.Year==DateTime.Today.Year)
                       .OrderByDescending(s => s.Id)
                       .ToList()); 
            }
            else
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
                      }).Where(s => s.DiaSalida.Year == DateTime.Today.Year)
                      .OrderByDescending(s => s.Id)
                      .ToList());
            }
        }

        private void LoadMonths()
        {
            var meses = new List<MonthsYear>
            {
                new MonthsYear
                {
                    MonthName=Languages.YearLbl
                },
                new MonthsYear
                {
                    MonthName=Languages.JanuaryLbl
                },
                new MonthsYear
                {
                    MonthName=Languages.FebruaryLbl
                },
                new MonthsYear
                {
                    MonthName=Languages.MarchLbl
                },
                new MonthsYear
                {
                    MonthName=Languages.AprilLbl
                },
                new MonthsYear
                {
                    MonthName=Languages.MayLbl
                },
                new MonthsYear
                {
                    MonthName=Languages.JuneLbl
                },
                new MonthsYear
                {
                    MonthName=Languages.JulyLbl
                },
                new MonthsYear
                {
                    MonthName=Languages.AugustLbl
                },
                new MonthsYear
                {
                    MonthName=Languages.SeptemberLbl
                },
                new MonthsYear
                {
                    MonthName=Languages.OctoberLbl
                },
                new MonthsYear
                {
                    MonthName=Languages.NovemberLbl
                },
                new MonthsYear
                {
                    MonthName=Languages.DecemberLbl
                }
            };

            this.Mes = new List<MonthsYear>(meses.Select(m => new MonthsYear
            {
                MonthName = m.MonthName
            }).ToList());
        }
        //private void IndexChanged()
        //{
        //    if (filterMonth != -1)
        //    {

        //    }
        //}
        #endregion

    }
}
