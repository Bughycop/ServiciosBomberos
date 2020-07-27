namespace ServiciosBomberos.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Views;

    public class SalidaItemViewModel : Salida
    {
        public ICommand SelectSalidaCommand => new RelayCommand(this.SelectSalida);

        private async void SelectSalida()
        {
            MainViewModel.GetInstance().EditSalida = new EditSalidaViewModel((Salida)this);
            await App.Navigator.PushAsync(new EditSalidaPage());
        }
    }
}
