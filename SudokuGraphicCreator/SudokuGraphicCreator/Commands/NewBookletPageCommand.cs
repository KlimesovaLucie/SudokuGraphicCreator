using SudokuGraphicCreator.ViewModel;
using System.Collections.Specialized;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Thic command is responsible for creating new page in booklet.
    /// </summary>
    public class NewBookletPageCommand : BaseCommand
    {
        private readonly ICreatingBookletViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of <see cref="NewBookletPageCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingBooklet"/> view.</param>
        public NewBookletPageCommand(ICreatingBookletViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Pages.CollectionChanged += PagesCollectionChanged;
            _viewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        private void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.Pages))
            {
                OnCanExecutedChanged();
            }
        }

        private void PagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
            foreach (var page in _viewModel.Pages)
            {
                page.Sudoku.CollectionChanged -= SudokuCollectionChanged;
                page.Sudoku.CollectionChanged += SudokuCollectionChanged;
            }
        }

        /// <summary>
        /// Add new page to booklet.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            PageViewModel newPage = new PageViewModel(_viewModel.Pages.Count + 1);
            newPage.Sudoku.CollectionChanged += SudokuCollectionChanged;
            _viewModel.Pages.Add(newPage);
        }

        private void SudokuCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        /// <summary>
        /// New page button is enabled when booklet has zero pages or all pages all full.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>If pages are full or booklet has zero pages, returns true. Otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            return _viewModel.Pages.Count == 0 || _viewModel.Pages[_viewModel.Pages.Count - 1].Sudoku.Count == 2;
        }
    }
}
