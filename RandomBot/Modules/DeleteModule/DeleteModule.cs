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
        [Summary("Deletes set amount of text")]
        [Alias("d")]
        public async Task DeleteMessages([Remainder] int num = 0)
        {
            await this.DeleteService.DeleteMessages(num, Context);
        }
    }
}
