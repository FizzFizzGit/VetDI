using System.ComponentModel;

namespace VetDI
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private const string KeywordSearchDefaultText = "検索";
        public string Keyword;

        // 必要に応じて他のプロパティやコマンドも追加

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void OnWindowClosing()
        {
            // データ保存やクリーンアップ処理
        }

        public void ClearKeywordIfActivate()
        {
            if (Keyword == KeywordSearchDefaultText)
                Keyword = "";
        }

        public void SetKeywordIfDeactivate()
        {
            if (Keyword == "")
                Keyword = KeywordSearchDefaultText;
        }
    }
}