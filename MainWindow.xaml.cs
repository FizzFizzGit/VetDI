using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Data.SQLite;

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
            var sqlConnectionSb = new SQLiteConnectionStringBuilder{DataSource = "vdi.db"};
            using(var cn = new SQLiteConnection(sqlConnectionSb.ToString())){
                cn.Open();
                using(var cmd = new SQLiteCommand(cn)){
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS vdi(" + 
                        "serial INTEGER NOT NULL PRIMARY KEY," +
                        "issuing INTEGER NOT NULL," +
                        "name TEXT NOT NULL," +
                        "address TEXT NOT NULL," +
                        "phone TEXT NOT NULL," +
                        "cause TEXT NOT NULL," +
                        "dragname TEXT NOT NULL," +
                        "dragqty TEXT NOT NULL," +
                        "species TEXT NOT NULL," +
                        "headage TEXT NOT NULL," +
                        "age TEXT NOT NULL," +
                        "aspect TEXT NOT NULL," +
                        "regimen TEXT NOT NULL," +
                        "dosage TEXT NOT NULL," +
                        "taking TEXT NOT NULL," +
                        "holidays TEXT NOT NULL," +
                        "other TEXT NOT NULL)";
                    cmd.ExecuteNonQuery();
                    cmd.Insertvdi(20130011, 
                        111111, 
                        "(有)大窪ファーム", 
                        "志布志市松山町新橋３８２５", 
                        "099-487-2690",
                        "AR・豚丹毒の予防",
                        "AR-BP豚丹毒混合不活化ワクチン",
                        "50ml*3",
                        "母豚",
                        "１５頭",
                        "１～３歳齢",
                        "無",
                        "筋注",
                        "5ml/頭、２回投与",
                        "分娩前１ヶ月、２ヶ月の２回接種",
                        "無",
                        "無");
                }
            }
            dataGrid1.ItemsSource = new ObservableCollection<Instructions>{
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
            if (Keyword.Text == SEARCH_DEFAULT_TEXT) { Keyword.Text = ""; }

        }

        private void OnLostFocusHandler (object sender, RoutedEventArgs e) {
            if (Keyword.Text == "") {
                Keyword.Text = SEARCH_DEFAULT_TEXT;

            }

        }

    }
    public static class SQLiteExtention{
        public static int Insertvdi(this SQLiteCommand cmd, int serial, int issuing, string name, string address, string phone, string cause, string dragname,
            string dragqty, string species, string headage, string age, string aspect, string regimen, string dosage, string taking, string holidays, string other){
            cmd.CommandText = "INSERT INTO vdi(serial, issuing, name, address, phone, cause, dragname, dragqty, species, headage, age, aspect, regimen, dosage, taking, holidays, other) " +
                $"VALUES({serial}, {issuing}, '{name}', '{address}', '{phone}', '{cause}', '{dragname}', '{dragqty}', '{species}', '{headage}', '{age}', '{aspect}', '{regimen}', '{dosage}', '{taking}', '{holidays}', '{other}')";
            return cmd.ExecuteNonQuery();
        }
    }

}