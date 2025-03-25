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

namespace ReadNote.Vistas.Material.Futuro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActualizarMatFutVista : ContentPage
    {
        public int IdSeleccionado;
        public string NomSeleccionado, DiSeleccionado, TelSeleccionado;
        private SQLiteAsyncConnection conexion;
        private T_Material MaterialSelec;
        private int IdMaterial;
        public ActualizarMatFutVista(int id, string nom, string autor, string des, string cat, int pag, DateTime fecha)
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();

            IdMaterial = id;
            btnActualizar.Clicked += BtnActualizar_Clicked;
            btnEliminar.Clicked += BtnEliminar_Clicked;

            // Asignar los valores directamente a los Entry
            txtNombreMaterial.Text = nom;
            txtAutor.Text = autor;
            txtDescripcion.Text = des;
            pickerCategoria.SelectedItem = cat;
            txtPag.Text = pag.ToString();
            txtFechaDP.Date = fecha;

            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            var materialExistente = await conexion.Table<T_Material>().FirstOrDefaultAsync(m => m.IdMaterial == IdMaterial);

            if (materialExistente != null)
            {
                // Crear el material actualizado, manteniendo los valores previos si no se modifican
                var materialActualizado = new T_Material
                {
                    IdMaterial = IdMaterial, // Usamos el ID recibido
                    TipoMaterial = materialExistente.TipoMaterial, // Mantener el valor previo
                    EstadoMaterial = materialExistente.EstadoMaterial, // Mantener el valor previo
                    nivelLector = materialExistente.nivelLector, // Mantener el valor previo
                    NombreMaterial = string.IsNullOrWhiteSpace(txtNombreMaterial.Text) ? materialExistente.NombreMaterial : txtNombreMaterial.Text,
                    Autor = string.IsNullOrWhiteSpace(txtAutor.Text) ? materialExistente.Autor : txtAutor.Text,
                    DescripcionMaterial = string.IsNullOrWhiteSpace(txtDescripcion.Text) ? materialExistente.DescripcionMaterial : txtDescripcion.Text,
                    CategoriaMaterial = pickerCategoria.SelectedItem?.ToString() ?? materialExistente.CategoriaMaterial, // Mantener el valor previo
                    noPaginasMaterial = string.IsNullOrWhiteSpace(txtPag.Text) ? materialExistente.noPaginasMaterial : Convert.ToInt32(txtPag.Text),
                    fechaCreacion = txtFechaDP.Date == default(DateTime) ? materialExistente.fechaCreacion : txtFechaDP.Date // Mantener el valor previo
                };

                await conexion.UpdateAsync(materialActualizado);
                await DisplayAlert("Éxito", "Material actualizado correctamente", "OK");
                await Navigation.PushAsync(new Vistas.Material.Futuro.InicioMatFutVista());
            }
        }


        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            bool respuesta = await DisplayAlert("Confirmar", "¿Deseas eliminar este material?", "Sí", "No");

            if (respuesta)
            {
                var materialAEliminar = new T_Material { IdMaterial = IdMaterial };
                await conexion.DeleteAsync(materialAEliminar);
                await DisplayAlert("Éxito", "Material eliminado correctamente", "OK");
                await Navigation.PushAsync(new Vistas.Material.Futuro.InicioMatFutVista());
            }
        }


        private async void OnVolverTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Material.Futuro.InicioMatFutVista());
        }

    }
}