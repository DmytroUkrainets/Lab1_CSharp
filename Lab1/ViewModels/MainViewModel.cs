using System.ComponentModel;
using System.Windows;

namespace Lab1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DateTime _selectedDate = DateTime.Today;
        private string? _age;
        private string? _westernZodiac;
        private string? _chineseZodiac;

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate == value) return;

                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                CalculateAgeAndZodiac();
            }
        }

        public string Age
        {
            get => _age;
            set { _age = value; OnPropertyChanged(nameof(Age)); }
        }

        public string WesternZodiac
        {
            get => _westernZodiac;
            set { _westernZodiac = value; OnPropertyChanged(nameof(WesternZodiac)); }
        }

        public string ChineseZodiac
        {
            get => _chineseZodiac;
            set { _chineseZodiac = value; OnPropertyChanged(nameof(ChineseZodiac)); }
        }

        private void CalculateAgeAndZodiac()
        {
            int age = DateTime.Today.Year - SelectedDate.Year;
            if (SelectedDate.Date > DateTime.Today.AddYears(-age))
                age--;

            if (age < 0 || age > 135)
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    MessageBox.Show("Введена дата є некоректною!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }));
                return;
            }

            Age = $"Ваш вік: {age} років";
            WesternZodiac = $"Західний знак: {Models.ZodiacHelper.GetWesternZodiac(SelectedDate)}";
            ChineseZodiac = $"Китайський знак: {Models.ZodiacHelper.GetChineseZodiac(SelectedDate.Year)}";

            if (SelectedDate.Month == DateTime.Today.Month && SelectedDate.Day == DateTime.Today.Day)
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    MessageBox.Show("Вітаємо з Днем Народження! 🎉", "Свято!", MessageBoxButton.OK, MessageBoxImage.Information);
                }));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
