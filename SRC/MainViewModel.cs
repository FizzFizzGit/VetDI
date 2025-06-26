using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Controls; // DataGridColumnを使うため
using System.Runtime.CompilerServices; // EventHandlerを使うため

namespace VetDI {
    // ViewModelクラス
    public class ViewModelBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class MainViewModel : ViewModelBase {

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

        public void OnWindowClosing() {
            // データ保存やクリーンアップ処理
        }

        public void OnSubmit() {
            // 検索処理の実装（例：キーワードでフィルタリングなど）
            //LoadDataFromSQLite();
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