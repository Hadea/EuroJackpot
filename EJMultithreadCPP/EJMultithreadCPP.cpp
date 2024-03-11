#include <iostream>
#include <random>
#include <chrono>

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

static void printArray(int* ticket, int length)
{

	for (int i = 0; i < length; ++i)
		std::cout << ticket[i] << " ";
	std::cout << std::endl;
}

static void printArray(int64_t* ticket, int length)
{

	for (int i = 0; i < length; ++i)
		std::cout << ticket[i] << " ";
	std::cout << std::endl;
}

int main()
{
	srand(time(NULL));

	std::cout << "EuroJackpot Lottery" << std::endl;

	int ticketA[7] = { 0 };
	int ticketB[7] = { 0 };
	int64_t statistic[8] = { 0 };


	fillTicket(ticketA);

	std::chrono::time_point start = std::chrono::steady_clock::now();

	while (std::chrono::steady_clock::now() - start < std::chrono::seconds(10))
	{
		fillTicket(ticketB);
		++statistic[compareTickets(ticketA, ticketB)];
	}

	printArray(ticketA, 7);
	printArray(statistic, 8);

	int sum = 0;
	for (size_t i = 0; i < 8; i++)
	{
		sum += statistic[i];
	}

	std::cout << "sumber of draws: " << sum;

}
