using ReadNote.Datos;
using ReadNote.Tablas;
using SQLite;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReadNote.Vistas.Alarmas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmaConsultada : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private T_Alarmas alarmaSeleccionada;

        // Constructor que recibe la alarma seleccionada
        public AlarmaConsultada(T_Alarmas alarma)
        {
            InitializeComponent();
            alarmaSeleccionada = alarma;
            NavigationPage.SetHasNavigationBar(this, false);
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            // Aquí actualizamos la vista con los detalles de la alarma
            MostrarDetallesAlarma();
        }

        private void MostrarDetallesAlarma()
        {
            if (alarmaSeleccionada != null)
            {
                // 1. Mostrar el mensaje de la alarma
                nombreAlarmaLabel.Text = alarmaSeleccionada.mensaje;

                // 2. Extraer y mostrar la hora de fechHoraAla (DateTime)
                TimeSpan horaAlarma = alarmaSeleccionada.fechHoraAla.TimeOfDay;
                timeAlarma.Time = horaAlarma;

                // 3. Opcional: Mostrar fecha (si lo deseas)
                // fechaLabel.Text = alarmaSeleccionada.fechHoraAla.ToString("dd/MM/yyyy");
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
            await Navigation.PushAsync(new Vistas.Alarmas.AlarmasInicio());
        }

        private async void OnCancelarClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Alarmas.ConsultarAlarmasVista());
        }

        private async void OnHechoClicked(object sender, EventArgs e)
        {
            alarmaSeleccionada.fechHoraAla = alarmaSeleccionada.fechHoraAla.Date + timeAlarma.Time;
            alarmaSeleccionada.mensaje = nombreAlarmaLabel.Text;

            await conexion.UpdateAsync(alarmaSeleccionada);
            await DisplayAlert("Éxito", "Alarma actualizada", "OK");

            // Volver a la vista ConsultarAlarmasVista y recargar alarmas
            await Navigation.PushAsync(new Vistas.Alarmas.ConsultarAlarmasVista());
            // Actualiza las alarmas
            var consultarAlarmasVista = (ConsultarAlarmasVista)Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            consultarAlarmasVista.CargarAlarmas();
        }


        private async void OnEliminarClicked(object sender, EventArgs e)
        {
            bool confirmar = await DisplayAlert("Confirmar", "¿Eliminar esta alarma?", "Sí", "No");
            if (confirmar)
            {
                await conexion.DeleteAsync(alarmaSeleccionada);
                await DisplayAlert("Éxito", "Alarma eliminada", "OK");

                // Volver a la vista ConsultarAlarmasVista y recargar alarmas
                await Navigation.PushAsync(new Vistas.Alarmas.ConsultarAlarmasVista());
                // Actualiza las alarmas
                var consultarAlarmasVista = (ConsultarAlarmasVista)Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                consultarAlarmasVista.CargarAlarmas();
            }
        }

    }
}
