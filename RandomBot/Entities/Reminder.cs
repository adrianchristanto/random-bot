using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomBot.Entities
{
    public partial class Reminder
    {
        public Guid ReminderId { get; set; }
        public int ReminderRecipientId { get; set; }
        public DateTime ReminderDateTime { get; set; }
        [Required]
        [StringLength(255)]
        public string ReminderMessage { get; set; }
        public bool IsDaily { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("ReminderRecipientId")]
        [InverseProperty("Reminder")]
        public ReminderRecipient ReminderRecipient { get; set; }
    }
}
