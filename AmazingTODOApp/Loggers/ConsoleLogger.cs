using System;

namespace AmazingTODOApp.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger(LoggerLevel levelThreshold)
        {
            LevelThreshold = levelThreshold;
        }

        public void Log(string message, LoggerLevel level)
        {
            if (level >= LevelThreshold)
            {
                Console.WriteLine(message);
            }
        }

        public LoggerLevel LevelThreshold { get; }
    }
}