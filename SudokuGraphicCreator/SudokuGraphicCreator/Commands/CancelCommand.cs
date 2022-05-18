using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This class cancel selected cell.
    /// </summary>
    public class CancelCommand : BaseCommand
    {
        private readonly CreatingSudokuViewModel _viewModel;

        private readonly string _nameOfButton;

        /// <summary>
        /// This method sets to all selected cell transparent color and clear this selection.
        /// </summary>
        /// <param name="viewModel"></param>
        public CancelCommand(CreatingSudokuViewModel viewModel, string nameOfButton = null)
        {
            _viewModel = viewModel;
            _viewModel.SelectedCells.CollectionChanged += SelectedCellsCollectionChanged;
            _nameOfButton = nameOfButton;
        }

        private void SelectedCellsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        /// <summary>
        /// This command can be execute only if is selected at least one cell.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return FindCheckedButton() == _nameOfButton && _viewModel.SelectedCells.Count > 0;
        }

        /// <summary>
        /// Unselects all cells and change their color.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            foreach (var item in _viewModel.SelectedCells)
            {
                item.Background = item.DefaultBrush;
            }
            _viewModel.SelectedCells.Clear();
        }

        private string FindCheckedButton()
        {
            foreach (var button in _viewModel.ButtonsConfirm)
            {
                if (button.Checked && button.NameOfElement == _nameOfButton)
                {
                    return _nameOfButton;
                }
            }
            return "";
        }
    }
}
