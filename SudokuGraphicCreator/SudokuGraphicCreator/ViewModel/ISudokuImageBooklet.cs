using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Provides name and path for given table of sudoku image and his table with result.
    /// </summary>
    public interface ISudokuImageBooklet : INotifyPropertyChanged
    {
        /// <summary>
        /// Image of sudoku table.
        /// </summary>
        BitmapImage SudokuTable { get; set; }

        /// <summary>
        /// Name of file of image of sudoku table.
        /// </summary>
        string SudokuTableName { get; set; }

        /// <summary>
        /// Full path to image of sudoku table.
        /// </summary>
        string SudokuTableFullPath { get; set; }

        /// <summary>
        /// Image of sudoku table with solution.
        /// </summary>
        BitmapImage SudokuSolution { get; set; }

        /// <summary>
        /// Name of file of sudoku table with result.
        /// </summary>
        string SudokuSolutionName { get; set; }

        /// <summary>
        /// Full path to image of sudoku table with result.
        /// </summary>
        string SudokuSolutionFullPath { get; set; }
    }
}
