using System;
using System.Globalization;
using System.Windows.Data;

namespace SudokuGraphicCreator.Convertor
{
    /// <summary>
    /// Convert number to string.
    /// </summary>
    public class NumberConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int? number = value as int?;
            if (number == null)
            {
                return "";
            }
            return number.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Only one way binding.");
        }
    }
}
