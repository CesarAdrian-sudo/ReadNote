using Android.Content;
using Android.Graphics.Drawables;
using ReadNote.CustomControls;
using ReadNote.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomTimePicker), typeof(CustomTimePickerRenderer))]
namespace ReadNote.Droid.Renderers
{
    public class CustomTimePickerRenderer : TimePickerRenderer
    {
        public CustomTimePickerRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackground(new ColorDrawable(Android.Graphics.Color.Transparent));
                Control.SetPadding(20, 20, 20, 20);
            }
        }
    }
}
