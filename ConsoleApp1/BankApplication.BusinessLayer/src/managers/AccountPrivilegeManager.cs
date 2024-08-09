using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.enums;

namespace BankApplication.BusinessLayer.src.managers
{
    public static class AccountPrivilegeManager
    {
        private static Dictionary<PrivilegeType, double> dailyLimits = new Dictionary<PrivilegeType, double>();

        static AccountPrivilegeManager()
        {
            string[] lines = File.ReadAllLines("src/properties/dailyLimit.properties");
            foreach (string line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2 && Enum.TryParse(parts[0], out PrivilegeType privilegeType))
                {
                    if (double.TryParse(parts[1], out double limit))
                    {
                        dailyLimits[privilegeType] = limit;
                    }
                }
            }
        }
        public static double GetDailyLimit(PrivilegeType privilegeType)
        {
            if (dailyLimits.TryGetValue(privilegeType, out double limit))
            {
                return limit;
            }
            throw new ArgumentException("Invalid privilege type");
        }
    }
}
