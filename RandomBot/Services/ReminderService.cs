﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using RandomBot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomBot.Services
{
    public class ReminderService : IDisposable
    {
        public ReminderService(DiscordSocketClient discordSocketClient, RandomBotDbContext context)
        {
            this.Client = discordSocketClient;
            this.DbContext = context;
        }
        private readonly DiscordSocketClient Client;
        private readonly RandomBotDbContext DbContext;

        public void Dispose()
        {
            this.Client.Dispose();
            this.DbContext.Dispose();
        }

        public SocketTextChannel GetSocketTextChannel(ulong guildId, ulong channelId)
        {
            var guild = this.Client.GetGuild(guildId);
            var channel = guild.GetTextChannel(channelId);
            return channel;
        }

        public async Task SetReminder(SocketCommandContext context, string message)
        {
            var (ReminderMessage, ReminderDate, IsValid) = this.ValidateMessage(message);
            if (IsValid == false)
            {
                await context.Channel.SendMessageAsync("Please type the correct format (optional: choose one or more) => @Message at @Date [(optional)dd/MM/yyyy (optional)HH:mm]");
            }
            else
            {
                var recipientId = await this.GetReminderRecipientId(context);

                var reminder = new Reminder
                {
                    ReminderId = new Guid(),
                    ReminderRecipientId = recipientId,
                    ReminderDateTime = ReminderDate,
                    ReminderMessage = ReminderMessage,
                    IsActive = true
                };
                this.DbContext.Reminder.Add(reminder);
                await this.DbContext.SaveChangesAsync();

                await context.Channel.SendMessageAsync($@"Ok, I will remind you for '{ ReminderMessage }' at { ReminderDate.ToString("dd/MM/yyyy HH:mm") }. Do apologize if I'm late because I got deactivated...");
            }
        }

        private (string ReminderMessage, DateTime ReminderDate, bool IsValid) ValidateMessage(string message)
        {
            var separator = " at ";
            var isValid = false;

            if (message.Contains(separator) == false)
            {
                return ("", new DateTime(), isValid);
            }
            else if (string.IsNullOrWhiteSpace(message.Substring(message.LastIndexOf(separator, StringComparison.OrdinalIgnoreCase) + separator.Length)))
            {
                return ("", new DateTime(), isValid);
            }

            var reminderMessage = message.Substring(0, message.LastIndexOf(separator, StringComparison.OrdinalIgnoreCase));
            var reminderDateString = message.Substring(message.LastIndexOf(separator, StringComparison.OrdinalIgnoreCase) + separator.Length);
            var reminderDate = DateTime.Now;

            if (reminderDateString.Length == 16)
            {
                isValid = DateTime.TryParse(reminderDateString, out reminderDate);
            }
            if (reminderDateString.Length == 10)
            {
                isValid = DateTime.TryParse(reminderDateString, out reminderDate);
            }
            if (reminderDateString.Length < 10)
            {
                var timeSeparator = ":";
                var hourString = reminderDateString.Substring(0, reminderDateString.LastIndexOf(timeSeparator, StringComparison.OrdinalIgnoreCase));
                var minuteString = reminderDateString.Substring(reminderDateString.LastIndexOf(timeSeparator, StringComparison.OrdinalIgnoreCase) + timeSeparator.Length);

                var isHourValid = int.TryParse(hourString, out var hour);
                var isMinuteValid = int.TryParse(minuteString, out var minute);

                if (isHourValid == true && isMinuteValid == true)
                {
                    var dateTime = new DateTime(reminderDate.Year, reminderDate.Month, reminderDate.Day, hour, minute, 0);
                    reminderDate = reminderDate > dateTime ? dateTime.AddDays(1) : dateTime;
                    isValid = true;
                }
                else
                {
                    return ("", new DateTime(), isValid);
                }
            }

            return (reminderMessage, reminderDate, isValid);
        }

        private async Task<int> GetReminderRecipientId(SocketCommandContext context)
        {
            var recipient = await this.DbContext.ReminderRecipient.AsNoTracking()
                .Where(Q => Q.GuildId == context.Guild.Id.ToString() && Q.ChannelId == context.Channel.Id.ToString())
                .FirstOrDefaultAsync();

            if (recipient == null)
            {
                var newRecipient = new ReminderRecipient
                {
                    GuildId = context.Guild.Id.ToString(),
                    ChannelId = context.Channel.Id.ToString()
                };

                this.DbContext.ReminderRecipient.Add(newRecipient);

                recipient = newRecipient;
            }

            return recipient.ReminderRecipientId;
        }

        public async Task SetDailyReminder(SocketCommandContext context, string guid)
        {
            var reminderId = new Guid(guid);
            var reminder = await this.DbContext.Reminder
                .Where(Q => Q.ReminderId == reminderId && Q.IsActive == true)
                .FirstOrDefaultAsync();

            if (reminder == null)
            {
                await context.Channel.SendMessageAsync("Reminder not found");
            }
            else
            {
                reminder.IsDaily = true;
                await this.DbContext.SaveChangesAsync();
                await context.Channel.SendMessageAsync($"Ok, I will remind you for '{ reminder.ReminderMessage }' daily at { reminder.ReminderDateTime.ToShortTimeString() }");
            }
        }

        public async Task ShowReminder(SocketCommandContext context, bool showAll = false)
        {
            var openReminder = await this.DbContext.Reminder
                .Join(this.DbContext.ReminderRecipient, r => r.ReminderRecipientId, rr => rr.ReminderRecipientId, (r, rr) => new
                {
                    r,
                    rr.GuildId,
                    rr.ChannelId
                })
                .AsNoTracking()
                .Where(Q => Q.GuildId == context.Guild.Id.ToString() && Q.r.IsActive == true && (Q.ChannelId == context.Channel.Id.ToString() || showAll == true))
                .Select(Q => Q.r)
                .OrderBy(Q => Q.ReminderDateTime)
                .ToListAsync();

            var embed = new EmbedBuilder()
                .WithColor(Color.DarkRed);

            if (openReminder.Count == 0)
            {
                embed.WithAuthor("There are no active reminder for this channel");
            }
            else
            {
                embed.WithAuthor("Current Reminder(s) for this channel:");
                for (var i = 0; i < openReminder.Count; i++)
                {
                    var reminderTime = openReminder[i].IsDaily == true ? openReminder[i].ReminderDateTime.ToShortTimeString() : openReminder[i].ReminderDateTime.ToString("dd/MM/yyyy HH:mm");
                    var isDaily = openReminder[i].IsDaily == true ? "(daily)" : "";
                    embed.AddField($"{ i + 1 }. { openReminder[i].ReminderId }", $@"{ openReminder[i].ReminderMessage } at { reminderTime } { isDaily }");
                }
            }

            await context.Channel.SendMessageAsync("", embed: embed.Build());
        }

        public async Task ExecuteReminder()
        {
            var dateNow = DateTime.Now;

            // Get reminder grouped by recipients.
            var reminderDictionary = await (from r in this.DbContext.Reminder
                                            join rr in this.DbContext.ReminderRecipient on r.ReminderRecipientId equals rr.ReminderRecipientId
                                            where r.ReminderDateTime <= dateNow && r.IsActive == true
                                            group r by new { rr.GuildId, rr.ChannelId } into gr
                                            select gr)
                                            .ToDictionaryAsync(Q => (Q.Key.GuildId, Q.Key.ChannelId), Q => Q.ToList());

            var remindersToUpdate = new List<Reminder>();
            var reminderMessage = new Dictionary<SocketTextChannel, EmbedBuilder>();
            foreach (var groupReminders in reminderDictionary)
            {
                var embed = new EmbedBuilder()
                    .WithAuthor($"Reminder(s) for this channel (Run at { dateNow.ToString("dd/MM/yyyy HH:mm") }):")
                    .WithColor(Discord.Color.DarkRed);

                var reminders = groupReminders.Value;
                var reminderCount = 0;
                for (var i = 0; i < reminders.Count; i++)
                {
                    var hourDifference = (dateNow - reminders[i].ReminderDateTime).TotalHours;
                    if (hourDifference < 2)
                    {
                        reminderCount++;
                        var reminderDate = reminders[i].IsDaily == true ? reminders[i].ReminderDateTime.ToShortTimeString() : reminders[i].ReminderDateTime.ToString("dd/MM/yyyy HH:mm");
                        embed.AddField($"{ reminderCount }. { reminderDate }", reminders[i].ReminderMessage);
                    }
                    remindersToUpdate.Add(reminders[i]);
                }


                if (reminderCount > 0)
                {
                    var channel = this.GetSocketTextChannel(ulong.Parse(groupReminders.Key.GuildId), ulong.Parse(groupReminders.Key.ChannelId));
                    reminderMessage.Add(channel, embed);
                }
            }

            await this.UpdateReminder(remindersToUpdate);
            Parallel.ForEach(reminderMessage, async reminder =>
            {
                await reminder.Key.SendMessageAsync(embed: reminder.Value.Build());
            });
        }

        private async Task UpdateReminder(List<Reminder> reminders)
        {
            foreach (var reminder in reminders)
            {
                if (reminder.IsDaily == true)
                {
                    reminder.ReminderDateTime = reminder.ReminderDateTime.AddDays(1);
                }
                else
                {
                    reminder.IsActive = false;
                }
            }

            await this.DbContext.SaveChangesAsync();
        }

        public async Task RemoveReminder(SocketCommandContext context, string guid)
        {
            var reminderId = new Guid(guid);
            var openReminder = await this.DbContext.Reminder
                .Where(Q => Q.ReminderId == reminderId && Q.IsActive == true)
                .FirstOrDefaultAsync();

            if (openReminder == null)
            {
                await context.Channel.SendMessageAsync("Reminder not found");
            }
            else
            {
                openReminder.IsActive = false;
                await this.DbContext.SaveChangesAsync();

                await context.Channel.SendMessageAsync("Reminder removed");
            }
        }
    }
}