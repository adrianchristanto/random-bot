using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YoutubeSearch;

namespace RandomBot
{
    public class RandomBot : ModuleBase
    {
        Random rand;
        private const string searchEngineId = "012877858684203379423:w7pgomqne_y";
        private const string apiKey = "AIzaSyCVPqDhznQp0wi7gOV3HdtOCH6y7YlgHy8";

        [Command("help", RunMode = RunMode.Async)]
        [Summary("Shows command list")]
        [Alias("h")]
        public async Task Help()
        {
            var messagesToDelete = await Context.Channel.GetMessageAsync(Context.Message.Id);
            var builder = new EmbedBuilder()
                .WithTitle("RandomBot Command List:")
                .AddField("$hello or $hi", "Say hi to someone")
                .AddField("$markdown or $m", "Edit your message")
                .AddField("$northstar or $kenshiro", "Omae wa mou shindeiru")
                .AddField("$uralreadydead", "NANI!?")
                .AddField("$fight or $f", "Be a man! Fight someone!")
                .AddField("$roll or $r", "Mini gamble. Do it!")
                .AddField("$dab", "Because I hate myself")
                .AddField("$yes or $no", "Your choice")
                .AddField("$delete or $d", "Delete message (max 99)")
                .AddField("$search or $s", "Search videos from youtube (only 5 top videos for now)")
                .AddField("$dev", "This shows my development status")
                .AddField("$who or $what", "Who am I? What is my purpose?")
                .AddField("$rigged", "Is it rigged?")
                .WithColor(Discord.Color.DarkRed);

            await ReplyAsync("", false, builder);
            await messagesToDelete.DeleteAsync();
        }

        [Command("hello", RunMode = RunMode.Async)]
        [Summary("Says basic hello")]
        [Alias("hi")]
        public async Task Hello()
        {
            await ReplyAsync("Hello " + Context.Message.Author.Mention + " <:AkarinWave:370855004921135104>");
        }
        [Command("hello", RunMode = RunMode.Async)]
        [Summary("Says basic hello")]
        [Alias("hi")]
        public async Task Hello(IUser user)
        {
            var messagesToDelete = await Context.Channel.GetMessageAsync(Context.Message.Id);
            if (Context.User.Id == user.Id)
            {
                await this.Hello();
            }
            else
            {
                await ReplyAsync(Context.User.Mention + " says hello to you, " + user.Mention + " <:AkarinWave:370855004921135104>");
            }
            await messagesToDelete.DeleteAsync();
        }

        [Command("markdown", RunMode = RunMode.Async)]
        [Summary("Change nickname")]
        [Alias("m")]
        public async Task Markdown()
        {
            var builder = new EmbedBuilder()
                .WithAuthor("Type List:")
                .AddField("italics or i", "*italics*")
                .AddField("bold or b", "**bold**")
                .AddField("bolditalics or bi", "***bold italics***")
                .AddField("underline or u", "__underline__")
                .AddField("underlineitalics or ui", "__*underline italics*__")
                .AddField("underlinebold or ub", "__**underline bold**__")
                .AddField("underlinebolditalics or ubi", "__***underline bold italics***__")
                .AddField("strikethrough or s", "~~strikethrough~~")
                .AddField("Example:", "$m i Huehuehue")
                .WithColor(Discord.Color.DarkRed);
            await ReplyAsync("", false, builder);
        }
        [Command("markdown", RunMode = RunMode.Async)]
        [Summary("Change nickname")]
        [Alias("m")]
        public async Task Markdown(string type, [Remainder]string message)
        {
            type = type.ToLower();
            var messageToSend = "";
            var messageContext = Context.Message;
            if (type == "italics" || type == "i") messageToSend = Context.User.Mention + ": *" + message + "*";
            else if (type == "bold" || type == "b") messageToSend = Context.User.Mention + ": **" + message + "**";
            else if (type == "bolditalics" || type == "bi") messageToSend = Context.User.Mention + ": ***" + message + "***";
            else if (type == "underline" || type == "u") messageToSend = Context.User.Mention + ": __" + message + "__";
            else if (type == "underlineitalics" || type == "ui") messageToSend = Context.User.Mention + ": _*" + message + "*_";
            else if (type == "underlinebold" || type == "ub") messageToSend = Context.User.Mention + ": __**" + message + "**__";
            else if (type == "underlinebolditalics" || type == "ubi") messageToSend = Context.User.Mention + ": __***" + message + "***__";
            else if (type == "strikethrough" || type == "s") messageToSend = Context.User.Mention + ": ~~" + message + "~~";
            else if (type == "codeblocks" || type == "c") messageToSend = Context.User.Mention + ": `" + message + "`";
            else messageToSend = Context.User.Mention + " is talking gibberish";
            await messageContext.DeleteAsync();
            await ReplyAsync(messageToSend);
        }

