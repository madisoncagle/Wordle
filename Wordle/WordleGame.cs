using System;
namespace Wordle
{
    public class WordleGame
    {
        public string SecretWord { get; set; }
        public int MaxGuesses { get; set; }

        public WordleGame(string secretWord = "arise")
        {
            SecretWord = secretWord;
        }

        public int Play(IWordleBot bot)
        {
            int guessNumber;
            for (guessNumber = 1; guessNumber < MaxGuesses; guessNumber++)
            {
                string guess = bot.GenerateGuess();
                Console.WriteLine($"guess {guessNumber}: {guess}");

                GuessResult guessResult = CheckGuess(guess);
                bot.Guesses.Add(guessResult);
                Console.WriteLine(guessResult);

                if (IsCorrect(guessResult))
                {
                    return guessNumber;
                }
            }

            return guessNumber;
        }

        public GuessResult CheckGuess(string guess)
        {
            GuessResult result = new GuessResult(guess);
            string copy = new string(SecretWord);

            // check for correct
            for (int i = 0; i < SecretWord.Length; i++)
            {
                if (guess[i] == SecretWord[i])
                {
                    result.Guess[i].LetterResult = LetterResult.Correct;
                    copy = copy.Remove(copy.IndexOf(guess[i]), 1);
                }
            }

            // check for misplaced
            for (int i = 0; i < SecretWord.Length; i++)
            {
                if (copy.Contains(guess[i]) && result.Guess[i].LetterResult == LetterResult.Incorrect)
                {
                    result.Guess[i].LetterResult = LetterResult.Misplaced;
                    copy = copy.Remove(copy.IndexOf(guess[i]), 1);
                }
            }

            return result;
        }

        private bool IsCorrect(GuessResult guessResult)
        {
            foreach (var letterGuess in guessResult.Guess)
            {
                if (letterGuess.LetterResult != LetterResult.Correct)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

