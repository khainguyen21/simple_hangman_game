# Simple Hangman Game

A classic implementation of the Hangman game built in C# using a graphical interface.

## Description

This is a simple Hangman game where players try to guess a secret word one letter at a time. The game features:
- A graphical interface using CDrawer for visualization
- A collection of 699 words to guess from
- Visual representation of the hangman that builds with each wrong guess
- 6 tries before game over
- Option to replay the game

## Features

- Random word selection from a predefined word list
- Visual feedback with hangman drawing
- Letter guessing validation
- Progress tracking for correct and wrong guesses
- Win/lose condition checking
- Replay functionality

## Implementation Details

The game is implemented with the following key components:

1. **Main Program Flow:**
   - Random word generation
   - Canvas creation (800x600)
   - Word loading from text file
   - Guess validation and checking
   - Win/lose condition monitoring
   - Replay option

2. **Core Functions:**
   - `DrawScreen()`: Renders the hangman graphics and game state
   - `GetGuess()`: Validates user input for letter guesses
   - `CheckGuess()`: Verifies if guessed letters are in the secret word

3. **Visual Elements:**
   - Hangman gallows drawing
   - Body parts appear progressively with wrong guesses
   - Display of correctly guessed letters
   - Display of previously guessed letters

## Setup

1. Ensure you have a C# development environment set up
2. Clone the repository
3. Make sure the `Hangman.txt` word list file is in the correct location
4. Build and run the program

## Game Rules

1. A random word is selected from the word list
2. Player gets 6 attempts to guess the word correctly
3. Each wrong guess adds a part to the hangman drawing
4. Correctly guessed letters are revealed in the word
5. Game ends when either:
   - The word is correctly guessed (Win)
   - The hangman drawing is completed (Loss)

## Technical Notes

- Built using C#
- Uses CDrawer for graphics
- Includes error handling for file operations
- Implements user input validation
- Written by: Khai Nguyen
- Created: 2022-12-09

## File Structure

- `Program.cs`: Main game logic and implementation
- `Hangman.txt`: Word list containing 699 words for the game

## Contributing

This is a personal project, but suggestions and improvements are welcome through issues or pull requests.
