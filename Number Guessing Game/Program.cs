/*
 * A game where the program guesses the number the user selects based on the user inputs.
 * 
 * Program has a random first guess so the user has a chance to win
 * 
 * Neil Doherty v1.0
 */

using System;
using System.Threading;

namespace Number_Guessing_Game
{
    class NumberGuessingProgram
    {
        //Greeting for the game to obtain user number
        static int Greeting()
        {
            int userNumber = 0;
            bool validEntry = false;

            Console.WriteLine("Welcome to the Number Guesser 1000!");
            Console.WriteLine("I will guess the number you enter in no more than 6 guesses!\r\n");
            

            //Validate the entry
            while (validEntry == false) {
                Console.WriteLine("Enter a random integer from 0 to 50 to begin:");

                try
                {
                    userNumber = Convert.ToInt32(Console.ReadLine());
                } catch (FormatException e)
                {
                    Console.WriteLine("\r\nInvalid input.  Please enter an integer from 0 to 50.");
                    continue;
                }

                if (userNumber < 0 || userNumber > 50)
                {
                    Console.WriteLine("\r\nNumber not in the specified range.  Please try again.");
                }
                else
                {
                    validEntry = true;
                }
            }

            return userNumber;
        }

        /*
         * Divide and conquer algorithm to attempt to guess the correct number.
         * Algorithm states that solution should be found in no more than log2(50) guesses.
         * To allow the possibility of user winning, random number is used for the initial guess rather than the median number.
         */
        static int DivideAndConquer(int guess, char direction)
        {
            int maxRange = 49;
            int newGuess = 0;
            double newRange = 0;

            if (direction.Equals('+'))
            {
                newRange = maxRange - guess;
                newGuess = (int)Math.Round(newRange / 2) + guess;
            }
            else
            {
                newRange = guess;
                newGuess = (int)Math.Round(newRange / 2);
            }

            return newGuess;
        }
        //Guesses the user number
        static bool GuessNumber(int userNumber)
        {
            //Variables needed to determine user selection
            int userChoice = userNumber;
            char userResponse = 'n';
            
            bool gameComplete = false;
            var rand = new Random();
            int numberGuessed = rand.Next(-1, 51);

            //Loop through guesses until max number of guesses reached
            while (gameComplete == false)
            {
                
                for (int i = 0; i < 5; i++)

                {
                    
                    bool responseValid = false;
                    Console.WriteLine("Determining the number you chose...");
                    Thread.Sleep(250);

                    //Guessed correctly
                    if (numberGuessed == userNumber)
                    {
                        Console.WriteLine($"\r\nYour number is: {numberGuessed}");
                        Console.WriteLine("\r\n I win!  Thanks for playing!");
                        gameComplete = true;
                        return true;
                    }
                    //Incorrect guess, route guess to DivideAndConquer algorithm and retry
                    else
                    {
                        Console.WriteLine($"\r\nIs your number greater than or less than: {numberGuessed}? (enter '+' for greater than or '-' for less than)");

                        //Validate user selection
                        while (responseValid == false)
                        {
                            try
                            {
                                userResponse = Convert.ToChar(Console.ReadLine());
                                if (userResponse.Equals('+') || userResponse.Equals('-'))
                                {
                                    responseValid = true;

                                }
                                else
                                {
                                    Console.WriteLine($"Invalid selection.  Please enter '+' or '-'");
                                }
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine($"Invalid selection.  Please enter '+' or '-'");
                            }
                        }

                            
                      
                        

                        //Send to algorithm
                        numberGuessed = DivideAndConquer(numberGuessed, userResponse);

                    }
                       
                }
                gameComplete = true;
                
            }

            return false;

        }

        static void Main(string[] args)
        {
            bool gameEnded = false;
            //Main Game loop
            while (gameEnded != true)
            {
                //bool to terminate program
                bool numberGuessed;
                //user entry
                int userSecretNumber;

                //Obtain and validate user input
                userSecretNumber = Greeting();

                numberGuessed = GuessNumber(userSecretNumber);

                if (numberGuessed == true)
                {
                    Console.WriteLine("\r\nI was able to guess your number!");
                }
                else
                {
                    Console.WriteLine("\r\nCongratulations!  You stumped me!");
                }

                //Continue or end game
                Console.WriteLine("Would you like to play again? (Y/N)");
                string userSelection = Console.ReadLine();
                while (userSelection != "Y" && userSelection != "y" && userSelection != "N" && userSelection != "n")
                {
                    Console.WriteLine("\r\nInvalid Selection, try again.\r\n");
                    userSelection = Console.ReadLine();
                }

                switch (userSelection) 
                {
                    case "Y":
                    case "y":
                        break;
                    case "N":
                    case "n":
                        Console.WriteLine("\r\nGoodbye!");
                        gameEnded = true;
                        break;
                }

            }
            
        }
    }
}
