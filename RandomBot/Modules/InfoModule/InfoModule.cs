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
                .AddField("Build 0.3.4", @"
Add text edit on image feature
Add Freedom Module
Add precise search feature for GunfuService.cs
Improved flexibility on adding Reminder")
                .AddField("Build 0.3.3", @"
Added Voice Channel module
Added Doll module
Added Kokoro module")
                .AddField("Build 0.3.2", @"
Removed any gif related command (use the one discord provide instead)
Removed $markdown command
Small changes in Reminder command
A few command rigged")
                .AddField("More?", @"Details on https://gitlab.com/adrianch/RandomBot/blob/master/CHANGELOG.md")
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
https://gitlab.com/adrianch/RandomBot/blob/master/CHANGELOG.md 
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
