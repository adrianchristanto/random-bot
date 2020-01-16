using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Addons.Interactive;
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
        private DiscordSocketClient Client;
        private IServiceProvider Services;

        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            var basePath = Directory.GetCurrentDirectory();

            // On debug mode only.
            if (basePath.Contains("netcoreapp"))
            {
                basePath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath.ToString())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            var token = Configuration.GetSection("Token").Value;

            Client = new DiscordSocketClient();
            Services = this.ConfigureServices();
            await this.Services.GetRequiredService<CommandHandler>().InstallCommands(Configuration);

            await Client.LoginAsync(TokenType.Bot, token);
            await Client.StartAsync();
            await Client.SetGameAsync("$help");

            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(Client)
                .AddTransient<CommandHandler>()
                .AddTransient<DeleteService>()
                .AddTransient<FightService>()
                .AddTransient<GunfuService>()
                .AddTransient<ImageManipulationService>()
                .AddTransient<InteractiveService>()
                .AddTransient<ReminderService>()
                .AddTransient<ShipfuService>()
                .AddTransient<TimeConvertService>()
                .AddSingleton<VoiceChannelService>()
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
