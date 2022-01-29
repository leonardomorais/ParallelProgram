using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel.Program.Cap6
{
    public static class WaitingForTasks
    {
        public static void Exec()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var task1 = new Task(() =>
            {
                Console.WriteLine("I take 5 seconds");

                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Task 1 done");
            }, token);
            task1.Start();

            var task2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

            //Task.WaitAll(task1, task2);
            //Task.WaitAny(task1, task2);
            Task.WaitAll(new[] { task1, task2 }, 4000, token);

            Console.WriteLine($"Status Task1 : {task1.Status}");
            Console.WriteLine($"Status Task2 : {task2.Status}");

            Console.WriteLine("Cap6 done.");
            Console.ReadKey();
        }
    }
}
