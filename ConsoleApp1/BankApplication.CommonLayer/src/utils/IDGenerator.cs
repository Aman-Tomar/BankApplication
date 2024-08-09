using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.utils
{
    public class IDGenerator
    {
        private static readonly string filePath = "src/properties/IdNumber.properties";

        public static string GenerateAccNo(string accType)
        {
            return $"{accType.Substring(0, 3).ToUpper()}{GenerateId()}";
        }

        public static int GenerateId()
        {
            int currentID = ReadCurrentId();
            int newID = currentID + 1;
            WriteNewId(newID);
            return newID;
        }

        private static int ReadCurrentId()
        {
            if (!File.Exists(filePath))
            {
                // Create the file with an initial ID if it doesn't exist
                File.WriteAllText(filePath, "1001");
            }

            string idStr = File.ReadAllText(filePath);
            if (int.TryParse(idStr, out int currentID))
            {
                return currentID;
            }
            else
            {
                throw new Exception("Invalid ID in file.");
            }
        }

        private static void WriteNewId(int newID)
        {
            File.WriteAllText(filePath, newID.ToString());
        }
    }
}
