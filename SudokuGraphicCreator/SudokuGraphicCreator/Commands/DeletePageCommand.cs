using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Command for deleting selected booklet page.
    /// </summary>
    public class DeletePageCommand : BaseCommand
    {
        private readonly ICreatingBookletViewModel _viewModelCreate;

        private readonly IDeleteSudokuPageViewModel _viewModelDelete;

        /// <summary>
        /// Initializes a new instance of <see cref="DeletePageCommand"/> class.
        /// </summary>
        /// <param name="viewModelCreate">ViewModel class for <see cref="CreatingBooklet"/> view.</param>
        /// <param name="viewModelDelete">ViewModel class for <see cref="DeleteSudokuPage"/> view.</param>
        public DeletePageCommand(ICreatingBookletViewModel viewModelCreate, IDeleteSudokuPageViewModel viewModelDelete)
        {
            _viewModelCreate = viewModelCreate;
            _viewModelDelete = viewModelDelete;
        }

        /// <summary>
        /// Determine if <see cref="DeletePageCommand"/> can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if booklet has at least one page, othewise false.</returns>
        public override bool CanExecute(object parameter)
        {
            return _viewModelDelete.Pages.Count > 0;
        }

        /// <summary>
        /// Delete selected page from booklet.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            PageViewModel page = FindPage();
            _viewModelCreate.Pages.Remove(page);
            page.RemoveFromModel();
            _viewModelDelete.OnPropertyChanged(nameof(_viewModelDelete.Pages));
            ChangeNumberOfPages();
            OnCanExecutedChanged();
        }

        private PageViewModel FindPage()
        {
            foreach (var page in _viewModelCreate.Pages)
            {
                if (page.PageNumber == _viewModelDelete.SelectedPageOrder)
                {
                    return page;
                }
            }
            return null;
        }

        private void ChangeNumberOfPages()
        {
            for (int i = 0; i < _viewModelCreate.Pages.Count; i++)
            {
                _viewModelCreate.Pages[i].PageNumber = i + 1;
            }
        }
    }
}
