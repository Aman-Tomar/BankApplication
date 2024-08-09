using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.interfaces
{
    public interface IPolicy
    {
        public double GetMinBalance();

        public double GetRateOfInterest();
    }
}
