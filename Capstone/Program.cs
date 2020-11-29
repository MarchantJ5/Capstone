using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public enum HangmanAlphabet
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z,
        DONE
    }
    class Program
    {
        static void Main(string[] args)
        {
            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;
            List<string> wordToGuess = new List<string>();
            List<string> hiddenWord = new List<string>();
            int numberOfGuesses = 0;

            bool quitApplication = false;
            string menuChoice;



            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Game Parameters");
                Console.WriteLine("\tb) Play the game");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        wordToGuess = DisplayChangeWordToGuessScreen(wordToGuess);
                        numberOfGuesses = DisplayNumberOfGuessesScreen(numberOfGuesses);
                        hiddenWord = DisplayInitializeHiddenWord(wordToGuess, hiddenWord);

                        break;

                    case "b":
                        DisplayPlayGameScreen(wordToGuess, hiddenWord, numberOfGuesses);
                        break;

                    case "q":

                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        static List<string> DisplayInitializeHiddenWord(List<string> WordToGuess, List<string> HiddenWord)
        {
            string underscoreToAdd;
            foreach (string Letter in WordToGuess)
            {
                underscoreToAdd = "_";
                HiddenWord.Add(underscoreToAdd);
            }
            return HiddenWord;
        }

        static int DisplayNumberOfGuessesScreen(int numberOfGuesses)
        {
            bool validResponse = false;
            int possibleNumber;
            DisplayScreenHeader("\tNumber of Guesses");

            Console.WriteLine("\tEnter number of incorrect guesses the player will have, between 1 and 5.");

            //
            // get number of incorrect guesses
            //
            do
            {
                string userResponse = Console.ReadLine();
                int.TryParse(userResponse, out possibleNumber);
                if (possibleNumber >= 1 && possibleNumber <= 5)
                {
                    validResponse = true;

                }
                else
                {
                    Console.WriteLine("\tPlease enter a number between 1 and 5.");
                }
            } while (!validResponse);
            numberOfGuesses = possibleNumber;
            Console.WriteLine($"\tNumber of guesses:{numberOfGuesses}");
            DisplayContinuePrompt();
            return numberOfGuesses;
        }

        static List<string> DisplayChangeWordToGuessScreen(List<string> WordToGuess)
        {
            bool isDoneAdding = false;
            bool validResponse = false;
            string userResponse;
            HangmanAlphabet letter;

            //
            // Display Instructions
            //
            DisplayScreenHeader("\tEnter word to guess");
            Console.WriteLine("\tYou will enter letters individually until the word is finished.");
            Console.WriteLine("\tEnter [done] when the word is finished.");
            Console.WriteLine();

            //
            // Loop to enter letters for word, length of list is important
            //
            do
            {
                Console.WriteLine();
                Console.Write("\tCurrent Progress on word:");
                foreach (string Letter in WordToGuess)
                {
                    Console.Write(Letter);
                }
                Console.WriteLine();
                Console.Write("\tEnter a single letter:");


                userResponse = Console.ReadLine().ToUpper();
                validResponse = true;
                if (userResponse == "DONE")
                {
                    isDoneAdding = true;
                }
                else if (!Enum.TryParse(userResponse, out letter))
                {
                    Console.WriteLine();
                    Console.WriteLine("\tPlease enter a valid command");
                    Console.WriteLine("\tPress any key to continue.");
                    Console.ReadKey();
                    validResponse = false;
                }
                else
                {
                    WordToGuess.Add(letter.ToString());
                }
            } while (!validResponse || !isDoneAdding);
            Console.Write("\tCurrent Word:");
            foreach (string Letter in WordToGuess)
            {
                Console.Write(Letter);
            }
            DisplayContinuePrompt();
            return WordToGuess;
        }

        static void DisplayPlayGameScreen(List<string> wordToGuess, List<string> hiddenWord, int numberOfGuesses)
        {
            bool gameEnd = false;
            do
            {
                DisplayScreenHeader("\tHangman");
                //
                //display all parameters to player
                //
                Console.WriteLine($"\tNumber of incorect guesses left: {numberOfGuesses}");
                Console.WriteLine("\tCurrent word:");
                Console.Write("\t");
                foreach (string letter in hiddenWord)
                {
                    Console.Write(letter + " ");
                }
                numberOfGuesses = DisplayGuessMechanic(wordToGuess, hiddenWord, numberOfGuesses);
            } while (!gameEnd);


            DisplayContinuePrompt();


        }

        static int DisplayGuessMechanic(List<string> wordToGuess, List<string> hiddenWord, int numberOfGuesses)
        {
            string letterBeingGuessed;
            bool validResponse = false;
            HangmanAlphabet letter;
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("\tEnter a letter to guess:");
            

            //
            // have player enter a letter, test to see if it is a letter
            //

            do
            {
                letterBeingGuessed = Console.ReadLine().ToUpper();
                if (Enum.TryParse(letterBeingGuessed, out letter))
                {
                    hiddenWord = DisplayChangeHiddenWord(wordToGuess, hiddenWord, letterBeingGuessed);
                    numberOfGuesses = DisplayCorrectGuessCheck(hiddenWord, letterBeingGuessed, numberOfGuesses);
                    validResponse = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\tPlease enter a valid letter");
                    Console.WriteLine("\tPress any key to continue.");
                    Console.ReadKey();
                    validResponse = false;
                }
            } while (!validResponse);

            return numberOfGuesses;
        }

        static int DisplayCorrectGuessCheck(List<string> hiddenWord, string letterBeingGuessed, int numberOfGuesses)
        {
            bool isCorrectGuess = false;
            //
            // if letter is in word, do not decrease number of guesses
            //

            for (int index = 0; index < hiddenWord.Count; index++)
            {
                if (letterBeingGuessed == hiddenWord[index])
                {
                    isCorrectGuess = true;
                }
            }
            if (isCorrectGuess == false)
            {
                numberOfGuesses = numberOfGuesses - 1;
            }
            return numberOfGuesses;
        }

        static List<string> DisplayChangeHiddenWord(List<string> wordToGuess, List<string> hiddenWord, string letterBeingGuessed)
        {
            //
            // if letter is in word, change underscore to letters in correct place
            //
            for (int index = 0; index < wordToGuess.Count; index++)
            {
                if (letterBeingGuessed == wordToGuess[index])
                {
                    hiddenWord[index] = letterBeingGuessed;
                }
            }
            return hiddenWord;
        }
        #region USER INTERFACE

        // <summary>
        // *****************************************************************
        // *                     Welcome Screen                            *
        // *****************************************************************
        // </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tCapstone Project");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        // <summary>
        //*****************************************************************
        // *                     Closing Screen                            *
        // *****************************************************************
        // </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        // <summary>
        // display continue prompt
        // </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        // <summary>
        // display menu prompt
        // </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        // <summary>
        // display screen header
        // </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
