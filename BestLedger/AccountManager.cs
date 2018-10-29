using System;
using System.IO;
using Newtonsoft.Json;

namespace BestLedger
{
    /** 
     * Handles the creation of accounts and login/logout functionality 
     **/
    class AccountManager
    {
        private string path = Directory.GetParent(@"./").FullName;

        /** 
         * Given a username and a password, attempts to fetch the file matching that user
         * and converts it to an Account object. BCrypt is used to check for a password match
         * since passwords are hashed when stored in the file.
         **/
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
         * When a user logs out the account is converted to json format and 
         * written out to the file matching their username. This saves their balance
         * for the next time they log in. Generally any of this info is stored in 
         * a database and shielded from prying eyes.
         **/
        public void Logout(Account user)
        {
            try
            {
                string json = JsonConvert.SerializeObject(user);
                string fileName = $"{user.Username}.json";
                File.WriteAllText(path + $"/Users/{fileName}", json);
            } catch (Exception e)
            {
                Console.WriteLine("There were some issues during logout.");
            }
        }

        /** 
         * Given a username and a password we create an account for them by 
         * hashing their password, creating an account object from that info,
         * creating json data from it and writing that info to a file.
         * 
         * Salting is always good for passwords as an added measure of security
         * to thwart various kinds of attacks but this is fairly basic. 
         **/
        public void CreateAccount(string username, string password)
        {
            try
            {
                // Create directory for file storage if it doesn't exist
                Directory.CreateDirectory($"{path}/Users");

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
         * Make a deposit to the user's account. Takes the account by reference since
         * we are acting upon some account data and we want those changes reflected outside
         * of this method. Creates a transaction and returns that to the caller so the
         * transaction can be saved by the transaction manager.
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
         * Make a withdrawal from the user's account. Takes the account by reference for
         * the same reasoning as MakeDeposit. Returns a transaction for the transaction
         * manager to save. 
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
