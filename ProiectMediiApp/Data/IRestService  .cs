using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProiectMediiApp.Models;

namespace ProiectMediiApp.Data
{
    public interface IRestService
    {
        // Metode pentru TrainingSession
        Task<List<TrainingSession>> GetTrainingSessionsAsync();
        Task SaveTrainingSessionAsync(TrainingSession session, bool isNewItem);
        Task DeleteTrainingSessionAsync(int id);

        // Metode pentru Trainer
        Task<List<Trainer>> GetTrainersAsync();
        Task SaveTrainerAsync(Trainer trainer, bool isNewItem);
        Task DeleteTrainerAsync(int id);

        // Metode pentru Location
        Task<List<ProiectMediiApp.Models.Location>> GetLocationsAsync();
        Task SaveLocationAsync(ProiectMediiApp.Models.Location location, bool isNewItem);
        Task DeleteLocationAsync(int id);

        Task<User> LoginUserAsync(string username, string password);
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> RegisterUserAsync(User user);


    }
}
