using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.DeleteModule
{
    public class DeleteModule : ModuleBase<SocketCommandContext>
    {
        public DeleteModule(DeleteService deleteService)
        {
            this.DeleteService = deleteService;
        }
        private readonly DeleteService DeleteService;

        [Command("delete", RunMode = RunMode.Async)]
        [Alias("d")]
        public async Task DeleteMessages([Remainder] int num = 0)
        {
            await this.DeleteService.DeleteMessages(Context, num);
        }

        [Command("cleanup", RunMode = RunMode.Async)]
        [Alias("cu")]
        public async Task CleanUpMessages(ulong messageId)
        {
            if (Context.User.Id != 318035086375387136)
            {
                await Context.Channel.SendMessageAsync("DENIED <:dushANGERY:1082367480422482030>");
            }

            await this.DeleteService.CleanUpMessages(Context, messageId);
        }
    }
}
