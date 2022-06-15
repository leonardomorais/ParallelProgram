using Parallel.Program.Cap5;
using Parallel.Program.Cap6;
using Parallel.Program.Cap7;
using Parallel.Program.ConcurrentCollections;
using Parallel.Program.TaskCoordination;

namespace Parallel.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            //WaitingForTimeToPass.Exec();
            // WaitingForTasks.Exec();
            //CriticalSections.Exec();
            //InterlockedOperations.Exec();
            //ConcurrentDictionaryEx.Exec();
            //BlockingCollectionEx.Exec();
            //Continuations.Exec();
            //ChildTasks.Exec();
            //ManualResetEventSlimExample.Exec();
            AutoResetEventExample.Exec();
        }
    }
}