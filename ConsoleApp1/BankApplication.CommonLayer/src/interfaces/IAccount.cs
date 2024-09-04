using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.enums;

namespace BankApplication.CommonLayer.src.interfaces
{
    /// <summary>
    /// Defines the contract for an account in the banking system. Implementing classes must provide
    /// details for account management, including properties for account details and methods for
    /// opening, closing, and retrieving account types.
    /// </summary>
    public interface IAccount
    {
        public string AccNo { get; set; }
        public string Name { get; set; }
        public string Pin { get; set; }
        public bool Active { get; set; }
        public DateTime DateOfOpening { get; set; }
        public double Balance { get; set; }
        public PrivilegeType PrivilegeType { get; set; }
        public IPolicy Policy { get; set; }

        bool Open();
        bool Close();
        string GetAccType();
    }
}
