
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week4StreamAndData
{
    


    [DelimitedRecord(",")]
    public class RosterItem
    {
        //Why do we use enums?
        //enums are used to limit the value for a property
        //If we change the type of our Gender and Position
        //properties to their coresponding enum instead of
        //a string then anyone using our class MUST choose
        //one of the values in the corresponding enum
        public enum Genders
        {
            Female,
            Male
        }

        public enum Positions
        {
            Coach,
            TeamCaptain,

        }


        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TeamColor { get; set; }
        public string Gender { get; set; } //could be chamged from string to Genders
        public string Position { get; set; } //could be changed from string to Positions
    }
}
