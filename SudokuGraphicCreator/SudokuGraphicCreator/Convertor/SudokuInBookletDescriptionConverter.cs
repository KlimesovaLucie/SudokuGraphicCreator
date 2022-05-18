using System;
using System.Globalization;
using System.Windows.Data;

namespace SudokuGraphicCreator.Convertor
{
    /// <summary>
    /// Convertor for description of sudoku in booklet.
    /// </summary>
    public class SudokuInBookletDescriptionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string name = (string)values[0];
            int points = (int)values[1];
            return name + " [" + points + "]";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] result = new string[2];
            string description = (string)value;
            string[] descriptions = description.Split("[");

            result[0] = descriptions[0].TrimEnd();

            descriptions = descriptions[1].Split("]");
            result[1] = descriptions[1];
            return result;
        }
    }
}
