using ServiciosBomberos.UIForms.ViewModels;

namespace ServiciosBomberos.UIForms.Infrastucture
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
