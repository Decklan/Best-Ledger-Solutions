using System;
using System.Collections.Generic;
using System.Text;

namespace BestLedger
{
    class Account
    {
        private string username;
        public string Username
        {
            get => username;
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("Please enter a valid username");
                }
                else if (value.Length < 6)
                {
                    throw new Exception("Usernames must be at least 6 characters in length.");
                }
                username = value;
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("Please enter a valid password");
                }
                else if (value.Length < 8)
                {
                    throw new Exception("Passwords must be at least 8 characters in length.");
                }
                password = value;
            }
        }

        private double balance;
        public double Balance
        {
            get => balance;
            set
            {
                balance = value;
            }
        }
    }
}
