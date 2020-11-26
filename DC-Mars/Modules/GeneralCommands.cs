using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Diagnostics;
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
            Stopwatch sw = Stopwatch.StartNew();
            await Context.Channel.SendMessageAsync("pong!");
            sw.Stop();
            await Context.Channel.SendMessageAsync($"[DEBUG] Response took: {sw.ElapsedMilliseconds}ms");
        }

        [Command("embedtest")]
        public async Task embedtest()
        {
            Stopwatch sw = Stopwatch.StartNew();
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
                .WithDescription("Embed Debug Message")
                .WithColor(new Color(33, 176, 252))
                .AddField("User ID", Context.User.Id, true)
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync($"[DEBUG] Embed took {sw.ElapsedMilliseconds}ms to build.", false, embed);
            sw.Stop();
            await Context.Channel.SendMessageAsync($"[DEBUG] Process took {sw.ElapsedMilliseconds}ms to complete.", false);
        }

        [Command("version")]
        public async Task version()
        {
            var builder = new EmbedBuilder()
                .WithDescription("Assembly:\n " + Assembly.GetExecutingAssembly().GetName().Name + " " + Assembly.GetExecutingAssembly().GetName().Version)
                .WithColor(new Color(0, 0, 0))
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("systest")]
        public async Task systest()
        {
            //Removed
        }
    }
}