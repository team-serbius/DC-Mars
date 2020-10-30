using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;
using DC_Mars.Debug;
using DC_Mars.Core;
using Discord.Commands;

/* ================================================================================
 *  DISCORD MARS/ROVER PROJECT             COPYRIGHT 2020 VEKTOR TECHNOLOGIES EIRL
 *  THIS IS NOT OPEN SOURCE SOFTWARE AND IS EXCLUSIVE RESPONSIBILITY OF IT'S OWNER.
 *  UNAUTHORIZED ACCESS, REDISTRIBUTION, VIEWING AND/OR MODIFICATION OF THIS CODE,
 *  INCLUDING BUT NOT LIMITED TO REVERSE ENGINEERING, SCREENSHOTS, EXECUTION,
 *  AND/OR DECOMPILATION CAN AND WILL BE PUNISHED TO THE FULL EXTENT OF THE LAW.
 *                  VEKTOR TECHNOLOGIES E.I.R.L. - ALL RIGHTS RESERVED.
 * ================================================================================
 */

namespace DC_Mars
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private Logging logger = new Logging();

        public async Task MainAsync()
        {
            Console.Title = "DC-MARS";
            // Create new client for connecting to the Discord Gateway
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            // Send log events to our Logging class
            _client.Log += logger.LogSys;

            // Discord Bot Token
            var token = Settings.Connection.Default.token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            logger.LogCustom("[DEBUG] Initialization starts", 0);
            await Initialize();
            logger.LogCustom("[DEBUG] Initialization ends", 0);

            await Task.Delay(-1);
        }

        private async Task Initialize()
        {
            CommandHandler CommandSystem = new CommandHandler(_client, _commands);
            await logger.LogCustom("[DEBUG] CommandHandler Created", 0);
            await CommandSystem.InstallCommandsAsync();
        }
    }
}