using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMediiApp.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        // Relație one-to-many cu TrainingSession
        public ICollection<TrainingSession>? TrainingSessions { get; set; }
    }
}
