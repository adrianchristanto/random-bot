using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Entities
{
    public class ReminderRecipient
    {
        public string ChannelId { get; set; }

        public string GuildId { get; set; }

        [Key]
        public int ReminderRecipientId { get; set; }

        public string UserId { get; set; }
    }
}
