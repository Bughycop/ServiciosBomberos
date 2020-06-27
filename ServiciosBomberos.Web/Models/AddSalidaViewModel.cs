namespace ServiciosBomberos.Web.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Data.Entities;
    using Org.BouncyCastle.Utilities;
    using System;

    public class AddSalidaViewModel : Salida
    {
        public IEnumerable<SelectListItem> TiposDeSalida { get; set; }

        public IEnumerable<SelectListItem> Bomberos { get; set; }

        public IEnumerable<SelectListItem> Bomberos2 { get; set; }

        public TimeSpan TotalHorasSalida { get { return this.HoraRegreso - this.HoraSalida; } }

    }
}
