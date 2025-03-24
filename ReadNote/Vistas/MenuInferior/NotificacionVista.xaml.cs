using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReadNote.Vistas.MenuInferior
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificacionVista : ContentPage
    {
        public NotificacionVista()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}