namespace MauiApp1
{
    public partial class WeightIndicatorPage : ContentPage
    {
        public WeightIndicatorPage()
        {
            InitializeComponent();
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            // Считываем рост и вес из Entry
            if (double.TryParse(HeightEntry.Text, out double heightCm) &&
                double.TryParse(WeightEntry.Text, out double weightKg))
            {
                // Переводим рост в метры
                double heightM = heightCm / 100.0;
                if (heightM <= 0)
                {
                    ResultLabel.Text = "Некорректный рост.";
                    return;
                }

                // Рассчитываем BMI = вес / (рост^2)
                double bmi = weightKg / (heightM * heightM);

                // Предположим, что "нормальный" BMI = 22
                double normalBmi = 22;
                // Вес, соответствующий нормальному BMI:
                double normalWeight = normalBmi * heightM * heightM;
                double diff = weightKg - normalWeight;

                // Формируем сообщение
                // Если diff > 0, человек имеет "лишний" вес
                // Если diff < 0, недобор
                // Примерно 7000 ккал = 1 кг массы
                if (diff > 0)
                {
                    double excessKg = diff; // лишние кг
                    double neededCalories = excessKg * 7000;
                    ResultLabel.Text =
                        $"Ваш BMI: {bmi:F1}. " +
                        $"Вам нужно сбросить ~{excessKg:F1} кг.\n" +
                        $"Для этого требуется сжечь примерно {neededCalories:F0} ккал.";
                }
                else if (diff < 0)
                {
                    double missingKg = -diff;
                    ResultLabel.Text =
                        $"Ваш BMI: {bmi:F1}. " +
                        $"У вас недобор ~{missingKg:F1} кг.\n" +
                        $"Для набора веса нужно увеличить калорийность рациона.";
                }
                else
                {
                    ResultLabel.Text =
                        $"Ваш BMI: {bmi:F1}. " +
                        $"Вы находитесь ровно в 'норме' (BMI = {normalBmi}).";
                }
            }
            else
            {
                ResultLabel.Text = "Ошибка ввода. Укажите рост и вес цифрами.";
            }
        }
    }
}
