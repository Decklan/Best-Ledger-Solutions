using System;
using System.IO;
using Newtonsoft.Json;

namespace BestLedger
{
    // Handles session details like login, logout, and account creation
    class AccountManager
    {
        private string path = Directory.GetParent(@"./").FullName;

        public Account Login(string username, string password)
        {
            try
            {
                // Attempt to login
                string fileName = $"{username}.json";
                string rawJSON = File.ReadAllText(path + $"/Users/{fileName}");
                Account account = JsonConvert.DeserializeObject<Account>(rawJSON);

                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Either your username or password was incorrect.");
                return null;
            }
        }

        /** 
         * Handles what should happen when a user logs out
         **/
        public void Logout(Account user)
        {
            string json = JsonConvert.SerializeObject(user);
            string fileName = $"{user.Username}.json";
            File.WriteAllText(path + $"/Users/{fileName}", json);
        }

        /** 
         * Create a new account  
         **/
        public void CreateAccount(string username, string password)
        {
            try
            {
                string hash = BCrypt.Net.BCrypt.HashPassword(password);

                Account newAccount = new Account
                {
                    Username = username,
                    Password = hash,
                    Balance = 0
                };

                string json = JsonConvert.SerializeObject(newAccount);
                string fileName = $"{newAccount.Username}.json";
                File.WriteAllText(path + $"/Users/{fileName}", json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /** 
         * Make a deposit to the user's account
         **/
        public Transaction MakeDeposit(ref Account user)
        {
            Console.Write("Deposit Amount: ");
            string amount = Console.ReadLine();
            bool success = double.TryParse(amount, out double result);

            if (success && result > 0)
            {
                user.Balance += result;
                Transaction deposit = new Transaction
                {
                    Type = "deposit",
                    Amount = result,
                    ResultingBalance = user.Balance,
                    TransactionDate = DateTime.Now
                };
                return deposit;
            }
            else
            {
                Console.WriteLine("Please deposit a valid amount greater than 0");
                return null;
            }
        }

        /** 
         * Withdraw an amount from the user's account
         **/
        public Transaction MakeWithdrawal(ref Account user)
        {
            Console.Write("Withdrawal Amount: ");
            string amount = Console.ReadLine();
            bool success = double.TryParse(amount, out double result);

            if (success)
            {
                if (user.Balance - result >= 0)
                {
                    user.Balance -= result;
                    Transaction withdrawal = new Transaction
                    {
                        Type = "withdrawal",
                        Amount = result,
                        ResultingBalance = user.Balance,
                        TransactionDate = DateTime.Now
                    };
                    return withdrawal;
                }
                else
                {
                    Console.WriteLine("You have insufficient funds to process this transaction");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid amount greater than 0");
                return null;
            }
        }

        /** 
         * Display the current account balance 
         **/
        public void CurrentBalance(Account user)
        {
            Console.WriteLine($"Your current balance is: {user.Balance}");
        }
    }
}
