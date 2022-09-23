from wordle_game import WordleGame
from regina import Regina

regina = Regina()
game = WordleGame("glory")
guesses = game.play(regina)

print(guesses)
