using System;
using System.Text.RegularExpressions;

namespace Wordle
{
    class Program
    {
        static void Main(string[] args)
        {
            var regina = new Regina();

            var game = new WordleGame("foyer");
            game.MaxGuesses = 20;

            int guesses = game.Play(regina);

            Console.WriteLine(guesses);
        }
    }
}

