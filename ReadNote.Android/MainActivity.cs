using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using System;
using Xamarin.Essentials;

namespace ReadNote.Droid
{
    [Activity(Label = "ReadNote", Icon = "@mipmap/ic_ReadNote", Theme = "@style/MainTheme",
              MainLauncher = true,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation |
                                   ConfigChanges.UiMode | ConfigChanges.ScreenLayout |
                                   ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Bloquear la orientación en vertical
            this.RequestedOrientation = ScreenOrientation.Portrait;

            // Activar pantalla completa
            SetFullScreenMode();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        private void SetFullScreenMode()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.R) // Android 11 (API 30) o superior
            {
                var windowInsetsController = Window.InsetsController;
                if (windowInsetsController != null)
                {
                    windowInsetsController.Hide(WindowInsets.Type.StatusBars() | WindowInsets.Type.NavigationBars());
                    windowInsetsController.SystemBarsBehavior = (int)WindowInsetsControllerBehavior.ShowTransientBarsBySwipe;
                }
            }
            else // Android 10 o inferior
            {
#pragma warning disable CS0618 // Desactivar advertencia de obsolescencia para compatibilidad
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
                    SystemUiFlags.HideNavigation |
                    SystemUiFlags.Fullscreen |
                    SystemUiFlags.ImmersiveSticky
                );
#pragma warning restore CS0618
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
