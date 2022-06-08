using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Xaml.Behaviors;

namespace BuildGrid.Behaviors {
    class BuildGrid : Behavior<Canvas> {
        private const int GRID_SIZE = 10;

        protected override void OnAttached () {
            base.OnAttached ();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }
        protected override void OnDetaching () {
            base.OnDetaching ();
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }
        private void AssociatedObject_Loaded (object sender, RoutedEventArgs e) {
            CreateCanvasGrid ();
        }

        private void CreateCanvasGrid () {
            AssociatedObject.Children.Clear ();

            for (int i = 0; i < AssociatedObject.ActualWidth; i += GRID_SIZE) {
                Line GridLine = new Line () {
                X1 = i,
                Y1 = 0,
                X2 = i,
                Y2 = AssociatedObject.ActualHeight,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush (Colors.LightGray),
                SnapsToDevicePixels = true
                };
                AssociatedObject.Children.Add (GridLine);
            }
            for (int i = 0; i < AssociatedObject.ActualHeight; i += GRID_SIZE) {
                Line GridLine = new Line () {
                X1 = 0,
                Y1 = i,
                X2 = AssociatedObject.ActualWidth,
                Y2 = i,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush (Colors.LightGray),
                SnapsToDevicePixels = true
                };
                AssociatedObject.Children.Add (GridLine);
            }
        }
    }
}