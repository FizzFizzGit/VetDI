using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using VetDI.Vdi;

namespace VetDI.Csv {
    public static class CsvImporter {
        // CSVファイルを読み込んでMainDataTypeリストに変換
        public static List<MainDataType> ReadCsv(string filePath) {
            var result = new List<MainDataType>();
            using (var reader = new StreamReader(filePath)) {
                string headerLine = reader.ReadLine();
                if (headerLine == null) return result;
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var fields = ParseCsvLine(line);
                    if (fields.Count < 18) continue; // 必須項目数
                    result.Add(new MainDataType {
                        Serial = int.TryParse(fields[0], out var serial) ? serial : 0,
                        Issuing = int.TryParse(fields[1], out var issuing) ? issuing : 0,
                        Name = fields[2],
                        Address = fields[3],
                        Phone = fields[4],
                        Cause = fields[5],
                        Prescription = fields[6],
                        Amount = fields[7],
                        Type = fields[8],
                        Count = fields[9],
                        Age = fields[10],
                        Aspect = fields[11],
                        Regimen = fields[12],
                        Dosage = fields[13],
                        Taking = fields[14],
                        Holidays = fields[15],
                        Other = fields[16]
                    });
                }
            }
            return result;
        }

        // 列マッピングに対応したCSV読み込み
        public static List<MainDataType> ReadCsvWithMapping(string filePath, List<string> fieldOrder) {
            var result = new List<MainDataType>();
            using (var reader = new StreamReader(filePath)) {
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var fields = ParseCsvLine(line);
                    var data = new MainDataType();
                    for (int i = 0; i < fields.Count && i < fieldOrder.Count; i++) {
                        var value = fields[i];
                        switch (fieldOrder[i]) {
                            case "Serial": data.Serial = int.TryParse(value, out var serial) ? serial : 0; break;
                            case "Issuing": data.Issuing = int.TryParse(value, out var issuing) ? issuing : 0; break;
                            case "Name": data.Name = value; break;
                            case "Address": data.Address = value; break;
                            case "Phone": data.Phone = value; break;
                            case "Cause": data.Cause = value; break;
                            case "Prescription": data.Prescription = value; break;
                            case "Amount": data.Amount = value; break;
                            case "Type": data.Type = value; break;
                            case "Count": data.Count = value; break;
                            case "Age": data.Age = value; break;
                            case "Aspect": data.Aspect = value; break;
                            case "Regimen": data.Regimen = value; break;
                            case "Dosage": data.Dosage = value; break;
                            case "Taking": data.Taking = value; break;
                            case "Holidays": data.Holidays = value; break;
                            case "Other": data.Other = value; break;
                        }
                    }
                    result.Add(data);
                }
            }
            return result;
        }

        // シンプルなCSVパース（カンマ区切り、ダブルクォート対応）
        private static List<string> ParseCsvLine(string line) {
            var result = new List<string>();
            bool inQuotes = false;
            string current = "";
            foreach (char c in line) {
                if (c == '"') {
                    inQuotes = !inQuotes;
                } else if (c == ',' && !inQuotes) {
                    result.Add(current);
                    current = "";
                } else {
                    current += c;
                }
            }
            result.Add(current);
            return result;
        }
    }
}
