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

            var game = new WordleGame("cargo");
            game.MaxGuesses = 20;

            int guesses = game.Play(regina);

            Console.WriteLine(guesses);

            /*Regex keepers = new Regex(@"(?=.*a)(?=.*r)(?=.*c)(?=.*o)");
            Regex rgx = new Regex("c[^l][^o][^u][^d]");
            Console.WriteLine(keepers.IsMatch("cargo"));*/
        }
    }
}
