#include <iostream>
#include <random>

#define rnd50 rand() % 50 +1;
#define rnd12 rand() % 12 +1;

static void fillTicket(int* ticketToFill)
{

	int foundNumbers = 1;
	ticketToFill[0] = rnd50;
	bool unique;

	do
	{
		ticketToFill[foundNumbers] = rnd50;
		unique = true;

		for (int i = foundNumbers - 1; i >= 0; i--)
		{
			if (ticketToFill[i] == ticketToFill[foundNumbers])
			{
				unique = false;
				break;
			}
		}

		if (unique) ++foundNumbers;
	} while (foundNumbers < 5);

	ticketToFill[5] = rnd12;

	do
	{
		ticketToFill[6] = rnd12;
	} while (ticketToFill[5] == ticketToFill[6]);
}

static int compareTickets(int* ticketA, int* ticketB)
{
	int wins = 0;

	for (size_t i = 0; i < 5; i++)
		for (size_t j = 0; j < 5; j++)
			if (ticketA[i] == ticketB[j])
				++wins;

	if (ticketA[5] == ticketB[5] || ticketA[5] == ticketB[6]) wins++;
	if (ticketA[6] == ticketB[5] || ticketA[6] == ticketB[6]) wins++;
	return wins;
}

static void printTicket(int* ticket)
{

	for (int i = 0; i < 7; ++i)
		std::cout << ticket[i] << " ";
	std::cout << std::endl;
}

int main()
{
	srand(time(NULL));

	std::cout << "EuroJackpot Lottery" << std::endl;

	int* ticketA = new int[7];
	int* ticketB = new int[7];

	fillTicket(ticketA);
	fillTicket(ticketB);

	printTicket(ticketA);
	printTicket(ticketB);

	std::cout << "Identical: " << compareTickets(ticketA, ticketB);

}
