namespace ProiectMedii.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string Phone { get; set; }
        public ICollection<TrainingSession> TrainingSessions { get; set; }

    }
}
