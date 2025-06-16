
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace VetDI {

    public class MainViewModel : INotifyPropertyChanged {

        //定数
        private const string KeywordSearchDefaultText = "検索";

        //メンバ変数
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

        public void LoadDataFromSQLite(string tableName = "MainTable") {
            Items.Clear();
            var vdiDb = new VdiDb(tableName);
            foreach (var item in vdiDb.SelectAll()) {
                Items.Add(item);
            }
        }
    }
}