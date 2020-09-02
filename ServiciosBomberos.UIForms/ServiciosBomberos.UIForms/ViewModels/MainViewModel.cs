namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using ServiciosBomberos.UIForms.Helpers;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Views;

    public class MainViewModel
    {
        #region Atributos
        private static MainViewModel instance;
        #endregion

        #region Propiedades

        public User User { get; set; }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public TokenResponse Token { get; set; }

        public string UserEmail { get; set; }

        public string USerPassword { get; set; }

        public LoginViewModel Login { get; set; }

        public SalidasViewModel Salidas { get; set; }

        public EstadisticaSalidasViewModel Estadistica { get; set; }

        public TiposViewModel Tipos { get; set; }

        public AddTipoSalidaViewModel AddTipoSalida { get; set; }

        public EditTipoSalidaViewModel EditTipoSalida { get; set; }

        public AddSalidaViewModel AddSalida { get; set; }

        public EditSalidaViewModel EditSalida { get; set; }

        public RememberPasswordViewModel RememberPassword { get; set; }

        public ProfileViewModel Profile { get; set; }

        public ChangePasswordViewModel ChangePassword { get; set; }

        public ICommand AddTipoSalidaCommand => new RelayCommand(this.GoAddTipoSalida);

        public ICommand AddSalidaCommand => new RelayCommand(this.GoAddSalida);


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
                    Title=Languages.AboutLbl
                },

                new Menu
                {
                    Icon="ic_person_add",
                    PageName="ProfilePage",
                    Title= Languages.ModifyUserLbl
                },

                new Menu
                {
                    Icon = "ic_fiber_new",
                    PageName = "TiposPage",
                    Title = Languages.TypesTitle
                },

                new Menu
                {
                    Icon = "ic_chart_outlined",
                    PageName = "EstadisticaTabbedPage",
                    Title = Languages.StatisticsLbl
                },

                new Menu
                {
                    Icon="exit_phone",
                    PageName="LoginPage",
                    Title= Languages.LogoutLbl
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

        private async void GoAddSalida()
        {
            this.AddSalida = new AddSalidaViewModel();
            await App.Navigator.PushAsync(new AddSalidaPage());
        }

        #endregion
    }
}
