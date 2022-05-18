using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System.Collections.Specialized;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command is responsible for showing <see cref="DeleteSudokuTable"/> dialog window.
    /// </summary>
    public class SudokuDeleteWindowCommand : BaseCommand
    {
        private readonly ICreatingBookletViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of <see cref="SudokuDeleteWindowCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingBooklet"/> view.</param>
        public SudokuDeleteWindowCommand(ICreatingBookletViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Pages.CollectionChanged += PagesCollectionChanged;
            _viewModel.ListOfSudoku.CollectionChanged += ListOfSudokuCollectionChanged;
        }

        private void ListOfSudokuCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        private void PagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        /// <summary>
        /// Show <see cref="DeleteSudokuTable"/> dialog window.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            App.DialogService.ShowDialog(new DeleteSudokuTableViewModel(_viewModel));
        }

        /// <summary>
        /// This command can be executed if booklet has at leat one sudoku.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return _viewModel.Pages.Count > 0 && _viewModel.Pages[_viewModel.Pages.Count - 1].Sudoku.Count > 0;
        }
    }
}
