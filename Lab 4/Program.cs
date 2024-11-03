/* Lab 4
 * Khai Nguyen
 * 2022-12-09
 * 
 * 
 * Main program:
 * * Generate the random number 
 * * Create the canvas (800x600)
 * * Use System.IO to take the words from the textfiles 
 * * Create the array for only correct guessed 
 * * Using the DrawScreen() to draw the tools for hangman man 
 * * Using GetGuess() to check the valid of the letter they are typing 
 * * Using CheckGuess() to check if that letter have it in srecret word
 * * Also count correct and wrong each time check the valid of the word
 * * Using that value of correct and wrong to determine the win or lose 
 * * Using user if they want to replay the game 

 *  
 *  
 *  DrawScreen()
 * * Draw hangman tools and including the 6 parts of the hangman represent 6 tries
 * * Since user has 6 tries, each time they're wrong, increment one wrong and the draw one part body
 * * Replace the dashline if user guess correct, if not, stay the same
 * * Display character user already used
 * 
 *  GetGuess()
 * * Check invalid if user try to type something invalid or the letter that already used
 * * If it's all passed the requirement, return that letter and use it in CheckGuess()
 * 
 * 
 *  CheckGuess()
 * * Create the array of correct letter that user type
 * * Each time it's correct put that letter into the array
 * * Each time it's wrong, increment wrong counter
 * * Use foreach loop to check if each time user guess right, increment for one
 * * if that word has same letter at the time, increment by amount of that letter
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;
using System.Drawing;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;

namespace Lab_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();//generate the random number 
            CDrawer canvas = new CDrawer(800,600); // create the canvas
            bool checking = true;
            bool valid; 

            //play the again the game 
            do
            {
                Console.Clear(); //catch the console when play again
                string secretword = ""; //create secret word with the blank 
                string letterguessed = "";//create the letter already with the blank
                string rerun; //store string to ask user to play again the game
                string[] wordinfiles = new string[699];//create the array for all the word in filestext 


                //now let's read from a file!
                StreamReader textfiles = null;
                try
                {
                    //use the text files
                     textfiles = new StreamReader(@"C:\Users\khain\Downloads\hangman.txt");

                    //Read gives you one word at a time, so to read everything:
                    for (int i = 0; i < wordinfiles.Length; i++)
                    {
                        wordinfiles[i] = textfiles.ReadLine();
                    }
                    secretword = wordinfiles[random.Next(0, 700)];
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (textfiles != null)
                    {
                        textfiles.Close();
                    }
                }
                Console.WriteLine(secretword);

                //create the array for only correct letter 
                char[] correctguessed = new char[secretword.Length];
               
                int counter = 0; // counter number
                int wrong = 0; //store the amount of wrong 
                int correct = 0; //store the amount of correct 
                valid = true; //if it's valid run the game 
                
                //play the game
                while(valid)
                { 
                    canvas.Clear();
                    DrawScreen(canvas, ref wrong, ref letterguessed, secretword, correctguessed);
                    char letter = GetGuess("\nGuess a letter: ", letterguessed);
                    letterguessed += letter;
                    CheckGuess(letter, secretword, ref counter, ref wrong, ref correctguessed, ref correct);
                    // checking if it's 6 wrong display "you lose" and ask want to play again
                    if (wrong >= 6)
                    {
                        Console.WriteLine("Current score: " + correct + "/" + secretword.Length);
                        DrawScreen(canvas, ref wrong, ref letterguessed, secretword, correctguessed);
                        canvas.AddText("The answer is "+secretword, 40, 30, 500, 600, 100, Color.Orange);
                        canvas.AddText("You lose!", 100, 100, 250, 600, 100, Color.LightGray);

                        Console.WriteLine("You wanna play again?");
                        rerun = Console.ReadLine();
                        if (rerun == "yes")
                        {
                            valid = false;
                            checking = true; //playagain
                        }
                        else
                        {
                            valid = false;
                            checking = false; //exit the game 
                        }
                    }
                    //checking if it's all correct, display "you win" and ask want to play again
                    else if (correct == secretword.Length)
                    {
                        Console.WriteLine("Current score: " + correct + "/" + secretword.Length);
                        DrawScreen(canvas, ref wrong, ref letterguessed, secretword, correctguessed);
                        canvas.AddText("You Won!", 100, 100, 250, 700, 100, Color.LightGray);

                        Console.WriteLine("You wanna play again?");
                        rerun = Console.ReadLine();
                        if (rerun == "yes")
                        {
                            valid = false;
                            checking = true;//play the again
                        }
                        else
                        {
                            valid = false; 
                            checking = false; //exit the game 
                        }
                    }
                    Console.WriteLine("Current score: " + correct + "/" + secretword.Length);
                }
            } while (checking);
        }
        //********************************************************************************************
        //Method: static private void DrawScreen(CDrawer canvas, ref int wrong, string lettergussed, string secretword, char []correctguessed )
        //Purpose: Draws the all tools for hangman
        //Parameters: 
        //CDrawer canvas: use canvas in main 
        //ref int wrong: counting the each time it's wrong
        //int[] Array: array value either iArray1 or iArray2
        //string lettergussed : store the value letter
        //string secretword : store the value 
        //char [] correctguessed: store the value user correctguessed
        //*********************************************************************************************
        static private void DrawScreen(CDrawer canvas, ref int wrong, ref string lettergussed, string secretword, char []correctguessed)
        {
            canvas.Clear();

            //draw the hangman tools 
            canvas.AddLine(300, 100, 300, 420, Color.Cyan, 5);
            canvas.AddLine(440, 370, 440, 420, Color.Cyan, 5);

            canvas.AddLine(300, 110, 380, 110, Color.Cyan, 5);
            canvas.AddLine(300, 370, 470, 370, Color.Cyan, 5);

            canvas.AddLine(300, 380, 440, 410, Color.Cyan, 5);
            canvas.AddLine(300, 410, 440, 380, Color.Cyan, 5);

            //Red Rope
            canvas.AddLine(360, 110, 360, 150, Color.Red, 2);

            if (wrong > 0)
            {
                //head
                canvas.AddEllipse(345,150,30,25, Color.LightYellow);
            }
            if (wrong > 1)
            {
                //body
                canvas.AddEllipse(345, 170, 30, 95, Color.LightYellow);
            }
            if (wrong > 2)
            {   
            //left hand 
                canvas.AddLine(330, 220, 350, 188, Color.LightYellow, 5);
            }
            if (wrong > 3)
            {
            //right hand
                canvas.AddLine(360, 170, 390, 220, Color.LightYellow, 5);
            }
            if (wrong > 4)
            {
            //left leg
                canvas.AddLine(330, 300, 360,250, Color.LightYellow, 5 );
            }
            if (wrong > 5)
            {
            //right leg
                canvas.AddLine(360, 250, 390, 300, Color.LightYellow, 5);
            }      

            char charTodraw; // generate the char to draw dashlines

            //go through every characters in the game to check if's the same as correctguessed array
            for (int i = 0; i < secretword.Length; i++)
            {
                charTodraw = '-';

                if (correctguessed.Contains(secretword[i]))
                {
                    charTodraw = secretword[i];
                }
                
                //replace the dashlines with correct words 
                canvas.AddText(charTodraw.ToString(), 60, 120+i*50, 420,300,100, Color.Orange);
                //display the letter already guessed 
                canvas.AddText($"Letters used: {lettergussed}", 20, 10, 10, 400, 50, Color.Cyan);
            }

        }
        //********************************************************************************************
        //Method: static char GetGuess(string prompt, string letterguessed)
        //Purpose: Check the valid of letter they type in
        //Parameters: 
        //string prompt: store the value prompt        
        //string letterguessed: store the user already guessed 
        //*********************************************************************************************
        static char GetGuess(string prompt, string letterguessed)
        {
            string word; //store the letter user type in
            bool valid; //checking the valid of the words
            char letter; // store the letter after passing all requirements

            //check the valid of the letter 
            do
            {
                valid = true;
                Console.Write(prompt);
                word = Console.ReadLine();
 
                foreach (char c in word)
                {
                    if (char.IsNumber(c))
                    {
                        valid = false;
                    }
                    else if (letterguessed.Contains(c))
                    {
                        valid = false; 
                    }
                    else if (char.IsWhiteSpace(c) || char.IsPunctuation(c) || char.IsSymbol(c))
                    {
                        valid = false;
                    }
                }
                if (word.Length > 1 || !valid)
                {
                    Console.WriteLine("You have entered invalid input.");
                }
            } while (!valid || word.Length > 1) ;
 
            char.TryParse(word, out letter);
            return letter; 
        }
        //********************************************************************************************
        //Method: static public void CheckGuess(char guess, string secretword, ref int correct, ref int wrong,string letterGuessed, ref char[] correctguessed)
        //Purpose: Check if the letter they typing is valid 
        //Parameters: 
        //char guess: store the letter in guess got from GetGuess()
        //string prompt: store the value prompt        
        //string letterguessed: store the user already guessed 
        //ref int correct: couting each it's correct
        //ref int wrong : counting each it's wrong
        //ref char [] correctguessed: store all correct value into the arrays 
      
        //*********************************************************************************************
        static public void CheckGuess(char guess, string secretword, ref int counter, ref int wrong, ref char[] correctguessed, ref int correct)
        {
            //string letter = guess.ToString();

            //adding the correct guess letter to array 
            //if wrong change guess to letter 
            if (secretword.Contains(guess))
            {
                counter++;
                Console.WriteLine("You're correct");
                //correctguessed[counter - 1] = letter[0];
                correctguessed[counter - 1] = guess;

            }
            else
            {
                Console.WriteLine("You're wrong!!!");
                wrong++;
            }
            //checking if the secretword is containing the letter user type in, increase the correct by 1 
            foreach (char c in secretword)
            {
                if (guess == c)
                {
                    correct++;
                }
            }
        }
    }
}

