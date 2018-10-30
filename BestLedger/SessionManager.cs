using System;
using System.IO;

namespace BestLedger
{
    class SessionManager
    {
        private Account user;
        private DisplayManager displayManager;
        private AccountManager accountManager;
        private TransactionManager transactionManager;

        public SessionManager()
        {
            displayManager = new DisplayManager();
            accountManager = new AccountManager();
        }

        private void StartSession()
        {
            transactionManager = new TransactionManager(user);
            int choice = 0;
            do
            {
                accountManager.CurrentBalance(user);
                displayManager.DisplayOptions();
                choice = displayManager.GetMenuChoice();
                Console.Clear();

                switch (choice)
                {
                    case 1:
                        Transaction deposit = accountManager.MakeDeposit(ref user);
                        if (deposit != null)
                            transactionManager.SaveTransaction(user, deposit);
                        break;
                    case 2:
                        Transaction withdrawal = accountManager.MakeWithdrawal(ref user);
                        if (withdrawal != null)
                            transactionManager.SaveTransaction(user, withdrawal);
                        break;
                    case 3:
                        transactionManager.DisplayTransactions();
                        break;
                    case 4:
                        accountManager.Logout(user);
                        break;
                }
            } while (choice != 4);
        }

        public void Run()
        {
            int choice;
            do
            {
                displayManager.LoginOptions();
                choice = displayManager.GetMenuChoice();

                if (choice == 1)
                {
                    string username = displayManager.GetUserName();
                    string password = displayManager.GetPassword();
                    Console.Clear();
                    user = accountManager.Login(username, password);
                    
                    if (user != null)
                        StartSession();
                }
                else if (choice == 2)
                {
                    Console.WriteLine("We're glad you decided to open an account with Best Ledger. Please enter a username and password.");
                    string username;
                    string password;
                    bool userExists;
                    do
                    {
                        username = displayManager.GetUserName();
                        password = displayManager.GetPassword();
                        Console.Clear();

                        userExists = File.Exists(Directory.GetParent(@"./").FullName + "/Users/" + username + ".json");

                        // Check for a valid username and password
                        if (username == "" || password == "")
                            Console.WriteLine("Please enter a valid username and password.");
                        // Check to see if the username already exists so we don't overwrite an existing user
                        else if (userExists)
                            Console.WriteLine("This username already exists. Please choose a different username.");
                    } while (username == "" || password == "" || userExists);
                    accountManager.CreateAccount(username, password);
                }
            } while (choice != 3);
        }
    }
}
