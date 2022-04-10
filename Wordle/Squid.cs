using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public class Squid : IWordleBot
    {
        public List<GuessResult> Guesses { get; set; }

        private List<string> options = new List<string>();
        private string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public Squid()
        {
            Guesses = new List<GuessResult>();
            // put all 5-letter words into options array
        }

        // TODO
        public string GenerateGuess()
        {
            if (Guesses.Count == 0)
            {
                return "stare";
            }

            // remove incorrect letters from alphabet
            foreach (LetterGuess letter in Guesses[^1].Guess)
            {
                if (letter.LetterResult == LetterResult.Incorrect && alphabet.Contains(letter.Letter))
                {
                    alphabet = alphabet.Remove(alphabet.IndexOf(letter.Letter), 1);
                }
            }

            Console.WriteLine(alphabet);

            // find words that:
            // contain correct letter(s) in the right place(s)
            // contain misplaced letter(s) NOT in any previous place(s)
            // don't contain any incorrect letter(s)

            // throw out impossible solutions
            foreach (string word in options)
            {
                foreach (char letter in word)
                {
                    if (!alphabet.Contains(letter))
                    {
                        options.Remove(word);
                        break;
                    }
                }
            }

            // heapify by frequency and choose max?
            // or choose alphabetically, generally seems to work better
                // ex. cater vs. later/rater/hater/water, scare vs. snare/spare/share/stare
                // counterpoint: stove vs. stone/stoke/store

            //return "cloud";

            #region testing purposes only
            switch (Guesses.Count)
            {
                case 1:
                    return "cloud";
                case 3:
                    return "timid";
                case 4:
                    return "block";
                case 5:
                    return "focus";
                case 6:
                    return "saber";
                default:
                    return "squid";
            }
            #endregion
        }
    }
}
