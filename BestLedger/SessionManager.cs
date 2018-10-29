using System;
using System.Collections.Generic;
using System.Text;

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
                    do
                    {
                        string username = displayManager.GetUserName();
                        string password = displayManager.GetPassword();
                        Console.Clear();
                        user = accountManager.Login(username, password);
                    } while (user == null);
                    StartSession();
                }
                else if (choice == 2)
                {
                    Console.WriteLine("We're glad you decided to open an account with us. Create a username and password.");
                    string username = displayManager.GetUserName();
                    string password = displayManager.GetPassword();
                    Console.Clear();
                    accountManager.CreateAccount(username, password);
                }
            } while (choice != 3);
        }
    }
}
