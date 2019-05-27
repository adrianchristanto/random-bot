using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace RandomBot.Modules.UserEditorModule
{
    public class BotGameStatusModule : ModuleBase<SocketCommandContext>
    {
        public BotGameStatusModule(DiscordSocketClient client)
        {
            this.Client = client;
        }
        private readonly DiscordSocketClient Client;

        [Command("botgs", RunMode = RunMode.Async)]
        public async Task ChangeBotGameStatus([Remainder]string newGameStatus)
        {
            await Context.Message.DeleteAsync();

            if (Context.User.Id == 318035086375387136)
            {
                await this.Client.SetGameAsync(newGameStatus);
            }
            else
            {
                await Context.Channel.SendMessageAsync("D:");
            }
        }
    }
}
