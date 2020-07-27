using ServiciosBomberos.Common.Models;

namespace ServiciosBomberos.UIForms.ViewModels
{
    public class EditSalidaViewModel
    {
        public Salida Salida { get; set; }

        public EditSalidaViewModel(Salida salida)
        {
            this.Salida = salida;
        }
    }
}
