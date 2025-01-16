using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMediiApp.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }

        // Relație cu Trainer
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        // Relație cu Location
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
