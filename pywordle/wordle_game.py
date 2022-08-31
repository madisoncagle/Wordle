from guess_result import GuessResult
from letter_guess import LetterResult


class WordleGame:
    def __init__(self, word: str = "rebut"):
        self.secret_word = word
        self.max_guesses = 20

    def play(self, bot):
        for guess_num in range(1, self.max_guesses + 1, 1):
            guess = bot.generate_guess()
            print(f"guess {guess_num}: {guess}")

            guess_result = self._check_guess(guess)
            bot.guesses.append(guess_result)
            print(guess_result)

            if self._is_correct(guess_result):
                return guess_num

        return guess_num

    def _check_guess(self, guess: str):
        result = GuessResult(guess)
        copy = self.secret_word

        # check for correct
        for i in range(len(self.secret_word)):
            if guess[i] == self.secret_word[i]:
                result.guess[i].letter_result = LetterResult.correct
                copy = copy.replace(guess[i], "", 1)

        # check for misplaced
        for i in range(len(self.secret_word)):
            if copy.__contains__(guess[i]) and result.guess[i].letter_result == LetterResult.incorrect:
                result.guess[i].letter_result = LetterResult.misplaced
                copy = copy.replace(guess[i], "", 1)

        return result

    def _is_correct(self, guess_result: GuessResult):
        for lg in guess_result.guess:
            if lg.letter_result != LetterResult.correct:
                return False

        return True
    
    def __str__(self):
        return f"Solution: {self.secret_word}\nMax guesses allowed: {self.max_guesses}"
