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
                .AddField("Build 0.3.1", @"
Added showallremind module
User now able to set reminder to daily with reminddaily command
Fix message when no reminder found")
                .AddField("Build 0.3.0", @"
Added time scheduler that run once every minute
Added remind module, show remind module, and remove remind module")
                .AddField("Build 0.2.7", @"Improved every image editting module")
                .AddField("More?", "Details on $changelog")
                .WithColor(Discord.Color.DarkRed);
            await ReplyAsync("", false, embed);
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
            await ReplyAsync("Expect some gamble rigged by " + kappa.Mention + "'s favor <:pepesmug:370857798415941633>");
        }

        [Command("changelog", RunMode = RunMode.Async)]
        [Summary("shrugs")]
        [Alias("cl")]
        public async Task ChangeLog()
        {
            await Context.Channel.SendFileAsync("CHANGELOG.txt", "Why are you looking for this?", true);
        }

        [Command("upcoming", RunMode = RunMode.Async)]
        [Summary("List of upcoming features")]
        public async Task Upcoming()
        {
            var embed = new EmbedBuilder()
                .WithAuthor("List of feature not yet implemented or in consideration")
                .AddField("Memes", "Moar memes?")
                .WithColor(Discord.Color.DarkRed);
            await ReplyAsync("", false, embed);
        }
    }
}
