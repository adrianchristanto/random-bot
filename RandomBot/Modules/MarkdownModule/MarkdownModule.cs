using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.MarkdownModule
{
    public class MarkdownModule : ModuleBase<SocketCommandContext>
    {
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
    }
}
