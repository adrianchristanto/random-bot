using Discord;
using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.DiscordGifEmojiModule
{
    public class DiscordGifEmojiModule : ModuleBase<SocketCommandContext>
    {
        public DiscordGifEmojiModule(DiscordGifEmojiService gifEmojiService)
        {
            this.GifEmojiService = gifEmojiService;
        }
        private readonly DiscordGifEmojiService GifEmojiService;

        [Command("gifList", RunMode = RunMode.Async)]
        [Summary("Give list of gif")]
        [Alias("gif")]
        public async Task GifList()
        {
            var embed = new EmbedBuilder()
                .WithAuthor("Gif List:")
                .AddField(":2bright:", "$2bright or $2br")
                .AddField(":hypeDoge:", "$hypeDoge or $hdoge")
                .AddField(":Karen_Intensifies:", "$karenintensifies or $karen")
                .AddField(":pepeBang:", "$pepebang or $bang")
                .AddField(":pokopoko:", "$pokopoko or $poko")
                .AddField(":slurpyslurp:", "$slurpyslurp or $slurp")
                .AddField(":shimakazetablebang:", "$shimakazetablebang or $st")
                .AddField(":wao:", "$wao")
                .WithColor(Color.DarkRed);
            await ReplyAsync("", embed: embed.Build());
        }

        [Command("2bright", RunMode = RunMode.Async)]
        [Summary("2bright.gif")]
        [Alias("2br")]
        public async Task Bright(int count = 1)
        {
            await this.GifEmojiService.SendGifEmoji(Context, count, "2bright:399164235189321728");
        }

        [Command("hypeDoge", RunMode = RunMode.Async)]
        [Summary("hypeDoge.gif")]
        [Alias("hdoge")]
        public async Task HypeDoge(int count = 1)
        {
            await this.GifEmojiService.SendGifEmoji(Context, count, "hypeDoge:398867123277135874");
        }

        [Command("karenintensifies", RunMode = RunMode.Async)]
        [Summary("hypeDoge.gif")]
        [Alias("karen")]
        public async Task KarenIntensifies(int count = 1)
        {
            await this.GifEmojiService.SendGifEmoji(Context, count, "Karen_Intensifies:427817542359318529");
        }

        [Command("pepebang", RunMode = RunMode.Async)]
        [Summary("pepebang.gif")]
        [Alias("bang")]
        public async Task PepeBang(int count = 1)
        {
            await this.GifEmojiService.SendGifEmoji(Context, count, "pepebang:402134206832050178");
        }

        [Command("pokopoko", RunMode = RunMode.Async)]
        [Summary("pokopoko.gif")]
        [Alias("poko")]
        public async Task PokoPoko(int count = 1)
        {
            await this.GifEmojiService.SendGifEmoji(Context, count, "pokopoko:398865071427551233");
        }

        [Command("shimakazetablebang", RunMode = RunMode.Async)]
        [Summary("shimakazeTablebang.gif")]
        [Alias("st")]
        public async Task Tablebang(int count = 1)
        {
            await this.GifEmojiService.SendGifEmoji(Context, count, "shimakazeTablebang:394548471929110539");
        }

        [Command("slurpyslurp", RunMode = RunMode.Async)]
        [Summary("slurpyslurp.gif")]
        [Alias("slurp")]
        public async Task SlurpySlurp(int count = 1)
        {
            await this.GifEmojiService.SendGifEmoji(Context, count, "slurpyslurp:394548168127152128");
        }

        [Command("wao", RunMode = RunMode.Async)]
        [Summary("Wao.gif")]
        public async Task Wao(int count = 1)
        {
            await this.GifEmojiService.SendGifEmoji(Context, count, "wao:400368321834385419");
        }
    }
}
