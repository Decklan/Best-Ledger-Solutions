using System;
using System.Collections.Generic;
using System.Text;

namespace BestLedger
{
    class Transaction
    {
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private double amount;
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private double resultingBalance;
        public double ResultingBalance
        {
            get { return resultingBalance; }
            set { resultingBalance = value; }
        }

        private DateTime transactionDate;
        public DateTime TransactionDate
        {
            get { return transactionDate; }
            set
            {
                transactionDate = value;
            }
        }
    }
}
