using Xamarin.Forms;

namespace ReadNote.CustomControls
{
    public class CustomEntry : Entry { }
    public class CustomEditor : Editor { }
    public class CustomTimePicker : TimePicker { }
    public class CustomDatePicker : DatePicker { }
    public class CustomPicker : Picker
    {
        public CustomPicker()
        {
            BackgroundColor = Color.Transparent;
        }
    }
}
