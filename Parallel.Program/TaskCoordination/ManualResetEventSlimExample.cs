using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel.Program.TaskCoordination
{
    public static class ManualResetEventSlimExample
    {
        public static void Exec()
        {
            EventSignaled();
            EventNonSignaled();
        }

        private static void EventSignaled()
        {
            var evt = new ManualResetEventSlim();

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water...");
                evt.Wait();
                Console.WriteLine("Here is your tea");
            });

            makeTea.Wait();
        }

        private static void EventNonSignaled()
        {
            var evt = new ManualResetEventSlim();

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water...");
                bool signaled = evt.Wait(2000);

                if (signaled) 
                    Console.WriteLine("Here is your tea");
                else
                    Console.WriteLine("No tea for you");
            });

            makeTea.Wait();
        }



    }
}