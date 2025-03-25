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
using ReadNote.Vistas.Colecciones;

namespace ReadNote.Vistas.Material.Actual
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarMatActVista : ContentPage
    {
        private SQLiteAsyncConnection conexion;

        public RegistrarMatActVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            conexion.CreateTableAsync<T_Material>().Wait();

            btnRegistrarMaterial.Clicked += BtnRegistrar_Clicked;
        }

        private bool valorBooleano;  // Para almacenar el valor del pickerTipoMaterial (Material Futuro/Actual)
        private bool valorBooleano1; // Para almacenar el valor del pickerEstadoMaterial (Disponible/No Disponible)

        // Manejador del Picker "pickerTipoMaterial"
        private void OnPickerTipoMaterialChanged(object sender, EventArgs e)
        {
            int selectedIndex = pickerTipoMaterial.SelectedIndex;

            if (selectedIndex != -1)
            {
                valorBooleano = selectedIndex == 1; // true si "Material Actual" está seleccionado, false si es "Material Futuro"
            }
        }

        // Manejador del Picker "pickerEstadoMaterial"
        private void OnPickerEstadoMaterialChanged(object sender, EventArgs e)
        {
            int selectedIndex = pickerEstadoMaterial.SelectedIndex;

            if (selectedIndex != -1)
            {
                valorBooleano1 = selectedIndex == 1; // true si "Disponible" está seleccionado, false si es "No Disponible"
            }
        }

        // Manejador del botón "Registrar"
        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            // Validar que los campos obligatorios no estén vacíos
            if (string.IsNullOrWhiteSpace(nombreMaterial.Text) || string.IsNullOrWhiteSpace(txtDescrip.Text))
            {
                await DisplayAlert("Error", "Debe completar todos los campos", "OK");
                return;
            }

            var nuevoMaterial = new T_Material
            {
                TipoMaterial = valorBooleano,  // Almacena el valor seleccionado del pickerTipoMaterial
                NombreMaterial = nombreMaterial.Text,
                Autor = nombreAutor.Text,
                DescripcionMaterial = txtDescrip.Text,
                CategoriaMaterial = pickerCategoria.SelectedItem?.ToString(), // Obtenemos la categoría seleccionada
                noPaginasMaterial = int.Parse(txtPaginas.Text),
                EstadoMaterial = valorBooleano1,  // Almacena el valor seleccionado del pickerEstadoMaterial
                nivelLector = "Bueno",
                fechaCreacion = DateTime.Now
            };

            // Inserta el nuevo material en la base de datos
            await conexion.InsertAsync(nuevoMaterial);

            // Muestra un mensaje de confirmación
            await DisplayAlert("Confirmación", "El material se registró correctamente", "OK");

            // Navegar automáticamente a la vista de consulta
            await Navigation.PushAsync(new IndexMaterialVista());
        }


        private async void OnVolverTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Material.IndexMaterialVista());
        }
    }
}