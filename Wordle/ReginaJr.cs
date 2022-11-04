using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wordle
{
    class ReginaJr : IWordleBot
    {
        public List<GuessResult> Guesses { get; set; }

        private Dictionary<string, double> options = new Dictionary<string, double>();

        public ReginaJr()
        {
            Guesses = new List<GuessResult>();
            List<string> tmp = new List<string>();

            // read all 5 letter words into tmp
            try
            {
                using (StreamReader streamReader = new StreamReader("../../../data/five_letter_words.txt"))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        tmp.Add(line);
                        continue;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            options = GetWordWeights(tmp);
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
            foreach (string word in options.Keys)
            {
                if (!(rgx.IsMatch(word) && keepers.IsMatch(word)))
                {
                    options.Remove(word);
                }
            }

            // update weights
            options = GetWordWeights(options);

            if (Guesses.Count == 1)
            {
                return "toils";
            }

            // return max
            string maxWord = options.FirstOrDefault(x => x.Value == options.Values.Max()).Key;
            //return options[0];
            return maxWord;
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

        private Dictionary<char, int> GetLetterFrequencies(List<string> words)
        {
            Dictionary<char, int> frequencies = new Dictionary<char, int>();

            foreach (string w in words)
            {
                foreach (char l in w)
                {
                    if (!frequencies.ContainsKey(l))
                    {
                        frequencies[l] = 0;
                    }

                    frequencies[l] += 1;
                }
            }

            return frequencies;
        }

        private Dictionary<char, int> GetLetterFrequencies(Dictionary<string, double> words)
        {
            Dictionary<char, int> frequencies = new Dictionary<char, int>();

            foreach (string w in words.Keys)
            {
                foreach (char l in w)
                {
                    if (!frequencies.ContainsKey(l))
                    {
                        frequencies[l] = 0;
                    }

                    frequencies[l] += 1;
                }
            }

            return frequencies;
        }

        private Dictionary<char, double> GetLetterWeights(List<string> words)
        {
            Dictionary<char, double> weights = new Dictionary<char, double>();
            Dictionary<char, int> frequencies = GetLetterFrequencies(words);

            foreach (char l in frequencies.Keys)
            {
                weights[l] = (double)frequencies[l] / frequencies.Values.Sum();
            }

            return weights;
        }

        private Dictionary<char, double> GetLetterWeights(Dictionary<string, double> list)
        {
            Dictionary<char, double> weights = new Dictionary<char, double>();
            Dictionary<char, int> frequencies = GetLetterFrequencies(list);

            foreach (char l in frequencies.Keys)
            {
                weights[l] = (double)frequencies[l] / frequencies.Values.Sum();
            }

            return weights;
        }

        private Dictionary<string, double> GetWordWeights(List<string> words)
        {
            Dictionary<string, double> wordWeights = new Dictionary<string, double>();
            Dictionary<char, double> letterWeights = GetLetterWeights(words);

            foreach (string word in words)
            {
                wordWeights[word] = 0;

                foreach (char l in word)
                {
                    wordWeights[word] += letterWeights[l];
                }
            }

            return wordWeights;
        }

        private Dictionary<string, double> GetWordWeights(Dictionary<string, double> words)
        {
            Dictionary<string, double> wordWeights = new Dictionary<string, double>();
            Dictionary<char, double> letterWeights = GetLetterWeights(words);

            foreach (string word in words.Keys)
            {
                wordWeights[word] = 0;

                foreach (char l in word)
                {
                    wordWeights[word] += letterWeights[l];
                }
            }

            return wordWeights;
        }
    }
}
