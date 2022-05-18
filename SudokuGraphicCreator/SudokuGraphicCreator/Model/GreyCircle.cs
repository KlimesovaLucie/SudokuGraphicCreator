using System.Windows.Media;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents grey circle as graphic element with <see cref="Brushes.DarkGray"/> color.
    /// </summary>
    public class GreyCircle : Circle
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GreyCircle"/> class/
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance form left up corner of grid.</param>
        /// <param name="rowIndex">Index of row in sudoku table.</param>
        /// <param name="colIndex">Index of column in sudoku table.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="location">Location of element in grid.</param>
        public GreyCircle(double left, double top, int rowIndex, int colIndex, SudokuElementType type, ElementLocationType location)
        {
            Left = left;
            Top = top;
            RowIndex = rowIndex;
            ColIndex = colIndex;
            FillColor = Brushes.DarkGray;
            BorderColor = Brushes.DarkGray;
            SudokuElemType = type;
            Location = location;
        }
    }
}
