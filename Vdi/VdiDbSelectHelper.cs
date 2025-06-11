using System.Collections.Generic;
using System.Data.SQLite;

namespace VetDI.Vdi {
    public static class VdiDbSelectHelper {
        public static List<MainDataType> ToMainDataTypeList(SQLiteDataReader reader) {
            var result = new List<MainDataType>();
            while (reader.Read()) {
                result.Add(new MainDataType {
                    Serial = reader.GetInt32(reader.GetOrdinal("serial")),
                    Issuing = reader.GetInt32(reader.GetOrdinal("issuing")),
                    Name = reader["name"].ToString(),
                    Address = reader["address"].ToString(),
                    Phone = reader["phone"].ToString(),
                    Cause = reader["cause"].ToString(),
                    Prescription = reader["Prescription"].ToString(),
                    Amount = reader["amount"].ToString(),
                    Type = reader["type"].ToString(),
                    Count = reader["count"].ToString(),
                    Age = reader["age"].ToString(),
                    Aspect = reader["aspect"].ToString(),
                    Regimen = reader["regimen"].ToString(),
                    Dosage = reader["dosage"].ToString(),
                    Taking = reader["taking"].ToString(),
                    Holidays = reader["holidays"].ToString(),
                    Other = reader["other"].ToString()
                });
            }
            return result;
        }
    }
}
