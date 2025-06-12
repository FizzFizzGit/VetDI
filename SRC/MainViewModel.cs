using System.ComponentModel;
using System.Collections.ObjectModel;
using VetDI.Vdi;
using System.Collections.Generic;
using System.Linq;
using VetDI.Csv;

namespace VetDI {
    public class MainViewModel : INotifyPropertyChanged {
        private const string KeywordSearchDefaultText = "検索";
        private string _keyword = KeywordSearchDefaultText;
        public string Keyword {
            get => _keyword;
            set {
                if (_keyword != value) {
                    _keyword = value;
                    OnPropertyChanged(nameof(Keyword));
                }
            }
        }
        public ObservableCollection<MainDataType> Items { get; } = new ObservableCollection<MainDataType>();
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void OnWindowClosing() {
            // データ保存やクリーンアップ処理
        }

        public void OnSubmit() {
            // 検索処理の実装（例：キーワードでフィルタリングなど）
            LoadDataFromSQLite();
        }

        public void ClearKeywordIfActivate() {
            if (Keyword == KeywordSearchDefaultText)
                Keyword = "";
        }

        public void SetKeywordIfDeactivate() {
            if (Keyword == "")
                Keyword = KeywordSearchDefaultText;
        }

        public void LoadDataFromSQLite() {
            Items.Clear();
            var vdiDb = new VdiDb();
            foreach (var item in vdiDb.SelectAll()) {
                Items.Add(item);
            }
        }

        // CSVファイルを指定してDBに一括追加（マッピング対応）
        public void ImportCsvToDb(string csvFilePath) {
            var vdiDb = new VdiDb();
            VetDI.Csv.CsvMapping mapping = VetDI.Csv.CsvMapping.Load();
            List<string> fieldOrder = null;
            if (mapping == null) {
                // CSVの最初の行を読み込んで列数を取得
                var firstLine = System.IO.File.ReadLines(csvFilePath).FirstOrDefault();
                if (firstLine == null) return;
                var columns = firstLine.Split(',');
                // DBフィールド一覧
                var dbFields = new List<string> {
                    "Serial","Issuing","Name","Address","Phone","Cause","Prescription","Amount","Type","Count","Age","Aspect","Regimen","Dosage","Taking","Holidays","Other"
                };
                // マッピングダイアログ表示
                var dialog = new VetDI.Csv.CsvMappingDialog(
                    columns.Select((c, i) => $"列{i+1}").ToList(), dbFields);
                if (dialog.ShowDialog() == true) {
                    fieldOrder = dialog.GetFieldOrder();
                    // 保存
                    new VetDI.Csv.CsvMapping { FieldOrder = fieldOrder }.Save();
                } else {
                    return; // キャンセル
                }
            } else {
                fieldOrder = mapping.FieldOrder;
            }
            var items = VetDI.Csv.CsvImporter.ReadCsvWithMapping(csvFilePath, fieldOrder);
            foreach (var item in items) {
                vdiDb.InsertVDI(item);
            }
            LoadDataFromSQLite(); // 画面リロード
        }

        // ファイルダイアログも含めてインポートをViewModelで完結
        public void ImportCsvWithDialog() {
            var dialog = new Microsoft.Win32.OpenFileDialog {
                Filter = "CSVファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*",
                Title = "CSVファイルを選択"
            };
            if (dialog.ShowDialog() == true) {
                var result = System.Windows.MessageBox.Show(
                    "既存のvdi.dbにCSVデータを追加しますか？\n（OKで追加、キャンセルで中止）\n\n※列のマッピングをやり直したい場合はcsv_mapping.jsonを削除してください。",
                    "DBへ追加確認",
                    System.Windows.MessageBoxButton.OKCancel,
                    System.Windows.MessageBoxImage.Question
                );
                if (result == System.Windows.MessageBoxResult.OK) {
                    ImportCsvToDb(dialog.FileName);
                    System.Windows.MessageBox.Show("CSVからDBへのインポートが完了しました。", "インポート完了");
                }
            }
        }
    }
}