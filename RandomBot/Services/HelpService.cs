using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Services
{
    public class HelpService
    {
        public async Task HelpMessage(SocketCommandContext Context)
        {
            var messagesToDelete = await Context.Channel.GetMessageAsync(Context.Message.Id);
            var builder = new EmbedBuilder()
                .WithAuthor("RandomBot Command List:")
                .AddField("Normal Commands", @"
$hello or $hi
$markdown or $m
$uralreadydead
$delete or $d (max 99)
$roll or $r")
                .AddField("Image Commands", @"
$northstar or $kenshiro
$fight or $f
$dab
$yes or $no
$bitch or $b
$ree
$soon
$facepalm
$gocrazy or $gc")
                .AddField("Youtube Search", "$search or $s")
                .AddField("Shipfu", "$shipfugacha or $sg")
                .AddField("Info", @"
$dev
$who or $what
$changelog
$rigged
")
                .WithColor(Discord.Color.DarkRed);

            await Context.Channel.SendMessageAsync("", false, builder);
            await messagesToDelete.DeleteAsync();
        }
    }
}
