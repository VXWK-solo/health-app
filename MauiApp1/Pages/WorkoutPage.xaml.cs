namespace MauiApp1
{
    public partial class WorkoutPage : ContentPage
    {
        public WorkoutPage()
        {
            InitializeComponent();
        }

        private async void OnBadmintonTapped(object sender, TappedEventArgs e)
        {
            // Переход на страницу BadmintonPage
            await Shell.Current.GoToAsync(nameof(BadmintonPage));
        }

        private async void OnRunTapped(object sender, TappedEventArgs e)
        {
            // Переход на страницу RunPage
            await Shell.Current.GoToAsync(nameof(RunPage));
        }

        private async void OnBasketballTapped(object sender, TappedEventArgs e)
        {
            // Переход на страницу BasketballPage
            await Shell.Current.GoToAsync(nameof(BasketballPage));
        }
    }
}