        [Command("nick", RunMode = RunMode.Async)]
        [Summary("Change nickname")]
        [Alias("n")]
        public async Task ChangeNickName([Remainder]string newNickName)
        {
            var server = Context.Guild;
            var user = await server.GetUserAsync(Context.User.Id);
            var oldNickname = user.Nickname ?? user.Username;
            await user.ModifyAsync(Q =>
            {
                Q.Nickname = newNickName;
            }).ConfigureAwait(false);
            await ReplyAsync(user.Mention + " ganti nickname dari " + oldNickname);
        }

        [Command("northstar", RunMode = RunMode.Async)]
        [Summary("Omae wa mou shindeiru")]
        [Alias("kenshiro")]
        public async Task NorthStar(IUser user)
        {
            var channel = Context.Channel;
            await ReplyAsync(user.Mention + " is already dead");
            await channel.SendFileAsync(@"Image\youAreAlreadyDead.jpg");
        }

        [Command("uralreadydead", RunMode = RunMode.Async)]
        [Summary("Omae wa mou shindeiru")]
        public async Task UrAlreadyDead()
        {
            await ReplyAsync("NANI!? <:monkaS:370873758744969216>");
        }

        [Command("fight", RunMode = RunMode.Async)]
        [Summary("Fight someone by mentioning him/her")]
        [Alias("f")]
        public async Task Fight(IUser user)
        {
            if (Context.User.Id == user.Id)
            {
                await ReplyAsync("Fighting by yourself.... sad");
            }
            else
            {
                await this.GetAvatarFromUrl(Context.User);
                this.ManipulateImage("Fight.jpg", Context.User.AvatarId, 327, 237, "tempFight1.jpg");
                await this.GetAvatarFromUrl(user);
                this.ManipulateImage(@"ToUpload\tempFight1.jpg", user.AvatarId, 622, 213, "Fight.jpg");
                await Context.Channel.SendFileAsync(@"Image\ToUpload\Fight.jpg");
                File.Delete(@"Image\ToUpload\tempFight1.jpg");
                File.Delete(@"Image\ToUpload\Fight.jpg");

                rand = new Random();
                var result = rand.Next(1, 101);
                if (result >= 1 && result <= 40) await ReplyAsync(Context.User.Mention + " win!");
                if (result >= 41 && result <= 60) await ReplyAsync("It's a draw!");
                if (result >= 61 && result <= 100) await ReplyAsync(user.Mention + " win!");
            }
        }
        [Command("fight", RunMode = RunMode.Async)]
        [Summary("Fight someone by mentioning him/her")]
        [Alias("f")]
        public async Task Fight(IUser user1, IUser user2)
        {
            if (user1.Id == user2.Id)
            {
                await ReplyAsync("Fighting by yourself.... sad");
            }
            else
            {
                await this.GetAvatarFromUrl(user1);
                this.ManipulateImage("Fight.jpg", user1.AvatarId, 327, 237, "tempFight1.jpg");
                await this.GetAvatarFromUrl(user2);
                this.ManipulateImage(@"ToUpload\tempFight1.jpg", user2.AvatarId, 622, 213, "Fight.jpg");
                await Context.Channel.SendFileAsync(@"Image\ToUpload\Fight.jpg");
                File.Delete(@"Image\ToUpload\tempFight1.jpg");
                File.Delete(@"Image\ToUpload\Fight.jpg");

                rand = new Random();
                var result = rand.Next(1, 101);
                if (result >= 1 && result <= 40) await ReplyAsync(user1.Mention + " win!");
                if (result >= 41 && result <= 60) await ReplyAsync("It's a draw!");
                if (result >= 61 && result <= 100) await ReplyAsync(user2.Mention + " win!");
            }
        }

        [Command("roll")]
        [Summary("Roll from 1 - 100")]
        [Alias("r")]
        public async Task Roll()
        {
            rand = new Random();
            var result = 0;
            if (Context.User.Id == 318035086375387136) result = rand.Next(80, 101);
            else result = rand.Next(1, 101);
            await ReplyAsync(Context.User.Mention + " rolled " + result.ToString());
        }

        [Command("dab", RunMode = RunMode.Async)]
        [Summary("Dab image")]
        public async Task Dab()
        {
            await this.GetAvatarFromUrl(Context.User);
            this.ManipulateImage("Dab.png", Context.User.AvatarId, 163, 145, "Dab.png");
            await Context.Channel.SendFileAsync(@"Image\ToUpload\Dab.png");
            File.Delete(@"Image\ToUpload\Dab.png");
        }

        [Command("yes", RunMode = RunMode.Async)]
        [Summary("Say yes with image")]
        public async Task YesImage()
        {
            await this.GetAvatarFromUrl(Context.User);
            this.ManipulateImage("Yes.png", Context.User.AvatarId, 10, 120, "Yes.png");
            await Context.Channel.SendFileAsync(@"Image\ToUpload\Yes.png");
            File.Delete(@"Image\ToUpload\Yes.png");
        }

