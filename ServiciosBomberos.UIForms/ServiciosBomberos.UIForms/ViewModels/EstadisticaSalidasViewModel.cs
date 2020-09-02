namespace ServiciosBomberos.UIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Common.Models;
    using ServiciosBomberos.UIForms.Helpers;
    using Xamarin.Forms;

    public class EstadisticaSalidasViewModel : BaseViewModel
    {
        #region Atributos
        private ObservableCollection<ModeloEstadisticoSalidas> estadisticaSalidas;
        private List<Salida> listaEstadisticaSalidas;
        #endregion
        #region Propiedades
        public ObservableCollection<ModeloEstadisticoSalidas> EstadisticaSalidas 
        {
            get => this.estadisticaSalidas;
            set => this.SetValue(ref this.estadisticaSalidas, value);
        }

        #endregion

        #region Constructores
        public EstadisticaSalidasViewModel()
        {
            this.FillLista();
        }

        #endregion
        #region Metodos
        private void FillLista()
        {
            var filtro = MainViewModel.GetInstance().Salidas.FilterMonth;
            if (filtro!=-1 & filtro!=0)
            {
                this.listaEstadisticaSalidas = new List<Salida>(MainViewModel.GetInstance().Salidas.mySalidas.Select(
                    s => new Salida
                    {
                        TipoSalida = s.TipoSalida,
                        DiaSalida = s.DiaSalida,
                        Bombero1 = s.Bombero1,
                        Bombero2 = s.Bombero2,
                        EsReten1 = s.EsReten1,
                        EsReten2 = s.EsReten2,
                        HoraRegreso = s.HoraRegreso,
                        HoraSalida = s.HoraSalida,
                        Descripcion = s.Descripcion,
                        Id = s.Id,
                        User = s.User
                    }).Where(s => s.DiaSalida.Month == filtro)
                      .ToList());
            }
            else
            {
                this.listaEstadisticaSalidas = new List<Salida>(MainViewModel.GetInstance().Salidas.mySalidas.Select(
                  s => new Salida
                  {
                      TipoSalida = s.TipoSalida,
                      DiaSalida = s.DiaSalida,
                      Bombero1 = s.Bombero1,
                      Bombero2 = s.Bombero2,
                      EsReten1 = s.EsReten1,
                      EsReten2 = s.EsReten2,
                      HoraRegreso = s.HoraRegreso,
                      HoraSalida = s.HoraSalida,
                      Descripcion = s.Descripcion,
                      Id = s.Id,
                      User = s.User
                  }).Where(s => s.DiaSalida.Year == DateTime.Now.Year)
                    .ToList());
            }
            var estadisticaPorTipos = this.listaEstadisticaSalidas.GroupBy(s => s.TipoSalida);

                this.EstadisticaSalidas = new ObservableCollection<ModeloEstadisticoSalidas>(
                   estadisticaPorTipos.Select(
                        s => new ModeloEstadisticoSalidas
                        {
                            //
                            TipoServicio = s.Key,
                            NumeroServicios = s.Count(),

                        }).OrderBy(s => s.TipoServicio)
                        .ToList());
        }

        #endregion
    }
}
