using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace VetDI {
    /// <summary>
    /// CSVカラムとDBフィールドのマッピングダイアログ
    /// </summary>
    public partial class CsvMappingDialog : Window {
        // 1列分のマッピング情報
        public class MappingRow {
            public string CsvColumn { get; set; } // CSV列名
            public string DbField { get; set; }   // DBフィールド名
        }

        public List<MappingRow> MappingRows { get; set; } // マッピング行リスト
        public List<string> DbFields { get; set; } // 選択可能なDBフィールド名

        // コンストラクタ
        public CsvMappingDialog(List<string> csvColumns, List<string> dbFields) {
            InitializeComponent();
            DbFields = dbFields;
            MappingRows = csvColumns.Select(col => new MappingRow { CsvColumn = col, DbField = dbFields.FirstOrDefault() }).ToList();
            DataContext = this;
        }

        private void Ok_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
            Close();
        }
        // マッピング結果（DBフィールドの並び順リスト）を取得
        public List<string> GetFieldOrder() {
            return MappingRows.Select(r => r.DbField).ToList();
        }
    }
}
