using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Reflection;
using Serilog.Core;
using DC_Mars.Debug;

namespace DC_Mars.Core
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly Logging logger = new Logging();

        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _commands = commands;
            _client = client;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await logger.LogCustom("[DEBUG] MessageReceived Event Handler added", 0);
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
            await logger.LogCustom("[DEBUG] CommandHandler Modules Installed", 0);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            //Don't process as sysmsg
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if (!(message.HasCharPrefix('!', ref argPos) ||
                  message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                  message.Author.IsBot)
                return;

            // Create new CommandContext that we can pass to the command processor
            var context = new SocketCommandContext(_client, message);
            await _commands.ExecuteAsync(context: context, argPos: argPos, services: null);
        }
    }
}