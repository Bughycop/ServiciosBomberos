﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ServiciosBomberos.UIForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GraficoEstadisticoPage : ContentPage
    {
        public GraficoEstadisticoPage()
        {
            //Licencia
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjgwMzU1QDMxMzgyZTMxMmUzMExFSGhubEMwR2NTS0lMK1JuM1ZudmtuUjNPSkhlcnRYelhObS8xc0Z6aWs9");
            
            InitializeComponent();
        }
    }
}