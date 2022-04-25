using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Wordle
{
    class Program
    {
        static void Main(string[] args)
        {
            var regina = new Regina();

            var game = new WordleGame("askew") { MaxGuesses = 20 };

            int guesses = game.Play(regina);

            Console.WriteLine(guesses);
        }
    }
}
