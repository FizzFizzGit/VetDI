using System;
using System.Windows;
using System.Windows.Media.Imaging;

public partial class Common{
    public void setClipboard(string text){
            Clipboard.SetData(DataFormats.Text,text);

    }
    public string getStatusText(string prefix, string content){
        return prefix + content;

    }
    public static BitmapImage getBitmapImageFromURI(string uri){
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(uri);
            image.EndInit();
            return image;

    }

}
//URLを直接リンクに書き換えるクラス
public partial class GDUtil{
    private const string KEY_WORD = "open?id=";
    private const string KEY_WORD2 = "file/d/";
    private const string REPLACE_WORD = "uc?export=view&id=";
    
    public static string ConvetUrl(string inputString){
        string temp = inputString;

        if(temp.Length <= 0){return "";}

        if(temp.Contains(KEY_WORD)){
            return temp.Replace(KEY_WORD,REPLACE_WORD);
            
        }else if(temp.Contains(KEY_WORD2)){
            string[] splitTemp = temp.Split(KEY_WORD2);
            string domainWord = splitTemp[0];
            string urlParamater = splitTemp[1].Split("/")[0];
            return domainWord + REPLACE_WORD + urlParamater;

        }

        return "";

    }

}
