using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomBot.Entities
{
    public partial class ReminderRecipient
    {
        public ReminderRecipient()
        {
            Reminder = new HashSet<Reminder>();
        }

        public int ReminderRecipientId { get; set; }
        [Required]
        [StringLength(20)]
        public string GuildId { get; set; }
        [Required]
        [StringLength(20)]
        public string ChannelId { get; set; }

        [InverseProperty("ReminderRecipient")]
        public ICollection<Reminder> Reminder { get; set; }
    }
}
