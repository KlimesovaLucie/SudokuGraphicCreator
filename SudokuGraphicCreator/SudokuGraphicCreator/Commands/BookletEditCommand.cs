using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System.Collections.Specialized;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command is responsible for showing <see cref="EditBooklet"/> dialog window.
    /// </summary>
    public class BookletEditCommand : BaseCommand
    {
        private readonly ICreatingBookletViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of <see cref="BookletEditCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingBooklet"/> view.</param>
        public BookletEditCommand(ICreatingBookletViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Pages.CollectionChanged += PagesCollectionChanged;
        }

        private void PagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
            if (_viewModel.Pages.Count == 1)
            {
                _viewModel.Pages[0].Sudoku.CollectionChanged += SudokuCollectionChanged;
            }
        }

        private void SudokuCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        /// <summary>
        /// Show <see cref="EditBooklet"/> dialog window.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            App.DialogService.ShowDialog(new EditBookletViewModel(_viewModel));
            SetCorrectOrders();
        }

        private void SetCorrectOrders()
        {
            int order = 1;
            foreach (var page in _viewModel.Pages)
            {
                foreach (var sudoku in page.Sudoku)
                {
                    sudoku.OrderNumber = order;
                    order++;
                }
            }
        }

        /// <summary>
        /// Can execute only if booklet has at least one page and on the first page are two tables.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if booklet has at least one page and on the first page are two tables, otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            return _viewModel.Pages.Count > 0 && _viewModel.Pages[0].Sudoku.Count == 2;
        }
    }
}
