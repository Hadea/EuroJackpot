using EJSingleTicket;

namespace EJTests
{
    [TestClass]
    public class SingleTicketTests
    {
        [TestMethod]
        public void TestUniqueness()
        {
            byte[] ticket = new byte[7];
            EuroJackpot Lottery = new();
            for (int i = 0; i < 1000; i++)
            {
                for (int ticketWalker = 0; ticketWalker < 7; ticketWalker++)
                    ticket[ticketWalker] = 0;
                Lottery.GetTicket(ticket);

                for (int ticketWalker = 0; ticketWalker < 5; ticketWalker++)
                {
                    for (int ticketSearcher = ticketWalker+1; ticketSearcher < 5; ticketSearcher++)
                    {
                        if (ticket[ticketSearcher] == ticket[ticketWalker])
                        {
                            Assert.Fail($"Duplicate found {ticket[ticketSearcher]} = {ticket[ticketWalker]} TS{ticketSearcher} TW{ticketWalker} Ticket number {i}");
                        }
                    }
                }

                if (ticket[5] == ticket[6])
                {
                    Assert.Fail("Duplicate in super numbers");
                }

            }
        }
    }
}