        [Command("no", RunMode = RunMode.Async)]
        [Summary("Say no with image")]
        public async Task NoImage()
        {
            await this.GetAvatarFromUrl(Context.User);
            this.ManipulateImage("No.png", Context.User.AvatarId, 175, 133, "No.png");
            await Context.Channel.SendFileAsync(@"Image\ToUpload\No.png");
            File.Delete(@"Image\ToUpload\No.png");
        }

        [Command("delete", RunMode = RunMode.Async)]
        [Summary("Deletes set amount of text")]
        [Alias("d")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task DeleteMessages([Remainder] int num = 0)
        {
            if (num < 100)
            {
                var messagesToDelete = await Context.Channel.GetMessagesAsync(num + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(messagesToDelete);

                var messageReply = await ReplyAsync(Context.User.Mention + " deleted " + num + " message(s). This message will be deleted in 3 seconds");
                await Task.Delay(1000);
                for (var i = 2; i > 0; i--)
                {
                    await messageReply.ModifyAsync(x => x.Content = Context.User.Mention + " deleted " + num + " message(s). This message will be deleted in " + i + " seconds");
                    await Task.Delay(1000);
                }
                await messageReply.DeleteAsync();
            }
        }

        [Command("search", RunMode = RunMode.Async)]
        [Summary("Search video from youtube")]
        [Alias("s")]
        public async Task VideoSearch([Remainder]string search)
        {
            var context = Context;
            var items = new VideoSearch();
            var videoList = items.SearchQuery(search, 1).Take(5).ToList();
            if (videoList.Count < 1)
            {
                await ReplyAsync("Video not found..");
            }
            else
            {
                await ReplyAndWaitResponse(videoList, context);
            }
        }

        [Command("dev", RunMode = RunMode.Async)]
        [Summary("My dev status")]
        [Alias("d")]
        public async Task DevelopmentStatus()
        {
            var kappa = await Context.Channel.GetUserAsync(318035086375387136);
            await ReplyAsync(@"
You could say I'm build version 0.0.2, don't ask why only " + kappa.Mention + @" knows <:pepesmug:370857798415941633>
I'm more stable now <:thumbsup:370864237616168960>
But more error is to be expected <:hifumiSpook:370854277767364608>
");
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
            await ReplyAsync("<:akariShrug:370858391297589250>");
        }

        [Summary("Get avatar image")]
        private async Task GetAvatarFromUrl(IUser user)
        {
            var fileName = user.AvatarId + ".png";
            if (File.Exists(@"Image\AvatarTemplate\" + fileName) == false)
            {
                var userImage = new Uri(user.GetAvatarUrl());
                using (var client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(userImage, @"Image\AvatarTemplate\" + fileName);
                }
            }
        }

        [Summary("Manipulate image")]
        private void ManipulateImage(string directory, string avatarId, int xCoor, int yCoor, string saveFile)
        {
            using (var image = System.Drawing.Image.FromFile(@"Image\" + directory))
            using (var avatarImage = System.Drawing.Image.FromFile(@"Image\AvatarTemplate\" + avatarId + ".png"))
            using (var imageGraphics = Graphics.FromImage(image))
            using (var avatarBrush = new TextureBrush(avatarImage))
            {
                var x = xCoor;
                var y = yCoor;
                avatarBrush.TranslateTransform(x, y);
                imageGraphics.FillRectangle(avatarBrush, new Rectangle(new Point(x, y), new Size(avatarImage.Width + 1, avatarImage.Height)));
                image.Save(@"Image\ToUpload\" + saveFile);
            }
        }

        [Summary("Send video list and wait for user response")]
        private async Task ReplyAndWaitResponse(List<VideoInformation> videoList, ICommandContext context)
        {
            var message = new StringBuilder();
            message.Append("```css" + Environment.NewLine);
            for (var i = 0; i < 5; i++)
            {
                message.Append((i + 1) + ". " + videoList[i].Title + Environment.NewLine);
            }
            message.Append("```");
            var videoListMessage = new List<IMessage>
            {
                await ReplyAsync(message.ToString())
            };
            var isChoosen = false;
            while (isChoosen == false)
            {
                var answerEnum = await context.Channel.GetMessagesAsync(1).Flatten();
                var answer = answerEnum.FirstOrDefault();
                if (answer != null)
                {
                    if (context.User.Id == answer.Author.Id)
                    {
                        int.TryParse(answer.Content, out var number);
                        if (number > 0 && number < 6)
                        {
                            videoListMessage.Add(answer);
                            await ReplyAsync(videoList[number - 1].Url);
                            await context.Channel.DeleteMessagesAsync(videoListMessage);
                            isChoosen = true;
                        }
                    }
                }
            }
        }
    }
}
