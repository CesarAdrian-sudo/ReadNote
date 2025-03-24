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
using SQLite;
using System.Security.Cryptography;
using System.Collections.ObjectModel;


namespace ReadNote.Vistas.Colecciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActualizarColeccionVista : ContentPage
    {
        public int IdSeleccionado;
        public string NomSeleccionado, DiSeleccionado, TelSeleccionado;
        private SQLiteAsyncConnection conexion;
        private T_Coleccion coleccionSeleccionada;
        private int IdColeccion;
        public ActualizarColeccionVista(int id, string nom, string des, string tipo, string cont)
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();

            IdColeccion = id;
            btnActualizar.Clicked += BtnActualizar_Clicked;
            btnEliminar.Clicked += BtnEliminar_Clicked;
            btnExaminar.Clicked += BtnExaminar_Clicked;

            // Asignar los valores directamente a los Entry
            txtNombre.Text = nom;
            txtDescripcion.Text = des;
            txtTipos.Text = tipo;
            txtContador.Text = cont;

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            var coleccionActualizada = new T_Coleccion
            {
                Id = IdColeccion, // Usamos el ID recibido
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                TipoMaterial = txtTipos.Text,
                Contador = txtContador.Text
            };

            await conexion.UpdateAsync(coleccionActualizada);
            await DisplayAlert("Éxito", "Colección actualizada correctamente", "OK");
            await Navigation.PopAsync();
        }

        private async void BtnExaminar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.MaterialColeccion.MaterialColeccionInicioVista());
        }
        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            bool respuesta = await DisplayAlert("Confirmar", "¿Deseas eliminar esta colección?", "Sí", "No");

            if (respuesta)
            {
                var coleccionAEliminar = new T_Coleccion { Id = IdColeccion };
                await conexion.DeleteAsync(coleccionAEliminar);
                await DisplayAlert("Éxito", "Colección eliminada correctamente", "OK");
                await Navigation.PopAsync();
            }
        }


        private async void OnVolverTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Colecciones.ConsultarColeccionVista());
        }

    }
}