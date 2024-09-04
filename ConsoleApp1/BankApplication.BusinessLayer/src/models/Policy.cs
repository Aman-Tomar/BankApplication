using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.interfaces;

namespace BankApplication.BusinessLayer.src.models
{
    /// <summary>
    /// Represents a policy that defines the minimum balance and rate of interest for an account.
    /// </summary>
    public class Policy : IPolicy
    {
        public double MinimumBalance { get; }
        public double RateOfInterest { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Policy"/> class with the specified minimum balance and rate of interest.
        /// </summary>
        /// <param name="minimumBalance">The minimum balance required by the policy.</param>
        /// <param name="rateOfInterest">The rate of interest defined by the policy.</param>
        public Policy(double minimumBalance, double rateOfInterest)
        {
            MinimumBalance = minimumBalance;
            RateOfInterest = rateOfInterest;
        }

        /// <summary>
        /// Gets the minimum balance required by the policy.
        /// </summary>
        /// <returns>The minimum balance required.</returns>
        public double GetMinBalance()
        {
            return MinimumBalance; ;
        }

        /// <summary>
        /// Gets the rate of interest defined by the policy.
        /// </summary>
        /// <returns>The rate of interest.</returns>
        public double GetRateOfInterest()
        {
            return RateOfInterest;
        }
    }
}
