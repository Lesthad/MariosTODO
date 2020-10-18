using System;
using System.IO;

namespace AmazingTODOApp.Loggers
{
    public class FileLogger : ILogger
    {
        public LoggerLevel LevelThreshold { get; }

        public FileLogger(LoggerLevel levelThreshold)
        {
            LevelThreshold = levelThreshold;
        }

        public void Log(string message, LoggerLevel level)
        {
            if (level <= LevelThreshold)
            {
                var formatedMessage = string.Format("{0:f} - {1}\n", DateTime.Now, message);
                File.AppendAllText("Log.txt", formatedMessage);
            }
        }

    }
}