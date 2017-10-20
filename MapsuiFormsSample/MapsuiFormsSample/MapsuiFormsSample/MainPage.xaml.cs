using System.Diagnostics;
using Mapsui.Geometries;
using Mapsui.Layers;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.Utilities;

namespace MapsuiFormsSample
{
	public partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();

	        var mapControl = new MapsUIView();
	        mapControl.NativeMap.Layers.Add(OpenStreetMap.CreateTileLayer());

	        var layer = GenerateIconLayer();
	        mapControl.NativeMap.Layers.Add(layer);
	        mapControl.NativeMap.InfoLayers.Add(layer);

	        mapControl.NativeMap.Info += (sender, args) =>
	            {
	                var layername = args.Layer?.Name;
	                var featureLabel = args.Feature?["Label"]?.ToString();
	                var featureType = args.Feature?["Type"]?.ToString();

	                Debug.WriteLine("Info Event was invoked.");
	                Debug.WriteLine("Layername: " + layername);
	                Debug.WriteLine("Feature Label: " + featureLabel);
	                Debug.WriteLine("Feature Type: " + featureType);

	                Debug.WriteLine("World Postion: {0:F4} , {1:F4}", args.WorldPosition?.X, args.WorldPosition?.Y);
	                Debug.WriteLine("Screen Postion: {0:F4} , {1:F4}", args.ScreenPosition?.X, args.ScreenPosition?.Y);
	            };

	        ContentGrid.Children.Add(mapControl);
	    }

	    private ILayer GenerateIconLayer()
	    {
	        var layername = "My Local Layer";
	        return new Layer(layername)
	            {
	                Name = layername,
	                DataSource = new MemoryProvider(GetIconFeatures()),
                    Style = new SymbolStyle
	                    {
	                        SymbolScale = 0.8,
	                        Fill = new Brush(Color.Red),
	                        Outline = { Color = Color.Black, Width = 1 }
	                    }
                };
	    }

	    private Features GetIconFeatures()
	    {
	        var features = new Features();
	        var feature = new Feature
	            {
	                Geometry = new Polygon(new LinearRing(new[]
	                    {
	                        new Point(1066689.6851, 6892508.8652),
	                        new Point(1005540.0624, 6987290.7802),
	                        new Point(1107659.9322, 7056389.8538),
	                        new Point(1066689.6851, 6892508.8652)
	                    })),
	                ["Label"] = "My Feature Label",
	                ["Type"] = "My Feature Type"
	            };

	        features.Add(feature);
	        return features;
	    }
    }
}
