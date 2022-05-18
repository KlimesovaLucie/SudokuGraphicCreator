using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System.ComponentModel;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Ok action for <see cref="EditSudokuTableViewModel"/> class.
    /// </summary>
    public class OkSudokuTableEditCommand : BaseCommand
    {
        private ISudokuInBookletViewModel _sudokuViewModel;

        private readonly EditSudokuTableViewModel _editViewModel;

        /// <summary>
        /// Initializes a new instance of <see cref="OkSudokuTableEditCommand"/> class.
        /// </summary>
        /// <param name="sudokuViewModel">Editing sudoku.</param>
        /// <param name="insertedSudokuTable">The viewModel class of <see cref="EditSudokuTable"/> view.</param>
        public OkSudokuTableEditCommand(ISudokuInBookletViewModel sudokuViewModel, EditSudokuTableViewModel insertedSudokuTable)
        {
            _sudokuViewModel = sudokuViewModel;
            _editViewModel = insertedSudokuTable;
            _sudokuViewModel.PropertyChanged += SudokuViewModelPropertyChanged;
            _editViewModel.PropertyChanged += InsertSudokuTablePropertyChanged;
        }

        /// <summary>
        /// Change actual editing sudoku.
        /// </summary>
        /// <param name="newSudoku">New sudoku for edit.</param>
        public void ChangeActualSudoku(ISudokuInBookletViewModel newSudoku)
        {
            _sudokuViewModel.PropertyChanged -= SudokuViewModelPropertyChanged;
            _sudokuViewModel = newSudoku;
            _sudokuViewModel.PropertyChanged += SudokuViewModelPropertyChanged;
        }

        private void InsertSudokuTablePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_editViewModel.GenerateSolution) || e.PropertyName == nameof(_editViewModel.SelectedSudoku) ||
                e.PropertyName == nameof(_editViewModel.Name) || e.PropertyName == nameof(_editViewModel.Points) ||
                e.PropertyName == nameof(_editViewModel.Rules) || e.PropertyName == nameof(_editViewModel.SudokuTable) ||
                e.PropertyName == nameof(_editViewModel.SudokuTableName) || e.PropertyName == nameof(_editViewModel.SudokuTableFullPath) ||
                e.PropertyName == nameof(_editViewModel.SudokuSolution) || e.PropertyName == nameof(_editViewModel.SudokuSolutionName) ||
                e.PropertyName == nameof(_editViewModel.SudokuSolutionFullPath) || e.PropertyName == nameof(_editViewModel.IsSelectedSupported) ||
                e.PropertyName == nameof(_editViewModel.IsSelectedUnsupported))
            {
                OnCanExecutedChanged();
            }
        }

        private void SudokuViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_sudokuViewModel.Name) || e.PropertyName == nameof(_sudokuViewModel.Rules) ||
                e.PropertyName == nameof(_sudokuViewModel.Points) || e.PropertyName == nameof(_sudokuViewModel.TableName) ||
                e.PropertyName == nameof(_sudokuViewModel.SolutionName))
            {
                OnCanExecutedChanged();
            }
        }

        /// <summary>
        /// Command can be executed if all informations are properly filled.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if command can be executed, otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            bool result =  !string.IsNullOrEmpty(_sudokuViewModel.Name) && !string.IsNullOrEmpty(_sudokuViewModel.Rules) &&
                _sudokuViewModel.Points > 0 && !string.IsNullOrEmpty(_sudokuViewModel.TableName) &&
                (!string.IsNullOrEmpty(_sudokuViewModel.SolutionName) || _editViewModel.GenerateSolution);

            _editViewModel.IsSelectedEnable = result;

            return result;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked. Close window and generate solution if have to.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            if (_editViewModel.GenerateSolution)
            {
                IO.SvgImageImport.OpenSvg(_sudokuViewModel.TableFullPath, _sudokuViewModel.TableName);
                _sudokuViewModel.SolutionFullPath = "./GeneratedSolutions/" + "sol_" + _sudokuViewModel.TableName;
                _sudokuViewModel.SolutionName = "sol_" + _sudokuViewModel.TableName;
            }

            _editViewModel.CloseWindowWithOk();
        }
    }
}
