using Microsoft.Maui.Controls;
using ProiectMediiApp.Models;


namespace ProiectMediiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Navigare către secțiunea Traineri
        async void OnManageTrainersClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TrainerEntryPage());
        }

        //// Navigare către secțiunea Locații
        //async void OnManageLocationsClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new LocationEntryPage());
        //}

        //// Navigare către secțiunea Sesiuni de Antrenament
        //async void OnManageTrainingSessionsClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new TrainingSessionEntryPage());
        //}

        async void OnLoginClicked(object sender, EventArgs e)
        {
            // Navighează către pagina de Login
            await Navigation.PushAsync(new LoginPage());
        }

        async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Navighează către pagina de Register
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void OnCreateTrainingSessionClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateTrainingSessionPage());
        }
        private async void OnManageLocationsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageLocationsPage());
        }
    }
}
