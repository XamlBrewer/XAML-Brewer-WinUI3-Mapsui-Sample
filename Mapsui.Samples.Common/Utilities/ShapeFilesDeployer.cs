using Mapsui.Samples.Common.Extensions;
using Mapsui.Samples.Common.Maps.DataFormats;
using System;
using System.IO;
using System.Reflection;

namespace Mapsui.Samples.Common.Utilities;

public static class ShapeFilesDeployer
{
    public static string ShapeFilesLocation { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mapsui.Samples");

    public static void CopyEmbeddedResourceToFile(string shapefile)
    {
        var res = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

        shapefile = Path.GetFileNameWithoutExtension(shapefile);
        var assembly = typeof(ShapefileSample).GetTypeInfo().Assembly;
        assembly.CopyEmbeddedResourceToFile("XamlBrewer.WinUI3.Mapsui.Sample.Mapsui.Samples.Common.GeoData.World.", ShapeFilesLocation, shapefile + ".dbf");
        assembly.CopyEmbeddedResourceToFile("XamlBrewer.WinUI3.Mapsui.Sample.Mapsui.Samples.Common.GeoData.World.", ShapeFilesLocation, shapefile + ".prj");
        assembly.CopyEmbeddedResourceToFile("XamlBrewer.WinUI3.Mapsui.Sample.Mapsui.Samples.Common.GeoData.World.", ShapeFilesLocation, shapefile + ".shp");
        assembly.CopyEmbeddedResourceToFile("XamlBrewer.WinUI3.Mapsui.Sample.Mapsui.Samples.Common.GeoData.World.", ShapeFilesLocation, shapefile + ".shx");
        assembly.CopyEmbeddedResourceToFile("XamlBrewer.WinUI3.Mapsui.Sample.Mapsui.Samples.Common.GeoData.World.", ShapeFilesLocation, shapefile + ".shp.sidx");
    }
}
