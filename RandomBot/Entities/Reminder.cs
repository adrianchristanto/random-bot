using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Entities
{
    public class Reminder
    {
        [Key]
        public Guid ReminderId { get; set; }

        public int ReminderRecipientId { get; set; }

        public string ReminderMessage { get; set; }

        public DateTime ReminderDateTime { get; set; }

        public bool IsDaily { get; set; }

        public bool IsActive { get; set; }
    }
}
