﻿using System;
using PUBGSharp.Data;
using PUBGSharp.Exceptions;
using PUBGSharp.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
//using PUBGSharp.Examples;
using System.Threading.Tasks;
using PUBGSharp;

namespace Area_Net
{
    public partial class PUBG : System.Web.UI.Page
    {
        //private Program PubgStart;
        public string DuoKda { get; set; }
        protected async Task Page_LoadAsync(object sender, EventArgs e)
        {
            await useApiAsync();
            //PubgStart = new PUBGSharp.Examples.Program();
        }

        public async Task useApiAsync()
        {
            var statsClient = new PUBGStatsClient("3922a41f-afb0-42b0-b401-af64563159bb");
            var stats = await statsClient.GetPlayerStatsAsync("Shroud");
            try
            {
                var kdr = stats.Stats.Find(x => x.Mode == Mode.Duo && x.Region == Region.AGG && x.Season == Seasons.EASeason1).Stats.Find(x => x.Stat == Stats.KDR);
                Console.WriteLine($"Duo KDR: {kdr.Value}");
                DuoKda = kdr.Value;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine($"Could not retrieve stats for {stats.PlayerName}..");
            }
        }
    }
}
/*
namespace PUBGSharp.Examples
{
    class Program
    {
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            // Create client and send a stats request You can either use the "using" keyword or
            // dispose the PUBGStatsClient manually with the Dispose method.
            using (var statsClient = new PUBGStatsClient("3922a41f-afb0-42b0-b401-af64563159bb"))
            {
                var stats = await statsClient.GetPlayerStatsAsync("Shroud").ConfigureAwait(false);

                // Print out player name and date the stats were last updated at.
                Console.WriteLine($"{stats.PlayerName}, last updated at: {stats.LastUpdated}");

                try
                {
                    // Print out amount of players KDR (Stats.KDR) in DUO mode (Mode.Duo) in ALL
                    // regions (Region.AGG) in SEASON 1 (Seasons.EASeason1).
                    var kdr = stats.Stats.Find(x => x.Mode == Mode.Duo && x.Region == Region.AGG && x.Season == Seasons.EASeason1).Stats.Find(x => x.Stat == Stats.KDR).Value;
                    Console.WriteLine($"Duo KDR: {kdr}");
                    // Print out amount of headshots kills in SOLO mode in NA region in SEASON 2.
                    var headshotKills = stats.Stats.Find(x => x.Mode == Mode.Solo && x.Region == Region.NA && x.Season == Seasons.EASeason2).Stats.Find(x => x.Stat == Stats.HeadshotKills);
                    // You can also display the stats by using .ToString() on the stat object, e.g:
                    Console.WriteLine(headshotKills.ToString());

                    // Print out amount of kills in the last season player has played in:
                    var latestSeasonSoloStats = stats.Stats.FindLast(x => x.Mode == Mode.Solo);
                    var kills = latestSeasonSoloStats.Stats.Find(x => x.Stat == Stats.Kills);
                    Console.WriteLine($"Season: {latestSeasonSoloStats.Season}, kills: {kills.Value}");
                }
                */
                /* IMPORTANT STUFF ABOUT EXCEPTIONS:
                 The LINQ and other selector methods (e.g. .Find) will throw NullReferenceException in case the stats don't exist.
                 So if player has no stats in specified region or game mode, it will throw NullReferenceException.
                 For example, if you only have played in Europe and try to look up your stats in the Asia server, instead of showing 0's everywhere it throws this. */
                /*catch (PUBGSharpException ex)
                {
                    Console.WriteLine($"Could not retrieve stats for {stats.PlayerName}, error: {ex.Message}");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine($"Could not retrieve stats for {stats.PlayerName}.");
                    Console.WriteLine("The player might not exist or have stats in the specified mode or region.");
                }

*/
                /* Outputs:
                Mithrain, last updated at: 2017-09-07T19:53:40.3611629Z
                Duo KDR: 2.87
                Stat: Headshot Kills, value: 69, Rank: #
                Season: 2017-pre4, kills: 32
                */
                /*
            }

            await Task.Delay(-1);
        }
    }
}
*/
