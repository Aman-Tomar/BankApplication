using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.interfaces
{
    /// <summary>
    /// Defines the contract for a policy in the banking system. Implementing classes must provide
    /// methods to retrieve the minimum balance required and the rate of interest for the policy.
    /// </summary>
    public interface IPolicy
    {
        /// <summary>
        /// Gets the minimum balance required for the policy.
        /// </summary>
        /// <returns>The minimum balance required.</returns>
        public double GetMinBalance();

        /// <summary>
        /// Gets the rate of interest associated with the policy.
        /// </summary>
        /// <returns>The rate of interest.</returns>
        public double GetRateOfInterest();
    }
}
