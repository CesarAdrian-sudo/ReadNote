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

namespace ReadNote.Vistas.Colecciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarColeccionVista : ContentPage
    {
        private SQLiteAsyncConnection conexion;

        public RegistrarColeccionVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();

            // 📌 Asegura que la tabla se cree antes de usarla
            conexion.CreateTableAsync<T_Coleccion>().Wait();

            btnCrear.Clicked += BtnCrear_Clicked;
        }

        private async void BtnCrear_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtTipos.Text))
            {
                await DisplayAlert("Error", "Debe completar todos los campos", "OK");
                return;
            }

            var nuevaColeccion = new T_Coleccion
            {
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                TipoMaterial = txtTipos.Text,
                Contador = txtContador.Text
            };

            await conexion.InsertAsync(nuevaColeccion);
            limpiarFormulario();
            await DisplayAlert("Confirmación", "La Colección se registró correctamente", "OK");

            // Navegar automáticamente a la vista de consulta
            await Navigation.PushAsync(new ConsultarColeccionVista());
        }

        void limpiarFormulario()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtContador.Text = "";
            txtTipos.Text = "";
        }

        private async void OnVolverTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Colecciones.ColeccionesInicioVista());
        }
    }
}