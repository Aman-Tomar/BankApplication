using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.BusinessLayer.src.utils
{
    public class IDGenerator
    {
        private static int ID = 1000;
        public static int GenerateId()
        {
            return ID++;
        }
    }
}
