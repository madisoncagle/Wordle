using System;

namespace Wordle
{
    class Program
    {
        static void Main(string[] args)
        {
            //var guessResult = new GuessResult("arise");

            //Console.WriteLine(guessResult);

            var bot = new Squid();

            var game = new WordleGame("saber");
            game.MaxGuesses = 6;

            int guesses = game.Play(bot);

            Console.WriteLine(guesses);
        }
    }
}

