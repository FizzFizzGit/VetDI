using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VetDI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {

        //メンバ変数
        private const string ABOUT_DESCRIPTION = "about\n\n";
        private bool IsDraw;
        private const int GRID_SIZE = 20;

        //コンストラクタ
        public MainWindow () {
            InitializeComponent ();
            CanvasView.MouseLeftButtonDown += CanvasView_MouseLeftButtonDown;
            CanvasView.MouseMove += CanvasView_MouseMove;
            BuildView ();
            this.SizeChanged += MainWindow_SizeChanged;
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;

        }

        private void MainWindow_SizeChanged (object sender, SizeChangedEventArgs e) {
            BuildView ();
        }
        
        private void MainWindow_Closing (object sender, System.ComponentModel.CancelEventArgs e) { }

        private void MainWindow_Loaded (object sender, RoutedEventArgs e) { }

        //イベントハンドラここから
        private void Button_About_Click (object sender, RoutedEventArgs e) {
            MessageBox.Show (messageBoxText: ABOUT_DESCRIPTION, caption: "About");
        }

        private void CanvasView_MouseMove (object sender, MouseEventArgs e) {
            if (IsDraw == true) {
                Point p = e.GetPosition (CanvasView);
                GridLine.X2 = p.X;
                GridLine.Y2 = p.Y;
            }
        }

        private void CanvasView_MouseLeftButtonDown (object sender, MouseButtonEventArgs e) {
            Point p = e.GetPosition (CanvasView);

            if (IsDraw == true) {
                IsDraw = false;
                GridLine.X2 = p.X;
                GridLine.Y2 = p.Y;
            } else {
                IsDraw = true;
                GridLine.X1 = p.X;
                GridLine.Y1 = p.Y;
            }
        }

        private void BuildView () {
            CanvasView.Children.Clear ();

            for (int i = 0; i < this.ActualWidth; i += GRID_SIZE) {
                GridLine = new Line () {
                X1 = i,
                Y1 = 0,
                X2 = i,
                Y2 = this.ActualHeight
                };
                CanvasView.Children.Add (GridLine);
            }
            for (int i = 0; i < this.ActualHeight; i += GRID_SIZE) {
                GridLine = new Line () {
                X1 = 0,
                Y1 = i,
                X2 = this.ActualWidth,
                Y2 = i
                };
                CanvasView.Children.Add (GridLine);
            }
        }
        //イベントハンドラここまで

    }

}