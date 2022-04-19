using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wordle
{
    public class Regina : IWordleBot
    {
        public List<GuessResult> Guesses { get; set; }

        private List<string> options = new List<string>();
        private string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public Regina()
        {
            Guesses = new List<GuessResult>();
            options.Sort();
        }

        public string GenerateGuess()
        {
            if (Guesses.Count == 0)
            {
                return "crane";
            }

            GuessResult lastGuess = Guesses[^1];
            Regex rgx = GenerateRegex(lastGuess);
            Regex keepers = GenerateKeeperSet();

            // eliminate impossible solutions
            foreach (string word in options.ToList())
            {
                if (!(rgx.IsMatch(word) && keepers.IsMatch(word)))
                {
                    options.Remove(word);
                }
            }

            // return word with highest frequency that matches pattern

            return options[0];
        }

        private Regex GenerateRegex(GuessResult guess)
        {
            string pattern = "";

            foreach (LetterGuess lg in guess.Guess)
            {
                if (lg.LetterResult == LetterResult.Correct)
                {
                    pattern += $@"{lg.Letter}";
                }
                else
                {
                    pattern += $@"[^{lg.Letter}]";
                }
            }

            return new Regex(pattern);
        }

        private Regex GenerateKeeperSet()
        {
            string keepers = "";

            foreach (GuessResult gr in Guesses)
            {
                foreach (LetterGuess lg in gr.Guess)
                {
                    if (lg.LetterResult != LetterResult.Incorrect && !keepers.Contains(lg.Letter))
                    {
                        keepers += $@"(?=.*{lg.Letter})";
                    }
                }
            }

            return new Regex(keepers);
        }
    }
}
