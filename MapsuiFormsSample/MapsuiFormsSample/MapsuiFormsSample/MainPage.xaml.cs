using System.Diagnostics;
using Mapsui.Geometries;
using Mapsui.Layers;
using Mapsui.Projection;
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

            var centerOfWarsaw = new Point(21.107886, 52.2127475);
            // OSM uses spherical mercator coordinates. So transform the lon lat coordinates to spherical mercator
            var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(centerOfWarsaw.X, centerOfWarsaw.Y);
            // Set the center of the viewport to the coordinate. The UI will refresh automatically
            mapControl.NativeMap.NavigateTo(sphericalMercatorCoordinate);
            // Additionally you might want to set the resolution, this could depend on your specific purpose
            mapControl.NativeMap.NavigateTo(mapControl.NativeMap.Resolutions[9]);

            var layer = GenerateIconLayer();
	        mapControl.NativeMap.Layers.Add(layer);
	        mapControl.NativeMap.InfoLayers.Add(layer);

	        mapControl.NativeMap.Info += (sender, args) =>
	            {
	                var layername = args.Layer?.Name;
	                var featureLabel = args.Feature?["Label"]?.ToString();
	                var featureType = args.Feature?["Type"]?.ToString();

	                if (!string.IsNullOrWhiteSpace(featureLabel))
	                {
	                    ShowPopup(featureLabel, featureType);
	                }

                    Debug.WriteLine("Info Event was invoked.");
	                Debug.WriteLine("Layername: " + layername);
	                Debug.WriteLine("Feature Label: " + featureLabel);
	                Debug.WriteLine("Feature Type: " + featureType);

	                Debug.WriteLine("World Postion: {0:F4} , {1:F4}", args.WorldPosition?.X, args.WorldPosition?.Y);
	                Debug.WriteLine("Screen Postion: {0:F4} , {1:F4}", args.ScreenPosition?.X, args.ScreenPosition?.Y);
	            };

	        ContentGrid.Children.Add(mapControl);
	    }

	    async void ShowPopup(string feature, string type)
	    {
	        if (await this.DisplayAlert(
	            "You have clicked " + feature,
	            "Do you want to see details?",
	            "Yes",
	            "No"))
	        {
	            Debug.WriteLine("User clicked OK");
	        }
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
	                ["Label"] = "West Germany",
	                ["Type"] = "My Feature Type"
	            };

	        features.Add(feature);

	        features.Add(feature);
	        var feature2 = new Feature
	        {
	            //warsaw
	            Geometry = new Polygon(new LinearRing(new[]
	                {
	                    SphericalMercator.FromLonLat(21.074181, 52.277191),
	                    SphericalMercator.FromLonLat(21.057358, 52.210131),
	                    SphericalMercator.FromLonLat(20.981483, 52.238944),
	                    SphericalMercator.FromLonLat(21.074181, 52.277191),
	                })
	            ),
	            ["Label"] = "Warsaw",
	            ["Type"] = "My Feature Type"
	        };
	        features.Add(feature2);
            return features;
	    }
    }
}
