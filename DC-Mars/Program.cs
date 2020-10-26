using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;
using DC_Mars.Debug;

namespace DC_Mars
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private Logging logger = new Logging();

        public async Task MainAsync()
        {
            Console.Title = "DC-MARS";
            // Create new client for connecting to the Discord Gateway
            _client = new DiscordSocketClient();
            // Send log events to our Logging class
            _client.Log += logger.LogSys;

            // Discord Bot Token
            var token = "NzY5ODQ3MjUyMzczNjAyMzMy.X5U-IA.q06A94q3MTlEJ71K8TGWitwML8s";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}