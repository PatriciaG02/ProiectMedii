using ProiectMediiApp.Models;
using ProiectMediiApp.Models;
namespace ProiectMediiApp;


public partial class TrainingSessionsPage : ContentPage
{
    public TrainingSessionsPage()
    {
        InitializeComponent();
        LoadSessions();
    }

    private async void LoadSessions()
    {
        var sessions = await App.Database.GetTrainingSessionsAsync();
        SessionsListView.ItemsSource = sessions;
    }

    private async void OnDeleteSessionClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var session = button.BindingContext as TrainingSession;

        bool confirm = await DisplayAlert("Confirm", "Are you sure you want to delete this session?", "Yes", "No");
        if (confirm)
        {
            await App.Database.DeleteTrainingSessionAsync(session.Id);
            LoadSessions();
        }
    }
}
