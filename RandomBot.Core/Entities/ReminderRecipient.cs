using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RandomBot.Core.Entities
{
    public partial class ReminderRecipient
    {
        public ReminderRecipient()
        {
            Reminder = new HashSet<Reminder>();
        }

        [Key]
        public int ReminderRecipientId { get; set; }
        [Required]
        [StringLength(20)]
        public string GuildId { get; set; }
        [Required]
        [StringLength(20)]
        public string ChannelId { get; set; }

        [InverseProperty("ReminderRecipient")]
        public virtual ICollection<Reminder> Reminder { get; set; }
    }
}
