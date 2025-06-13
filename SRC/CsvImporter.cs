using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VetDI {
    public static class CsvImporter {
        // ヘッダーなしCSVをMainDataTypeリストに変換（列順でマッピング）
        public static List<MainDataType> ReadCsv(string filePath) {
            var result = new List<MainDataType>();
            var props = typeof(MainDataType).GetProperties();
            foreach (var line in File.ReadLines(filePath)) {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var fields = line.Split(',');
                var data = new MainDataType();
                for (int i = 0; i < fields.Length && i < props.Length; i++) {
                    var prop = props[i];
                    object value = fields[i];
                    if (prop.PropertyType == typeof(int)) {
                        if (int.TryParse(fields[i], out int intVal))
                            value = intVal;
                        else
                            value = 0;
                    }
                    prop.SetValue(data, value);
                }
                result.Add(data);
            }
            return result;
        }
        // 列マッピングに対応したCSV読み込み
        public static List<MainDataType> ReadCsvWithMapping(string filePath, List<string> fieldOrder) {
            var result = new List<MainDataType>();
            var propDict = typeof(MainDataType).GetProperties().ToDictionary(p => p.Name);
            foreach (var line in File.ReadLines(filePath)) {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var fields = line.Split(',');
                var data = new MainDataType();
                for (int i = 0; i < fields.Length && i < fieldOrder.Count; i++) {
                    var fieldName = fieldOrder[i];
                    if (!propDict.TryGetValue(fieldName, out var prop)) continue;
                    object value = fields[i];
                    if (prop.PropertyType == typeof(int)) {
                        if (int.TryParse(fields[i], out int intVal))
                            value = intVal;
                        else
                            value = 0;
                    }
                    prop.SetValue(data, value);
                }
                result.Add(data);
            }
            return result;
        }
    }
}
