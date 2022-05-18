using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System.ComponentModel;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Ok action for <see cref="InsertNewSudokuTableViewModel"/> class.
    /// </summary>
    public class OkSudokuTableCommand : BaseCommand
    {
        private readonly ISudokuInBookletViewModel _sudokuViewModel;

        private readonly InsertNewSudokuTableViewModel _insertedSudokuTable;

        /// <summary>
        /// Initializes a new instance of <see cref="OkSudokuTableCommand"/> class.
        /// </summary>
        /// <param name="sudokuViewModel">Inserting sudoku.</param>
        /// <param name="insertedSudokuTable">The viewModel class of <see cref="InsertNewSudokuTable"/> view.</param>
        public OkSudokuTableCommand(ISudokuInBookletViewModel sudokuViewModel, InsertNewSudokuTableViewModel insertedSudokuTable)
        {
            _sudokuViewModel = sudokuViewModel;
            _insertedSudokuTable = insertedSudokuTable;
            _sudokuViewModel.PropertyChanged += SudokuViewModelPropertyChanged;
            _insertedSudokuTable.PropertyChanged += InsertSudokuTablePropertyChanged;
        }

        private void InsertSudokuTablePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_insertedSudokuTable.GenerateSolution) ||
                e.PropertyName == nameof(_insertedSudokuTable.Name) || e.PropertyName == nameof(_insertedSudokuTable.Points) ||
                e.PropertyName == nameof(_insertedSudokuTable.Rules) || e.PropertyName == nameof(_insertedSudokuTable.SudokuTable) ||
                e.PropertyName == nameof(_insertedSudokuTable.SudokuTableName) || e.PropertyName == nameof(_insertedSudokuTable.SudokuTableFullPath) ||
                e.PropertyName == nameof(_insertedSudokuTable.SudokuSolution) || e.PropertyName == nameof(_insertedSudokuTable.SudokuSolutionName) ||
                e.PropertyName == nameof(_insertedSudokuTable.SudokuSolutionFullPath) || e.PropertyName == nameof(_insertedSudokuTable.IsSelectedSupported) ||
                e.PropertyName == nameof(_insertedSudokuTable.IsSelectedUnsupported))
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
            return !string.IsNullOrEmpty(_sudokuViewModel.Name) && !string.IsNullOrEmpty(_sudokuViewModel.Rules) &&
                _sudokuViewModel.Points > 0 && !string.IsNullOrEmpty(_sudokuViewModel.TableName) &&
                (!string.IsNullOrEmpty(_sudokuViewModel.SolutionName) || _insertedSudokuTable.GenerateSolution);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked. Close window and generate solution if have to.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            if (_insertedSudokuTable.GenerateSolution)
            {
                IO.SvgImageImport.OpenSvg(_sudokuViewModel.TableFullPath, _sudokuViewModel.TableName);
                _sudokuViewModel.SolutionFullPath = "./GeneratedSolutions/" + "sol_" + _sudokuViewModel.TableName;
                _sudokuViewModel.SolutionName = "sol_" + _sudokuViewModel.TableName;
            }
            
            _insertedSudokuTable.CloseWindowWithOk();
        }
    }
}
