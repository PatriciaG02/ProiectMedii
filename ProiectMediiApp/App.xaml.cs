using ProiectMediiApp.Data;

namespace ProiectMediiApp
{
    public partial class App : Application
    {
        private static ProiectMediiDatabase database;
        // Proprietatea statică pentru accesarea bazei de date
      

        // Proprietatea statică pentru accesarea bazei de date
        public static ProiectMediiDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ProiectMediiDatabase(new RestService());
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            // Inițializăm baza de date cu RestService
            

            // Setăm pagina principală a aplicației
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
