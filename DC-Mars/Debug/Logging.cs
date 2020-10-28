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
        private Logger logs = new LoggerConfiguration().WriteTo.Console().CreateLogger();

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
        public Task LogCustom(string logMessage, string severity)
        {
            switch (severity.ToLower())
            {
                case "debug":
                    logs.Debug(logMessage);
                    break;

                case "fatal":
                    logs.Fatal(logMessage);
                    break;

                case "info":
                    logs.Information(logMessage);
                    break;

                case "error":
                    logs.Error(logMessage);
                    break;
            }
            return Task.CompletedTask;
        }
    }
}