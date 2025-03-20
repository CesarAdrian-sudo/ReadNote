using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReadNote.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class indexVista : ContentPage
    {
        public indexVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ActualizarSaludo();
        }

        private void ActualizarSaludo()
        {
            int horaActual = DateTime.Now.Hour;

            if (horaActual >= 6 && horaActual < 12)
                lblSaludo.Text = "Buenos días lector";
            else if (horaActual >= 12 && horaActual < 19)
                lblSaludo.Text = "Buenas tardes lector";
            else
                lblSaludo.Text = "Buenas noches lector";
        }

        private async void OnMaterialTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Material.IndexMaterialVista());
        }

        private async void OnColeccionesTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Colecciones.ColeccionesInicioVista());
        }

        private async void OnAlarmasTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Alarmas.AlarmasInicio());
        }

        private async void OnNotasTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Notas.InicioNotasVista());
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (Resources == null)
                return;

            if (width < 360) // Teléfonos pequeños
            {
                Resources["IconSize"] = 40;
                Resources["HeaderFontSize"] = 18;
                Resources["ButtonFontSize"] = 14;
            }
            else if (width < 600) // Teléfonos medianos
            {
                Resources["IconSize"] = 50;
                Resources["HeaderFontSize"] = 20;
                Resources["ButtonFontSize"] = 16;
            }
            else // Tablets y pantallas grandes
            {
                Resources["IconSize"] = 60;
                Resources["HeaderFontSize"] = 24;
                Resources["ButtonFontSize"] = 18;
            }
        }
    }
}