using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityClasses;

namespace BusinessLayer.services.Interfaces
{
    public interface IAccountBalanceService
    {
        // upload balance
        string UploadBalance(AccountBalance accountbalance);
        // View balance of a month
        AccountBalance ViewBalance(int year, int month);
        // view balances of a time period
        List<AccountBalance> ViewBalanceChart(int startYear, int startMonth, int endYear, int endMonth);
        // method to view current account balance
        double[] ViewCurrentBalance(string accountType);
    }
}
