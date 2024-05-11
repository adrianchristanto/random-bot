using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomBot.Services;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;

namespace RandomBot
{
    public class CommandHandler
    {
        private IConfigurationRoot Configuration;
        private CommandService Commands;
        private DiscordSocketClient Client;
        private readonly IServiceProvider Services;
        private Timer Timer;

        public CommandHandler(IServiceProvider services, DiscordSocketClient client)
        {
            Commands = new CommandService();
            Client = client;
            Services = services;
        }

        public async Task InstallCommandsAsync(IConfigurationRoot configuration)
        {
            Configuration = configuration;

            Client.Log += Log;
            Client.UserJoined += UserJoined;

            Timer = new Timer(this.SetInitialInterval(int.Parse(configuration.GetSection("ReminderInterval").Value)));
            Timer.Elapsed += new ElapsedEventHandler(ElapsedHandlerAsync);
            Timer.Enabled = true;

            Client.MessageReceived += HandleCommandAsync;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);
        }

        private Task Log(LogMessage msg)
        {
            if (msg.Exception != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (msg.Source.Contains("Audio #") == false)
            {
                Console.WriteLine(msg.ToString());
            }
            return Task.CompletedTask;
        }

        private async Task UserJoined(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.TextChannels.FirstOrDefault();

            await channel.SendMessageAsync($"Hey { user.Mention }! <:awoo:372995222625452044>");
        }

        private int SetInitialInterval(int interval)
        {
            var currentSecond = DateTime.Now.Second * 1000;
            return 60000 - currentSecond;
        }

        private async void ElapsedHandlerAsync(object source, ElapsedEventArgs e)
        {
            await this.Services.GetRequiredService<ReminderService>().ExecuteReminder();

            var interval = int.Parse(Configuration.GetSection("ReminderInterval").Value);
            this.Timer.Interval = this.SetInitialInterval(interval);
        }

        private async Task HandleCommandAsync(SocketMessage msgParam)
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
                await context.Channel.SendMessageAsync($"Apa sih <:PogYou:678303962876739596>");
            }
        }
    }
}
