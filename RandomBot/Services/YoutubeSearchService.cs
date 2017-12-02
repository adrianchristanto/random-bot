using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeSearch;

namespace RandomBot.Services
{
    public class YoutubeSearchService
    {
        public YoutubeSearchService(InteractiveService interactive)
        {
            this.Interactive = interactive;
        }
        private readonly InteractiveService Interactive;

        [Summary("Get list of videos from youtube.com")]
        public async Task YoutubeSearch(SocketCommandContext Context, [Remainder]string search)
        {
            var items = new VideoSearch();
            var videoList = items.SearchQuery(search, 1).Take(5).ToList();
            if (videoList.Count < 1)
            {
                await Context.Channel.SendMessageAsync("Video not found..");
            }
            else
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
                    await Context.Channel.SendMessageAsync(message.ToString())
                };

                var number = 0;
                var timeOutTime = (long)10000;
                var stopwatch = new Stopwatch();
                while (true)
                {
                    var answer = await this.Interactive.NextMessageAsync(Context, true, true, TimeSpan.FromMilliseconds(timeOutTime));
                    if (answer == null)
                    {
                        await Context.Channel.DeleteMessagesAsync(videoListMessage);
                        await Context.Channel.SendMessageAsync($"Answer timed out {Context.User.Mention} <:akariShrug:370858391297589250>");
                        return;
                    }
                    if (int.TryParse(answer.Content, out number) == false)
                    {
                        videoListMessage.Add(answer);
                    }
                    if (number > 0 && number < 6)
                    {
                        videoListMessage.Add(answer);
                        await Context.Channel.DeleteMessagesAsync(videoListMessage);
                        await Context.Channel.SendMessageAsync(videoList[number - 1].Url);
                        return;
                    }
                    timeOutTime -= stopwatch.ElapsedMilliseconds;
                }
            }
        }
    }
}
