# Eurojackpot Ticket Generator

This is a small C# project that uses four variants to draw eurojackpot tickets. Eurojackpot is a lottery game that consists of choosing five numbers from 1 to 50 and two additional numbers from 1 to 12.

## Variants

The project has four variants, each with a different approach to drawing tickets:

- **Variant 1**: This variant draws one ticket and prints it to the console. It uses simple loops to generate the numbers.
- **Variant 2**: This variant draws as many tickets as possible in 10 seconds and prints the total number of tickets and a statistic to the console. It uses a Stopwatch to measure the time and a while loops to generate the tickets.
- **Variant 3**: This variant draws as many tickets as possible in 10 seconds using all available cores and prints the total number of tickets and a statistic to the console. It uses threads to parallelize the ticket generation.
- **Variant 4**: This variant has the same task as variant 3 but is created with Copilot (and a lot of refining of the result)

## How to run

To run the project, you need to have Visual Studio installed. You can open the solution file (Eurojackpot.sln) in Visual Studio and choose the variant you want to run from the dropdown menu. Then, press F5 to start debugging or Ctrl+F5 to run without debugging. You should see the output in the console window.
