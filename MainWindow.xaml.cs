using System.Windows;
///using System.Data.SQLite;

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
            InitializeComponent();
            DataContext = new MainViewModel();
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;

        }

        private void MainWindow_Closing (object sender, System.ComponentModel.CancelEventArgs e) {
            if (DataContext is MainViewModel vm)
            {
                vm.OnWindowClosing();
            }
            // 必要ならe.Cancel = true; でキャンセルも可能
        }

        private void MainWindow_Loaded (object sender, RoutedEventArgs e) { }

        private void Button_About_Click (object sender, RoutedEventArgs e) {
            MessageBox.Show(messageBoxText: ABOUT_DESCRIPTION, caption: "About");
        }

        private void Button_Submit_Click(object sender, RoutedEventArgs e) {
            if (DataContext is MainViewModel vm){
                vm.OnSubmit();
            }
        }

        private void OnGotFocusHandler (object sender, RoutedEventArgs e) {
            if (DataContext is MainViewModel vm){
                vm.ClearKeywordIfActivate();
            }
        }

        private void OnLostFocusHandler (object sender, RoutedEventArgs e) {
            if (DataContext is MainViewModel vm){
                vm.SetKeywordIfDeactivate();
            }
        }
    }    
}