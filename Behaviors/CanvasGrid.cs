using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Xaml.Behaviors;

namespace ExcelWpf.Behaviors {
    class CanvasGridBehavior : Behavior<Canvas> {
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
            for (var x = 0; x < AssociatedObject.ActualWidth / 10; x++) {
                var geometry = new StreamGeometry {
                    FillRule = FillRule.EvenOdd
                };

                using (var ctx = geometry.Open ()) {
                    ctx.BeginFigure (new Point (x * 10, 0), false, false);
                    ctx.LineTo (new Point (x * 10, AssociatedObject.ActualHeight), true, false);

                }
                geometry.Freeze ();

                AssociatedObject.Children.Add (new Path {
                    Stroke = Brushes.Black,
                        StrokeThickness = 0.1,
                        Data = geometry
                });
            }

            for (var y = 0; y < AssociatedObject.ActualHeight / 10; y++) {
                var geometry = new StreamGeometry {
                    FillRule = FillRule.EvenOdd
                };

                using (var ctx = geometry.Open ()) {
                    ctx.BeginFigure (new Point (0, y * 10), false, false);
                    ctx.LineTo (new Point (AssociatedObject.ActualWidth, y * 10), true, false);

                }
                geometry.Freeze ();

                AssociatedObject.Children.Add (new Path {
                    Stroke = Brushes.Black,
                        StrokeThickness = 0.1,
                        Data = geometry
                });
            }
        }
    }
}