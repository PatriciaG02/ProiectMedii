
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ProiectMedii.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        
        public ICollection<TrainingSession> TrainingSessions { get; set; }
    }

}
