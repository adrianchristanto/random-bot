using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.InfoModule
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("dev", RunMode = RunMode.Async)]
        [Summary("My dev status")]
        public async Task DevelopmentStatus()
        {
            var embed = new EmbedBuilder()
                .WithTitle("Development Status")
                .AddField("Build 0.4.1", @"
Added $bn command
Added $cu command
Modified $d response message
")
                .AddField("Build 0.4.0", @"
Added $hoc command
Added $whale command
Added $scam command
Added $damedane command
Improved database access related task")
                .AddField("Build 0.3.7", @"
Added $Dorime command
Updated Entities")
                .AddField("Build 0.3.6", @"
Added $MusicList command
Changes on reminder module to improve code readability
Message for $delete command changed
Fixed message on $timeconvert command
Improved code readability on bot startup")
                .WithColor(Discord.Color.DarkRed);
            await ReplyAsync("", embed: embed.Build());
        }

        [Command("who", RunMode = RunMode.Async)]
        [Summary("My purpose")]
        [Alias("what")]
        public async Task MyPurpose()
        {
            var kappa = await Context.Channel.GetUserAsync(318035086375387136);
            await ReplyAsync(@"
I'm this server's personal bot made by " + kappa.Mention + @" <:daishouriKaren:427834555672690698>
Call him if you want to give some feedback
");
        }

        [Command("rigged", RunMode = RunMode.Async)]
        [Summary("Rigged gamble")]
        public async Task Rigged()
        {
            var kappa = await Context.Channel.GetUserAsync(318035086375387136);
            await ReplyAsync($"{ kappa.Mention } always wins!");
        }

        [Command("changelog", RunMode = RunMode.Async)]
        [Summary("shrugs")]
        [Alias("cl")]
        public async Task ChangeLog()
        {
            await Context.Channel.SendMessageAsync(@"
https://github.com/adrianchristanto/random-bot/blob/main/CHANGELOG.md 
Why are you looking for this?", true);
        }

        [Command("upcoming", RunMode = RunMode.Async)]
        [Summary("List of upcoming features")]
        public async Task Upcoming()
        {
            var embed = new EmbedBuilder()
                .WithAuthor("List of feature not yet implemented or in consideration")
                .AddField("Memes", "Moar memes?")
                .WithColor(Discord.Color.DarkRed);
            await ReplyAsync("", embed: embed.Build());
        }
    }
}
