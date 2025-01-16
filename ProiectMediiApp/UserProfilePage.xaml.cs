using Microsoft.Maui.Controls;
using ProiectMediiApp.Models;

namespace ProiectMediiApp
{
    public partial class UserProfilePage : ContentPage
    {
        public UserProfilePage(User user)
        {
            InitializeComponent();

          
            this.Title = $"Welcome, {user.Username}";
        }
    }
}
