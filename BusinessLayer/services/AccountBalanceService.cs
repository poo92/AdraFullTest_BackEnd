using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.services.Interfaces;
using DataAccessLibrary;
using DataAccessLibrary.Repository.Interfaces;
using EntityClasses;

namespace BusinessLayer.services
{
    public class AccountBalanceService : IAccountBalanceService
    {
        private IAccountBalanceRepo _AccountBalanceRepo;         // instance of a AccountBalanceRepo class on DAL

        public AccountBalanceService(IAccountBalanceRepo accountBalanceRepo)
        {
            _AccountBalanceRepo = accountBalanceRepo;
        }

        // upload balance
        public string UploadBalance(AccountBalance accountBalance)
        {
            string uploadStatus = "";

            // creating a object for DAL and set values
            accountbalance accountBalanceDAL = new accountbalance();
            accountBalanceDAL.year = accountBalance.year;
            accountBalanceDAL.month = accountBalance.month;
            accountBalanceDAL.rnd = accountBalance.rnd;
            accountBalanceDAL.canteen = accountBalance.canteen;
            accountBalanceDAL.ceocar = accountBalance.ceocar;
            accountBalanceDAL.marketing = accountBalance.marketing;
            accountBalanceDAL.parking = accountBalance.parking;
            accountBalanceDAL.uid = accountBalance.uid;

            // to check if balance is already in the database
            AccountBalance existingRecord = ViewBalance(accountBalance.year, accountBalance.month);

            // if balances exsists
            if (existingRecord.year != 0)
            {
                uploadStatus = "Balances of this month already exsists in the database";
            }
            else
            {
                // if balances doesn't exists call repository method to upload
                uploadStatus = _AccountBalanceRepo.UploadBalance(accountBalanceDAL);
            }

            // return the result
            return uploadStatus;


        }

        // View balance of a month
        public AccountBalance ViewBalance(int year, int month)
        {
            // call viewBalance method 
            accountbalance accountBalance = _AccountBalanceRepo.ViewBalance(year, month);

            // create object for BLL
            AccountBalance accountBalanceResult = new AccountBalance();

            // if balance record exists
            if (accountBalance != null)
            {
                accountBalanceResult.year = accountBalance.year;
                accountBalanceResult.month = accountBalance.month;
                accountBalanceResult.rnd = (double)accountBalance.rnd;
                accountBalanceResult.canteen = (double)accountBalance.canteen;
                accountBalanceResult.ceocar = (double)accountBalance.ceocar;
                accountBalanceResult.marketing = (double)accountBalance.marketing;
                accountBalanceResult.parking = (double)accountBalance.parking;
                if (accountBalance.uid != null)
                {
                    accountBalanceResult.uid = (int)accountBalance.uid;
                }
                else
                {
                    accountBalanceResult.uid = 0;
                }

            }
            else
            {
                // if balances ara not in the db
                accountBalanceResult.year = 0;
                accountBalanceResult.month = 0;
                accountBalanceResult.rnd = 0;
                accountBalanceResult.canteen = 0;
                accountBalanceResult.ceocar = 0;
                accountBalanceResult.marketing = 0;
                accountBalanceResult.parking = 0;
                accountBalanceResult.uid = 0;
            }


            return accountBalanceResult;
        }

        // view balances of a time period
        public List<AccountBalance> ViewBalanceChart(int startYear, int startMonth, int endYear, int endMonth)
        {
            // call repository method
            List<accountbalance> resultList = _AccountBalanceRepo.ViewBalanceChart(startYear, startMonth, endYear, endMonth);    // reuslt from the DB

            // create list for BLL
            List<AccountBalance> result = new List<AccountBalance>();

            // convert DAL objects into BLL objects
            if (resultList != null)
            {
                foreach (accountbalance accountBalance in resultList)
                {
                    AccountBalance accountBalaceBAL = new AccountBalance();
                    accountBalaceBAL.year = accountBalance.year;
                    accountBalaceBAL.month = accountBalance.month;
                    accountBalaceBAL.rnd = (double)accountBalance.rnd;
                    accountBalaceBAL.canteen = (double)accountBalance.canteen;
                    accountBalaceBAL.ceocar = (double)accountBalance.ceocar;
                    accountBalaceBAL.marketing = (double)accountBalance.marketing;
                    accountBalaceBAL.parking = (double)accountBalance.parking;
                    if (accountBalance.uid != null)
                    {
                        accountBalaceBAL.uid = (int)accountBalance.uid;

                    }

                    result.Add(accountBalaceBAL);
                }
            }


            // return list of BLL objects
            return result;
        }

        // method to view current balance account typewise
        public double[] ViewCurrentBalance(string accountType)
        {
            double[] balance = new double[2];

            // call repository method
            accountbalance accountBalance = _AccountBalanceRepo.ViewCurrentBalance();

            // if at least one record is in the db records in the database         
            if (accountBalance != null)
            {
                balance[0] = 0;
                if (accountType == "rnd")
                {
                    balance[1] = (double)accountBalance.rnd;
                }
                else if (accountType == "canteen")
                {
                    balance[1] = (double)accountBalance.canteen;
                }
                else if (accountType == "ceocar")
                {
                    balance[1] = (double)accountBalance.ceocar;
                }
                else if (accountType == "marketing")
                {
                    balance[1] = (double)accountBalance.marketing;
                }
                else if (accountType == "parking")
                {
                    balance[1] = (double)accountBalance.parking;
                }
            }
            else
            {
                balance[0] = 1;
            }

            // return the result
            return balance;
        }
    }
}
