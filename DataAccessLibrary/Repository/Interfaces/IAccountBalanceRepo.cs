using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.Interfaces
{
    public interface IAccountBalanceRepo
    {
        // upload account balance
        string UploadBalance(accountbalance accountBalance);

        // view balance of an account
        accountbalance ViewBalance(int year, int month);

        // view balances of a time period
        List<accountbalance> ViewBalanceChart(int startYear, int startMonth, int endYear, int endMonth);

        accountbalance ViewCurrentBalance();
        string DeleteAccountBalance(int year, int month);
    }
}
