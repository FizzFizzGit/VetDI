using System.ComponentModel;
using System.Collections.ObjectModel;
using VetDI.Vdi;

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
    }
}