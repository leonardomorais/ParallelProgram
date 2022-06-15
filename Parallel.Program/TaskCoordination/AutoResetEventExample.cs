using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel.Program.TaskCoordination
{
    public static class AutoResetEventExample
    {
        public static void Exec()
        {
            var evt = new AutoResetEvent(false);

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set();
            });

            //Task.Factory.StartNew(() =>
            //{
            //    Console.WriteLine("Preparing");
            //    evt.Set();
            //});

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water...");
                evt.WaitOne();
                Console.WriteLine("Here is your tea");
                bool received = evt.WaitOne(2000);
                Console.WriteLine(received ? "Enjoy your tea" : "No tea for you");
            });

            makeTea.Wait();
        }
    }
}