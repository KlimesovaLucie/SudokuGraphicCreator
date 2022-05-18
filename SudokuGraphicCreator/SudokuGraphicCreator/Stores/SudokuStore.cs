using SudokuGraphicCreator.Model;

namespace SudokuGraphicCreator.Stores
{
    /// <summary>
    /// Class for storing only one instance of sudoku created by this app.
    /// </summary>
    public class SudokuStore
    {
        private static SudokuStore _instance = new SudokuStore();

        /// <summary>
        /// Instance of this class.
        /// </summary>
        public static SudokuStore Instance => _instance;

        /// <summary>
        /// Created sudoku.
        /// </summary>
        public Sudoku Sudoku { get; set; }

        /// <summary>
        /// Solution of created sudoku.
        /// </summary>
        public Sudoku Solution { get; set; }

        private SudokuStore()
        {

        }

        static SudokuStore()
        {

        }
    }
}
