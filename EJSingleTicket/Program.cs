namespace EJSingleTicket
{
    internal class Program
    {
        static void Main()
        {
            byte[] ticket = new byte[7];
            byte[] result = new byte[7];
            EuroJackpot Lottery = new();

            Lottery.GetTicket(ticket);
            Lottery.GetTicket(result);

            foreach (byte number in ticket)
                Console.Write(number + " ");
            Console.WriteLine();
            foreach (byte number in result)
                Console.Write(number + " ");

            Console.WriteLine($"Korrekt: {EuroJackpot.CompareTickets(ticket,result)}");
        }
    }
}