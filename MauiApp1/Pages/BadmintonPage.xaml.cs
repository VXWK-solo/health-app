namespace MauiApp1
{
    public partial class BadmintonPage : ContentPage
    {
        private bool isTraining = false;
        private DateTime startTime;

        // Таймер, который будет каждую секунду обновлять статистику
        private IDispatcherTimer dispatcherTimer;

        public BadmintonPage()
        {
            InitializeComponent();

            // Создаём таймер с интервалом 1 секунда
            dispatcherTimer = Dispatcher.CreateTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += (s, e) =>
            {
                if (isTraining)
                {
                    var duration = DateTime.Now - startTime;
                    double seconds = duration.TotalSeconds;
                    int calories = (int)(seconds * 10); // Допустим, 10 калорий/сек

                    StatsLabel.Text = $"Время: {seconds:F0} сек | Калорий: {calories}";
                }
            };
            dispatcherTimer.Start();
        }

        private async void OnTrainingButtonClicked(object sender, EventArgs e)
        {
            if (!isTraining)
            {
                // Начинаем тренировку
                isTraining = true;
                TrainingButton.Text = "Остановить";
                startTime = DateTime.Now;
            }
            else
            {
                // Останавливаем тренировку
                isTraining = false;
                TrainingButton.Text = "Начать";

                var duration = DateTime.Now - startTime;
                double seconds = duration.TotalSeconds;
                int calories = (int)(seconds * 10);

                await DisplayAlert("Тренировка завершена",
                    $"Вы сожгли примерно {calories} калорий за {seconds:F0} секунд(ы).",
                    "OK");

                // Сброс значений Label
                StatsLabel.Text = "Время: 0 сек | Калорий: 0";
            }
        }
    }
}
