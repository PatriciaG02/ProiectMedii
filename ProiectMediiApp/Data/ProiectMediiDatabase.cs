using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProiectMediiApp.Models;
using ProiectMediiApp.Data;

namespace ProiectMediiApp.Data
{
    public class ProiectMediiDatabase
    {
        private readonly IRestService _restService;

        public ProiectMediiDatabase(IRestService restService)
        {
            _restService = restService;
        }

        public Task<List<Trainer>> GetTrainersAsync() => _restService.GetTrainersAsync();
        public Task SaveTrainerAsync(Trainer trainer, bool isNewItem) => _restService.SaveTrainerAsync(trainer, isNewItem);
        public Task DeleteTrainerAsync(int id) => _restService.DeleteTrainerAsync(id);

        public Task<List<ProiectMediiApp.Models.Location>> GetLocationsAsync() => _restService.GetLocationsAsync();
        public Task SaveLocationAsync(ProiectMediiApp.Models.Location location, bool isNewItem) => _restService.SaveLocationAsync(location, isNewItem);
        public Task DeleteLocationAsync(int id) => _restService.DeleteLocationAsync(id);

        public Task<List<TrainingSession>> GetTrainingSessionsAsync() => _restService.GetTrainingSessionsAsync();
        public Task SaveTrainingSessionAsync(TrainingSession session, bool isNewItem) => _restService.SaveTrainingSessionAsync(session, isNewItem);
        public Task DeleteTrainingSessionAsync(int id) => _restService.DeleteTrainingSessionAsync(id);


        public Task<User> LoginUserAsync(string username, string password)
           => _restService.LoginUserAsync(username, password);

        public Task<User> GetUserByUsernameAsync(string username)
            => _restService.GetUserByUsernameAsync(username);

        public Task<bool> AddUserAsync(User user)
            => _restService.RegisterUserAsync(user);

    }
}
