using System.Windows;

namespace VetDI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {

        //メンバ変数
        private const string ABOUT_DESCRIPTION = "about\n\n";
        private const string SEARCH_DEFAULT_TEXT = "検索";

        //コンストラクタ
        public MainWindow () {
            InitializeComponent ();
            Sqlite cls = new Sqlite();
            cls.Init(this);
            cls.Test();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;

        }

        private void MainWindow_Closing (object sender, System.ComponentModel.CancelEventArgs e) { }

        private void MainWindow_Loaded (object sender, RoutedEventArgs e) { }

        private void Button_About_Click (object sender, RoutedEventArgs e) {
            MessageBox.Show (messageBoxText: ABOUT_DESCRIPTION, caption: "About");
        }
        private void Button_Submit_Click (object sender, RoutedEventArgs e) { }

        private void OnGotFocusHandler (object sender, RoutedEventArgs e) {
            if (Keyword.Text == SEARCH_DEFAULT_TEXT) { Keyword.Text = ""; }

        }

        private void OnLostFocusHandler (object sender, RoutedEventArgs e) {
            if (Keyword.Text == "") {
                Keyword.Text = SEARCH_DEFAULT_TEXT;

            }

        }

    }
    
}