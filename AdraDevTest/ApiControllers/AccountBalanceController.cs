using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdraDevTest.ApiModels;
using BusinessLayer.services.Interfaces;
using EntityClasses;
using System.Web.Http.Cors;

namespace AdraDevTest.ApiControllers
{
    [EnableCors(origins: "http://localhost:9000", headers: "*", methods: "*")]

    public class AccountBalanceController : ApiController
    {
        private IAccountBalanceService _AccountBalanceService; // create object of AccountBalanceService in BL

        public AccountBalanceController(IAccountBalanceService accountBalanceService)
        {
            _AccountBalanceService = accountBalanceService;
        }

        // method to upload the account balances
        [Route("api/AccountBalance/UploadBalance")]
        [HttpPost]
        public string UploadBalance(UserRequest userRequest)
        {
            // array of moths to get index of the month
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            string result = "";

            // content of the file
            string fileContent = userRequest.fileContent;
            // year of the uploaded fbalance file
            int year = userRequest.year;


            char delimiterNewLine = '\n';
            char delimiterTab = '\t';
            List<string[]> tabSeperated = new List<string[]>();
            double[] balances = new double[5];

            // seperate the file content line by line
            string[] lineSeperated = fileContent.Split(delimiterNewLine);

            for (int i = 0; i < 6; i++)
            {
                // seperate the line seperated content by tab
                string[] splitted = lineSeperated[i].Split(delimiterTab);
                tabSeperated.Add(splitted);
            }

            // get the month from the file content
            string[] tabSeperatedArrayForMonth = tabSeperated[0];
            string month = tabSeperatedArrayForMonth[tabSeperatedArrayForMonth.Length - 1].Replace("\r", "");
            int monthIndex = 0;

            // get the month index from the months array
            for (int i = 0; i < 12; i++)
            {
                if (months[i].Equals(month))
                {
                    monthIndex = i + 1;
                }
            }

            // if valid month index is not found
            if (monthIndex == 0)
            {
                result = "The month in the file is incorrect.Please check again";
            }
            else
            {
                //if month is valid
                for (int i = 1; i < 6; i++)
                {
                    string[] tabSeperatedArray = tabSeperated[i];
                    //get the balance from tab seperated content and replace commas in the balances
                    string balance = tabSeperatedArray[tabSeperatedArray.Length - 1].Replace(",", "");
                    double balanceDouble;
                    try
                    {
                        // convert to double values and put to balances array
                        balanceDouble = Convert.ToDouble(balance);
                        balances[i - 1] = balanceDouble;
                    }
                    catch (FormatException)
                    {
                        // if balance amounts ara incorrect
                        result = "Balance amounts are incorrect.Please check again.";
                    }
                    catch (OverflowException)
                    {
                        result = "Balance amounts are incorrect.Please check again.";
                    }
                }

                //Created object for BLL
                AccountBalance accountBalance = new AccountBalance();
                accountBalance.year = year;
                accountBalance.month = monthIndex;
                accountBalance.rnd = balances[0];
                accountBalance.canteen = balances[1];
                accountBalance.ceocar = balances[2];
                accountBalance.marketing = balances[3];
                accountBalance.parking = balances[4];
                accountBalance.uid = 1;

                //call method in BLL
                result = _AccountBalanceService.UploadBalance(accountBalance);
            }
            return result;

        }

        // method to View  account balance of a month
        [Route("api/AccountBalance/ViewBalance")]
        [HttpPost]
        public AccountBalance ViewBalance(UserRequest userRequest)
        {
            // get year and month 
            int year = userRequest.year;
            int month = userRequest.month;

            //call method in BLL
            return _AccountBalanceService.ViewBalance(year, month);
        }

        // method to View  account balance of a time period
        [Route("api/AccountBalance/ViewBalanceChart")]
        [HttpPost]
        public List<AccountBalance> ViewBalanceChart(UserRequest userRequest)
        {
            // get variables from user request
            int startYear = userRequest.startYear;
            int startMonth = userRequest.startMonth;
            int endYear = userRequest.endYear;
            int endMonth = userRequest.endMonth;

            //call method in BLL
            return _AccountBalanceService.ViewBalanceChart(startYear, startMonth, endYear, endMonth);
        }

        // method to View current  account balance by account type
        [Route("api/AccountBalance/ViewCurrentBalance")]
        [HttpPost]
        public double[] ViewCurrentBalance(UserRequest userRequest)
        {
            // get the account type
            string accountType = userRequest.accountType;

            //call method in BLL
            return _AccountBalanceService.ViewCurrentBalance(accountType);
        }
    }
}
