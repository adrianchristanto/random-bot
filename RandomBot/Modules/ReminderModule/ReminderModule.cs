using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.ReminderModule
{
    public class ReminderModule : ModuleBase<SocketCommandContext>
    {
        public ReminderModule(ReminderService reminderService)
        {
            this.ReminderService = reminderService;
        }
        private readonly ReminderService ReminderService;

        [Command("remind", RunMode = RunMode.Async)]
        public async Task SetReminder([Remainder]string message)
        {
            await this.ReminderService.SetReminder(Context, message);
        }

        [Command("reminddaily", RunMode = RunMode.Async)]
        public async Task SetDailyReminder(string guid)
        {
            await this.ReminderService.SetDailyReminder(Context, guid);
        }

        [Command("showremind", RunMode = RunMode.Async)]
        public async Task ShowReminder()
        {
            await this.ReminderService.ShowReminder(Context);
        }

        [Command("showallremind", RunMode = RunMode.Async)]
        public async Task ShowAllReminder()
        {
            await this.ReminderService.ShowReminder(Context, true);
        }

        [Command("removeremind", RunMode = RunMode.Async)]
        public async Task RemoveReminder(string guid)
        {
            await this.ReminderService.RemoveReminder(Context, guid);
        }
    }
}