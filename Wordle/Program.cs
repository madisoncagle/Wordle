using System;
using System.Text.RegularExpressions;

namespace Wordle
{
    class Program
    {
        static void Main(string[] args)
        {
            var squid = new Squid();

            var game = new WordleGame("flair");
            game.MaxGuesses = 40;

            int guesses = game.Play(squid);

            Console.WriteLine(guesses);

            //GuessResult result = game.CheckGuess("rater");
            //Console.WriteLine(result);

            /*char c = 'x';
            string pattern = $@"{c}";
            pattern += @".";
            pattern += @".";
            pattern += @"[^n]";
            pattern += @"e";

            Regex rgx = new Regex(pattern);
            Console.WriteLine(rgx);*/
        }
    }
}

