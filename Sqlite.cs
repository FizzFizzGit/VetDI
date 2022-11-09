using System.Data;
using System.Windows;
using System.Data.SQLite;

namespace VetDI{
    public class Sqlite{
        private const string NewDBSQL = "CREATE TABLE IF NOT EXISTS vdi(" + 
            "serial INTEGER NOT NULL PRIMARY KEY," +
            "issuing INTEGER NOT NULL," +
            "name TEXT NOT NULL," +
            "address TEXT NOT NULL," +
            "phone TEXT NOT NULL," +
            "cause TEXT NOT NULL," +
            "dragname TEXT NOT NULL," +
            "dragqty TEXT NOT NULL," +
            "species TEXT NOT NULL," +
            "headage TEXT NOT NULL," +
            "age TEXT NOT NULL," +
            "aspect TEXT NOT NULL," +
            "regimen TEXT NOT NULL," +
            "dosage TEXT NOT NULL," +
            "taking TEXT NOT NULL," +
            "holidays TEXT NOT NULL," +
            "other TEXT NOT NULL)";
        private SQLiteConnection connection;
        public void Init(MainWindow par){
            connection = SQLiteController.GetConnection();
            connection.Open();
            var cmd = new SQLiteCommand(connection);
            cmd.CommandText = NewDBSQL;
            cmd.ExecuteNonQuery();
            cmd.InsertVDI(20130011, 
                111111, 
                "(有)大窪ファーム", 
                "志布志市松山町新橋３８２５", 
                "099-487-2690",
                "AR・豚丹毒の予防",
                "AR-BP豚丹毒混合不活化ワクチン",
                "50ml*3",
                "母豚",
                "１５頭",
                "１～３歳齢",
                "無",
                "筋注",
                "5ml/頭、２回投与",
                "分娩前１ヶ月、２ヶ月の２回接種",
                "無",
                "無");
            // データを全件取得する
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM vdi", connection);

            // adapterからDataTableにデータを読み込む
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // dataTableをDataGridのDataContextにセットする
            par.dataGrid1.DataContext = dataTable;

        }
    }
    public static class SQLiteController{
        private const string DBFile = "vdi.db";
        public static SQLiteConnection GetConnection(){
            var sbc = new SQLiteConnectionStringBuilder{DataSource = DBFile};
            return new SQLiteConnection(sbc.ToString());
        }

        //MethodExtended from SQLiteCommand
        public static int InsertVDI(this SQLiteCommand cmd, int serial, int issuing, string name, string address, string phone, string cause, string dragname,
            string dragqty, string species, string headage, string age, string aspect, string regimen, string dosage, string taking, string holidays, string other){
            cmd.CommandText = "INSERT INTO vdi(serial, issuing, name, address, phone, cause, dragname, dragqty, species, headage, age, aspect, regimen, dosage, taking, holidays, other) " +
                $"VALUES({serial}, {issuing}, '{name}', '{address}', '{phone}', '{cause}', '{dragname}', '{dragqty}', '{species}', '{headage}', '{age}', '{aspect}', '{regimen}', '{dosage}', '{taking}', '{holidays}', '{other}')";
            return cmd.ExecuteNonQuery();
        }
    }
}
