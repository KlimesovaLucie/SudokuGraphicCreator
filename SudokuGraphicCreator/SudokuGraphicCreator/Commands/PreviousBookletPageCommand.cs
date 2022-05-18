using SudokuGraphicCreator.ViewModel;
using System.Windows;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command show previous page from booklet.
    /// </summary>
    public class PreviousBookletPageCommand : BaseCommand
    {
        private readonly ICreatingBookletViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of <see cref="PreviousBookletPageCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingBooklet"/> view.</param>
        public PreviousBookletPageCommand(ICreatingBookletViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        private void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.ActualPage))
            {
                OnCanExecutedChanged();
            }
        }

        /// <summary>
        /// This command can be executed when actual showed page is not the first in booklet.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if is not showing first page of booklet, otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            return _viewModel.ActualPage != null;
        }

        /// <summary>
        /// Show previous page from booklet.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            if (_viewModel.ActualPage == null && _viewModel.Pages.Count == 0)
            {
                return;
            }
            int newIndex = _viewModel.ActualPage.PageNumber - 2;
            if (newIndex == -1)
            {
                ChangeSudokuPageVisibility();
                ChangeIntroductionPageVisibility();
                _viewModel.ActualPage = null;
                return;
            }
            _viewModel.ActualPage = _viewModel.Pages[newIndex];
            SudokuVisibilityOnActualPage();
        }

        private void ChangeSudokuPageVisibility()
        {
            _viewModel.SudokuPageVisibility = _viewModel.SudokuPageVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void ChangeIntroductionPageVisibility()
        {
            _viewModel.IntroductionPageVisibility = _viewModel.IntroductionPageVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void SudokuVisibilityOnActualPage()
        {
            if (_viewModel.ActualPage.Sudoku[0].Points == 0)
            {
                _viewModel.LeftSudokuVisibility = Visibility.Hidden;
            }
            else
            {
                _viewModel.LeftSudokuVisibility = Visibility.Visible;
            }

            if (_viewModel.ActualPage.Sudoku[1].Points == 0)
            {
                _viewModel.RightSudokuVisibility = Visibility.Hidden;
            }
            else
            {
                _viewModel.RightSudokuVisibility = Visibility.Visible;
            }
        }
    }
}
