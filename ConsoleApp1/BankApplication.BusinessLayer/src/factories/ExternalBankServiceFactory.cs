using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.interfaces;

namespace BankApplication.BusinessLayer.src.factories
{
    /// <summary>
    /// A singleton factory class responsible for providing instances of <see cref="IExternalBankService"/> based on bank codes.
    /// This class loads external bank service configurations from a properties file and manages access to the external bank services.
    /// </summary>
    public class ExternalBankServiceFactory
    {
        private static readonly ExternalBankServiceFactory instance = new ExternalBankServiceFactory();
        private readonly Dictionary<string, IExternalBankService> serviceBank;

        // Gets the singleton instance of the ExternalBankServiceFactory class.
        public static ExternalBankServiceFactory Instance => instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalBankServiceFactory"/> class.
        /// This constructor is private to enforce the singleton pattern.
        /// </summary>
        private ExternalBankServiceFactory()
        {
            serviceBank = new Dictionary<string, IExternalBankService>();
            LoadServices();
        }

        
        /// <summary>
        /// Loads external bank services from a properties file and populates the service bank dictionary.
        /// The properties file should contain bank code and service mappings.
        /// </summary>
        private void LoadServices()
        {
            var properties = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines("serviceBanks.properties");

            foreach (string line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length == 2)
                {
                    properties[parts[0]] = parts[1];
                }
            }
        }

        /// <summary>
        /// Retrieves the <see cref="IExternalBankService"/> instance associated with the specified bank code.
        /// </summary>
        /// <param name="bankCode">The bank code used to lookup the external bank service.</param>
        /// <returns>An instance of <see cref="IExternalBankService"/> corresponding to the provided bank code.</returns>
        /// <exception cref="ArgumentException">Thrown when the provided bank code does not match any known service.</exception>
        public IExternalBankService GetService(string bankCode)
        {
            if (serviceBank.TryGetValue(bankCode, out IExternalBankService service))
            {
                return service;
            }
            throw new ArgumentException("Invalid bank code.");
        }
    }
}
