using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReadNote.Datos;
using ReadNote.Tablas;
using System.IO;
using System.Collections.ObjectModel;
using ReadNote;
using SQLite;
using ReadNote.Vistas.Colecciones;
namespace ReadNote.Vistas.MaterialColeccion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarMaterialColeccion : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public RegistrarMaterialColeccion()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaMaterialColeccion.ItemSelected += ListaMaterialColeccion_ItemSelected;
        }
        private void ListaMaterialColeccion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Material)e.SelectedItem;
            var item = Obj.IdMaterial.ToString();

            var id = Convert.ToInt32(item);
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
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

        private void OnMasTapped(object sender, EventArgs e)
        {
            popupMenu.IsVisible = !popupMenu.IsVisible;
        }

        private async void OnNotaRapidaTapped(object sender, EventArgs e)
        {
            popupMenu.IsVisible = false;
            await Navigation.PushAsync(new Vistas.Notas.RegistrarNotaVista());
        }

        private async void OnMaterialFuturoTapped(object sender, EventArgs e)
        {
            popupMenu.IsVisible = false;
            await Navigation.PushAsync(new Vistas.Material.Actual.RegistrarMatActVista());
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