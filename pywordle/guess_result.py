from letter_guess import LetterResult
from letter_guess import LetterGuess


class GuessResult:
    def __init__(self, guess: str):
        self.guess = []

        for letter in guess:
            self.guess.append(LetterGuess(letter))

    def __str__(self):
        result = ""

        for letter in self.guess:
            result += letter.letter

        result += "\n"

        for letter in self.guess:
            if letter.letter_result == LetterResult.correct:
                result += "C"
            elif letter.letter_result == LetterResult.misplaced:
                result += "M"
            else:
                result += "I"

        result += "\n"

        return result
