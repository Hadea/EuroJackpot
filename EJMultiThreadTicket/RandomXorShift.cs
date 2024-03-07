namespace EJMultiThreadTicket
{
    public class RandomXorShift(uint seed)
    {
        public int Next(int MaxValueExclusive)
        {
            // Generate a random uint
            seed ^= seed << 13;
            seed ^= seed >> 17;
            seed ^= seed << 5;

            // Scale the uint to the desired range and return
            return (int)(seed % MaxValueExclusive);
        }
    }
}
