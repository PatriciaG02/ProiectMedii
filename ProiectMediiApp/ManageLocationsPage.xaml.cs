using System.Collections.ObjectModel;
using System.Windows.Input;
using ProiectMediiApp.Models;

namespace ProiectMediiApp
{
    public partial class ManageLocationsPage : ContentPage
    {
        public ObservableCollection<ProiectMediiApp.Models.Location> Locations { get; set; }
        public string NewLocationName { get; set; }

        public ICommand AddLocationCommand { get; set; }
        public ICommand DeleteLocationCommand { get; set; }

        public ManageLocationsPage()
        {
            InitializeComponent();

            Locations = new ObservableCollection<ProiectMediiApp.Models.Location>();

            AddLocationCommand = new Command(async () => await AddLocation());
            DeleteLocationCommand = new Command<int>(async (locationId) => await DeleteLocation(locationId));

            BindingContext = this;

         
            LoadLocations();
        }

        private async void LoadLocations()
        {
            try
            {
                var locationsFromDb = await App.Database.GetLocationsAsync();
                Locations.Clear();
                foreach (var location in locationsFromDb)
                {
                    Locations.Add(location);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load locations: {ex.Message}", "OK");
            }
        }

        private async Task AddLocation()
        {
            if (!string.IsNullOrWhiteSpace(NewLocationName))
            {
                var newLocation = new ProiectMediiApp.Models.Location { Name = NewLocationName };

               
                await App.Database.SaveLocationAsync(newLocation, true);

                
                Locations.Add(newLocation);

                
                NewLocationName = string.Empty;
                OnPropertyChanged(nameof(NewLocationName));
            }
            else
            {
                await DisplayAlert("Error", "Please enter a location name.", "OK");
            }
        }

        private async Task DeleteLocation(int locationId)
        {
            var location = Locations.FirstOrDefault(l => l.Id == locationId);
            if (location != null)
            {
                try
                {
                    
                    await App.Database.DeleteLocationAsync(location.Id);

                 
                    Locations.Remove(location);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to delete location: {ex.Message}", "OK");
                }
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

          
            var locationsFromDb = await App.Database.GetLocationsAsync();
            Locations.Clear();
            foreach (var location in locationsFromDb)
            {
                Locations.Add(location);
            }
        }
    }
}
