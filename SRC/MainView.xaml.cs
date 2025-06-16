using System;
using System.IO;
using System.Windows;
using System.Text.Json;
using System.Windows.Controls;
using System.Collections.Generic;

namespace VetDI {

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
                ///ImportCsvWithDialog();
            }
        }


        private void AddColumn() {
            if (DataContext is MainViewModel vm) {
                ///AddColumn?.Invoke("ColumnName", 0); // 引数は適宜変更
            }
        }
    }
}