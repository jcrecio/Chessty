# Chessty
Chessty is a simple C# engine that was originally thought to perform a little AI functionality test. After that it grew up quite a bit more than expected and the outcome is such an engine. 

![alt text](https://raw.githubusercontent.com/jcrecio/Chessty/master/chess.png)

## Features
### Negamax
The variation of the Alpha Beta Search algorithm Negamax is used to make an iterative deepening search of the best move. 

Other techniques are meanwhile being implemented and tested like aspiration search.

### Killer Moves
Killer moves table is used to order the moves for each node in a most desirable way.

### Null Moves
Null moves consists of letting a side move consecutively in order to analyse the position to be able to reduce the search for the best move.

### Zobrist Hashing
This is a technique created by Zobrist about 40 years ago that lets store board positions by hashing simple numbers instead of big structures standing for the board. 
Check out his article of 1970: http://research.cs.wisc.edu/techreports/1970/TR88.pdf

### Transposition
Transposition tables are used to store the nodes previously evaluated in a game in order to reduce the amount of nodes in future searches.

## State of the project

### Performance
The core functionalities of the engine are still under development and being tested. I will be gathering all the information I can in order to improve the algorithms and the way the code looks like to make it as clear as possible for any possible developer.

### Bugs
As well as the development is on going, I keep finding bugs when trying more strange and unlikely scenarios. If you find one and you don't mind, just report it =)

### UI
This is so far the aspect of the project I've been less focused in because I consider it's (at least at the beginning) out of the purpose of the engine.

### Chess quality
I'm just a chess fan and even I've played chess for many years I'm far to be a good player or someone who has quite strong knowledge about chess. The idea behind all of this is to implement the ideas to make it play as best as possible. Once done, I'd like to compare it with engines already made years ago.
