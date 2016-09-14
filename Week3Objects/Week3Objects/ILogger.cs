using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3Objects
{
    public interface ILogger
    {
        void LogError(string error);
        void LogInfo(string info);
    }

    //log classes
    public class ConsoleLogger : ILogger
    {
        public void LogError(string error)
        {
            Console.WriteLine("Error: " + error);
        }

        public void LogInfo(string info)
        {
            Console.WriteLine("Info: " + info);
        }
    }

    public class WindowsEventLogLogger : ILogger
    {
        public void LogError(string error)
        {
            Console.WriteLine("Logging error to Windows Event log: " + error);
        }
        public void LogInfo(string info)
        {
            Console.WriteLine("Logging info to Windows Event log: " + info);
        }
    }

    public class DatabaseLogger : ILogger
    {
        public void LogError(string error)
        {
            Console.WriteLine("Logging error to database: " + error);
        }
        public void LogInfo(string info)
        {
            Console.WriteLine("Logging info to database: " + info);
        }
    }



}
