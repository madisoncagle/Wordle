using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wordle
{
    class ReginaIII : IWordleBot
    {
        public List<GuessResult> Guesses { get; set; }

        private Dictionary<string, int> options = new Dictionary<string, int>();

        public ReginaIII()
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

            options = GetWordScores(tmp);
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
            options = GetWordScores(options);

            if (Guesses.Count == 1)
            {
                return "toils";
            }

            // return max
            string max = options.FirstOrDefault(x => x.Value == options.Values.Max()).Key;
            //return options[0];
            return max;
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

        private Dictionary<char, List<int>> GetLetterFrequencies(List<string> words)
        {
            Dictionary<char, List<int>> frequencies = new Dictionary<char, List<int>>();

            foreach (string w in words)
            {
                for (int i = 0; i < w.Length; i++)
                {
                    if (!frequencies.ContainsKey(w[i]))
                    {
                        frequencies[w[i]] = new List<int>() { 0, 0, 0, 0, 0 };
                    }

                    frequencies[w[i]][i]++;
                }
            }

            return frequencies;
        }

        private Dictionary<char, List<int>> GetLetterFrequencies(Dictionary<string, int> words)
        {
            Dictionary<char, List<int>> frequencies = new Dictionary<char, List<int>>();

            foreach (string w in words.Keys)
            {
                for (int i = 0; i < w.Length; i++)
                {
                    if (!frequencies.ContainsKey(w[i]))
                    {
                        frequencies[w[i]] = new List<int>() { 0, 0, 0, 0, 0 };
                    }

                    frequencies[w[i]][i]++;
                }
            }

            return frequencies;
        }

        /*public Dictionary<char, List<double>> GetLetterWeights(List<string> words)
        {
            Dictionary<char, List<double>> weights = new Dictionary<char, List<double>>();
            Dictionary<char, List<int>> frequencies = GetLetterFrequencies(words);

            foreach (char l in frequencies.Keys)
            {
                weights[l] = (double)frequencies[l] / frequencies.Values.Sum();
            }

            return weights;
        }*/

        /*public Dictionary<char, List<double>> GetLetterWeights(Dictionary<string, List<double>> list)
        {
            Dictionary<char, double> weights = new Dictionary<char, double>();
            Dictionary<char, int> frequencies = GetLetterFrequencies(list);

            foreach (char l in frequencies.Keys)
            {
                weights[l] = (double)frequencies[l] / frequencies.Values.Sum();
            }

            return weights;
        }*/

        private Dictionary<string, int> GetWordScores(List<string> words)
        {
            Dictionary<string, int> wordScores = new Dictionary<string, int>();
            Dictionary<char, List<int>> frequencies = GetLetterFrequencies(words);

            foreach (string w in words)
            {
                wordScores[w] = 0;

                for (int i = 0; i < w.Length; i++)
                {
                    wordScores[w] += frequencies[w[i]][i];
                }
            }

            return wordScores;
        }

        private Dictionary<string, int> GetWordScores(Dictionary<string, int> words)
        {
            Dictionary<string, int> wordScores = new Dictionary<string, int>();
            Dictionary<char, List<int>> frequencies = GetLetterFrequencies(words);

            foreach (string w in words.Keys)
            {
                wordScores[w] = 0;

                for (int i = 0; i < w.Length; i++)
                {
                    wordScores[w] += frequencies[w[i]][i];
                }
            }

            return wordScores;
        }
    }
}
