using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel.Program.Cap7
{
    public class InterLockBankAccount
    {
        private int balance;

        public int Balance 
        {
            get { return balance; }
            private set { balance = value; }
        }

        public void Deposit(int amount)
        {
            Interlocked.Add(ref balance, amount);
        }

        public void Withdraw(int amount)
        {
            Interlocked.Add(ref balance, -amount);
        }

    }

    public static class InterlockedOperations
    {
        public static void Exec()
        {
            var tasks = new List<Task>();
            var bankAccount = new InterLockBankAccount();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bankAccount.Deposit(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bankAccount.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {bankAccount.Balance}.");
        }
    }
}
