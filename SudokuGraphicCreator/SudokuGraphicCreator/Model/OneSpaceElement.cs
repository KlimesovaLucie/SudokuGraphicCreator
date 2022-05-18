namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents sudoku graphic element which lies on one space in sudoku table.
    /// </summary>
    public abstract class OneSpaceElement : SudokuElement
    {
        /// <summary>
        /// Left distance from left up corner of grid.
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// Top distance form left up corner of grid.
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// Index of row in sudoku table.
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// Index of col in sudoku table.
        /// </summary>
        public int ColIndex { get; set; }

        /// <summary>
        /// Type of location in table.
        /// </summary>
        public ElementLocationType Location { get; set; }
    }
}
