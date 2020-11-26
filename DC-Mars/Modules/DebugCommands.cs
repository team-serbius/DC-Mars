using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace DC_Mars.Modules
{
    public class DebugCommands : ModuleBase
    {
        [Command("health")]
        public async Task health()
        {
            using (Process myProcess = Process.GetCurrentProcess())
            {
                myProcess.Refresh();
                var builder = new EmbedBuilder()
                    .WithDescription("[DC-MARS] Health Check")
                    .WithColor(new Color(255, 0, 0))
                    .AddField("Memory Check (No GC): ", GC.GetTotalMemory(false).ToString(), true)
                    .AddField("Physical Memory Usage: ", myProcess.WorkingSet64.ToString(), true)
                    .AddField("User Processor Time: ", myProcess.UserProcessorTime.ToString(), true)
                    .AddField("Priv. Process. Time: ", myProcess.PrivilegedProcessorTime.ToString(), true)
                    .AddField("Total Process. Time: ", myProcess.TotalProcessorTime.ToString(), true)
                    .AddField("Paged Sysmem.  Size: ", myProcess.PagedSystemMemorySize64.ToString(), true)
                    .AddField("Paged T.Memory Size: ", myProcess.PagedMemorySize64.ToString(), true)
                    .WithCurrentTimestamp();

                var embed = builder.Build();
                await Context.Channel.SendMessageAsync("", false, embed);
            }
        }

        [Command("pop")]
        public async Task pop()
        {
            await Context.Channel.SendMessageAsync("frosh dummy.");
        }
    }
}