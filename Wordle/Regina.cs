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

        public Regina()
        {
            Guesses = new List<GuessResult>();

            // read all 5 letter words into options
            try
            {
                using (StreamReader streamReader = new StreamReader("../../../data/five_letter_words.txt"))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        options.Add(line);
                        continue;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            //options.Sort();
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

        public string GenerateEmojiString()
        {
            string emoji = $"Regina | {DateTime.Today.ToShortDateString()}";

            foreach (GuessResult gr in Guesses)
            {
                emoji += "\n";

                foreach (LetterGuess lg in gr.Guess)
                {
                    switch (lg.LetterResult)
                    {
                        case LetterResult.Correct:
                            emoji += "+";
                            break;
                        case LetterResult.Misplaced:
                            emoji += "o";
                            break;
                        case LetterResult.Incorrect:
                            emoji += "-";
                            break;
                        default:
                            break;
                    }
                }
            }

            return emoji;
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
