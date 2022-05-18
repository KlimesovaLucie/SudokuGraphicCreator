namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents a graphic element number.
    /// </summary>
    public class Number : OneSpaceElement
    {
        /// <summary>
        /// Value of number.
        /// </summary>
        public int? Value { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="Number"/> class.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance form left up corner of grid.</param>
        /// <param name="value">Value of element.</param>
        /// <param name="type">Type of graphic element.</param>
        public Number(double left, double top, int? value, SudokuElementType type)
        {
            Left = left;
            Top = top;
            Value = value;
            SudokuElemType = type;
        }
    }
}
