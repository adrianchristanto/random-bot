﻿using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileExternalModule
{
    public class InternetGifModule : ModuleBase<SocketCommandContext>
    {
        [Command("angery", RunMode = RunMode.Async)]
        public async Task Angery()
        {
            var builder = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://cdn.discordapp.com/attachments/370852077947060225/447238521137397810/68642058.gif");
            await ReplyAsync("", embed: builder);
        }

        [Command("bait", RunMode = RunMode.Async)]
        public async Task Bait()
        {
            var embed = new EmbedBuilder()
                .WithImageUrl("https://media2.giphy.com/media/zFK1drvvcGkAE/giphy.gif")
                .WithColor(Discord.Color.DarkRed);
            await ReplyAsync("", embed: embed);
        }

        [Command("dance", RunMode = RunMode.Async)]
        public async Task Dance()
        {
            var embed = new EmbedBuilder()
                .WithImageUrl("https://cdn.discordapp.com/attachments/370852077947060225/451276649326051328/Just-because.gif")
                .WithColor(Discord.Color.DarkRed);
            await ReplyAsync("", embed: embed);
        }

        [Command("dealwithit", RunMode = RunMode.Async)]
        [Summary("Dab image")]
        [Alias("deal")]
        public async Task DealWithIt()
        {
            var embed = new EmbedBuilder()
                .WithImageUrl("https://media.giphy.com/media/xULW8MVmflvOWrGve0/giphy.gif")
                .WithColor(Discord.Color.DarkRed);
            await ReplyAsync("", embed: embed);
        }

        [Command("gift", RunMode = RunMode.Async)]
        public async Task Gift()
        {
            var builder = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://cdn.discordapp.com/emojis/393624358066847754.gif");
            await ReplyAsync("", embed: builder);
        }

        [Command("hahaha", RunMode = RunMode.Async)]
        public async Task Hahaha()
        {
            var builder = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://media1.tenor.com/images/c1480d5e09ef758ddaea1545c8eaaf4f/tenor.gif");
            await ReplyAsync("", embed: builder);
        }

        [Command("hawawa", RunMode = RunMode.Async)]
        public async Task Hawawa()
        {
            var builder = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://media.giphy.com/media/1wrljJvRhCeT0SyGJd/giphy.gif");
            await ReplyAsync("", embed: builder);
        }

        [Command("repent", RunMode = RunMode.Async)]
        [Summary("Repent - Deviljho")]
        public async Task Repent()
        {
            var embed = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://media.giphy.com/media/1yTehCQbihxTQZkIvV/giphy.gif");
            await ReplyAsync("", embed: embed);
        }

        [Command("udidntseenothin", RunMode = RunMode.Async)]
        [Summary("Dab image")]
        [Alias("udidnt")]
        public async Task YouDidNotSeeNothing()
        {
            await ReplyAsync("https://www.youtube.com/watch?v=RN0ZxJwxCHs");
        }
    }
}
