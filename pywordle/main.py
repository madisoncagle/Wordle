from wordle_game import WordleGame
from regina import Regina

regina = Regina()
game = WordleGame("prize")
guesses = game.play(regina)

print(guesses)
