using ProiectMediiApp.Data;
using ProiectMediiApp.Models;
using Microsoft.Maui.Controls;

namespace ProiectMediiApp
{
    public partial class TrainerEntryPage : ContentPage
    {
        public TrainerEntryPage()
        {
            InitializeComponent();
        }

        
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            TrainerListView.ItemsSource = await App.Database.GetTrainersAsync();
        }

        async void OnTrainerAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TrainerPage
            {
                BindingContext = new Trainer()
            });
        }

        
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new TrainerPage
                {
                    BindingContext = e.SelectedItem as Trainer
                });
            }
        }

    }
}
