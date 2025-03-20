using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReadNote.Vistas.Material
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndexMaterialVista : ContentPage
    {
        public IndexMaterialVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
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
            await Navigation.PushAsync(new Vistas.indexVista());
        }

        private async void OnAgregarMaterialTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Material.Actual.RegistrarMatActVista());
        }

        private async void OnConsultarMaterialFuturoTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Material.Futuro.InicioMatFutVista());
        }

        private async void OnConsultarMaterialActualTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Material.Actual.InicioMatActVista());
        }
    }
}
