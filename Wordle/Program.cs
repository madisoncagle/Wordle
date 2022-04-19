using System;
using System.Text.RegularExpressions;

namespace Wordle
{
    class Program
    {
        static void Main(string[] args)
        {
            var regina = new Regina();

            var game = new WordleGame("flair");
            game.MaxGuesses = 20;

            int guesses = game.Play(regina);

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

            /*string word = "cider";
            Regex keepers = new Regex(@"(?=.*c)(?=.*i)(?=.*n)");
            Console.WriteLine(keepers);
            Console.WriteLine(keepers.IsMatch(word));*/
        }
    }
}

