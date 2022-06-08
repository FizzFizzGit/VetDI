using System;
using System.Collections.ObjectModel;
using System.Drawing;
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
        private const int GRID_SIZE = 10;

        //コンストラクタ
        public MainWindow () {
            InitializeComponent ();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;

        }

        private void MainWindow_Closing (object sender, System.ComponentModel.CancelEventArgs e) { }

        private void MainWindow_Loaded (object sender, RoutedEventArgs e) { }

        private void Button_About_Click (object sender, RoutedEventArgs e) {
            MessageBox.Show (messageBoxText: ABOUT_DESCRIPTION, caption: "About");
        }
    }
}