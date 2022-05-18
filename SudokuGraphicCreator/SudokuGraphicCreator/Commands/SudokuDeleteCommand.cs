using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Delete selected sudoku a move last on place of deleting sudoku.
    /// </summary>
    public class SudokuDeleteCommand : BaseCommand
    {
        private readonly ICreatingBookletViewModel _viewModelCreate;

        private readonly IDeleteSudokuTableViewModel _viewModelDelete;

        private readonly ObservableCollection<PageViewModel> _pagesInBooklet;

        /// <summary>
        /// Initializes a new instance of <see cref="SudokuDeleteCommand"/> class.
        /// </summary>
        /// <param name="viewModelCreate">ViewModel class of <see cref="CreatingBooklet"/> view.</param>
        /// <param name="viewModelDelete">ViewModel class of <see cref="DeleteSudokuTable"/> view.</param>
        public SudokuDeleteCommand(ICreatingBookletViewModel viewModelCreate, IDeleteSudokuTableViewModel viewModelDelete)
        {
            _viewModelCreate = viewModelCreate;
            _viewModelDelete = viewModelDelete;
            _pagesInBooklet = _viewModelCreate.Pages;
        }

        /// <summary>
        /// Determine if <see cref="SudokuDeleteCommand"/> can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if booklet has at least one sudoku, otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            return _viewModelDelete.AllSudoku.Count > 0;
        }

        /// <summary>
        /// Delete selected sudoku from booklet. If after delete is last page empty, delete it too.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            foreach (var page in _pagesInBooklet)
            {
                for (int i = 0; i < page.Sudoku.Count; i++)
                {
                    if (page.Sudoku[i].Equals(_viewModelDelete.SelectedSudoku))
                    {
                        int order = page.Sudoku[i].OrderNumber;
                        page.Sudoku[i] = FindLastSudoku();
                        page.Sudoku[i].OrderNumber = order;
                    }
                }
            }
            DeleteLastSudoku();
            _viewModelDelete.OnPropertyChanged(nameof(_viewModelDelete.AllSudoku));
            OnCanExecutedChanged();
            Stores.BookletStore.Instance.CreatingBookletViewModel.SudokuTotalPointsChanged();
        }

        private int LastPageSudokuCount()
        {
            return _pagesInBooklet[_pagesInBooklet.Count - 1].Sudoku.Count;
        }

        private SudokuInBookletViewModel FindLastSudoku()
        {
            return _pagesInBooklet[_pagesInBooklet.Count - 1].Sudoku[LastPageSudokuCount() - 1];
        }

        private void DeleteLastSudoku()
        {
            int count = LastPageSudokuCount();
            if (count == 1)
            {
                DeleteLastPage();
                return;
            }
            SudokuInBookletViewModel firstSudoku = _pagesInBooklet[_pagesInBooklet.Count - 1].Sudoku[0];
            _pagesInBooklet[_pagesInBooklet.Count - 1].Sudoku = new ObservableCollection<SudokuInBookletViewModel>();
            _pagesInBooklet[_pagesInBooklet.Count - 1].Sudoku.Add(firstSudoku);
        }

        private void DeleteLastPage()
        {
            int pageCount = _pagesInBooklet.Count;
            if (_viewModelCreate.ActualPage?.PageNumber == pageCount)
            {
                _viewModelCreate.PreviousPageCommand.Execute(null);
            }
            _pagesInBooklet[pageCount - 1].RemoveFromModel();
            _pagesInBooklet.Remove(_pagesInBooklet[pageCount - 1]);
        }
    }
}
