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

namespace ReadNote.Vistas.Material.Actual
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioMatActVista : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Material> TablaMaterial;

        public InicioMatActVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaMaterialesActuales.ItemSelected += ListaMaterialesActuales_ItemSelected;
            CrearBaseDeDatos(); 
            FiltrarMateriales(); // Llama al método de filtrado

            ListaMaterialesActuales.ItemSelected += ListaMaterialesActuales_ItemSelected;
        }

        private async void CrearBaseDeDatos()
        {
            try
            {
                await conexion.CreateTableAsync<T_Coleccion>();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo crear la tabla: " + ex.Message, "OK");
            }
        }

        private void ListaMaterialesActuales_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Material)e.SelectedItem;
            var item = Obj.IdMaterial.ToString();
            var nom = Obj.NombreMaterial;
            var autor = Obj.Autor;
            var des = Obj.DescripcionMaterial;
            var cat = Obj.CategoriaMaterial;
            var pag = Obj.noPaginasMaterial;
            var fecha = Obj.fechaCreacion;
            var id = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new ActMatActVista(id, nom, autor, des, cat, pag, fecha));
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }



        private async Task FiltrarMateriales()
        {
            try
            {
                // Obtener todos los materiales de la base de datos
                var materiales = await conexion.Table<T_Material>().ToListAsync();

                // Filtrar los materiales donde TipoMaterial es false
                var materialesFiltrados = materiales.Where(m => m.TipoMaterial == true).ToList();

                // Crear una ObservableCollection a partir de la lista filtrada
                TablaMaterial = new ObservableCollection<T_Material>(materialesFiltrados);

                // Asignar los materiales filtrados a la ListView
                ListaMaterialesActuales.ItemsSource = TablaMaterial;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudieron cargar los materiales filtrados: " + ex.Message, "OK");
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
            var ResulRegistros = await conexion.Table<T_Material>().ToListAsync();
            TablaMaterial = new ObservableCollection<T_Material>(ResulRegistros);
            ListaMaterialesActuales.ItemsSource = TablaMaterial;
            base.OnAppearing();
            await Navigation.PushAsync(new Vistas.Material.IndexMaterialVista());
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
