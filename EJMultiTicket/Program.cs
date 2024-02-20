using System.Diagnostics;

namespace EJMultiTicket
{
    internal class Program
    {
        static void Main()
        {
            byte[] result = new byte[7];
            byte[] ticket = new byte[7];
            UInt32[] statistic = new UInt32[8];
            EuroJackpot Lottery = new();
            Lottery.GetTicket(result);

            Stopwatch stopwatch = new();
            stopwatch.Start();

            while (stopwatch.Elapsed.TotalSeconds < 10)
            {
                for (int i = 0; i < 10000; i++)
                {
                    Lottery.GetTicket(ticket);
                    statistic[EuroJackpot.CompareTickets(ticket, result)]++;
                }
            }

            stopwatch.Stop();
            UInt32 sum = 0;
            foreach (var item in statistic) { sum += item; Console.WriteLine($"{item:N0} "); }
            Console.WriteLine($"\nAmount of tickets {sum:N0} in {stopwatch.ElapsedMilliseconds:N0}ms");
        }
    }
}
