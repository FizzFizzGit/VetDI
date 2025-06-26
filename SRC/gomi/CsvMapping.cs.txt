using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace VetDI {
    // CSV列とDBフィールドのマッピング情報
    public class CsvMapping {
        public List<string> FieldOrder { get; set; } = new List<string>();
        public static string MappingFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csv_mapping.json");

        // 保存
        public void Save() {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(MappingFilePath, json);
        }
        // 常にjsonがなければ自動生成し、存在すればそれを返す
        public static CsvMapping LoadOrCreate() {
            if (!File.Exists(MappingFilePath)) {
                var defaultOrder = typeof(MainDataType).GetProperties().Select(p => p.Name).ToList();
                var mapping = new CsvMapping { FieldOrder = defaultOrder };
                mapping.Save();
                return mapping;
            }
            var json = File.ReadAllText(MappingFilePath);
            return JsonSerializer.Deserialize<CsvMapping>(json);
        }
    }
}
