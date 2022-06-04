using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Parallel.Program.ConcurrentCollections
{
    public static class ConcurrentDictionaryEx
    {
        private static ConcurrentDictionary<string, string> capitals = new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            bool success = capitals.TryAdd("France", "Paris");
            string who = Task.CurrentId.HasValue ? $"Task {Task.CurrentId}" : "Main thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element.");
        }

        public static void Exec()
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();

            // 
            capitals["Brazil"] = "Rio de Janeiro";
            //capitals["Brazil"] = "Brasília";
            capitals.AddOrUpdate("Brazil", "Brasília", (k, old) => $"{old} --> Brasília");
            Console.WriteLine($"Capital of Brazil: {capitals["Brazil"]}");

            //capitals["Sweden"] = "Uppsala";
            var capOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"Capital of Sweden: {capOfSweden}");

            //capitals["Canada"] = "Ottawa";
            string toRemove = "Canada";
            string removed;
            var didRemove = capitals.TryRemove(toRemove, out removed);
            if (didRemove)
                Console.WriteLine($"We just removed {removed}");
            else
                Console.WriteLine($"Failed to remove the capital of {toRemove}");
        }
    }
}