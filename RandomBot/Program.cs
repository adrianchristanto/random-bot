using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using RandomBot.Services;
using RandomBot.Entities;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RandomBot
{
    public class Program
    {
        private IConfigurationRoot Configuration { get; set; }
        private CommandService Commands;
        private DiscordSocketClient Client;
        private IServiceProvider Services;
        private System.Timers.Timer Timer;

        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            var basePath = Directory.GetCurrentDirectory();

            // If on debug mode.
            if (basePath.Contains("netcoreapp"))
            {
                basePath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath.ToString())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            var token = Configuration.GetSection("Token").Value;

            Commands = new CommandService();
            Client = new DiscordSocketClient();
            Services = this.ConfigureServices();

            await this.InstallCommands();

            await Client.LoginAsync(TokenType.Bot, token);
            await Client.StartAsync();
            await Client.SetGameAsync("$help");

            Client.Log += Log;
            Client.UserJoined += UserJoined;

            Timer = new System.Timers.Timer(int.Parse(Configuration.GetSection("ReminderInterval").Value));
            Timer.Elapsed += new System.Timers.ElapsedEventHandler(ElapsedHandlerAsync);
            Timer.Enabled = true;

            await Task.Delay(-1);
        }

        public async Task UserJoined(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.TextChannels.FirstOrDefault();

            // If Kappa n frens server
            if (guild.Id == 370852077225771010) 
            {
                var roleId = user.Guild.Roles.FirstOrDefault(Q => Q.Id.ToString() == "370855541897035778");
                await user.AddRoleAsync(roleId);
            }

            await channel.SendMessageAsync($"Hey { user.Mention }! <:AkarinWave:370855004921135104>");
        }

        public async Task InstallCommands()
        {
            Client.MessageReceived += HandleCommand;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task HandleCommand(SocketMessage msgParam)
        {
            var prefix = '$';
            var argPos = 0;

            if (!(msgParam is SocketUserMessage msg))
            {
                return;
            }
            if ((msg.HasCharPrefix(prefix, ref argPos) || msg.HasMentionPrefix(Client.CurrentUser, ref argPos)) == false || msg.Author.IsBot == true)
            {
                return;
            }

            var context = new SocketCommandContext(Client, msg);
            var result = await Commands.ExecuteAsync(context, argPos, Services);
            if (result.IsSuccess == false)
            {
                var emoteExist = Emote.TryParse("<:confusednigga:370923106660909059>", out var emote);
                await context.Channel.SendMessageAsync($"Apa sih { emote }");
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async void ElapsedHandlerAsync(object source, System.Timers.ElapsedEventArgs e)
        {
            var reminderService = Services.GetService<ReminderService>();
            await reminderService.ExecuteReminder();
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(Client)
                .AddTransient<DeleteService>()
                .AddTransient<FightService>()
                .AddTransient<DiscordGifEmojiService>()
                .AddTransient<ImageManipulationService>()
                .AddTransient<InteractiveService>()
                .AddTransient<ReminderService>()
                .AddTransient<ShipfuService>()
                .AddTransient<TimeConvertService>()
                .AddDbContext<RandomBotDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("RandomBotDb"), strategy =>
                    {
                        strategy.EnableRetryOnFailure();
                    });
                })
                .BuildServiceProvider();
        }
    }
}
