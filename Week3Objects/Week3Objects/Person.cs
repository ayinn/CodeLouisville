using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3Objects
{
    public class Person : IPerson
    {
        public string FirstName { get; set; } = "George";
        public string LastName { get; set; }
        public virtual string GetFullName()
        {

            //return FirstName + " " + LastName;

            //return string.Format("{0} {1}", FirstName, LastName);

            return $"{FirstName} {LastName}";
        }
    }

    public class Employee : Person
    {
        public decimal Salary { get; set; }

        //TODO override GetFullname
        public override string GetFullName()
        {
            return LastName +", " + FirstName;
        }
    }

    //compistion vs inheritence IS A vs. HAS A
    public class Engine
    { 
        // ...
    }  

    public class Car : Engine
    {
        // ...
    }


    //Encapsulation
    //Private — only visible to the containing class. 
    //Protected — only visible to the containing class and inheritors.
    //Internal — only visible to classes in the same assembly.
    //protected internal — only visible to the containing class and inheritors 
    //      in the same assembly.
    //Public — visible to everyone


    //TODO Create a Person Interface

    //TODO Implement a person interface

    //TODO Show 3 slash comments

    public class SuperPerson : IPerson
    {
        public string FirstName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string LastName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string GetFullName()
        {
            throw new NotImplementedException();
        }
    }

}

