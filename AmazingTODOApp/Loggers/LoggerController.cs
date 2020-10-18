namespace AmazingTODOApp.Loggers
{
    using System.Collections.Generic;
    
    public class LoggerController : ILogger
    {
        List<ILogger> Loggers { get; set; }

        public LoggerController(params ILogger[] loggers)
        {
            Loggers = new List<ILogger>(loggers);
        }

        public void Log(string mesasage, LoggerLevel level)
        {
            foreach (var logger in Loggers)
            {
                logger.Log(mesasage, level);
            }
        }
    }
}