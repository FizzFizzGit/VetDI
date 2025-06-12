using System;
using System.Data.SQLite;
using System.Collections.Generic;
using VetDI.Vdi;

namespace VetDI {
    public class SQLiteController {
        private readonly string dbFile;
        private SQLiteConnection connection;

        public SQLiteController(string dbFile) {
            this.dbFile = dbFile;
        }

        public void Open() {
            if (connection == null) {
                connection = new SQLiteConnection($"Data Source={dbFile}");
                connection.Open();
            }
        }

        public void Close() {
            if (connection != null) {
                connection.Close();
                connection = null;
            }
        }

        public SQLiteConnection GetConnection() {
            if (connection == null) Open();
            return connection;
        }

        // SQL文とパラメータを受けて実行する汎用メソッドを追加
        public int ExecuteNonQuery(string sql, Dictionary<string, object> parameters) {
            if (connection == null) Open();
            using (var cmd = new SQLiteCommand(sql, connection)) {
                if (parameters != null) {
                    foreach (var kv in parameters) {
                        cmd.Parameters.AddWithValue(kv.Key, kv.Value ?? DBNull.Value);
                    }
                }
                return cmd.ExecuteNonQuery();
            }
        }

        // 新しいメソッド: SQL コマンドを作成する
        public SQLiteCommand CreateCommand(string sql)
        {
            var cmd = new SQLiteCommand(sql, GetConnection());
            return cmd;
        }
    }
}
