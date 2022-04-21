using System;
using System.Collections.Generic;
using System.IO;
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
        //private string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public Regina()
        {
            Guesses = new List<GuessResult>();

            // read all 5 letter words into options
            try
            {
                using (StreamReader streamReader = new StreamReader("../../../data/english_words_full.txt"))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Length == 5)
                        {
                            options.Add(line);
                            continue;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
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

            if (Guesses.Count == 1)
            {
                return "toils";
            }

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

        private double PercentCorrect()
        {
            double correct = 0;
            string letters = "";

            foreach (GuessResult gr in Guesses)
            {
                foreach (LetterGuess lg in gr.Guess)
                {
                    if (lg.LetterResult == LetterResult.Correct && !letters.Contains(lg.Letter))
                    {
                        letters += lg.Letter;
                        correct += 1;
                    }
                    else if (lg.LetterResult == LetterResult.Misplaced && !letters.Contains(lg.Letter))
                    {
                        letters += lg.Letter;
                        correct += 0.5;
                    }
                }
            }

            return correct / 5;
        }
    }
}
