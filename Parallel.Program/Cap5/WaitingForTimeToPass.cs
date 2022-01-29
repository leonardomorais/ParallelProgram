using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel.Program.Cap5
{
    public static class WaitingForTimeToPass
    {
        public static void Exec()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var task = new Task(() =>
            {
                Console.WriteLine("Press any key to disarm, You have 5 seconds.");

                bool cancelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine();
                Console.WriteLine(cancelled ? "Bomb disarmed." : "BOOM!!!");
            }, token);

            task.Start();

            Console.ReadKey();
            cts.Cancel();
            Console.WriteLine("Cap5 done.");
            Console.ReadKey();
        }
    }
}
