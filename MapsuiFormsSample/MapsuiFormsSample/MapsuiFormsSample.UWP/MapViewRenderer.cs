using MapsuiFormsSample;
using MapsuiFormsSample.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MapsUIView), typeof(MapViewRenderer))]
namespace MapsuiFormsSample.UWP
{
    public class MapViewRenderer : ViewRenderer<MapsUIView, Mapsui.UI.Uwp.MapControl>
    {
        Mapsui.UI.Uwp.MapControl mapNativeControl;
        MapsUIView mapViewControl;

        protected override void OnElementChanged(ElementChangedEventArgs<MapsUIView> e)
        {
            base.OnElementChanged(e);
            
            if (mapViewControl == null && e.NewElement != null)
                mapViewControl = e.NewElement as MapsUIView;

            if (mapNativeControl == null && mapViewControl != null)
            {
                mapNativeControl = new Mapsui.UI.Uwp.MapControl();
                mapNativeControl.Map = mapViewControl.NativeMap;
                
                SetNativeControl(mapNativeControl);
            }
        }
    }
}