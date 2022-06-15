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
        private const string SEARCH_DEFAULT_TEXT = "検索";

        //コンストラクタ
        public MainWindow () {
            InitializeComponent ();
            DataView.dataGrid.ItemsSource = new ObservableCollection<Instructions>{
                new Instructions {
                    Serial=20130011,
                    Issuing=111111,
                    Name="(有)大窪ファーム",
                    Address="志布志市松山町新橋３８２５",
                    Phone="099-487-2690",
                    Cause="AR・豚丹毒の予防",
                    Drugname="AR-BP豚丹毒混合不活化ワクチン",
                    Drugqty="50ml*3",
                    Species="母豚",
                    Headage="１５頭",
                    Age="１～３歳齢",
                    Aspect="無",
                    Regimen="筋注",
                    Dosage="5ml/頭、２回投与",
                    Taking="分娩前１ヶ月、２ヶ月の２回接種",
                    Holidays="無",
                    Other="無"
                }
            };
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
            if (DataView.Keyword.Text == SEARCH_DEFAULT_TEXT) { DataView.Keyword.Text = ""; }

        }

        private void OnLostFocusHandler (object sender, RoutedEventArgs e) {
            if (DataView.Keyword.Text == "") {
                DataView.Keyword.Text = SEARCH_DEFAULT_TEXT;
                DataView.Keyword.Background = Brushes.Transparent;

            }

        }

    }

}