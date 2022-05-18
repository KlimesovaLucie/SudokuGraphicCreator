using SudokuGraphicCreator.ViewModel;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SudokuGraphicCreator.Convertor
{
    /// <summary>
    /// Convert <see cref="CircleWithNumberViewModel.Value"/> into string.
    /// </summary>
    public class CircleNumberConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = (int)value;
            if (number == 0)
            {
                return "";
            }
            return number.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Only one way binding in circle number.");
        }
    }
}
