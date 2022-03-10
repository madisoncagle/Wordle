using System;

namespace Wordle
{
    class Program
    {
        static void Main(string[] args)
        {
            var guessResult = new GuessResult("arise");

            Console.WriteLine(guessResult);

            var bot = new BrycesSweetBot();

            var game = new WordleGame("saber");

            int guesses = game.Play(bot);
        }
    }
}

