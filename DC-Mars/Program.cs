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
        private static async Task Main(string[] args) => await Startup.RunAsync(args);

        private Logging logger = new Logging();
    }
}