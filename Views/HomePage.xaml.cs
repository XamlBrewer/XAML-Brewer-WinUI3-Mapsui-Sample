using Mapsui.Tiling;
using Microsoft.UI.Xaml.Controls;
namespace XamlBrewer.WinUI3.Mapsui.Sample.Views
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            MyMap.Map.Layers.Add(OpenStreetMap.CreateTileLayer());
        }
    }
}

