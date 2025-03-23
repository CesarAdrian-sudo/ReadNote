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

namespace ReadNote.Vistas.Colecciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultarColeccionVista : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Coleccion> TablaColeccion;
        public ConsultarColeccionVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaColeccion.ItemSelected += ListaColeccion_ItemSelected;

            CrearBaseDeDatos(); // Asegura que la tabla exista antes de usarla

            ListaColeccion.ItemSelected += ListaColeccion_ItemSelected;
        }
        private async void CrearBaseDeDatos()
        {
            try
            {
                await conexion.CreateTableAsync<T_Coleccion>(); // Crea la tabla si no existe
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo crear la tabla: " + ex.Message, "OK");
            }
        }
        private void ListaColeccion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Coleccion)e.SelectedItem;
            var item = Obj.Id.ToString();
            var nom = Obj.Nombre;
            var des = Obj.Descripcion;
            var tipo = Obj.TipoMaterial;
            var cont = Obj.Contador;
            var id = Convert.ToInt32(item);
            try
            {

                Navigation.PushAsync(new ActualizarColeccionVista(id, nom,des, tipo,cont));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CargarColecciones();
        }
        private async Task CargarColecciones()
        {
            var lista = await conexion.Table<T_Coleccion>().ToListAsync();
            ListaColeccion.ItemsSource = lista;
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
            var ResulRegistros = await conexion.Table<T_Coleccion>().ToListAsync();
            TablaColeccion = new ObservableCollection<T_Coleccion>(ResulRegistros);
            ListaColeccion.ItemsSource = TablaColeccion;
            base.OnAppearing();
            await Navigation.PushAsync(new Vistas.Colecciones.ColeccionesInicioVista());
        }
    }
}