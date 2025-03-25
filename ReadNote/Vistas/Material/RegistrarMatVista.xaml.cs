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

namespace ReadNote.Vistas.Material.Actual
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarMatActVista : ContentPage
    {
        private SQLiteAsyncConnection conexion;

        public RegistrarMatActVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            conexion.CreateTableAsync<T_Material>().Wait();

            btnRegistrarMaterial.Clicked += BtnRegistrar_Clicked;
        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        { }
        private async void OnVolverTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Material.IndexMaterialVista());
        }
    }
}