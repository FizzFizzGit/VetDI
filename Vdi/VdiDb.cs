using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace VetDI.Vdi {
    public class VdiDb {
        private static string GetTableDefinition() {
            var path = Path.Combine("Resources", "vdi_table.sql");
            return File.ReadAllText(path);
        }

        private static string GetInsertSql() {
            var path = Path.Combine("Resources", "vdi_insert.sql");
            return File.ReadAllText(path);
        }

        private static string GetSelectAllSql() {
            var path = Path.Combine("Resources", "vdi_select_all.sql");
            return File.ReadAllText(path);
        }

        private readonly string dbFile = "vdi.db";
        private readonly SQLiteController controller;
        private readonly SQLiteConnection connection;

        public VdiDb() {
            controller = new SQLiteController(dbFile);
            connection = controller.GetConnection();
            if (connection.State != System.Data.ConnectionState.Open) {
                connection.Open();
            }
            using (var cmd = new SQLiteCommand(GetTableDefinition(), connection)) {
                cmd.ExecuteNonQuery();
            }
        }

        public int InsertVDI(MainDataType data) {
            try {
                var sql = GetInsertSql();
                var parameters = VdiDbParameterHelper.ToParameterDictionary(data);
                return controller.ExecuteNonQuery(sql, parameters);
            } catch (SQLiteException ex) {
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
            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader()) {
                return VdiDbSelectHelper.ToMainDataTypeList(reader);
            }
        }

        ~VdiDb() {
            if (connection != null) {
                connection.Close();
            }
        }
    }
}
