using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// The viewModel class for <see cref="BookletPage"/> model. Presents one page in booklet with sudoku.
    /// </summary>
    public class PageViewModel : BaseViewModel
    {
        private readonly BookletPage _bookletPage;

        private ObservableCollection<SudokuInBookletViewModel> _sudoku = new ObservableCollection<SudokuInBookletViewModel>();

        /// <summary>
        /// Collection of sudoku on this page in booklet.
        /// </summary>
        public ObservableCollection<SudokuInBookletViewModel> Sudoku
        {
            get => _sudoku;
            set
            {
                _sudoku = value;
                OnPropertyChanged(nameof(Sudoku));
            }
        }

        /// <summary>
        /// Order number of page.
        /// </summary>
        public int PageNumber
        {
            get => _bookletPage.Order;
            set
            {
                _bookletPage.Order = value;
                OnPropertyChanged(nameof(PageNumber));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PageViewModel"/> class.
        /// </summary>
        /// <param name="pageNumber">Order number of page.</param>
        public PageViewModel(int pageNumber)
        {
            _bookletPage = new BookletPage(pageNumber);
            BookletStore.Instance.Booklet.AddPage(_bookletPage);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PageViewModel"/> class. Used for xml deserialization.
        /// </summary>
        private PageViewModel()
        {
            _bookletPage = new BookletPage(0);
            BookletStore.Instance.Booklet.AddPage(_bookletPage);
        }

        /// <summary>
        /// Remove <see cref="BookletPage"/> of this instance from <see cref="Booklet"/>.
        /// </summary>
        public void RemoveFromModel()
        {
            BookletStore.Instance.Booklet.RemovePage(_bookletPage);
        }

        /// <summary>
        /// Add <paramref name="sudokuViewModel"/> into <see cref="Sudoku"/> and corresponding model.
        /// </summary>
        /// <param name="sudokuViewModel"></param>
        public void AddSudoku(SudokuInBookletViewModel sudokuViewModel)
        {
            Sudoku.Add(sudokuViewModel);
            _bookletPage.SudokuOnPage.Add(sudokuViewModel.GetModel());
        }

        /// <summary>
        /// Add <paramref name="sudokuViewModel"/> into sudoku collection.
        /// </summary>
        /// <param name="sudokuViewModel"></param>
        public void AddSudokuInCollection(SudokuInBookletViewModel sudokuViewModel)
        {
            _bookletPage.SudokuOnPage.Add(sudokuViewModel.GetModel());
        }

        /// <summary>
        /// Get model for inserting.
        /// </summary>
        /// <returns></returns>
        public BookletPage GetForInsert()
        {
            return _bookletPage;
        }
    }
}
