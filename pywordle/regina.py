"""
Regina, the Wordle bot that uses regex to (almost) always win
"""
import re
from letter_guess import LetterGuess, LetterResult
from guess_result import GuessResult


class Regina:
    def __init__(self):
        self.guesses = []
        self._options = []

        # read all 5 letter words into options
        for word in open("./words.txt"):
            word = word.strip()
            if len(word) == 5:
                self._options.append(word)

    def generate_guess(self) -> str:
        if len(self.guesses) == 0:
            return "crane"

        last_guess = self.guesses[-1]
        rgx = self._generate_regex(last_guess)
        keepers = self._generate_keeper_set()

        # eliminate impossible solutions
        for word in list(self._options):
            if not (re.match(rgx, word) and re.match(keepers, word)):
                self._options.remove(word)

        if len(self.guesses) == 1:
            return "toils"

        return self._options[0]

    def _generate_regex(self, guess: GuessResult):
        pattern = ""

        for lg in guess.guess:
            if lg.letter_result == LetterResult.correct:
                pattern += lg.letter
            else:
                pattern += f"[^{lg.letter}]"

        return pattern

    def _generate_keeper_set(self):
        keepers = ""

        for gr in self.guesses:
            for lg in gr.guess:
                if lg.letter_result != LetterResult.incorrect and not keepers.__contains__(lg.letter):
                    keepers += f"(?=.*{lg.letter})"

        return keepers

    def __str__(self):
        return "Hi, I'm Regina, a Wordle-obsessed bot. Bet you can't solve today's word in fewer guesses than me."
