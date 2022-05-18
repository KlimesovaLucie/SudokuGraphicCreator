using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.View;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// The viewModel class for <see cref="EditSudokuTable"/> view. It is responsible for editing sudoku in booklet.
    /// </summary>
    public class EditSudokuTableViewModel : InsertNewSudokuTableViewModel
    {
        private readonly ObservableCollection<PageViewModel> _bookletPages;

        /// <summary>
        /// Selected sudoku for editing.
        /// </summary>
        public ISudokuInBookletViewModel SelectedSudoku
        {
            get => _sudokuViewModel;
            set
            {
                _sudokuViewModel = value;
                ChangeSudokuInCommand(value);
                OnPropertyChanged(nameof(SelectedSudoku));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Points));
                OnPropertyChanged(nameof(Rules));
                OnPropertyChanged(nameof(SudokuTableName));
                OnPropertyChanged(nameof(SudokuSolutionName));
                OnPropertyChanged(nameof(GenerateSolution));
            }
        }

        /// <summary>
        /// True if selection of sudoku is enabled, otherwise false.
        /// </summary>
        private bool _isSelectedEnabled;

        public bool IsSelectedEnable
        {
            get => _isSelectedEnabled;
            set
            {
                _isSelectedEnabled = value;
                OnPropertyChanged(nameof(IsSelectedEnable));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EditSudokuTableViewModel"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class of <see cref="CreatingBooklet"/> view.</param>
        public EditSudokuTableViewModel(ICreatingBookletViewModel viewModel)
            : base(viewModel.Pages[0].Sudoku[0], viewModel.Pages[0].Sudoku[0].OrderNumber, viewModel)
        {
            _bookletPages = viewModel.Pages;
            _sudokuViewModel = _bookletPages[0].Sudoku[0];
            OkCommand = new OkSudokuTableEditCommand(_sudokuViewModel, this);
        }

        private void ChangeSudokuInCommand(ISudokuInBookletViewModel sudoku)
        {
            OkSudokuTableEditCommand command = OkCommand as OkSudokuTableEditCommand;
            if (command == null)
            {
                return;
            }
            command.ChangeActualSudoku(sudoku);
        }
    }
}
