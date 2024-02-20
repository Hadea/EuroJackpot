using System.Diagnostics;

namespace EJMultiThreadTicket
{
    internal class Program
    {

        static void Main()
        {
            byte[] result = new byte[7];
            EuroJackpot Lottery = new();
            Lottery.GetTicket(result);
            LinkedList<EuroJackpot> threadDatas = new();

            int threadNumber = Environment.ProcessorCount/2;

            for (int i = 0; i < threadNumber; i++)
                threadDatas.AddLast(new EuroJackpot(result));

            foreach (EuroJackpot item in threadDatas)
                item.thread.Start();

            Thread.Sleep(10000);
            EuroJackpot.KeepRunning = false;
            
            foreach (EuroJackpot item in threadDatas) item.thread.Join();

            // collecting data of all threads
            UInt64[] statisticsAggregated = new UInt64[8];
            foreach (var statBlock in threadDatas)
                for (int i = 0; i < statBlock.statistic.Length; i++)
                    statisticsAggregated[i] += statBlock.statistic[i];

            // printing summary
            UInt64 sum = 0;
            foreach (var item in statisticsAggregated)
            {
                sum += item;
                Console.WriteLine($"{item:N0} ");
            }
            Console.WriteLine($"\nAmount of tickets {sum:N0} with {threadNumber} Threads");
        }
    }
}
