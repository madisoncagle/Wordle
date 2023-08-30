using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Wordle
{
    internal class NewBot : IWordleBot
    {
        public List<GuessResult> Guesses { get; set; }

        private List<string> options = new List<string>();
        private List<string> hunters = new List<string>();

        public NewBot()
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
        }

        public string GenerateGuess()
        {
            if (Guesses.Count == 0)
            {
                return "stare";
            }

            /*
             * HOW I PLAY WORDLE
             * "stare"
             * 0 or 1 vowel -> "cloud"
             * 2 vowels -> "lynch"
             */
            
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
