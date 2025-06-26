using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace VetDI {
    /// <summary>
    /// VDIデータベース操作クラス
    /// SQLiteControllerを利用してDBアクセスを行う
    /// </summary>
    public class VdiDb {
        private static string GetTableDefinition() {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SRC", "Resource", "vdi_table.sql");
            return File.ReadAllText(path);
        }

        private static string GetInsertSql() {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SRC", "Resource", "vdi_insert.sql");
            return File.ReadAllText(path);
        }

        private static string GetSelectAllSql() {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SRC", "Resource", "vdi_select_all.sql");
            return File.ReadAllText(path);
        }

        public string TableName { get; }
        private readonly string dbFile = "vdi.db";
        private readonly SQLiteController controller;

        public VdiDb(string tableName = "MainTable") {
            TableName = tableName;
            if (!File.Exists(dbFile)) {
                // データベースファイルが存在しない場合は作成
                SQLiteConnection.CreateFile(dbFile);
            }
            controller = new SQLiteController(dbFile);

            // テーブル定義（CREATE TABLE等）を実行
            using (var cmd = controller.CreateCommand(GetTableDefinition())) {
                cmd.ExecuteNonQuery();
            }
        }

        public int InsertVDI(MainDataType data) {
            try {
                var sql = GetInsertSql();
                var parameters = VdiDbParameterHelper.ToParameterDictionary(data);
                return controller.ExecuteNonQuery(sql, parameters);
            }
            catch (SQLiteException ex) {
                // エラーコード19はUNIQUE制約違反
                if (ex.ErrorCode == 19 || ex.Message.Contains("constraint failed") || ex.Message.Contains("UNIQUE")) {
                    // 重複データは無視
                    return 0;
                }
                throw;
            }
        }

        public List<MainDataType> SelectAll() {
            string query = GetSelectAllSql();
            using (var cmd = controller.CreateCommand(query))
            using (var reader = cmd.ExecuteReader()) {
                return VdiDbSelectHelper.ToMainDataTypeList(reader);
            }
        }

        ~VdiDb() {
            controller?.Close();
        }
    }
}
