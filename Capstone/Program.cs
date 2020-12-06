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
            List<string> lettersGuessed = new List<string>();
            int numberOfGuesses = 6;

            bool quitApplication = false;
            bool gameEnd = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Game Parameters");
                Console.WriteLine("\tb) Play the game");
                Console.WriteLine("\tc) Reset Parameters");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        if (gameEnd == true)
                        {
                            Console.WriteLine("\t Please reset the game parameters.");
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            wordToGuess = DisplayChangeWordToGuessScreen(wordToGuess);
                            numberOfGuesses = DisplayNumberOfGuessesScreen(numberOfGuesses);
                            hiddenWord = DisplayInitializeHiddenWord(wordToGuess, hiddenWord);
                        }

                        break;

                    case "b":
                        if (numberOfGuesses == 6)
                        {
                            Console.WriteLine("\tPlease set game parameters.");
                            DisplayContinuePrompt();
                        }
                        else if (gameEnd == true)
                        {
                            Console.WriteLine("\t Please reset the game parameters.");
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            gameEnd = DisplayPlayGameScreen(wordToGuess, hiddenWord, numberOfGuesses, lettersGuessed, gameEnd);
                        }

                        break;

                    case "c":
                        wordToGuess = DisplayResetWordToGuess(wordToGuess);
                        hiddenWord = DisplayResetHiddenWord(hiddenWord);
                        numberOfGuesses = DisplayResetNumberOfGuesses(numberOfGuesses);
                        lettersGuessed = DisplayResetLettersGuessed(lettersGuessed);
                        gameEnd = DisplayResetGameEnd(gameEnd);
                        Console.WriteLine("\tGame parameters have been reset.");
                        DisplayContinuePrompt();
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

        #region RESET

        //
        // these methods all set one variable back to its starting value
        //

        static bool DisplayResetGameEnd(bool gameEnd)
        {
            gameEnd = false;

            return gameEnd;
        }

        static int DisplayResetNumberOfGuesses(int numberOfGuesses)
        {
            numberOfGuesses = 6;

            return numberOfGuesses;
        }

        static List<string> DisplayResetLettersGuessed(List<string> lettersGuessed)
        {
            lettersGuessed.Clear();

            return lettersGuessed;
        }

        static List<string> DisplayResetHiddenWord(List<string> hiddenWord)
        {
            hiddenWord.Clear();

            return hiddenWord;
        }

        static List<string> DisplayResetWordToGuess(List<string> wordToGuess)
        {
            wordToGuess.Clear();

            return wordToGuess;
        }
        #endregion

        #region HANGMAN
        static List<string> DisplayInitializeHiddenWord(List<string> WordToGuess, List<string> HiddenWord)
        {
            //
            // this method intializes the list that will be displayed to the user and tested to se if the wor dis done being guessed
            //
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
            //
            // display number of guesses to user
            //
            numberOfGuesses = possibleNumber;
            Console.WriteLine($"\tNumber of guesses:{numberOfGuesses}");
            DisplayContinuePrompt();
            return numberOfGuesses;
        }

        static List<string> DisplayChangeWordToGuessScreen(List<string> WordToGuess)
        {
            bool isDoneAdding = false;
            bool validResponse = false;
            int checkNumber;
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
                else if (int.TryParse(userResponse, out checkNumber))
                {
                    Console.WriteLine();
                    Console.WriteLine("\tPlease enter a valid letter");
                    Console.WriteLine("\tPress any key to continue.");
                    Console.ReadKey();
                    validResponse = false;
                }
                else if (!Enum.TryParse(userResponse, out letter))
                {
                    Console.WriteLine();
                    Console.WriteLine("\tPlease enter a valid letter");
                    Console.WriteLine("\tPress any key to continue.");
                    Console.ReadKey();
                    validResponse = false;
                }
                else
                {
                    WordToGuess.Add(letter.ToString());
                }
            } while (!validResponse || !isDoneAdding);
            //
            // display finished word to user
            //
            Console.WriteLine();
            Console.Write("\tCurrent Word:");
            foreach (string Letter in WordToGuess)
            {
                Console.Write(Letter);
            }
            DisplayContinuePrompt();
            return WordToGuess;
        }

        static bool DisplayPlayGameScreen(List<string> wordToGuess, List<string> hiddenWord, int numberOfGuesses, List<string> lettersGuessed, bool gameEnd)
        {
            do
            {
                bool isWordGuessed = false;
                DisplayScreenHeader("\tHangman");
                //
                //display all parameters to player
                //

                Console.WriteLine($"\tNumber of incorect guesses left: {numberOfGuesses}");
                Console.Write("\tCurrent word:");
                Console.Write("\t");
                foreach (string letter in hiddenWord)
                {
                    Console.Write(letter + " ");
                }
                Console.WriteLine();
                switch (numberOfGuesses.ToString())
                {
                    case "0":
                        Console.WriteLine("\t----");
                        Console.WriteLine("\t|  |");
                        Console.WriteLine("\t|  O");
                        Console.WriteLine("\t| -|-");
                        Console.WriteLine(@"\t|/ \");
                        break;

                    case "1":
                        Console.WriteLine("\t----");
                        Console.WriteLine("\t|  |");
                        Console.WriteLine("\t|  O");
                        Console.WriteLine("\t| -|-");
                        Console.WriteLine("\t|  ");
                        break;
                    case "2":
                        Console.WriteLine("\t----");
                        Console.WriteLine("\t|  |");
                        Console.WriteLine("\t|  O");
                        Console.WriteLine("\t| -|");
                        Console.WriteLine("\t|  ");
                        break;

                    case "3":
                        Console.WriteLine("\t----");
                        Console.WriteLine("\t|  |");
                        Console.WriteLine("\t|  O");
                        Console.WriteLine("\t|  |");
                        Console.WriteLine("\t|");
                        break;

                    case "4":
                        Console.WriteLine("\t----");
                        Console.WriteLine("\t|  |");
                        Console.WriteLine("\t|  O");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        break;

                    case "5":
                        Console.WriteLine("\t----");
                        Console.WriteLine("\t|  |");
                        Console.WriteLine("\t| ");
                        Console.WriteLine("\t|");
                        Console.WriteLine("\t|");
                        break;
                }
                Console.WriteLine("\tLetters that have been guessed.");
                Console.Write("\t");
                for (int index = 0; index < lettersGuessed.Count; index++)
                {
                    Console.Write(lettersGuessed[index]);
                    Console.Write(" ");
                }
                numberOfGuesses = DisplayGuessMechanic(wordToGuess, hiddenWord, lettersGuessed, numberOfGuesses);
                if (numberOfGuesses == 0)
                {
                    gameEnd = true;
                }
                isWordGuessed = DisplayCheckWordGuessed(hiddenWord, isWordGuessed);
                if (isWordGuessed == true)
                {
                    gameEnd = true;
                }
            } while (!gameEnd);
            Console.Clear();
            DisplayScreenHeader("\tHangman");
            Console.WriteLine($"\tNumber of incorect guesses left: {numberOfGuesses}");
            Console.Write("\tCurrent word:");
            Console.Write("\t");
            foreach (string letter in hiddenWord)
            {
                Console.Write(letter + " ");
            }
            Console.WriteLine();
            switch (numberOfGuesses.ToString())
            {
                case "0":
                    Console.WriteLine("\t----");
                    Console.WriteLine("\t|  |");
                    Console.WriteLine("\t|  O");
                    Console.WriteLine("\t| -|-");
                    Console.WriteLine("\t| // ");
                    break;

                case "1":
                    Console.WriteLine("\t----");
                    Console.WriteLine("\t|  |");
                    Console.WriteLine("\t|  O");
                    Console.WriteLine("\t| -|-");
                    Console.WriteLine("\t|  ");
                    break;
                case "2":
                    Console.WriteLine("\t----");
                    Console.WriteLine("\t|  |");
                    Console.WriteLine("\t|  O");
                    Console.WriteLine("\t| -|");
                    Console.WriteLine("\t|  ");
                    break;

                case "3":
                    Console.WriteLine("\t----");
                    Console.WriteLine("\t|  |");
                    Console.WriteLine("\t|  O");
                    Console.WriteLine("\t|  |");
                    Console.WriteLine("\t|");
                    break;

                case "4":
                    Console.WriteLine("\t----");
                    Console.WriteLine("\t|  |");
                    Console.WriteLine("\t|  O");
                    Console.WriteLine("\t|");
                    Console.WriteLine("\t|");
                    break;

                case "5":
                    Console.WriteLine("\t----");
                    Console.WriteLine("\t|  |");
                    Console.WriteLine("\t| ");
                    Console.WriteLine("\t|");
                    Console.WriteLine("\t|");
                    break;
            }
            Console.WriteLine();
            if (numberOfGuesses == 0)
            {
                Console.WriteLine("\tYou have lost.");
            }
            else
            {
                Console.WriteLine("\tYou have won.");
            }
            Console.WriteLine("\tPlease reset the game to play again, otherwise please exit the application.");
            DisplayContinuePrompt();

            return gameEnd;

        }

        static bool DisplayCheckWordGuessed(List<string> hiddenWord, bool isWordGuessed)
        {
            //
            // check to see if any underscores are left in the list
            //
            isWordGuessed = true;
            for (int index = 0; index < hiddenWord.Count; index++)
            {
                if (hiddenWord[index] == "_")
                {
                    isWordGuessed = false;
                }
            }
            return isWordGuessed;
        }

        static int DisplayGuessMechanic(List<string> wordToGuess, List<string> hiddenWord, List<string> lettersGuessed, int numberOfGuesses)
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
                    numberOfGuesses = DisplayCorrectGuessCheck(hiddenWord, letterBeingGuessed, lettersGuessed, numberOfGuesses);
                    lettersGuessed.Add(letterBeingGuessed);
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

        static int DisplayCorrectGuessCheck(List<string> hiddenWord, string letterBeingGuessed, List<string> lettersGuessed, int numberOfGuesses)
        {
            bool isCorrectGuess = false;
            bool alreadyGuessedLetter = false;
            //
            // test if letter was already guessed
            //
            for (int index = 0; index < lettersGuessed.Count; index++)
            {
                if (lettersGuessed[index] == letterBeingGuessed)
                {
                    alreadyGuessedLetter = true;
                }
            }
            //
            // if letter was already guessed do no tests
            //
            if (alreadyGuessedLetter == true)
            {
                Console.WriteLine("\tYou have already guessed this letter.");
                Console.WriteLine("\tPress any key to continue.");
                Console.ReadKey();
            }
            //
            // if letter is in word, do not decrease number of guesses
            //
            else if (alreadyGuessedLetter == false)
            {
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
        #endregion

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
            Console.WriteLine("\t\tHangman");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\tYou will be playing a game of hangman.");

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
            Console.WriteLine("\t\tThank you for playing Hangman!");
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
