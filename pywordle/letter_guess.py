from enum import Enum


class LetterResult(Enum):
    correct = 1,
    misplaced = 2,
    incorrect = 3


class LetterGuess:
    def __init__(self, letter: str):
        self.letter = letter
        self.letter_result = LetterResult.incorrect

    def __str__(self):
        return f"{self.letter}: {self.letter_result.name}"
