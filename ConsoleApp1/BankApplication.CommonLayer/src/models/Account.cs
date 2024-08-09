using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.interfaces;

namespace BankApplication.CommonLayer.src.models
{
    public abstract class Account : IAccount
    {
        public string AccNo { get; set; }
        public string Name { get; set; }
        public string Pin { get; set; }
        public bool Active { get; set; }
        public DateTime DateOfOpening { get; set; }
        public double Balance { get; set; }
        public PrivilegeType PrivilegeType { get; set; }
        public IPolicy Policy { get; set; }
        public abstract bool Open();
        public abstract bool Close();
        public abstract string GetAccType();
    }
}
