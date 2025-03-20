using ReadNote.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ReadNote.Vistas.Notas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarNotaVista : ContentPage
    {
        public RegistrarNotaVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Establecer la fecha actual del sistema operativo en el CustomDatePicker
            CustomDatePicker.Date = DateTime.Now.Date;  // Usamos 'Date' en lugar de 'SelectedDate'
        }
        private async void OnVolverTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Notas.InicioNotasVista());
        }
    }
}