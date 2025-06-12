using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace VetDI.Vdi {
    /// <summary>
    /// VDIデータベース操作クラス
    /// SQLiteControllerを利用してDBアクセスを行う
    /// </summary>
    public class VdiDb {
        /// <summary>
        /// テーブル定義SQLをファイルから取得
        /// </summary>
        private static string GetTableDefinition() {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SRC", "Vdi", "Resources", "vdi_table.sql");
            return File.ReadAllText(path);
        }

        /// <summary>
        /// INSERT用SQLをファイルから取得
        /// </summary>
        private static string GetInsertSql() {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SRC", "Vdi", "Resources", "vdi_insert.sql");
            return File.ReadAllText(path);
        }

        /// <summary>
        /// 全件SELECT用SQLをファイルから取得
        /// </summary>
        private static string GetSelectAllSql() {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SRC", "Vdi", "Resources", "vdi_select_all.sql");
            return File.ReadAllText(path);
        }

        /// <summary>
        /// データベースファイル名
        /// </summary>
        private readonly string dbFile = "vdi.db";
        /// <summary>
        /// DBコントローラー
        /// </summary>
        private readonly SQLiteController controller;

        /// <summary>
        /// コンストラクタ
        /// DBファイルがなければ新規作成し、テーブル定義も実行
        /// </summary>
        public VdiDb() {
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

        /// <summary>
        /// データをINSERTする
        /// </summary>
        /// <param name="data">登録するデータ</param>
        /// <returns>登録件数（重複時は0）</returns>
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

        /// <summary>
        /// 全件SELECTしてリストで返す
        /// </summary>
        /// <returns>全データのリスト</returns>
        public List<MainDataType> SelectAll() {
            string query = GetSelectAllSql();
            using (var cmd = controller.CreateCommand(query))
            using (var reader = cmd.ExecuteReader()) {
                return VdiDbSelectHelper.ToMainDataTypeList(reader);
            }
        }

        /// <summary>
        /// デストラクタ
        /// コントローラーのクローズ処理
        /// </summary>
        ~VdiDb() {
            controller?.Close();
        }
    }
}
