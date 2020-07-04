namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Views;

    public class TipoItemViewModel : Tipo
    {
        public ICommand SelectTipoCommand => new RelayCommand(this.SelectTipo);

        private async void SelectTipo()
        {
            MainViewModel.GetInstance().EditTipoSalida = new EditTipoSalidaViewModel(this);
            await App.Navigator.PushAsync(new EditTipoSalidaPage());
        }
    }
}
