using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command is responsible for displaying <see cref="InsertNewSudokuTable"/> dialog window and creating new sudoku in booklet.
    /// </summary>
    public class NewSudokuTableBookletCommand : BaseCommand
    {
        private readonly ICreatingBookletViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of <see cref="NewSudokuTableBookletCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingBooklet"/> view.</param>
        public NewSudokuTableBookletCommand(ICreatingBookletViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Pages.CollectionChanged += PagesCollectionChanged;
            _viewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.Pages))
            {
                OnCanExecutedChanged();
            }
        }

        private void PagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        /// <summary>
        /// Display <see cref="InsertNewSudokuTable"/> dialog window and create new sudoku in booklet.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            DisplayInsertNewTableMessage();
        }

        private void DisplayInsertNewTableMessage()
        {
            SudokuInBookletViewModel sudokuInBooklet = new SudokuInBookletViewModel();
            bool? result = App.DialogService.ShowDialog(new InsertNewSudokuTableViewModel(sudokuInBooklet, CalculateOrderNumber(), _viewModel));

            if (result.HasValue)
            {
                if (result.Value)
                {
                    _viewModel.Pages[_viewModel.Pages.Count - 1].AddSudoku(sudokuInBooklet);
                    sudokuInBooklet.OrderNumber = CalculateOrderNumber();
                    PagesCollectionChanged(null, null);
                    ChangeVisibilityLastAddingSudoku();
                }
            }
        }

        private int CalculateOrderNumber()
        {
            int result = _viewModel.Pages.Count * 2;
            if (_viewModel.Pages[_viewModel.Pages.Count - 1].Sudoku.Count == 2)
            {
                return result;
            }
            return result - 1;
        }

        private void ChangeVisibilityLastAddingSudoku()
        {
            if (_viewModel.LeftSudokuVisibility == Visibility.Hidden)
            {
                ChangeLeftSudokuVisibility();
            }
            else
            {
                ChangeRightSudokuVisibility();
            }
        }

        private void ChangeLeftSudokuVisibility()
        {
            _viewModel.LeftSudokuVisibility = _viewModel.LeftSudokuVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void ChangeRightSudokuVisibility()
        {
            _viewModel.RightSudokuVisibility = _viewModel.RightSudokuVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        /// <summary>
        /// This command can be executed when booklet has empty space on some of his pages.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if new sudoku can be placed in booklet, otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            return _viewModel.Pages.Count != 0 && _viewModel.Pages[_viewModel.Pages.Count - 1].Sudoku.Count != 2;
        }
    }
}
