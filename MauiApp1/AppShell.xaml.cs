namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Регистрируем маршруты для навигации
            Routing.RegisterRoute(nameof(BadmintonPage), typeof(BadmintonPage));
            Routing.RegisterRoute(nameof(RunPage), typeof(RunPage));
            Routing.RegisterRoute(nameof(BasketballPage), typeof(BasketballPage));
        }
    }
}
