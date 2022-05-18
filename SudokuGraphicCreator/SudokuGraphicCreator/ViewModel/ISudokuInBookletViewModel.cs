using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Provides informations for sudoku in booklet.
    /// </summary>
    public interface ISudokuInBookletViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Name of sudoku.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Points of sudoku.
        /// </summary>
        int Points { get; set; }

        /// <summary>
        /// Order number of sudoku in booklet.
        /// </summary>
        int OrderNumber { get; set; }

        /// <summary>
        /// Rules of sudoku.
        /// </summary>
        string Rules { get; set; }

        /// <summary>
        /// Image of sudoku in booklet.
        /// </summary>
        BitmapImage Table { get; set; }

        /// <summary>
        /// Name of file with image of sudoku in booklet.
        /// </summary>
        string TableName { get; set; }

        /// <summary>
        /// Full path in file system to file with image of sudoku in booklet.
        /// </summary>
        string TableFullPath { get; set; }

        /// <summary>
        /// Full path in file system to file with image of sudoku in booklet in format SVG.
        /// </summary>
        string TableFullPathSvg { get; set; }

        /// <summary>
        /// Image with solution of sudoku in booklet.
        /// </summary>
        BitmapImage Solution { get; set; }

        /// <summary>
        /// Name of file with image with solution of sudoku in booklet.
        /// </summary>
        string SolutionName { get; set; }

        /// <summary>
        /// Full path in file system to file with image with solution of sudoku in booklet.
        /// </summary>
        string SolutionFullPath { get; set; }
    }
}
