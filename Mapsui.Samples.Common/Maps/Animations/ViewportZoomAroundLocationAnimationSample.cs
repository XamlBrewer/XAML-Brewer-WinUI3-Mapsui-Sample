﻿using Mapsui.Animations;
using Mapsui.Extensions;
using Mapsui.Tiling;
using Mapsui.UI;
using Mapsui.Widgets;
using Mapsui.Widgets.ScaleBar;
using Mapsui.Widgets.Zoom;
using System.Threading.Tasks;

namespace Mapsui.Samples.Common.Maps.Animations;

public class ViewportZoomAroundLocationAnimationSample : ISample
{
    public string Name => "Animated Viewport - Zoom Around Location";
    public string Category => "Animations";

    public static int mode = 1;

    public Task<Map> CreateMapAsync() => Task.FromResult(CreateMap());

    public static Map CreateMap()
    {
        var map = new Map
        {
            CRS = "EPSG:3857"
        };
        map.Layers.Add(OpenStreetMap.CreateTileLayer());
        map.Widgets.Add(new ScaleBarWidget(map)
        {
            TextAlignment = Alignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top
        });
        map.Widgets.Add(new ZoomInOutWidget { MarginX = 20, MarginY = 40 });
        map.Info += (s, a) =>
        {
            if (a.MapInfo?.WorldPosition != null)
            {
                // Zoom in while keeping centerOfZoom at the same position. If you click somewhere to zoom in the mousepointer
                // will still be above the same location in the map. This can be you used for mouse wheel zoom.
                map.Navigator.ZoomTo(a.MapInfo.Resolution * 0.5, a.MapInfo.ScreenPosition!, 500, Easing.CubicOut);
            }
        };
        return map;
    }
}
