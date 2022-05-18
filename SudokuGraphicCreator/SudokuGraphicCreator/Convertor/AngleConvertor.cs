using SudokuGraphicCreator.ViewModel;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SudokuGraphicCreator.Convertor
{
    /// <summary>
    /// Convertor for rotate <see cref="CharacterViewModel.Text"/>.
    /// </summary>
    public class AngleConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Only one way binding in Angle");
        }
    }
}
