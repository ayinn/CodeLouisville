using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingWithLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            //some Linq Examples
            //first we'll need and object list to run queries on
            //well go back to last weeks roster item list
            //using the filehelpers library (from NuGet) we'll load our CSV file
            //into a list

            //Convert a CSV file to an object list
            //using FileHelpers (downloaded via nuget)
            var engine = new FileHelperEngine<RosterItem>();

            //we don't want to read the header of the file
            engine.Options.IgnoreFirstLines = 1;

            //create the list of roster items
            List<RosterItem> myRosterItemList = engine.ReadFile("Roster.csv").ToList();

            //now that we have a list it's time to see what we can find
            //in the list using Linq
            Console.WriteLine("");
            Console.WriteLine("*** Now let's use linq to find a player from the list by id ***");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            //a var to store the player id
            int playerId = 0;

            //ask for the player id to find
            Console.WriteLine("Enter a player ID:");

            //get the user's answer
            string answer = Console.ReadLine();

            //make sure the answer is a number
            int.TryParse(answer, out playerId);

            //if the user answered with a number see if we can find it
            if (playerId > 0)
            {
                //find a player by their id -- USING FIND
                RosterItem playerById = myRosterItemList.Find(x => x.ID == playerId);

                //make sure we have an object before we try to use it
                if (playerById != null)
                {
                    Console.WriteLine($"ID: {playerById.ID} FirstName: {playerById.FirstName} LastName: {playerById.LastName}");
                }
                else
                {
                    //if the object is null we did not find a match
                    Console.WriteLine("No matching ID.");
                }
                
            }
            else //user did not answer with a number
            {
                Console.WriteLine("Error: Could not parse your answer into a number.");
            }

            Console.WriteLine("");
            Console.WriteLine("*** Now let's find all players by team color ***");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            // ask for the player id to find
             Console.WriteLine("Enter a Team Color:");

            //get the user's answer
            string teamColor = Console.ReadLine();

            //find all members of the team by color
            //find all players on a team -- USING FINDALL
            List<RosterItem> playersByColor = myRosterItemList.FindAll(x => x.TeamColor == teamColor);

            //print the header
            Console.WriteLine("First Name   Last Name    Position     Team Color");
            Console.WriteLine("-------------------------------------------------");

            foreach (var player in playersByColor)
            {
                //print each player, their position and team color
                //notice the format option at the end of the variable
                //a negative number specifies left aligned with a minimum width
                //in this case we're asking for a minimum width of 13 characters
                //a postive number would right align the coloum
                Console.WriteLine($"{player.FirstName,-13}{player.LastName,-13}{player.Position,-13}{player.TeamColor,-13}");
            }
            
            Console.WriteLine("");
            Console.WriteLine("*** Now report player count by team color ***");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            //find all members of the team by color
            //find all players on a team -- USING GROUPBY
            //this uses the linq Ienumerable pattern
            var numberOfPlayerByTeamColor = from p in myRosterItemList
                                            group p.TeamColor by p.TeamColor into g
                                            select new { TeamColor = g.Key, Count = g.Count() }; //creates a new list
            //print the header
            Console.WriteLine("Team Color   Number of Players");
            Console.WriteLine("-------------------------------------------------");

            //print the count of players for each team color
            foreach (var item in numberOfPlayerByTeamColor)
            //count of players by team color order by most number of players to least number of player
            //foreach (var item in numberOfPlayerByTeamColor.OrderByDescending(x => x.Count))
            {
                //print each team color and count
                Console.WriteLine($"{item.TeamColor,-13}{item.Count,-13}");
                
            }

            //find the average number of players by team color -- USING AVERAGE
            double averagePlayersPerTeam = myRosterItemList.Average(x => x.TeamColor.Count());

            Console.WriteLine(""); //print a blank line
            Console.WriteLine($"The average number of players by team is: {averagePlayersPerTeam}");
            Console.WriteLine(""); //print a blank line

            Console.WriteLine("*** Now let's list the top 3 teams with the most number of players ***");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            //print the header
            Console.WriteLine("Team Color   Number of Players");
            Console.WriteLine("------------------------------");

            //using the same list we've already created, get the top 3 items --USING TAKE
            foreach (var item in numberOfPlayerByTeamColor.OrderByDescending(x => x.Count).Take(3))
            {
                Console.WriteLine($"{item.TeamColor,-13}{item.Count,-13}");
            }

            //more linq examples on stack overflow
            //http://stackoverflow.com/documentation/linq/topics
            //also see: http://linqsamples.com/

            //stop the program
            Console.ReadLine();

        }
    }
}
