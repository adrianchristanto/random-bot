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

            Timer = new System.Timers.Timer(this.SetInitialInterval(int.Parse(Configuration.GetSection("ReminderInterval").Value)));
            Timer.Elapsed += new System.Timers.ElapsedEventHandler(ElapsedHandlerAsync);
            Timer.Enabled = true;

            await Task.Delay(-1);
        }

        public async Task UserJoined(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.TextChannels.FirstOrDefault();

            await channel.SendMessageAsync($"Hey { user.Mention }! <:AkarinWave:370855004921135104>");
        }

        public async Task InstallCommands()
        {
            Client.MessageReceived += HandleCommand;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);
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
                var emoteExist = Emote.TryParse("<:veriConfuse:528044464795680787>", out var emote);
                await context.Channel.SendMessageAsync($"Apa sih { emote }");
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private int SetInitialInterval(int interval)
        {
            var currentSecond = DateTime.Now.Second * 1000;
            return 60000 - currentSecond;
        }

        private async void ElapsedHandlerAsync(object source, System.Timers.ElapsedEventArgs e)
        {
            var reminderService = Services.GetService<ReminderService>();
            await reminderService.ExecuteReminder();

            var interval = int.Parse(Configuration.GetSection("ReminderInterval").Value);
            this.Timer.Interval = this.SetInitialInterval(interval);
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(Client)
                .AddTransient<DeleteService>()
                .AddTransient<DiscordGifEmojiService>()
                .AddTransient<FightService>()
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
