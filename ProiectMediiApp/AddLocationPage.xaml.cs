using ProiectMediiApp.Models;

namespace ProiectMediiApp
{
    public partial class AddLocationPage : ContentPage
    {
        public AddLocationPage()
        {
            InitializeComponent();
        }

        private async void OnAddLocationClicked(object sender, EventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(LocationNameEntry.Text) || string.IsNullOrWhiteSpace(LocationAddressEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

   
            var newLocation = new ProiectMediiApp.Models.Location
            {
                Name = LocationNameEntry.Text,
                Address = LocationAddressEntry.Text
            };

            try
            {
               
                await App.Database.SaveLocationAsync(newLocation, true);

            
                await DisplayAlert("Success", "Location added successfully!", "OK");

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to add location: {ex.Message}", "OK");
            }
        }
    }
}
