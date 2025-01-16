namespace ProiectMediiApp;
using ProiectMediiApp.Models;
using ProiectMediiApp.Data;
public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ErrorMessage.Text = "Please fill in all fields.";
            ErrorMessage.IsVisible = true;
            return;
        }

        var user = await App.Database.LoginUserAsync(username, password);
        if (user != null)
        {
            ErrorMessage.IsVisible = false;
            await DisplayAlert("Success", "Login successful!", "OK");
            await Navigation.PushAsync(new UserProfilePage(user)); 
        }
        else
        {
            ErrorMessage.Text = "Invalid username or password.";
            ErrorMessage.IsVisible = true;
        }
    }
}
