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
			int guessNumber = 0;
			for(guessNumber = 0; guessNumber < MaxGuesses; guessNumber++)
            {
				string guess = bot.GenerateGuess();
                Console.WriteLine($"guess {i+1}: {guess}");

				GuessResult guessResult = CheckGuess(guess);
				bot.Guesses.Add(guessResult);
                Console.WriteLine(guessResult);
            }

			return guessNumber;
        }

		public GuessResult CheckGuess( string guess )
        {
			return new GuessResult(guess);
        }




	}
}

