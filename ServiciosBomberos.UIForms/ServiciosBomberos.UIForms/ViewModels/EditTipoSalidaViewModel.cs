﻿using GalaSoft.MvvmLight.Command;
using ServiciosBomberos.Common.Models;
using ServiciosBomberos.Common.Services;
using System;
using System.IO.Compression;
using System.Windows.Input;
using Xamarin.Forms;

namespace ServiciosBomberos.UIForms.ViewModels
{
    public class EditTipoSalidaViewModel : BaseViewModel
    {
        #region Atributos
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;
        #endregion
        #region Propiedades
        public Tipo Tipo { get; set; }
        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }
        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public ICommand SaveCommand => new RelayCommand(this.Save);

        public ICommand DeleteCommand => new RelayCommand(this.Delete);

        #endregion
        #region Constructores
        public EditTipoSalidaViewModel(Tipo tipo)
        {
            this.Tipo = tipo;
            this.apiService = new ApiService();
            this.isEnabled = true;
        }
        #endregion
        #region Metodos
        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Tipo.Nombre))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Debe insertar un nombre para el tipo de Servicio", 
                    "Aceptar");
                return;
            }
            
            if (string.IsNullOrEmpty(this.Tipo.Prioridad))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    "Debe elegir una prioridad para el Tipo de Servicio",
                    "Aceptar");
                return;
            }

            this.isRunning = true;
            this.isEnabled = false;

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.PutAsync(
                url,
                "/api",
                "/Tipos",
                this.Tipo.Id,
                this.Tipo,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.isRunning = false;
            this.isEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    response.Message,
                    "Aceptar");
                return;
            }

            var modifiedTipo = (Tipo)response.Result;
            MainViewModel.GetInstance().Tipos.UpdateTipoInList(modifiedTipo);
            await App.Navigator.PopAsync();
        }

        private async void Delete()
        {
            var confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirme",
                "¿Esta seguro de borrar el Tipo de Salida?",
                "SI",
                "NO");

            if (!confirm)
            {
                return;
            }

            this.isRunning = true;
            this.isEnabled = false;

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.DeleteAsync(
                url,
                "/api",
                "/Tipos",
                this.Tipo.Id,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.isRunning = false;
            this.isEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    response.Message,
                    "Aceptar");
                return;
            }

            MainViewModel.GetInstance().Tipos.DeleteTipoinList(this.Tipo.Id);
            await App.Navigator.PopAsync();
        }

        #endregion
    }
}