using System.Windows.Media;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents graphic element circle with and number inside of it.
    /// </summary>
    public class CircleWithNumber : Circle
    {
        /// <summary>
        /// Value inside of circle.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="CircleWithNumber"/> class.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance form left up corner of grid.</param>
        /// <param name="rowIndex">Index of row in sudoku table.</param>
        /// <param name="colIndex">Index of column in sudoku table.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="location">Location in grid.</param>
        public CircleWithNumber(double left, double top, int rowIndex, int colIndex, SudokuElementType type, ElementLocationType location)
        {
            Left = left;
            Top = top;
            RowIndex = rowIndex;
            ColIndex = colIndex;
            FillColor = Brushes.White;
            BorderColor = Brushes.Black;
            SudokuElemType = type;
            Location = location;
        }
    }
}
