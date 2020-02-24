using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.HelpModule
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        [Command("help", RunMode = RunMode.Async)]
        [Summary("Shows command list")]
        [Alias("h")]
        public async Task Help()
        {
            var messagesToDelete = await Context.Channel.GetMessageAsync(Context.Message.Id);
            await messagesToDelete.DeleteAsync();

            var embed = new EmbedBuilder()
                .WithAuthor("RandomBot Command List:")
                .AddField("Normal Commands", @"
$aware
$hello/$hi @Someone (Someone: User mentioned. Optional.)
$roll/$r
$decide @Option1 @Option2
$delete/$d @Number (Number: Messages to delete. Max 99.)
$timeconvert/$tc @Number (Number: Add or subtract hours from current time.)")
                .AddField("Image Commands", @"
$bait
$bitch/$b @Someone (Someone: User mentioned. Optional.)
$dab
$dance
$dieshouri
$dorime
$drool
$facepalm
$fight/$f @Person1 @Person2 (Person: User mentioned. Person2: Optional)
$ganbaru
$orz
$soon
$yes/$no")
                .AddField("Reminder Commands", @"
$remind @Message at @Date 
    (Message: Topic to remind. Date: dd/MM/yyyy HH:mm OR HH:mm)
$reminddaily @ID (ID: Reminder identification code)
$showremind (Show open reminder on current channel)
$showallremind (Show all open reminder on current guild)
$removeremind @ID (ID: Reminder identification code)
")
                .AddField("Shipfu", @"
$shipfugacha/$sg
$shipfuhistory/$sh")
                .AddField("Voice Commands", @"
$join
$leave
$play @MusicName
$musiclist
$skip")
                .AddField("Info", @"
$dev
$who/$what
$changelog
$rigged
")
                .WithColor(Discord.Color.DarkRed);

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}
