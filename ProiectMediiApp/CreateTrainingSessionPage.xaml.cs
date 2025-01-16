using ProiectMediiApp.Models;

namespace ProiectMediiApp;

public partial class CreateTrainingSessionPage : ContentPage
{
    public CreateTrainingSessionPage()
    {
        InitializeComponent();
        LoadTrainers();
        LoadLocations();
    }

    private async void LoadTrainers()
    {
        var trainers = await App.Database.GetTrainersAsync();
        TrainerPicker.ItemsSource = trainers;
        TrainerPicker.ItemDisplayBinding = new Binding("Name");
    }

    private async void LoadLocations()
    {
        try
        {
            var locations = await App.Database.GetLocationsAsync();
            LocationPicker.ItemsSource = locations; 
            LocationPicker.ItemDisplayBinding = new Binding("Name"); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading locations: {ex.Message}");
        }
    }

    private async void OnSaveSessionClicked(object sender, EventArgs e)
    {
        if (TrainerPicker.SelectedItem == null || LocationPicker.SelectedItem == null || string.IsNullOrWhiteSpace(DescriptionEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        var session = new TrainingSession
        {
            Date = DatePicker.Date,
            Description = DescriptionEntry.Text,
            Duration = int.Parse(DurationEntry.Text),
            TrainerId = ((Trainer)TrainerPicker.SelectedItem).Id,
            LocationId = ((ProiectMediiApp.Models.Location)LocationPicker.SelectedItem).Id,
        };

        await App.Database.SaveTrainingSessionAsync(session, true);

        await DisplayAlert("Success", "Training session created!", "OK");
        await Navigation.PopAsync();
    }
}
