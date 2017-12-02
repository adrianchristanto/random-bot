using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("dev", RunMode = RunMode.Async)]
        [Summary("My dev status")]
        [Alias("d")]
        public async Task DevelopmentStatus()
        {
            var embed = new EmbedBuilder()
                .WithTitle("Development Status")
                .AddField("Build 0.2.0", @"
Added Database
Added $shipfugacha command
Thanks to <@!114003530674733060> for filling the shipfu list")
                .AddField("Build 0.1.3", @"
Added $facepalm and $soon command
Changed emote for $uralreadydead command as the emote is no longer available")
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
I'm this server's personal bot made by " + kappa.Mention + @" <:tehepero:370925314538340362>
Call him if you want to give some feedback
");
        }

        [Command("rigged", RunMode = RunMode.Async)]
        [Summary("Rigged gamble")]
        public async Task Rigged()
        {
            var kappa = await Context.Channel.GetUserAsync(318035086375387136);
            await ReplyAsync("Expect some gamble rigged by " + kappa.Mention + "'s favor <:smug:370852548749426689>");
        }

        [Command("changelog", RunMode = RunMode.Async)]
        [Summary("shrugs")]
        [Alias("cl")]
        public async Task ChangeLog()
        {
            await Context.Channel.SendFileAsync("CHANGELOG.txt", "<:akariShrug:370858391297589250>");
        }

        [Command("upcoming", RunMode = RunMode.Async)]
        [Summary("List of upcoming features")]
        [Alias("cl")]
        public async Task Upcoming()
        {
            var embed = new EmbedBuilder()
                .WithAuthor("List of feature not yet implemented or in consideration")
                .AddField("Video", "Random youtube search, List pagination")
                .AddField("Memes", "Moar memes?")
                .WithColor(Discord.Color.DarkRed);
            await ReplyAsync("", false, embed);
        }
    }
}
