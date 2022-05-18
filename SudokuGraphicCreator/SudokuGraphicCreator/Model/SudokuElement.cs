namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents sudoku graphic element.
    /// </summary>
    public abstract class SudokuElement
    {
        /// <summary>
        /// Type of sudoku graphic element.
        /// </summary>
        public SudokuElementType SudokuElemType { get; set; }
    }
}
