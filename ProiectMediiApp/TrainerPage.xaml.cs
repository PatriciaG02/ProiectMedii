using ProiectMediiApp.Data;
using ProiectMediiApp.Models;
using Microsoft.Maui.Controls;

namespace ProiectMediiApp
{
    public partial class TrainerPage : ContentPage
    {
        public TrainerPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var trainer = (Trainer)BindingContext;
            await App.Database.SaveTrainerAsync(trainer, trainer.Id == 0);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var trainer = (Trainer)BindingContext;
            await App.Database.DeleteTrainerAsync(trainer.Id);
            await Navigation.PopAsync();
        }
    }
}
