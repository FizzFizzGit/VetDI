using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace VetDI.Csv {
    public partial class CsvMappingDialog : Window {
        public class MappingRow {
            public string CsvColumn { get; set; }
            public string DbField { get; set; }
        }
        public List<MappingRow> MappingRows { get; set; }
        public List<string> DbFields { get; set; }
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
        public List<string> GetFieldOrder() {
            return MappingRows.Select(r => r.DbField).ToList();
        }
    }
}
