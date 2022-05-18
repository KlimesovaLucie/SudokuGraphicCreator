using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command is responsible for showing <see cref="EditSudokuTable"/> dialog window.
    /// </summary>
    public class SudokuEditCommand : BaseCommand
    {
        private readonly ICreatingBookletViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of <see cref="SudokuEditCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingBooklet"/> view.</param>
        public SudokuEditCommand(ICreatingBookletViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Pages.CollectionChanged += PagesCollectionChanged;
        }

        private void PagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        /// <summary>
        /// Show <see cref="EditSudokuTable"/> dialog window.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            List<SudokuInBookletViewModel> sudoku = GetCopyOfSudokuInBooklet();
            bool? result = App.DialogService.ShowDialog(new EditSudokuTableViewModel(_viewModel));

            if (result.HasValue)
            {
                if (!result.Value)
                {
                    SaveBackChanges(sudoku);
                }
            }
        }

        /// <summary>
        /// Can execute when booklet has at least one sudoku.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if booklet has at least one sudoku, otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            return _viewModel.Pages.Count != 0 && _viewModel.Pages[0].Sudoku.Count > 0;
        }

        private List<SudokuInBookletViewModel> GetCopyOfSudokuInBooklet()
        {
            var result = new List<SudokuInBookletViewModel>();

            foreach (var page in _viewModel.Pages)
            {
                foreach (var sudoku in page.Sudoku)
                {
                    result.Add(CreateCopyOfSudoku(sudoku));
                }
            }

            return result;
        }

        private SudokuInBookletViewModel CreateCopyOfSudoku(SudokuInBookletViewModel previous)
        {
            var result = new SudokuInBookletViewModel();
            result.Name = previous.Name;
            result.OrderNumber = previous.OrderNumber;
            result.Points = previous.Points;
            result.Rules = previous.Rules;
            result.Table = previous.Table;
            result.TableName = previous.TableName;
            result.TableFullPath = previous.TableFullPath;
            result.Solution = previous.Solution;
            result.SolutionName = previous.SolutionName;
            result.SolutionFullPath = previous.SolutionFullPath;
            return result;
        }

        private void SaveBackChanges(List<SudokuInBookletViewModel> previousSudoku)
        {
            int index = 0;
            foreach (var page in _viewModel.Pages)
            {
                page.Sudoku[0] = previousSudoku[index];
                index++;
                if (page.Sudoku.Count == 2)
                {
                    page.Sudoku[1] = previousSudoku[index];
                    index++;
                }
            }
        }
    }
}
