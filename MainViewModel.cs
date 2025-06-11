using System.ComponentModel;

namespace VetDI {
    public class MainViewModel : INotifyPropertyChanged {
        private const string KeywordSearchDefaultText = "検索";
        private string _keyword = KeywordSearchDefaultText; // 初期値をデフォルトテキストに設定
        public string Keyword {
            get => _keyword;
            set {
                if (_keyword != value) {
                    _keyword = value;
                    OnPropertyChanged(nameof(Keyword));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void OnWindowClosing() {
            // データ保存やクリーンアップ処理
        }

        public void OnSubmit() {
            // 検索処理の実装
            if (string.IsNullOrEmpty(Keyword)) { return; } // キーワードが空の場合は何もしない
            else if (Keyword == KeywordSearchDefaultText) { return; } // デフォルトテキストの場合も何もしない
            else { Keyword = KeywordSearchDefaultText; } // 検索後にデフォルトテキストに戻す
        }

        public void ClearKeywordIfActivate() {
            if (Keyword == KeywordSearchDefaultText)
                Keyword = "";
        }

        public void SetKeywordIfDeactivate() {
            if (Keyword == "")
                Keyword = KeywordSearchDefaultText;
        }
    }
}