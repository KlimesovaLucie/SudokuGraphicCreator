using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Provides informations for deleting sudoku from booklet.
    /// </summary>
    public interface IDeleteSudokuTableViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Selected sudoku for delete.
        /// </summary>
        SudokuInBookletViewModel SelectedSudoku { get; set; }

        /// <summary>
        /// Invoke <see cref="PropertyChanged"/> for <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Name of changed property.</param>
        public void OnPropertyChanged(string name = null);

        /// <summary>
        /// All sudoku in booklet.
        /// </summary>
        ObservableCollection<SudokuInBookletViewModel> AllSudoku { get; }
    }
}
