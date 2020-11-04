using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DC_Mars.Modules
{
    public class GeneralCommands : ModuleBase
    {
        [Command("ping")]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync("pong!");
        }

        [Command("embedtest")]
        public async Task embedtest()
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
                .WithDescription("Embed message test")
                .WithColor(new Color(33, 176, 252))
                .AddField("User ID", Context.User.Id, true)
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync("embed test", false, embed);
        }
    }
}