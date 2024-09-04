using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.exceptions;
using BankApplication.CommonLayer.src.interfaces;
using BankApplication.BusinessLayer.src.models;

namespace BankApplication.BusinessLayer.src.factories
{
    /// <summary>
    /// A singleton factory class responsible for managing and providing instances of <see cref="IPolicy"/> based on account type and privilege.
    /// This class loads policy configurations from a properties file and provides methods to create and retrieve policies.
    /// </summary>
    public class PolicyFactory
    {
        private readonly Dictionary<string, IPolicy> policies;
        private static readonly PolicyFactory instance = new PolicyFactory();

        // Gets the singleton instance of the PolicyFactory class.
        public static PolicyFactory Instance => instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyFactory"/> class.
        /// This constructor is private to enforce the singleton pattern and to initialize the policies.
        /// </summary>
        private PolicyFactory()
        {
            policies = new Dictionary<string, IPolicy>();
            LoadPolicies();
        }

        /// <summary>
        /// Loads policy configurations from a properties file and populates the policies dictionary.
        /// The properties file should contain policy key-value pairs where each key is a combination of account type and privilege,
        /// and each value contains minimum balance and rate of interest.
        /// </summary>
        private void LoadPolicies()
        {

            string[] lines = File.ReadAllLines("src/properties/Policies.properties");
            foreach (string line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length == 2)
                {
                    string policyKey = parts[0];
                    var policyValues = parts[1].Split(',');

                    if (policyValues.Length == 2 &&
                        double.TryParse(policyValues[0], out var minBalance) &&
                        double.TryParse(policyValues[1], out var rateOfInterest))
                    {
                        policies[policyKey] = new Policy(minBalance, rateOfInterest);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a policy based on the specified account type and privilege.
        /// </summary>
        /// <param name="accType">The account type (e.g., saving, current).</param>
        /// <param name="privilege">The privilege type (e.g., regular, gold, premium).</param>
        /// <returns>An instance of <see cref="IPolicy"/> corresponding to the specified account type and privilege.</returns>
        /// <exception cref="InvalidPolicyTypeException">Thrown when an invalid policy type is specified.</exception>
        public IPolicy CreatePolicy(string accType, string privilege)
        {
            string key = $"{accType}-{privilege}";
            if (policies.TryGetValue(key, out IPolicy policy))
            {
                return policy;
            }
            throw new InvalidPolicyTypeException("Invalid policy type.");
        }

        /// <summary>
        /// Retrieves all loaded policies.
        /// </summary>
        /// <returns>A dictionary of policies with keys representing the account type and privilege, and values representing <see cref="IPolicy"/> instances.</returns>
        public Dictionary<string, IPolicy> GetPolicies()
        {
            return policies;
        }
    }
}
