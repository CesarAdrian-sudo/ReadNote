using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReadNote.Vistas.Colecciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarColeccionVista : ContentPage
    {
        public RegistrarColeccionVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void OnVolverTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Colecciones.ColeccionesInicioVista());
        }
    }
}