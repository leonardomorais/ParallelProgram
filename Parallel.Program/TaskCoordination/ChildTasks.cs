using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel.Program.TaskCoordination
{
    public static class ChildTasks
    {
        public static void Exec()
        {
            var parent = new Task(() =>
            {
                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting.");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child task finishing.");
                    throw new Exception("ERROR");
                }, TaskCreationOptions.AttachedToParent);

                var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Unfortunately, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                child.Start();
            });
            parent.Start();

            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }
    }
}