using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Provides opportunity for creating booklet.
    /// </summary>
    public interface ICreatingBookletViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Pages of booklet.
        /// </summary>
        ObservableCollection<PageViewModel> Pages { get; }

        /// <summary>
        /// <see cref="Visibility"/> of left paged sudoku on booklet page.
        /// </summary>
        Visibility LeftSudokuVisibility { get; set; }

        /// <summary>
        /// <see cref="Visibility"/> of right paged sudoku on booklet page.
        /// </summary>
        Visibility RightSudokuVisibility { get; set; }

        /// <summary>
        /// Actual showed page from booklet.
        /// </summary>
        PageViewModel ActualPage { get; set; }

        /// <summary>
        /// <see cref="Visibility"/> of page from booklet.
        /// </summary>
        Visibility SudokuPageVisibility { get; set; }

        /// <summary>
        /// <see cref="Visibility"/> of introduction page of booklet.
        /// </summary>
        Visibility IntroductionPageVisibility { get; set; }

        /// <summary>
        /// All sudoku in booklet.
        /// </summary>
        ObservableCollection<SudokuInBookletViewModel> ListOfSudoku { get; }

        /// <summary>
        /// Command for showing previous page in booklet.
        /// </summary>
        ICommand PreviousPageCommand { get; }
    }
}
