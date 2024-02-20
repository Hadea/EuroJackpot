using System.Collections.Concurrent;

namespace EJMultiThreadCopilot
{
    class Program
    {
        // Use ThreadLocal to create a separate XorShiftRandom instance for each thread
        private static ThreadLocal<XorShiftRandom> random = new ThreadLocal<XorShiftRandom>(() => new XorShiftRandom((uint)DateTime.Now.Ticks & 0x0000FFFF));
        private static volatile bool keepRunning = true;

        static void Main(string[] args)
        {
            var userTicket = GenerateTicket();
            Console.WriteLine($"Your ticket numbers are: {string.Join(", ", userTicket.Item1)} and additional numbers: {string.Join(", ", userTicket.Item2)}");

            var totalMatchCounts = new int[8];
            var allLocalMatchCounts = new ConcurrentBag<int[]>();
            var threads = new List<Thread>();

            // Get the number of processor cores
            int coreCount = Environment.ProcessorCount;

            // Create and start one thread per core
            for (int i = 0; i < coreCount; i++)
            {
                var thread = new Thread(() =>
                {
                    var localMatchCounts = new int[8];

                    // Generate tickets until keepRunning is set to false
                    while (keepRunning)
                    {
                        var (mainNumbers, additionalNumbers) = GenerateTicket();
                        var mainMatches = CountMatches(userTicket.Item1, mainNumbers);
                        var additionalMatches = CountMatches(userTicket.Item2, additionalNumbers);
                        var totalMatches = mainMatches + additionalMatches;
                        localMatchCounts[totalMatches]++;
                    }

                    allLocalMatchCounts.Add(localMatchCounts);
                });

                threads.Add(thread);
                thread.Start();
            }

            // Let the threads run for 10 seconds
            Thread.Sleep(10000);

            // Signal the threads to stop
            keepRunning = false;

            // Wait for all threads to complete
            foreach (var thread in threads)
            {
                thread.Join();
            }

            // Combine all local match counts into total match counts
            foreach (var localMatchCounts in allLocalMatchCounts)
            {
                for (int i = 0; i < localMatchCounts.Length; i++)
                {
                    totalMatchCounts[i] += localMatchCounts[i];
                }
            }

            Console.WriteLine("Statistic of matches:");
            Int64 Sum = 0;
            for (int i = 0; i < totalMatchCounts.Length; i++)
            {
                Sum += totalMatchCounts[i];
                Console.WriteLine($"{i} matches: {totalMatchCounts[i]} tickets");
            }
            Console.WriteLine("Total : " + Sum);
        }

        static (HashSet<int>, HashSet<int>) GenerateTicket()
        {
            var mainNumbers = new HashSet<int>();
            var additionalNumbers = new HashSet<int>();

            // EuroJackpot includes 5 numbers from 1 to 50
            while (mainNumbers.Count < 5)
            {
                mainNumbers.Add(random.Value.Next(1, 51));
            }

            // And 2 additional numbers from 1 to 12
            while (additionalNumbers.Count < 2)
            {
                additionalNumbers.Add(random.Value.Next(1, 13));
            }

            return (mainNumbers, additionalNumbers);
        }

        static int CountMatches(HashSet<int> set1, HashSet<int> set2)
        {
            return set1.Count(set2.Contains);
        }
    }

    public class XorShiftRandom
    {
        private uint _seed;

        public XorShiftRandom(uint seed)
        {
            _seed = seed;
        }

        public int Next(int minValue, int maxValue)
        {
            // Generate a random uint
            _seed ^= _seed << 13;
            _seed ^= _seed >> 17;
            _seed ^= _seed << 5;

            // Scale the uint to the desired range and return
            return (int)(_seed % (maxValue - minValue)) + minValue;
        }
    }
}
