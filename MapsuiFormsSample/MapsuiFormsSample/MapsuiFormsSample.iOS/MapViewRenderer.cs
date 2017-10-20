using CoreGraphics;
using Foundation;
using MapsuiFormsSample;
using MapsuiFormsSample.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MapsUIView), typeof(MapViewRenderer))]
namespace MapsuiFormsSample.iOS
{
    [Preserve(AllMembers = true)]
    public class MapViewRenderer : ViewRenderer<MapsUIView, Mapsui.UI.iOS.MapControl>
    {
        Mapsui.UI.iOS.MapControl mapNativeControl;
        MapsUIView mapViewControl;

        protected override void OnElementChanged(ElementChangedEventArgs<MapsUIView> e)
        {
            base.OnElementChanged(e);

            if (mapViewControl == null && e.NewElement != null)
                mapViewControl = e.NewElement;

            if (mapNativeControl == null && mapViewControl != null)
            {
                var rectangle = mapViewControl.Bounds;
                var rect = new CGRect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

                mapNativeControl = new Mapsui.UI.iOS.MapControl(rect);
                mapNativeControl.Map = mapViewControl.NativeMap;
                mapNativeControl.Frame = rect;

                SetNativeControl(mapNativeControl);
            }
        }
    }
}