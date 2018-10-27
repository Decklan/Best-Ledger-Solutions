using System;

namespace BestLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Best Ledger Banking");
            Console.WriteLine("------------------------------");
            SessionManager sessionManager = new SessionManager();
            sessionManager.Run();
        }
    }
}
