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

        private string[] options;
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
            // find words that:
                // contain correct letter(s) in the right place(s)
                // contain misplaced letter(s) NOT in any previous place(s)
                // don't contain any incorrect letter(s)

            // heapify by frequency and choose max?
            // or choose alphabetically, generally seems to work better
                // ex. cater vs. later/rater/hater/water, scare vs. snare/spare/share/stare
                // counterpoint: stove vs. stone/stoke/store

            return "cloud";
        }
    }
}
