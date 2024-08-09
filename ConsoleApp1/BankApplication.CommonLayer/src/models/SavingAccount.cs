using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.utils;
using BankApplication.CommonLayer.src.enums;

namespace BankApplication.CommonLayer.src.models
{
    public class SavingAccount : Account
    {
        public SavingAccount() { }
        public SavingAccount(string name, string pin, PrivilegeType privilegeType)
        {
            Name = name;
            Pin = pin;
            privilegeType = privilegeType;
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
            return "SAVING";
        }

        public override bool Close()
        {
            Active = false;
            Balance = 0;
            return !Active;
        }
    }
}

