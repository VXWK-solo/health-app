using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace MauiApp1
{
    public partial class TimerPage : ContentPage
    {
        private bool isRunning = false;            // Флаг: секундомер запущен?
        private DateTime startTime;                // Время последнего запуска (Start)
        private TimeSpan elapsedBeforePause;       // Накопленное время до остановки
        private IDispatcherTimer dispatcherTimer;  // Таймер для обновления экрана

        // Класс модели для хранения кругов
        public class Lap
        {
            public int LapNumber { get; set; }
            public string LapTime { get; set; }
        }

        // ObservableCollection, чтобы CollectionView обновлялся автоматически
        public ObservableCollection<Lap> Laps { get; set; } = new ObservableCollection<Lap>();
        private int lapCount = 0;

        public TimerPage()
        {
            InitializeComponent();
            BindingContext = this;

            // Инициализируем таймер c интервалом ~10 мс (100 обновлений/сек)
            dispatcherTimer = Dispatcher.CreateTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(10);
            dispatcherTimer.Tick += OnTimerTick;
            dispatcherTimer.Start();

            // По умолчанию "Круг" недоступен, пока не нажали "Старт"
            LapButton.IsEnabled = false;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (isRunning)
            {
                // Текущее общее время = то, что было до паузы + сколько прошло с момента старта
                var total = (DateTime.Now - startTime) + elapsedBeforePause;

                // Формат "мм:сс,дд" (минуты, секунды, сотые)
                var minutes = (int)total.TotalMinutes;
                var seconds = total.Seconds;
                var hundredths = total.Milliseconds / 10; // сотые

                TimeLabel.Text = $"{minutes:D2}:{seconds:D2},{hundredths:D2}";
            }
        }

        // Кнопка "Старт" / "Стоп"
        private void OnStartStopClicked(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                // Запуск
                isRunning = true;
                StartStopButton.Text = "Стоп";
                LapButton.IsEnabled = true;

                // Запоминаем текущее время как точку отсчёта
                startTime = DateTime.Now;
            }
            else
            {
                // Остановка
                isRunning = false;
                StartStopButton.Text = "Старт";
                LapButton.IsEnabled = false;

                // Запоминаем накопленное время
                var total = (DateTime.Now - startTime) + elapsedBeforePause;
                elapsedBeforePause = total;
            }
        }

        // Кнопка "Круг" (Lap)
        private void OnLapClicked(object sender, EventArgs e)
        {
            lapCount++;
            // Текущее отображаемое время
            var lapTime = TimeLabel.Text;

            Laps.Add(new Lap
            {
                LapNumber = lapCount,
                LapTime = lapTime
            });
        }

        // Кнопка "Сброс"
        private void OnResetClicked(object sender, EventArgs e)
        {
            // Останавливаем если идёт
            isRunning = false;

            // Сбрасываем все переменные
            elapsedBeforePause = TimeSpan.Zero;
            lapCount = 0;
            Laps.Clear();

            // Сбрасываем UI
            TimeLabel.Text = "00:00,00";
            StartStopButton.Text = "Старт";
            LapButton.IsEnabled = false;
        }
    }
}
