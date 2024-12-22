namespace MauiApp1
{
    public partial class BasketballPage : ContentPage
    {
        private bool isTraining = false;
        private DateTime startTime;

        private IDispatcherTimer dispatcherTimer;

        public BasketballPage()
        {
            InitializeComponent();

            dispatcherTimer = Dispatcher.CreateTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += (s, e) =>
            {
                if (isTraining)
                {
                    var duration = DateTime.Now - startTime;
                    double seconds = duration.TotalSeconds;
                    int calories = (int)(seconds * 10);

                    StatsLabel.Text = $"Время: {seconds:F0} сек | Калорий: {calories}";
                }
            };
            dispatcherTimer.Start();
        }

        private async void OnTrainingButtonClicked(object sender, EventArgs e)
        {
            if (!isTraining)
            {
                isTraining = true;
                TrainingButton.Text = "Остановить";
                startTime = DateTime.Now;
            }
            else
            {
                isTraining = false;
                TrainingButton.Text = "Начать";

                var duration = DateTime.Now - startTime;
                double seconds = duration.TotalSeconds;
                int calories = (int)(seconds * 10);

                await DisplayAlert("Тренировка завершена",
                    $"Вы сожгли примерно {calories} калорий за {seconds:F0} секунд(ы).",
                    "OK");

                StatsLabel.Text = "Время: 0 сек | Калорий: 0";
            }
        }
    }
}
