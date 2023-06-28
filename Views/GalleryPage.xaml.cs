using Mapsui.Extensions;
using Mapsui.Samples.Common;
using Mapsui.Samples.CustomWidget;
using Mapsui;
using Mapsui.Tiling;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

using System;
using Mapsui.Samples.Common.Extensions;
using System.Linq;

namespace XamlBrewer.WinUI3.Mapsui.Sample.Views
{
    public sealed partial class GalleryPage : Page
    {
        private DispatcherTimer _timer = new()
        { 
            Interval = TimeSpan.FromSeconds(3)
        };

        public GalleryPage()
        {
            InitializeComponent();

            MapControl.Map.Layers.Add(OpenStreetMap.CreateTileLayer());
            MapControl.Map.Navigator.RotationLock = false;
            MapControl.UnSnapRotationDegrees = 30;
            MapControl.ReSnapRotationDegrees = 5;
            MapControl.Renderer.WidgetRenders[typeof(CustomWidget)] = new CustomWidgetSkiaRenderer();

            CategoryComboBox.SelectionChanged += CategoryComboBoxSelectionChanged;

            FillComboBoxWithCategories();
            FillListWithSamples();

            _timer.Tick += (s, a) =>
            {
                FeatureInfo.Text = "";
                _timer.Stop();
            };
        }

        private void FillComboBoxWithCategories()
        {
            var categories = AllSamples.GetSamples()?.Select(s => s.Category).Distinct().OrderBy(c => c);
            if (categories == null)
                return;

            foreach (var category in categories)
            {
                CategoryComboBox.Items?.Add(category);
            }
            CategoryComboBox.SelectedIndex = 1;
        }

        private void MapOnInfo(object? sender, MapInfoEventArgs args)
        {
            if (args.MapInfo?.Feature != null)
            {
                FeatureInfo.Text = $"Click Info: '{args.MapInfo.Feature.ToDisplayText()}'";
                _timer.Start();
            }
        }

        private void FillListWithSamples()
        {
            var selectedCategory = CategoryComboBox.SelectedValue?.ToString() ?? "";
            SampleList.Children.Clear();
            var samples = AllSamples.GetSamples()?.Where(s => s.Category == selectedCategory);
            if (samples == null)
                return;

            foreach (var sample in samples)
                SampleList.Children.Add(CreateRadioButton(sample));
        }

        private void CategoryComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillListWithSamples();
        }

        private UIElement CreateRadioButton(ISampleBase sample)
        {
            var radioButton = new RadioButton
            {
                //FontSize = 16,
                Content = sample.Name,
                Margin = new Thickness(4)
            };

            radioButton.Click += (s, a) =>
            {
                Catch.Exceptions(async () =>
                {
                    MapControl.Map!.Layers.Clear();
                    MapControl.Info -= MapOnInfo;
                    await sample.SetupAsync(MapControl);
                    MapControl.Info += MapOnInfo;
                    MapControl.Refresh();
                });
            };

            return radioButton;
        }

        private void RotationSlider_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            var percent = RotationSlider.Value / (RotationSlider.Maximum - RotationSlider.Minimum);
            MapControl.Map.Navigator.RotateTo(percent * 360);
        }
    }
}

