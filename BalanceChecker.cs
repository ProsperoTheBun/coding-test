using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core
{
    public class BalanceChecker
    {
       public bool Process (decimal amount, Persistence persistence, ExternalApi eA, string aType)
       {
           if (amount < 10)
           {
               Process10();
               return true;
           }

           if (amount > 50 && DateTime.Now.Day > 15)
           {
               return persistence.GetInfo();
           }

           if (amount > 100000)
           {
               return eA.CheckAccountBalance(amount, aType);
           }

           return true;
       }


       private void Process10() => Console.WriteLine("less 10");
    }

    public class Persistence
    {
        public bool GetInfo()
        {
            return true;
        }
    }

    public class ExternalApi
    {
        public bool CheckAccountBalance(decimal amount, string accountType)
        {
            if (amount > 1000000 && accountType == "gold")
                return true;
            else return false;
        }
    }
}
