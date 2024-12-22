namespace MauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // При запуске приложения откроется AppShell
            MainPage = new AppShell();
        }
    }
}
