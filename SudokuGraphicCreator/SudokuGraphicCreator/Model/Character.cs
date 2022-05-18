namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents character as graphic element.
    /// </summary>
    public class Character : OneSpaceElement
    {
        /// <summary>
        /// Value in grid.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="Character"/> class.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance form left up corner of grid.</param>
        /// <param name="rowIndex">Index of row in sudoku table.</param>
        /// <param name="colIndex">Index of column in sudoku table.</param>
        /// <param name="value">Text displayed in grid.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="location">Location of element in grid.</param>
        public Character(double left, double top, int rowIndex, int colIndex, string value, SudokuElementType type, ElementLocationType location)
        {
            Left = left;
            Top = top;
            RowIndex = rowIndex;
            ColIndex = colIndex;
            Text = value;
            SudokuElemType = type;
            Location = location;
        }
    }
}
