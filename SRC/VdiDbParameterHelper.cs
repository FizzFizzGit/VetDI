using System.Collections.Generic;

namespace VetDI {
    public static class VdiDbParameterHelper {
        public static Dictionary<string, object> ToParameterDictionary(MainDataType data) {
            return new Dictionary<string, object> {
                {"@serial", data.Serial},
                {"@issuing", data.Issuing},
                {"@name", data.Name},
                {"@address", data.Address},
                {"@phone", data.Phone},
                {"@cause", data.Cause},
                {"@Prescription", data.Prescription},
                {"@amount", data.Amount},
                {"@type", data.Type},
                {"@count", data.Count},
                {"@age", data.Age},
                {"@aspect", data.Aspect},
                {"@regimen", data.Regimen},
                {"@dosage", data.Dosage},
                {"@taking", data.Taking},
                {"@holidays", data.Holidays},
                {"@other", data.Other}
            };
        }
    }
}
