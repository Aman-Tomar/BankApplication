using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.enums;

namespace BankApplication.BusinessLayer.src.managers
{
    /// <summary>
    /// Manages account privilege-related operations, such as retrieving daily withdrawal limits based on privilege type.
    /// </summary>
    public static class AccountPrivilegeManager
    {
        private static Dictionary<PrivilegeType, double> dailyLimits = new Dictionary<PrivilegeType, double>();

        static AccountPrivilegeManager()
        {
            LoadDailyLimits();
        }

        /// <summary>
        /// Loads daily withdrawal limits from a properties file.
        /// </summary>
        private static void LoadDailyLimits()
        {
            // Reads each line from the daily limit properties file
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

        /// <summary>
        /// Retrieves the daily withdrawal limit for a specific privilege type.
        /// </summary>
        /// <param name="privilegeType">The privilege type to query.</param>
        /// <returns>The daily withdrawal limit associated with the given privilege type.</returns>
        /// <exception cref="ArgumentException">Thrown if the privilege type is invalid.</exception>
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
