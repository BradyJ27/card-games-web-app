# Card Game Web App

This project was made as a part of the capstone project for CSCE 361 in Fall 2021. It was developed by 4 UNL students:
Brady Johnson, Daniel Ha, Jaron David Nallathambi, and Joel Matzen. 
It contains 2 card games that are playable, blackjack and poker. 

## Table of Contents

- [Installation](#installation)
- [Games](#games)
  - [Blackjack](#blackjack)
  - [Poker](#poker)
- [Tech Stack](#tech-stack)

## Installation

- Download and install [Visual Studio 2019](https://docs.microsoft.com/en-us/visualstudio/releases/2019/release-notes), 
making sure to check the ".NET Core 3.1" when installing.
- Clone this repository to your local machine.
- Open the repository in Visual Studio.
- In the solution explorer, double click the .sln file.
- On the top toolbar, click "IIS Express" next to the green play button. 
- The website will open in a browser, you should now be able to use the website like normal.

## Games

### Blackjack

Blackjack is a single player game, where the user will play against the dealer (a computer.)
The goal of this game is to beat the dealer and get closest to 21 in your hand. You win if you draw a higher
hand than the dealer. If you draw over 21, you lose. You can place a bet 

### Poker

Poker is a multi player game, where multiple players can play at the same time. To see more on how to play poker,
refer to the "How To Play" page on the poker page.

## Tech Stack

This project was made using C# and ASP.NET Core 3.1, along with a front end of Blazor. We decided that we did not need
a database on the backend, as the data that we deal with is small enough to store in memory. None of the developers
had previously worked with C#, so we decided to keep our tech stack as simple as possible. 