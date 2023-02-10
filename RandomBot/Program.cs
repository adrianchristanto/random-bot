using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Addons.Interactive;
using Discord.WebSocket;
using RandomBot.Services;
using RandomBot.Core.Entities;

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

            Configuration = new ConfigurationBuilder()
                .SetBasePath(basePath.ToString())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>(optional: true)
                .Build();

            var token = Configuration.GetSection("Token").Value;

            var discordConfig = ConfigureDiscordSocket();

            Client = new DiscordSocketClient(discordConfig);
            Services = this.ConfigureServices();
            await this.Services.GetRequiredService<CommandHandler>().InstallCommandsAsync(Configuration);

            await Client.LoginAsync(TokenType.Bot, token);
            await Client.StartAsync();
            await Client.SetGameAsync("$help");

            await Task.Delay(-1);
        }

        private DiscordSocketConfig ConfigureDiscordSocket()
        {
            return new DiscordSocketConfig()
            {
                GatewayIntents = 
                    GatewayIntents.Guilds |
                    GatewayIntents.GuildMembers |
                    GatewayIntents.GuildBans | 
                    GatewayIntents.GuildEmojis |
                    GatewayIntents.GuildIntegrations |
                    GatewayIntents.GuildWebhooks |
                    GatewayIntents.GuildVoiceStates |
                    GatewayIntents.GuildMessages |
                    GatewayIntents.GuildMessageReactions |
                    GatewayIntents.GuildMessageTyping |
                    GatewayIntents.DirectMessages |
                    GatewayIntents.DirectMessageReactions |
                    GatewayIntents.DirectMessageTyping |
                    GatewayIntents.MessageContent
            };
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(Client)
                .AddTransient<CommandHandler>()
                .AddTransient<DeleteService>()
                .AddTransient<FightService>()
                .AddTransient<GunfuService>()
                .AddTransient<HeavyOrdnanceCorpService>()
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
