using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReadNote.Vistas.Material.Futuro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioMatFutVista : ContentPage
    {
        public InicioMatFutVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void OnVolverTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Material.IndexMaterialVista());
        }
    }
}