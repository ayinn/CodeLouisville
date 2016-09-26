using FileHelpers;
using FluentValidation.Results;
using ShellProgressBar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Week4StreamAndData
{
    class Program
    {
        static void Main(string[] args)
        {
            //Remember a backslash is a special escape character
            //If you want a line with a quote in the middle of the line you have to "escape"
            //the special meaning of a quote. Uncomment each line and see which one
            //passes
            //string illegalQuoteString = "Don't "Quote" me on that!";
            string properlyEscapedQuoteString = "Don't \"Quote\" me on that!";

            //The same way the Dollar sign can be used for string interpolation
            //the @ symbol can be used to automatically escape characters between
            //the quotes. So a file path stored in a variable could be assigned like:
            string myExampleFilePath = "C:\\Some\\Path\\To\\A\\File";

            //but it's easier to use the @
            string myBetterExampleFilePath = @"C:\Some\Path\To\A\File";

            //both are correct but I think the second one is easier to read

            Console.WriteLine("*** Creating the list by paring the file ourselves ***");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            //Create a Roster --A list of RosterItems (Yes I renamed them since the meetup)
            //A list is nothing more than more than one of a class
            //This is a generic list List<FillInTheBlankWithYourObject>
            //It automatically gives us methods like Add(), Remove(), Find() and FindAll()
            //The <> angle brackets is a SIGNIFICANT pattern in c# you will find other libraries that
            //use the same pattern. When you see them you usually fill in the blank with a TYPE
            //The next line instantiates the generic list
            List<RosterItem> myRoster = new List<RosterItem>();

            //using System.IO file methods we can open the file and Read each line one at a time
            //Let's read the CSV file straight into our list
            //we don't have to include the entire file path since we set the file to "Copy Always"
            //this places the file in the same debug directory as our EXE
            myRoster = ReadFileToRosterList("Roster.csv");

            //loop through the list and write the first and last name of each roster item
            //along with their position
            foreach (RosterItem item in myRoster)
            {
                //by putting the dollar sign in fromt of the string we're telling
                //c# that we want the variable names inside the string to be translated
                //to their values (String Interpolation)
                Console.WriteLine($"Player: {item.FirstName} {item.LastName}, Position: {item.Position}");

                //lets add some drama by adding a quarter a second wait between lines
                System.Threading.Thread.Sleep(250);
            }

            Console.WriteLine("");
            Console.WriteLine("*** Now let's do it using the file helpers library ***");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            //Convert a CSV file to an object list
            //using FileHelpers (downloaded via nuget)
            var engine = new FileHelperEngine<RosterItem>();

            //we don't want to read the header of the file
            engine.Options.IgnoreFirstLines = 1;
            
            //Now we do it an easier way
            var result = engine.ReadFile("Roster.csv");

            //we managed to convert the file to our class
            //and only had to use 4 lines of code and add
            //an attribute of [DelimitedRecord(",")] to the class
            //pretty easy
            //lets print the info to screen again
            //this time we'll use a for loop instead of a foreach loop
            //loop through the list and write the first and last name of each roster item
            //along with their position
            for (int i = 0; i < myRoster.Count; i++)
            {
                //assign the item in the array to item
                //I think this makes the code a bit cleaner
                //you can, of course, just use myRoster[i].FirstName
                RosterItem item = myRoster[i];
                Console.WriteLine($"Player: {item.FirstName} {item.LastName}, Position: {item.Position}");

                //lets add some drama by adding a quarter a second wait between lines
                System.Threading.Thread.Sleep(250);
            }
            //so when should we use a for loop insatead of a foreach? when we need to
            //track the item number or when we need a counter for the items we're looping through

            //now that we have the list of items
            //let's "Serialize" them
            //The easy way to think about serialzation is that it's the process of
            //turning an object or object list into text (usually XML or JSON)
            //deserilization is the process of taking serialized text and turning it
            //back inot the object or object list
            //this code requires the XML.Serialzation library

            Console.WriteLine("");
            Console.WriteLine("*** Now let's serialize our list ***");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            // Create a new XmlSerializer instance with the type of the test class
            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<RosterItem>));

            // Create a new filestream so we can write the serialized object to a file
            using (TextWriter WriteFileStream = new StreamWriter("roster.xml"))
            {
                //serialize our list<>
                SerializerObj.Serialize(WriteFileStream, myRoster);
            }

            //open the xml file with notepad
            //how did I figure out how to open a file in notepad?
            //easy I googled "C# Open File Notepad"
            Console.WriteLine("Opening our serialized data in notepad...");
            System.Threading.Thread.Sleep(500);
            Process.Start("notepad.exe", "roster.xml");

            Console.WriteLine("");
            Console.WriteLine("*** Now let's deserialize our list ***");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            //Deserialze the XML file
            // Create a new file stream for reading the XML file
            using (FileStream ReadFileStream = new FileStream("roster.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                // Load the object saved above by using the Deserialize function
                //the parenthesses mean we are "casting" the return object to that type
                List<RosterItem> reLoadedRoster = (List<RosterItem>)SerializerObj.Deserialize(ReadFileStream);

                //how many items are in our reloaded list
                Console.WriteLine($"We reloaded {reLoadedRoster.Count} items from our xml file.");
            }


            Console.WriteLine("");
            Console.WriteLine("*** Now let's add a new item to the list ***");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            //one of the challanges of adding a new entry is that it must be valid data
            //whenever possible you should prevent the user from entering bad data
            //in a windows or web application this is easier since most data being entered
            //is from a control (dropdown list or textbox), it's much more difficult to validate
            //when the user can only enter text, like in our console app
            
            //we're going to need to get the highest ID number so the next entry
            //will have a unique ID
            //This is a bit of a cheat since I know the items in the file we read in are numbered sequentially
            //so a simple count of the number of the items in the list + 1 should give us the next ID
            //there are probably a 100 other ways we can do this
            int NextId = myRoster.Count + 1;

            //get a new roster item
            //I broke this into its own method
            RosterItem newRosterItem = GetRosterItem(NextId);

            //Add the new item to the list
            myRoster.Add(newRosterItem);
            Console.WriteLine($"There are now {myRoster.Count} items in our list.");
            Console.WriteLine($"You just added {newRosterItem.FirstName} {newRosterItem.LastName} to the roster.");

            //Console.WriteLine(myString);
            Console.ReadLine(); 
        }

        private static RosterItem GetRosterItem(int NextId)
        {
            RosterItem returnValue = new RosterItem();

            //were only collecting 3 of the properties
            string FirstName = "";
            string LastName = "";
            string Gender = "";

            //create a validator with our new fluent validator class
            //take alook at the comments in the RosterItemValidator class
            RosterItemValidator validator = new RosterItemValidator();

            //start with the object as invalid
            bool IsValid = false;

            //this condition should always be triggered the first time through
            //then only triggered if a property is invalid
            while (!IsValid)
            {
                //this prevents a user having to re-enter valid values
                //if the field has an error we blank it out
                if (String.IsNullOrEmpty(FirstName))
                {
                    //get the first name
                    Console.WriteLine("Enter a FirstName: ");
                    FirstName = Console.ReadLine();
                }

                if (String.IsNullOrEmpty(LastName))
                {
                    //get the lastname
                    Console.WriteLine("Enter a LastName:");
                    LastName = Console.ReadLine();
                }

                if (String.IsNullOrEmpty(Gender))
                {
                    //get the gender
                    Console.WriteLine("Enter a Gender:");
                    Gender = Console.ReadLine();
                }

                //assign our values to the roster item
                returnValue = new RosterItem
                {
                    ID = NextId,
                    FirstName = FirstName,
                    LastName = LastName,
                    Gender = Gender
                };

                //check to see if the values are valid
                ValidationResult results = validator.Validate(returnValue);

                //set the variable for the loop
                IsValid = results.IsValid;

                //loop through all of the errors and display
                //an error message for each error
                foreach (var failure in results.Errors)
                {
                    //change the foreground color to red
                    Console.ForegroundColor = ConsoleColor.Red;

                    //display the error (blank line in front and behind)
                    Console.WriteLine("");
                    Console.WriteLine($"Property {failure.PropertyName} failed validation. \r\nError was: {failure.ErrorMessage}");
                    Console.WriteLine("");

                    //reset the console color
                    Console.ResetColor();

                    //blank out the bad value so it will be repeated to the user
                    switch (failure.PropertyName)
                    {
                        case "FirstName":
                            FirstName = "";
                            break;
                        case "LastName":
                            LastName = "";
                            break;
                        case "LastName.Length":
                            LastName = "";
                            break;
                        case "Gender":
                            Gender = "";
                            break;
                    }
                }
            }

            return returnValue;
        }

        //this method is declared as static so it can be run in
        //another static method
        //I've changed this method a bit to read directly into a List<>
        //instead of just a string
        private static List<RosterItem> ReadFileToRosterList(string FileName)
        {
            //I always like to start my method by declaring the return
            //value when there is a value to be returned
            List<RosterItem> returnValue = new List<RosterItem>();

            //set up a counter and line variable
            int counter = 0;
            string line;

            //always be aware of what resources your program is using
            //The C# garbage collector does a pretty good job of maintaining program
            //resources, but you shouldn't count on it whe using librarys that
            //acces external resources sunch as files or databases

            //create a progress bar using a nuget package called ShellProgressBar
            //https://github.com/Mpdreamz/shellprogressbar
            //found this by googling "c# console app progress bar"
            //the max number of items to process
            var maxTicks = File.ReadLines(FileName).Count();

            //create a progress bar
            using (var pbar = new ProgressBar(maxTicks, "Starting", ConsoleColor.DarkGreen))
            {
                //we use a "using" statement to make sure the file resource is released from
                //memory. A try..catch..finally can be used but a "using" statement is easier. 
                using (System.IO.StreamReader file = new System.IO.StreamReader(FileName))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        //update the progress bar
                        pbar.Tick("Currently processing " + counter);

                        //skip the first line
                        if (counter > 0)
                        {
                            //returnValue += line;
                            RosterItem myRosterItem = new RosterItem();

                            string[] myLineArray = line.Split(',');

                            myRosterItem.ID = Convert.ToInt32(myLineArray[0]);
                            myRosterItem.FirstName = myLineArray[1];
                            myRosterItem.LastName = myLineArray[2];
                            myRosterItem.Email = myLineArray[3];
                            myRosterItem.TeamColor = myLineArray[4];
                            myRosterItem.Gender = myLineArray[5];
                            myRosterItem.Position = myLineArray[6];

                            //now that we have a completed object
                            //add it to the returnvalue (Remember the special List<> gives us the ad method automatically) 
                            returnValue.Add(myRosterItem);

                            //artifically add a little time to the process
                            System.Threading.Thread.Sleep(250);
                        }

                        counter++;
                    }
                }
            }
            return returnValue;
        }

        //private static string AppendFile()
        //{

        //}

    }
}
