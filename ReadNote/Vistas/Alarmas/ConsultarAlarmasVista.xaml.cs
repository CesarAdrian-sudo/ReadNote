using ReadNote.Datos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReadNote.Tablas;

namespace ReadNote.Vistas.Alarmas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultarAlarmasVista : ContentPage
    {
        private SQLiteAsyncConnection conexion;

        public ConsultarAlarmasVista()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            // Conexión a la base de datos
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            CargarAlarmas();
        }

        public async void CargarAlarmas()
        {
            try
            {
                // Obtener las alarmas de la base de datos
                var alarmas = await conexion.Table<T_Alarmas>().ToListAsync();
                // Limpiar cualquier dato anterior
                alarmasContainer.Children.Clear();
                // Agregar cada alarma a la interfaz
                foreach (var alarma in alarmas)
                {
                    Frame frameAlarma = new Frame
                    {
                        BackgroundColor = Color.FromHex("#A1A1A1"),
                        CornerRadius = 20,
                        Padding = 15,
                        HeightRequest = 60,
                        Margin = new Thickness(0, 10, 0, 10),
                        Content = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Spacing = 10,
                            VerticalOptions = LayoutOptions.Center,
                            Children =
                            {
                                new Label
                                {
                                    Text = alarma.mensaje, // Mostrar el mensaje como título
                                    FontSize = 20,
                                    TextColor = Color.Black,
                                    VerticalOptions = LayoutOptions.Center,
                                    FontFamily = "Lemon"
                                },
                                new Switch
                                {
                                    HorizontalOptions = LayoutOptions.EndAndExpand,
                                    VerticalOptions = LayoutOptions.Center
                                }
                            }
                        }
                    };

                    // Agregar el TapGestureRecognizer para navegar a la vista de detalles
                    frameAlarma.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() => OnAlarmaTapped(alarma))
                    });

                    // Agregar el frame al contenedor
                    alarmasContainer.Children.Add(frameAlarma);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudieron cargar las alarmas: " + ex.Message, "OK");
            }
        }

        // Método para manejar el evento Tap y navegar a AlarmaConsultadaVista
        private async void OnAlarmaTapped(T_Alarmas alarma)
        {
            // Navegar a la vista AlarmaConsultadaVista y pasar los datos de la alarma
            await Navigation.PushAsync(new AlarmaConsultada(alarma));
        }

        private async void OnVolverTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.Alarmas.AlarmasInicio());
        }
    }
}
