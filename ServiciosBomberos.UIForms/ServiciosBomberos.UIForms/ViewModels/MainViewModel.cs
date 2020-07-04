namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Views;

    public class MainViewModel
    {
        #region Atributos
        private static MainViewModel instance;
        #endregion

        #region Propiedades
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public TokenResponse Token { get; set; }

        public string UserEmail { get; set; }

        public string USerPassword { get; set; }

        public LoginViewModel Login { get; set; }

        public SalidasViewModel Salidas { get; set; }

        public TiposViewModel Tipos { get; set; }

        public AddTipoSalidaViewModel AddTipoSalida { get; set; }

        public EditTipoSalidaViewModel EditTipoSalida { get; set; }

        public ICommand AddTipoSalidaCommand => new RelayCommand(this.GoAddTipoSalida);


        #endregion

        #region Constructores
        public MainViewModel()
        {

            instance = this;
            this.LoadMenus();
        }

        #endregion

        #region Metodos
        //Singleton
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }

        private void LoadMenus()
        {
            var menus = new List<Menu>
            {
                new Menu
                {
                    Icon="ic_perm_device_information",
                    PageName="AboutPage",
                    Title="Acerca de..."
                },

                new Menu
                {
                    Icon="ic_phonelink_setup",
                    PageName="SetupPage",
                    Title="Setup"
                },

                new Menu
                {
                    Icon="exit_phone",
                    PageName="LoginPage",
                    Title="Cerrar Sesión"
                },
            };

            this.Menus = new ObservableCollection<MenuItemViewModel>(menus.Select(m => new MenuItemViewModel
            {
                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title
            }).ToList());
        }

        private async void GoAddTipoSalida()
        {
            this.AddTipoSalida = new AddTipoSalidaViewModel();
            await App.Navigator.PushAsync(new AddTipoSalidaPage());
        }
        #endregion
    }
}
