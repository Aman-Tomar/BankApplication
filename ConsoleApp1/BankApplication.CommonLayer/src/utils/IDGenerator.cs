using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.utils
{
    /// <summary>
    /// The <c>IDGenerator</c> class is responsible for generating unique identifiers
    /// for bank accounts. It reads and updates the current ID value stored in a file,
    /// ensuring that each generated ID is unique and sequential.
    /// </summary>
    public class IDGenerator
    {
        // The file path where the current ID number is stored.
        private static readonly string filePath = "src/properties/IdNumber.properties";

        /// <summary>
        /// Generates a unique account number based on the account type.
        /// The account number is composed of the first three characters of the account type
        /// in uppercase, followed by a unique numeric ID.
        /// </summary>
        /// <param name="accType">The type of the account (e.g., "SAVING", "CURRENT").</param>
        /// <returns>A unique account number as a string.</returns>
        public static string GenerateAccNo(string accType)
        {
            return $"{accType.Substring(0, 3).ToUpper()}{GenerateId()}";
        }

        /// <summary>
        /// Generates a unique numeric ID by incrementing the current ID stored in the file.
        /// </summary>
        /// <returns>A unique numeric ID as an integer.</returns>
        public static int GenerateId()
        {
            int currentID = ReadCurrentId();
            int newID = currentID + 1;
            WriteNewId(newID);
            return newID;
        }

        /// <summary>
        /// Reads the current ID from the file. If the file does not exist, it creates the file
        /// with an initial ID of 1000.
        /// </summary>
        /// <returns>The current ID as an integer.</returns>
        /// <exception cref="Exception">Thrown if the ID in the file is not a valid integer.</exception>
        private static int ReadCurrentId()
        {
            if (!File.Exists(filePath))
            {
                // Create the file with an initial ID if it doesn't exist
                File.WriteAllText(filePath, "1000");
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

        /// <summary>
        /// Writes the new ID to the file, overwriting the current ID.
        /// </summary>
        /// <param name="newID">The new ID to be written to the file.</param>
        private static void WriteNewId(int newID)
        {
            File.WriteAllText(filePath, newID.ToString());
        }
    }
}
