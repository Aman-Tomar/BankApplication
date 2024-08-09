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
    public class PolicyFactory
    {
        private readonly Dictionary<string, IPolicy> policies;
        private static readonly PolicyFactory instance = new PolicyFactory();
        private PolicyFactory()
        {
            policies = new Dictionary<string, IPolicy>();
            LoadPolicies();
        }

        public static PolicyFactory Instance => instance;

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

        public IPolicy CreatePolicy(string accType, string privilege)
        {
            string key = $"{accType}-{privilege}";
            if (policies.TryGetValue(key, out IPolicy policy))
            {
                return policy;
            }
            throw new InvalidPolicyTypeException("Invalid policy type.");
        }

        public Dictionary<string, IPolicy> GetPolicies()
        {
            return policies;
        }
    }
}
