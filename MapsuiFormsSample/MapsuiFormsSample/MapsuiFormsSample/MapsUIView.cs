using Mapsui.Styles;

namespace MapsuiFormsSample
{
    public class MapsUIView : Xamarin.Forms.View
    {
        public Mapsui.Map NativeMap { get; }

        protected internal MapsUIView()
        {
            NativeMap = new Mapsui.Map();
            NativeMap.BackColor = Color.Black; //This Color should match the map - I prefer Black over White here
        }
    }
}