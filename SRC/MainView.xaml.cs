using System.Windows;
using System.Text.Json;
using System.IO;
using System.Windows.Controls;
using System;
using System.Collections.Generic;

namespace VetDI {
    public class ColumnConfig {
        public string Header { get; set; }
        public string Binding { get; set; }
        public int Width { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>

    public partial class MainView : Window {
        //メンバ変数
        private const string ABOUT_DESCRIPTION = "about\n\n";
        private const string SEARCH_DEFAULT_TEXT = "検索";
        //コンストラクタ
        public MainView() {
            InitializeComponent();
            DataContext = new MainViewModel();
            Loaded += MainView_Loaded;
            Closing += MainView_Closing;
        }

        private void MainView_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (DataContext is MainViewModel vm) {
                vm.OnWindowClosing();
            }
            // 必要ならe.Cancel = true; でキャンセルも可能
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e) {
            if (DataContext is MainViewModel vm) {
                vm.Keyword = SEARCH_DEFAULT_TEXT; // 初期キーワード設定
                vm.LoadDataFromSQLite(); // データの読み込み
            }
            LoadColumnFromJson(); // JSONからDataGridカラムを動的生成

        }

        private void Button_About_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(messageBoxText: ABOUT_DESCRIPTION, caption: "About");
        }

        private void Button_Submit_Click(object sender, RoutedEventArgs e) {
            if (DataContext is MainViewModel vm) {
                vm.OnSubmit();
            }
        }

        private void OnGotFocusHandler(object sender, RoutedEventArgs e) {
            if (DataContext is MainViewModel vm) {
                vm.ClearKeywordIfActivate();
            }
        }

        private void OnLostFocusHandler(object sender, RoutedEventArgs e) {
            if (DataContext is MainViewModel vm) {
                vm.SetKeywordIfDeactivate();
            }
        }

        private void Button_Convert_Click(object sender, RoutedEventArgs e) {
            if (DataContext is MainViewModel vm) {
                vm.ImportCsvWithDialog();
            }
        }
        
        private void LoadColumnFromJson() {
            try {
                var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SRC", "Resource", "columns.json");
                if (File.Exists(path)) {
                    var json = File.ReadAllText(path);
                    var columns = JsonSerializer.Deserialize<List<ColumnConfig>>(json);
                    MainDataGrid.Columns.Clear();
                    foreach (var col in columns) {
                        MainDataGrid.Columns.Add(new DataGridTextColumn {
                            Header = col.Header,
                            Binding = new System.Windows.Data.Binding(col.Binding),
                            Width = col.Width
                        });
                    }
                }
                else {
                    MessageBox.Show($"{path} が見つかりません。");
                }
            }
            catch { /* 読み込み失敗時は何もしない */ }
        }

    }
}