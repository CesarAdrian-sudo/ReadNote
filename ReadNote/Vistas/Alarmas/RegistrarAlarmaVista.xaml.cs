using ReadNote.Datos;
using ReadNote.Tablas;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReadNote.Vistas.Alarmas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarAlarmaVista : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private Dictionary<int, string> materialesDict;

        public RegistrarAlarmaVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();

            InicializarTablas();
            CargarMateriales();
            btnCrear.Clicked += BtnCrear_Clicked;
        }

        private async void InicializarTablas()
        {
            await conexion.CreateTableAsync<T_Material>();
            await conexion.CreateTableAsync<T_Alarmas>();
        }

        private async void CargarMateriales()
        {
            var listaMateriales = await conexion.Table<T_Material>().ToListAsync();

            // Diccionario para mapear ID -> Nombre
            materialesDict = listaMateriales.ToDictionary(m => m.IdMaterial, m => m.NombreMaterial);

            // Mostrar solo nombres en el Picker
            pickerMaterial.ItemsSource = materialesDict.Values.ToList();
        }

        private async void BtnCrear_Clicked(object sender, EventArgs e)
        {
            if (pickerMaterial.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtMensajeAlarma.Text))
            {
                await DisplayAlert("Error", "Debe completar todos los campos", "OK");
                return;
            }

            // Obtener el ID del material seleccionado en base a su nombre
            string nombreSeleccionado = pickerMaterial.SelectedItem.ToString();
            int idMaterialSeleccionado = materialesDict.FirstOrDefault(x => x.Value == nombreSeleccionado).Key;

            DateTime fechaHoraAlarma = DateTime.Today.Add(timeAlarma.Time);
            bool esAutomatica = pickerTipoAlarma.SelectedItem?.ToString() == "AUTOMATICA";

            var nuevaAlarma = new T_Alarmas
            {
                id_material = idMaterialSeleccionado, // Guarda el ID como string
                mensaje = txtMensajeAlarma.Text,
                fechHoraAla = fechaHoraAlarma,
                tipoAlarma = esAutomatica
            };

            await conexion.InsertAsync(nuevaAlarma);
            limpiarFormulario();
            await DisplayAlert("Confirmación", "La alarma se registró correctamente", "OK");

            await Navigation.PushAsync(new ConsultarAlarmasVista());
        }

        void limpiarFormulario()
        {
            pickerMaterial.SelectedIndex = -1;
            txtMensajeAlarma.Text = "";
            timeAlarma.Time = new TimeSpan(0, 0, 0);
            pickerTipoAlarma.SelectedIndex = -1;
        }

        private async void OnVolverTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

