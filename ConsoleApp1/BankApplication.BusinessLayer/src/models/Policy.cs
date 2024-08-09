using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.interfaces;

namespace BankApplication.BusinessLayer.src.models
{
    public class Policy : IPolicy
    {
        public double MinimumBalance { get; }
        public double RateOfInterest { get; }

        public Policy(double minimumBalance, double rateOfInterest)
        {
            MinimumBalance = minimumBalance;
            RateOfInterest = rateOfInterest;
        }

        public double GetMinBalance()
        {
            return MinimumBalance; ;
        }

        public double GetRateOfInterest()
        {
            return RateOfInterest;
        }
    }
}
