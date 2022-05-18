using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command show next page from booklet.
    /// </summary>
    public class NextBookletPageCommand : BaseCommand
    {
        private readonly ICreatingBookletViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of <see cref="NextBookletPageCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingBooklet"/> view.</param>
        public NextBookletPageCommand(ICreatingBookletViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Pages.CollectionChanged += PagesCollectionChanged;
            _viewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.Pages) || e.PropertyName == nameof(_viewModel.ActualPage))
            {
                OnCanExecutedChanged();
            }
        }

        private void PagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        /// <summary>
        /// This command can be executed when actual showed page is not the last in booklet.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if is not showing last page of booklet, otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            return (_viewModel.ActualPage == null && _viewModel.Pages.Count > 0) ||
                (_viewModel.ActualPage != null && _viewModel.ActualPage.PageNumber < _viewModel.Pages.Count);
        }

        /// <summary>
        /// Show next page from booklet.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            if (_viewModel.ActualPage == null && _viewModel.Pages.Count == 0)
            {
                return;
            }
            int newIndex = 0;
            if (_viewModel.ActualPage == null)
            {
                ChangeSudokuPageVisibility();
                ChangeIntroductionPageVisibility();
            }
            else
            {
                newIndex = _viewModel.ActualPage.PageNumber;
            }
            _viewModel.ActualPage = _viewModel.Pages[newIndex];

            SudokuVisibilityOnActualPage();
            OnCanExecutedChanged();
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
            for (int i = 0; i < _viewModel.ActualPage.Sudoku.Count; i++)
            {
                ChangeSudokuVisibility(i);
            }
        }

        private void ChangeSudokuVisibility(int index)
        {
            if (index == 0)
            {
                _viewModel.LeftSudokuVisibility = Visibility.Visible;
            }
            else if (index == 1)
            {
                _viewModel.RightSudokuVisibility = Visibility.Visible;
            }
        }
    }
}
