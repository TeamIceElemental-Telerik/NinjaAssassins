# NinjaAssassins
A console card game - a team project for Telerik Software Academy, course C# Part 2

#Game Design (Singleplayer console game):

###Players (4 players):
* main player - you
* 3 AI players

###Deck with N number of cards (N = Deck: 56 cards / Endless):
- each player draw A CARD one after another (?)
- after a draw he decideds to play a card (? - or not)
- each play can have X number of maximum cards (X = 3)

###Cards Logic:
1.	Ninja Assassin (Exploding Ninja) – if you draw a ninja assassin you die, unless you have one of the cards below (Green Ninja, Fight, Hide or Escape)
2.	Green Ninjas (Defuse) – save your life
3.	Fight – dice? random number – bigger wins
4.	Escape - run for your life (yeah right, outrun a ninja) – draw another card – if it’s a Green Ninja/Fight/Hide or escape – you have a chance, if not – bye, bye.
5.	Hide –- use this card and try to hide behind one of the other players (or guess a word?/answer a question? – if correct, you save yourself or…) – if the one on your left (or by your choice) has a saving card, you can ask them for it, use it, then place both the saving and assassin card back in the deck at a random position. The player who has given you a card skips a turn in return.
6.	Attack – attack another player – make next player draw two card (or decrease their score or something else)
7.	Skip turn – use this card and skip drawing (if you draw it and use it – you skip the next drawing. If you have it in your hand and play it – you skip the current drawing)
8.	Shuffle (Confuse the ninja) – shuffle the deck
9.	More cards?

###Base Logic:
4.	If the drawn card is not a Ninja Assassin, you can hold on to it, deciding when to play it. A player can hold on to no more than 3? cards. When you have 3, on your next turn, you have to draw one and play one of the four by choice.
5.	If you draw a Ninja Assassin – you either die or use: Green Ninja to defuse it, Fight – to try and beat them, Hide or Escape (see above)
6.	The game ends when you’re killed (or there are no more cards in the deck if it’s not endless). The more cards you have in your hand when the game ends – the worse for your score
7.	Scoring – each card has different score

###Tactic Logic(optimal scenario)
 Try not to draw Ninja Assassin or use Green Ninja to defuse it, Fight – to try and beat them, Hide or Escape

###Game Goal :
 Be a ninja as a last man standing



