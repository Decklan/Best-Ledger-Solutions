using System;

namespace BestLedger
{
    /** 
     * Handles various displaying of information to the user 
     **/
    class DisplayManager
    {
        public DisplayManager() { }

        public void LoginOptions()
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Sign Up");
            Console.WriteLine("3. Exit");
            Console.Write("Enter a choice: ");
        }

        public void DisplayOptions()
        {
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Transaction History");
            Console.WriteLine("4. Logout");
            Console.Write("Enter a choice: ");
        }

        public string GetUserName()
        {
            Console.Write("Username: ");
            return Console.ReadLine();
        }

        public string GetPassword()
        {
            Console.Write("Password: ");
            return Console.ReadLine();
        }

        public int GetMenuChoice()
        {
            string choice = Console.ReadLine();

            bool success = int.TryParse(choice, out int result);

            if (success)
                return result;
            else return -1;
        }
    }
}
