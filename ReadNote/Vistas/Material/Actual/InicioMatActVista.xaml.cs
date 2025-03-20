using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReadNote.Vistas.Material.Actual
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioMatActVista : ContentPage
    {
        public InicioMatActVista()
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