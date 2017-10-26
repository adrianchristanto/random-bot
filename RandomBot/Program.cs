using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Linq;

namespace RandomBot
{
    class Program
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private IServiceProvider services;

        string token = "MzcxOTMzODE5NzA1NzUzNjAw.DM-fQQ.QEeRhZ7ESZEYuIERGyyZEhZeU00";

        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            commands = new CommandService();
            client = new DiscordSocketClient();
            services = new ServiceCollection().BuildServiceProvider();

            await InstallCommands();

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            await client.SetGameAsync("$help");

            client.Log += Log;
            client.UserJoined += UserJoined;

            await Task.Delay(-1);
        }

        public async Task UserJoined(SocketGuildUser user)
        {
            var channel = client.GetChannel(370852077947060225) as SocketTextChannel;
            var roleId = user.Guild.Roles.FirstOrDefault(Q => Q.Name.Equals("Plebs"));
            await user.AddRoleAsync(roleId);
            await channel.SendMessageAsync("Hey " + user.Mention + "! <:AkarinWave:370855004921135104>");
        }

        public async Task InstallCommands()
        {
            client.MessageReceived += HandleCommand;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task HandleCommand(SocketMessage msgParam)
        {
            var msg = msgParam as SocketUserMessage;
            var prefix = '$';
            if (msg == null)
            {
                return;
            }

            var argPos = 0;

            if ((msg.HasCharPrefix(prefix, ref argPos) || msg.HasMentionPrefix(client.CurrentUser, ref argPos)) == false)
            {
                return;
            }
            if (msg.Author.IsBot == true)
            {
                return;
            }
            var context = new CommandContext(client, msg);
            var result = await commands.ExecuteAsync(context, argPos, services);
            if (result.IsSuccess == false)
            {
                var errorReason = result.ErrorReason;
                //await context.Channel.SendMessageAsync(errorReason);
                await context.Channel.SendMessageAsync("Apa sih <:confusednigga:370923106660909059>");
            }
            return;
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
