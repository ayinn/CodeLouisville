using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3Objects
{
    class Program
    {
        static void Main(string[] args)
        {
            //create a person object
            Person personObject = new Person();
            personObject.FirstName = "Han";
            personObject.LastName = "Solo";
            string fullName = personObject.GetFullName();

            //use the fullname method of person
            Console.WriteLine(fullName);

            //create another person object using the initializer style
            //create a person object
            Person person2Object = new Person() {
                FirstName = "Luke",
                LastName = "Skywalker"
            };
            string fullName2 = person2Object.GetFullName();

            //use the fullname method of second person
            Console.WriteLine(fullName2);


            //create an employee which is a subclass of person
            Employee employee = new Employee();
            employee.FirstName = "Darth";
            employee.LastName = "Vader";
            string fullEmployeeName = employee.GetFullName();
            employee.Salary = 1000000;

            //write the employee name
            Console.WriteLine(fullEmployeeName);

            //Console.WriteLine("The employee named {0} {1} has a salary of {2:c} a year", 
            //employee.FirstName, 
            //employee.LastName, 
            //employee.Salary);

            Console.WriteLine($"The employee named {employee.FirstName} {employee.LastName} has a salary of {employee.Salary:c0} a year");
            Console.ReadLine();


            //implementing interfaces

            //log messages to the console
            ConsoleLogger cLogger = new ConsoleLogger();
            cLogger.LogError("Some error occurred.");
            cLogger.LogInfo("All's well!");

            //log messages to the event log
            WindowsEventLogLogger wLogger = new WindowsEventLogLogger();
            wLogger.LogError("Some error occurred.");
            wLogger.LogInfo("All's well!");

            //log messages to the database
            DatabaseLogger dLogger = new DatabaseLogger();
            dLogger.LogError("Some error occurred.");
            dLogger.LogInfo("All's well!");



            List<ILogger> loggers = new List<ILogger>();
            loggers.Add(new ConsoleLogger());
            loggers.Add(new WindowsLogLogger());
            loggers.Add(new DatabaseLogger());  
            foreach (ILogger logger in loggers)
            {
                logger.LogError("Some error occurred.");
                logger.LogInfo("All's well!");
            }
            Console.ReadKey();
            
        

        }
    }
}
