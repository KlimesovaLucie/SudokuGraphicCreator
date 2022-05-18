using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System.Collections.Specialized;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command is responsible for showing <see cref="DeleteSudokuPage"/> dialog window.
    /// </summary>
    public class SudokuPageDeleteWindowCommand : BaseCommand
    {
        private readonly ICreatingBookletViewModel _viewModelCreate;

        /// <summary>
        /// Initializes a new instance of <see cref="SudokuPageDeleteWindowCommand"/> class.
        /// </summary>
        /// <param name="viewModelCreate">ViewModel class for <see cref="CreatingBooklet"/> view.</param>
        public SudokuPageDeleteWindowCommand(ICreatingBookletViewModel viewModelCreate)
        {
            _viewModelCreate = viewModelCreate;
            _viewModelCreate.Pages.CollectionChanged += PagesCollectionChanged;
        }

        private void PagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        /// <summary>
        /// Show <see cref="DeleteSudokuPage"/> dialog window.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            App.DialogService.ShowDialog(new DeleteSudokuPageViewModel(_viewModelCreate));
        }

        /// <summary>
        /// Can be invoke if booklet has at least one page.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if booklet has at least one page, otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            return _viewModelCreate.Pages.Count > 0;
        }
    }
}
