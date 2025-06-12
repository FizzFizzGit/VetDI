using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace VetDI.Csv {
    // CSV列とDBフィールドのマッピング情報
    public class CsvMapping {
        public List<string> FieldOrder { get; set; } = new List<string>();
        public static string MappingFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csv_mapping.json");

        // 保存
        public void Save() {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(MappingFilePath, json);
        }
        // 読み込み
        public static CsvMapping Load() {
            if (!File.Exists(MappingFilePath)) return null;
            var json = File.ReadAllText(MappingFilePath);
            return JsonSerializer.Deserialize<CsvMapping>(json);
        }
    }
}
