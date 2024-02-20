namespace EJMultiTicket
{
    public class EuroJackpot
    {
        private readonly Random rndGen = new();
        private bool unique;
        private byte numbersFound;
        public void GetTicket(byte[] TicketToFill)
        {
            // first number is always unique
            TicketToFill[0] = (byte)(rndGen.Next(50) + 1);
            numbersFound = 1;

            do
            {
                TicketToFill[numbersFound] = (byte)(rndGen.Next(50) + 1);
                unique = true;
                for (int i = numbersFound - 1; i > -1; --i)
                {
                    if (TicketToFill[i] == TicketToFill[numbersFound]) { unique = false; break; }
                }

                if (unique)
                {
                    numbersFound++;
                }
            }
            while (numbersFound < 5);

            TicketToFill[5] = (byte)(rndGen.Next(12) + 1); // first super number is always unique
            do
            {
                TicketToFill[6] = (byte)(rndGen.Next(12) + 1);
            } while (TicketToFill[5] == TicketToFill[6]);


        }

        public static byte CompareTickets(in byte[] ticket, in byte[] result)
        {
            byte wins = 0;
            for (int ticketID = 0; ticketID < 5; ticketID++)
                for (int resultID = 0; resultID < 5; resultID++)
                    if (ticket[ticketID] == result[resultID])
                        wins++;

            if (ticket[5] == result[5] || ticket[5] == result[6]) wins++;
            if (ticket[6] == result[5] || ticket[6] == result[6]) wins++;
            return wins;
        }
    }
}