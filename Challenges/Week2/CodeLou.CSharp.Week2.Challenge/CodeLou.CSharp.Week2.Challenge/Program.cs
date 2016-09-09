using System;
using System.IO;
using System.Threading;

namespace CodeLou.CSharp.Week2.Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            //let's set the size of the window:
            Console.SetWindowSize(80, 20);

            // Task 1:
            // The Console class is in the System namespace in the .Net framework. Add a "using" statement
            // to the top of this file to import the System namespace so that the C# compiler knows where
            // to look for the Console class.

            // Task 2:
            // This application won't build because of a syntax error. Fix the code below to make it build.
            // Hint: In Visual Studio, you can build the project by selecting "Build Solution" from the
            //       build menu, or by pressing <Ctrl>+<Shift>+B.
            Console.WriteLine("Welcome to the Code Louisville C# week 2 code challenge!");
            Console.WriteLine("Press <Enter> to begin...");
            Console.ReadLine();
            
            //convert the user's input from a string
            //we've refactored everything down into a method
            int countDown = GetUserInput();

            // Task 5:
            // Add an "else" block to the condition from Task 4. This should be run in the case that the
            // number is greater than zero. Write each number to the console, counting backwards, from the 
            // user's number to zero. Then write, "LIFTOFF!".
            // Hint: You can accomplish this with one of several kinds of loops, including "while" and
            //       "for". You can choose whichever you'd like to solve the task. The Microsoft
            //       Developer Network (MSDN) website contains all of the documentation for C#. If you want
            //       to learn more about loops, visit https://msdn.microsoft.com/en-us/library/32dbftby.aspx.

            if (IsLessThanOrEqualToZero(countDown))
            {
                Console.WriteLine("Number must be greater than zero.");
            }
            else
            {
                

                for (int i = countDown; i >= 0; i--)
                {
                    //clear the console before each number in the countdown
                    Console.Clear();

                    //center the console text
                    //use a little math to find the middel of the
                    //screen using the screen size and the length of the text
                    Console.SetCursorPosition((Console.WindowWidth - i.ToString().Length) / 2, Console.CursorTop);

                    //write the numer to the screen
                    Console.WriteLine(i);
                    Thread.Sleep(1000);
                }

                //center the console text
                string liftOff = "Prepare for LIFTOFF!";
                Console.SetCursorPosition((Console.WindowWidth - liftOff.Length) / 2, Console.CursorTop);
                Console.WriteLine(liftOff);

                //flash the screen and wait a bit between colors
                //Console.Clear() must be called when colors are changed
                Console.Clear();
                Thread.Sleep(400);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Clear();
                Thread.Sleep(400);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Clear();
                Thread.Sleep(400);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Clear();
                Thread.Sleep(400);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Clear();
                Thread.Sleep(400);
                Console.BackgroundColor = ConsoleColor.Red;


                //set it back to black and launch the rocket
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                //scroll the rocket up the screen
                PrintRocket();
            }

            Console.WriteLine("Press <Enter> to exit...");
            Console.ReadLine();
        }

        static bool IsLessThanOrEqualToZero(int num)
        {
            return num <= 0;
        }

        //method to print a rocket on screen
        //one line at a time
        //Notcie I am using Pascal Case for my method names
        private static void PrintRocket()
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            //this line required a new using statement
            //art.text has had it's "copy to output directory" property changed to "copy if newer"
            StreamReader file = new System.IO.StreamReader("art.txt");
            while ((line = file.ReadLine()) != null)
            {
                //write the line to the screen
                Console.WriteLine(line);
                
                //wait a few milliseconds
                Thread.Sleep(200);

                //increment the counter
                counter++;
            }

            file.Close();
        }

        private static int GetUserInput()
        {
            //set up the methods return value
            int returnValue = 0;

            while (returnValue == 0)
            {
                try
                {
                    //clear the console;
                    Console.Clear();

                    //ask the question
                    Console.WriteLine("This is the launch application for the first human mission to Mars.");
                    Console.Write("Enter the number of seconds you would like to count down from: ");

                    string input = Console.ReadLine();
                    returnValue = int.Parse(input);

                    if (returnValue < 0)
                    {
                        returnValue = 0;
                    }
                }
                catch (Exception)
                {
                    //write an error because of the data conversion
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("Error: Please enter a number greater than zero.");

                    //give the user long enough to see the error
                    Thread.Sleep(1000);
                }
            }

            //retuirn the result
            return returnValue;
        }


    }
}
