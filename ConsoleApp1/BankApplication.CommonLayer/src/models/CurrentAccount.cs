using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.utils;

namespace BankApplication.CommonLayer.src.models
{
    public class CurrentAccount : Account
    {
        public CurrentAccount() { }
        public CurrentAccount(string name, string pin, PrivilegeType privilegeType)
        {
            Name = name;
            Pin = pin;
            PrivilegeType = privilegeType;
            AccNo = "SAV" + IDGenerator.GenerateId(); 
        }
        public override bool Open()
        {
            Active = true;
            DateOfOpening = DateTime.Now;

            return Active;
        }

        public override string GetAccType()
        {
            return "CURRENT";
        }

        public override bool Close()
        {
            Active = false;
            Balance = 0;
            return !Active;
        }
    }
}
