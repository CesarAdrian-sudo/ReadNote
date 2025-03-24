using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using ReadNote.Tablas;
using ReadNote.Datos;
using ReadNote.Vistas.MaterialColeccion;
namespace ReadNote.Vistas.MaterialColeccion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarIndividualMaterialColeccionVista : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public RegistrarIndividualMaterialColeccionVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnAñadir.Clicked += BtnAñadir_Clicked;
        }

        private async void BtnAñadir_Clicked(object sender, EventArgs e)
        {
            var datos = new T_Material
            {
                NombreMaterial = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                no_paginas = Convert.ToInt32(txtPaginas.Text)
            };
            var datos1 = new T_MaterialColeccion
            {
                Fecha = DateTime.Today,
                Hora = DateTime.Now
            };
            conexion.InsertAsync(datos);
            DisplayAlert("Registro", "Tu material ha sido registrado a la colección", "OK");
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (Resources == null)
                return;

            if (width < 360) // Teléfonos pequeños
            {
                Resources["IconSize"] = 40;
                Resources["HeaderFontSize"] = 20;
                Resources["ButtonFontSize"] = 14;
                Resources["ButtonSize"] = 180;
                Resources["ButtonHeight"] = 100;
            }
            else if (width < 600) // Teléfonos medianos
            {
                Resources["IconSize"] = 50;
                Resources["HeaderFontSize"] = 22;
                Resources["ButtonFontSize"] = 16;
                Resources["ButtonSize"] = 200;
                Resources["ButtonHeight"] = 120;
            }
            else // Tablets y pantallas grandes
            {
                Resources["IconSize"] = 60;
                Resources["HeaderFontSize"] = 26;
                Resources["ButtonFontSize"] = 18;
                Resources["ButtonSize"] = 240;
                Resources["ButtonHeight"] = 140;
            }
        }
        private async void OnVolverTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.MaterialColeccion.MaterialColeccionInicioVista());
        }
        private async void OnNotificacionTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.MenuInferior.NotificacionVista());
        }

        private async void OnCuentaTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.MenuInferior.CuentaVista());
        }

    }
}