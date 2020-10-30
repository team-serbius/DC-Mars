using System;
using System.Threading;
using Serilog;
using System.Threading.Tasks;
using Discord;
using Serilog.Core;
using System.Security.Cryptography.X509Certificates;

namespace DC_Mars.Debug
{
    internal class Logging
    {
        private Logger logs = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.Console().CreateLogger();

        // Logs messages from Discord.NET
        public Task LogSys(LogMessage msg)
        {
            switch (msg.Severity)
            {
                case LogSeverity.Info:
                    logs.Information(msg.ToString());
                    break;

                case LogSeverity.Critical:
                    logs.Fatal(msg.ToString());
                    break;

                case LogSeverity.Debug:
                    logs.Debug(msg.ToString());
                    break;
            }
            return Task.CompletedTask;
        }

        // Log custom messages
        public async Task LogCustom(string logMessage, int severity)
        {
            switch (severity)
            {
                //Make a log wall to prevent immigrant bugs from sneaking into the Program
                //And obviously make them pay for it.
                //Debug
                case 0:
                    logs.Debug(logMessage);
                    //LogToFile(logMessage);
                    break;
                //Fatal
                case 1:
                    logs.Fatal(logMessage);
                    Environment.Exit(1);
                    //LogToFile(logMessage);
                    break;
                //Info
                case 2:
                    logs.Information(logMessage);
                    //LogToFile(logMessage);
                    break;
                //Error
                case 3:
                    logs.Error(logMessage);
                    //LogToFile(logMessage);
                    break;
            }
        }

        public Task LogToFile(string logMessage)
        {
            // TODO: Implement logging to file.
            return Task.CompletedTask;
        }
    }
}