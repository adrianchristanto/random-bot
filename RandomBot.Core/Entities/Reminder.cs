using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RandomBot.Core.Entities
{
    public partial class Reminder
    {
        [Key]
        public Guid ReminderId { get; set; }
        public int ReminderRecipientId { get; set; }
        public DateTime ReminderDateTime { get; set; }
        [Required]
        [StringLength(255)]
        public string ReminderMessage { get; set; }
        public bool IsDaily { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey(nameof(ReminderRecipientId))]
        [InverseProperty("Reminder")]
        public virtual ReminderRecipient ReminderRecipient { get; set; }
    }
}
