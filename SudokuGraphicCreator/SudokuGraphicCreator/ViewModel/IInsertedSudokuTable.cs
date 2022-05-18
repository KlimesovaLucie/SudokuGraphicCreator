using System.ComponentModel;

namespace SudokuGraphicCreator.ViewModel
{
    public interface IInsertedSudokuTable : INotifyPropertyChanged
    {
        /// <summary>
        /// true if user wants to generate solution for inserted sudoku.
        /// </summary>
        bool GenerateSolution { get; }

        /// <summary>
        /// Rules of inserted sudoku.
        /// </summary>
        string Rules { get; set; }
    }
}
