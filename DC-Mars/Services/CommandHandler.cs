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
using Serilog.Data;
using Microsoft.Extensions.Configuration;

namespace DC_Mars.Core
{
    public class CommandHandler
    {
        public static IServiceProvider _provider;
        public static DiscordSocketClient _discord;
        public static CommandService _commands;
        public static IConfigurationRoot _config;

        public Logging logger = new Logging();
        public bool isReconnection = false;

        public CommandHandler(DiscordSocketClient discord, CommandService commands, IConfigurationRoot config, IServiceProvider provider)
        {
            _provider = provider;
            _config = config;
            _discord = discord;
            _commands = commands;

            _discord.Ready += OnReady;
            _discord.Connected += OnConnection;
            _discord.MessageReceived += OnMessageReceived;
        }

        private async Task OnMessageReceived(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;

            if (msg.Author.IsBot) return;
            var context = new SocketCommandContext(_discord, msg);

            int pos = 0;
            if (msg.HasStringPrefix(_config["prefix"], ref pos) || msg.HasMentionPrefix(_discord.CurrentUser, ref pos))
            {
                var result = await _commands.ExecuteAsync(context, pos, _provider);

                if (!result.IsSuccess)
                {
                    await logger.LogCustom($"Error: {result.Error}\nReason: {result.ErrorReason}", 2);
                    await context.Channel.SendMessageAsync($"**Error**: {result.Error}\n**Reason**: {result.ErrorReason}");
                }
            }
        }

        private async Task OnReady()
        {
            await logger.LogCustom($"[DEBUG] Client initialized as {_discord.CurrentUser.Username}#{_discord.CurrentUser.Discriminator}", 0);
        }

        private async Task OnConnection()
        {
            if (!isReconnection)
            {
                await logger.LogCustom($"[DEBUG] Client connected to the Gateway.", 0);
            }
            else
            {
                await logger.LogCustom($"[DEBUG] Client has reconnected to the gateway successfully.", 0);
            }
        }

        private async void OnDisconnection()
        {
            await logger.LogCustom($"[DEBUG] Client disconnected from the Gateway.", 0);
        }
    }
}