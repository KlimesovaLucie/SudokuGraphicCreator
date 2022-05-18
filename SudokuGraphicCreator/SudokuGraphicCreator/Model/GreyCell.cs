using System.Windows.Media;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents grey cell of grid.
    /// </summary>
    public class GreyCell : OneSpaceElement
    {
        /// <summary>
        /// Color of cell.
        /// </summary>
        public Brush FillColor { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="GreyCell"/> class.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance form left up corner of grid.</param>
        /// <param name="rowIndex">Index of row in sudoku table.</param>
        /// <param name="colIndex">Index of column in sudoku table.</param>
        /// <param name="type">Type of graphic element.</param>
        public GreyCell(double left, double top, int rowIndex, int colIndex, SudokuElementType type)
        {
            Left = left;
            Top = top;
            RowIndex = rowIndex;
            ColIndex = colIndex;
            FillColor = Brushes.DarkGray;
            SudokuElemType = type;
            Location = ElementLocationType.Cell;
        }
    }
}
