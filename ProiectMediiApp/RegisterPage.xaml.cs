namespace ProiectMediiApp;
using ProiectMediiApp.Models;
public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    async void OnRegisterClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            ErrorMessage.Text = "Please fill in all fields.";
            ErrorMessage.IsVisible = true;
            return;
        }

        if (password != confirmPassword)
        {
            ErrorMessage.Text = "Passwords do not match.";
            ErrorMessage.IsVisible = true;
            return;
        }

        var existingUser = await App.Database.GetUserByUsernameAsync(username);
        if (existingUser != null)
        {
            ErrorMessage.Text = "Username already exists.";
            ErrorMessage.IsVisible = true;
            return;
        }

        await App.Database.AddUserAsync(new User
        {
            Username = username,
            Password = password 
        });

        await DisplayAlert("Success", "Account created successfully!", "OK");
        await Navigation.PopAsync(); 
    }
}
