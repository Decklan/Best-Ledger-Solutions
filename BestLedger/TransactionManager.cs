using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BestLedger
{
    /** 
     * Manages transaction logic while a user is logged in. Saving a transaction,
     * listing transactions, getting a user's transactions, etc
     **/
    class TransactionManager
    {
        private string path = Directory.GetParent(@"./").FullName; // Project path
        private List<Transaction> transactions;                    // List of transactions

        public TransactionManager(Account user)
        {
            transactions = FetchTransactions(user);
        }

        /** 
         * Fetches JSON transaction data for a user from storage. Storage, in this case, 
         * is just a folder in the project directory since there is no database storing 
         * this information.
         * 
         * Stored as JSON format because it is easy to work with for many different
         * types of applications. 
         **/
        public List<Transaction> FetchTransactions(Account user)
        {
            string fileName = $"{user.Username}-transactions.json";
            
            try
            {
                string rawJSON = File.ReadAllText(path + $"/Transactions/{fileName}");
                return JsonConvert.DeserializeObject<List<Transaction>>(rawJSON);
            }
            catch (Exception e)
            {
                return new List<Transaction>();
            }
        }

        /** 
         * Displays a list of transactions for the current user.
         **/
        public void DisplayTransactions()
        {
            Console.WriteLine("Your Transactions: ");
            Console.WriteLine("------------------");
            if (transactions.Count > 0)
            {
                foreach (Transaction transaction in transactions)
                {
                    Console.WriteLine($"Type: {transaction.Type}");
                    Console.WriteLine($"Amount: {transaction.Amount}");
                    Console.WriteLine($"Remaining Balance: {transaction.ResultingBalance}");
                    Console.WriteLine($"Transaction Date: {transaction.TransactionDate.ToString()}");
                    Console.WriteLine("------------------");
                }
            }
            else
            {
                Console.WriteLine("You don't have any transactions yet.");
            }
        }

        /** 
         * Saves a transaction that a user performs on their account to a transaction.json
         * file in the project directory (since there is no database in this context).
         **/
        public void SaveTransaction(Account user, Transaction transaction)
        {
            transactions.Add(transaction);
            string json = JsonConvert.SerializeObject(transactions);
            string fileName = $"{user.Username}-transactions.json";
            File.WriteAllText(path + $"/Transactions/{fileName}", json);
        }
    }
}
