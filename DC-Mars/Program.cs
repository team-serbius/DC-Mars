using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DC_Mars
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            // Create new client for connecting to the Discord Gateway
            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler;

            // Add an Event to our Log, so we can log events and actions that happen.
            _client.Log += Log;

            // Discord Bot Token
            var token = "NzY5ODQ3MjUyMzczNjAyMzMy.X5U-IA.q06A94q3MTlEJ71K8TGWitwML8s";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task CommandHandler(SocketMessage message)
        {
            string command = "";
            int lengthOfCommand = -1;

            if (!message.Content.StartsWith('!')) //Bot command prefix
                return Task.CompletedTask;

            if (message.Author.IsBot) //Ignore all commands from other bots
                return Task.CompletedTask;

            if (message.Content.Contains(' '))
                lengthOfCommand = message.Content.IndexOf(' ');
            else
                lengthOfCommand = message.Content.Length;

            command = message.Content.Substring(1, lengthOfCommand - 1).ToLower();

            if (command.Equals("hello"))
                message.Channel.SendMessageAsync($@"Hello {message.Author.Mention}");

            return Task.CompletedTask;
        }
    }
}