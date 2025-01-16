using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ProiectMediiApp.Models;
//using Android.Content;

namespace ProiectMediiApp.Data
{
    public class RestService : IRestService
    {
        private readonly HttpClient _client;

        // URL-uri pentru fiecare model 
        private const string TrainerUrl = "https://10.0.2.2:7176/api/trainers/";
        private const string LocationUrl = "https://10.0.2.2:7176/api/locations/";
        private const string TrainingSessionUrl = "https://10.0.2.2:7176/api/trainingsessions/";
        private const string RegisterUrl = "https://10.0.2.2:7176/api/users/register";
        private const string LoginUrl = "https://10.0.2.2:7176/api/users/login";

        public RestService()
        {
            _client = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });
        }

        // ---------- Trainer Methods ----------
        public async Task<List<Trainer>> GetTrainersAsync()
        {
            var items = new List<Trainer>();
            try
            {
                Uri uri = new Uri(string.Format(TrainerUrl, string.Empty));
                var response = await _client.GetAsync(uri);

                
                Console.WriteLine($"Response status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<Trainer>>(content);
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Connection error: {ex.Message}");
            }

            return items;
        }

        public async Task SaveTrainerAsync(Trainer trainer, bool isNewItem)
        {
            string json = JsonConvert.SerializeObject(trainer);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(TrainerUrl, string.Empty));
            HttpResponseMessage response;

            if (isNewItem)
            {
                response = await _client.PostAsync(uri, content);
            }
            else
            {
                response = await _client.PutAsync(uri, content);
            }

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ERROR: {response.ReasonPhrase}");
            }
        }

        public async Task DeleteTrainerAsync(int id)
        {
            await DeleteAsync(TrainerUrl, id);
        }

        // ---------- Location Methods ----------
        public async Task<List<ProiectMediiApp.Models.Location>> GetLocationsAsync()
        {
            var items = new List<ProiectMediiApp.Models.Location>();
            try
            {
                Uri uri = new Uri(LocationUrl);
                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<ProiectMediiApp.Models.Location>>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return items;
        }

        public async Task SaveLocationAsync(ProiectMediiApp.Models.Location location, bool isNewItem)
        {
            try
            {
                string json = JsonConvert.SerializeObject(location);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                Uri uri = new Uri(LocationUrl);

                HttpResponseMessage response;
                if (isNewItem)
                {
                    response = await _client.PostAsync(uri, content); // POST pentru locații noi
                }
                else
                {
                    uri = new Uri($"{LocationUrl}/{location.Id}");
                    response = await _client.PutAsync(uri, content); // PUT pentru locații existente
                }

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to save location: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SaveLocationAsync: {ex.Message}");
            }
        }

        public async Task DeleteLocationAsync(int id)
        {
            try
            {
                Uri uri = new Uri($"{LocationUrl}{id}");
                HttpResponseMessage response = await _client.DeleteAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // ---------- TrainingSession Methods ----------
        public async Task<List<TrainingSession>> GetTrainingSessionsAsync()
        {
            return await GetAsync<TrainingSession>(TrainingSessionUrl);
        }

        public async Task SaveTrainingSessionAsync(TrainingSession session, bool isNewItem)
        {
            await SaveAsync(TrainingSessionUrl, session, isNewItem);
        }

        public async Task DeleteTrainingSessionAsync(int id)
        {
            await DeleteAsync(TrainingSessionUrl, id);
        }

        // ---------- Helper Methods ----------
        private async Task<List<T>> GetAsync<T>(string url)
        {
            var items = new List<T>();
            try
            {
                Uri uri = new Uri(string.Format(url, string.Empty));
                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<T>>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }

            return items;
        }

        private async Task SaveAsync<T>(string url, T item, bool isNewItem)
        {
            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                Uri uri = new Uri(string.Format(url, string.Empty));
                HttpResponseMessage response;

                if (isNewItem)
                {
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    response = await _client.PutAsync(uri, content);
                }

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"ERROR: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        private async Task DeleteAsync(string url, int id)
        {
            try
            {
                Uri uri = new Uri(string.Format(url, id));
                HttpResponseMessage response = await _client.DeleteAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"ERROR: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
        public async Task<bool> RegisterUserAsync(User user)
        {
            try
            {
                string json = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                Uri uri = new Uri("https://10.0.2.2:7176/api/users/register"); 

                HttpResponseMessage response = await _client.PostAsync(uri, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Register Error: {ex.Message}");
                return false;
            }
        }

        public async Task<User> LoginUserAsync(string username, string password)
        {
            try
            {
                var loginData = new { Username = username, Password = password };
                string json = JsonConvert.SerializeObject(loginData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                Uri uri = new Uri("https://10.0.2.2:7176/api/users/login"); 

                HttpResponseMessage response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(responseData);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login Error: {ex.Message}");
                return null;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                Uri uri = new Uri($"https://10.0.2.2:7176/api/users?username={username}"); 
                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(content);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUserByUsernameAsync: {ex.Message}");
                return null;
            }
        }


       




    }
}
