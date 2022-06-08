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
        private const string TEXTBOX_DEFAULT_TEXT = "検索";

        //コンストラクタ
        public MainWindow () {
            InitializeComponent ();
            ListItems = new ObservableCollection<ListItem> ();
            this.DataContext = ListItems;
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;

        }

        private void MainWindow_Closing (object sender, System.ComponentModel.CancelEventArgs e) { }

        private void MainWindow_Loaded (object sender, RoutedEventArgs e) { }

        private void Button_About_Click (object sender, RoutedEventArgs e) {
            MessageBox.Show (messageBoxText: ABOUT_DESCRIPTION, caption: "About");
        }
        private void Button_Submit_Click (object sender, RoutedEventArgs e) { }

        private void Button_Erase_Click (object sender, RoutedEventArgs e) {
            int itemIndex = URLList.SelectedIndex;

            if (itemIndex >= 0) { ListItems.RemoveAt (index: itemIndex); }

        }

        private void OnGotFocusHandler (object sender, RoutedEventArgs e) {
            if (URL_Text.Text == TEXTBOX_DEFAULT_TEXT) { URL_Text.Text = ""; }

        }

        private void OnLostFocusHandler (object sender, RoutedEventArgs e) {
            if (URL_Text.Text == "") {
                URL_Text.Text = TEXTBOX_DEFAULT_TEXT;
                URL_Text.Background = Brushes.Transparent;

            }

        }

    }