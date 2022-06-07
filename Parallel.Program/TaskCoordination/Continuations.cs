using System;
using System.Threading.Tasks;

namespace Parallel.Program.TaskCoordination
{
    public static class Continuations
    {
        public static void Exec()
        {
            ExecBoilingWater();
            ContinueWhenAll();
            ContinueWhenAny();
        }

        private static void ExecBoilingWater()
        {
            var task = Task.Factory.StartNew(() => Console.WriteLine("Boiling water"));
            var task2 = task.ContinueWith(t => Console.WriteLine($"Completed task {t.Id}, pour water into cup."));

            task2.Wait();
        }

        private static void ContinueWhenAll()
        {
            var task1 = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");
            
            var task3 = Task.Factory.ContinueWhenAll(new[] { task1, task2 }, tasks =>
            {
                Console.WriteLine("Tasks completed:");
                foreach (var t in tasks)
                    Console.WriteLine($"- {t.Result}");

                Console.WriteLine("All tasks done.");
            });
            task3.Wait();
        }

        private static void ContinueWhenAny()
        {
            var task1 = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");

            var task3 = Task.Factory.ContinueWhenAny(new[] { task1, task2 }, t =>
            {
                Console.WriteLine($"Task completed: {t.Result}");
            });
            task3.Wait();
        }
    }
}