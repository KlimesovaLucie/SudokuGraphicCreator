using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System.Threading;
using SudokuGraphicCreator.View;
using System.Threading.Tasks;
using System.Windows;
using SudokuGraphicCreator.Rules;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command is responsible for show dialog window <see cref="CountOfSolutions"/>. And starts computation of count of solution. 
    /// </summary>
    public class CountSolutionCommand : BaseCommand
    {
        private CountOfSolutionsViewModel _solutionViewModel;

        /// <summary>
        /// Initializes a new instance of <see cref="CountSolutionCommand"/> class.
        /// </summary>
        public CountSolutionCommand()
        {
            SudokuStore.Instance.Sudoku.SudokuVariants.CollectionChanged += SudokuVariantsCollectionChanged;
        }

        private void SudokuVariantsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        /// <summary>
        /// Count of solution is valid only for supported variants.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if sudoku computation of solution can be invoked, otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            foreach (var elem in SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                if (elem.SudokuElemType == Model.SudokuElementType.NoMeaning)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Start computation of count of solution and show <see cref="CountOfSolutions"/> dialog window.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            int[,] grid = new int[SudokuStore.Instance.Sudoku.Grid.Size, SudokuStore.Instance.Sudoku.Grid.Size];

            CopyGivenNumbers(grid, SudokuStore.Instance.Sudoku.GivenNumbers, SudokuStore.Instance.Sudoku.Grid.Size);
            if (!ClassicRules.IsCorrectByClassic(grid, grid.GetLength(0)))
            {
                return;
            }

            StartSolveSudoku(grid);
        }

        private void StartSolveSudoku(int[,] grid)
        {
            using var cancelTokenSrc = new CancellationTokenSource();
            _solutionViewModel = new CountOfSolutionsViewModel(cancelTokenSrc);
            var solveTask = new TaskFactory().StartNew(
                () => _solutionViewModel.SetSolutionCount(DoSolutionCount(grid, cancelTokenSrc.Token)),
                cancelTokenSrc.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            DisplayMessage();
            solveTask.Wait();
        }

        private void CopyGivenNumbers(int[,] firstGrid, int[,] secondGrid, int gridSize)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    firstGrid[i, j] = secondGrid[i, j];
                }
            }
        }

        private int DoSolutionCount(int[,] grid, CancellationToken token)
        {
            int count = 0;
            int[,] finalSolution = new int[SudokuStore.Instance.Sudoku.Grid.Size, SudokuStore.Instance.Sudoku.Grid.Size];
            InvokeRightAlgo(grid, ref count, finalSolution, token);
            if (count == 1)
            {
                int size = SudokuStore.Instance.Sudoku.Grid.Size;
                int xSize = SudokuStore.Instance.Sudoku.Grid.XBoxCells;
                int ySize = SudokuStore.Instance.Sudoku.Grid.YBoxCells;
                SudokuStore.Instance.Solution = new Model.Sudoku(size, xSize, ySize);
                for (int i = 0; i < SudokuStore.Instance.Sudoku.Grid.Size; i++)
                {
                    for (int j = 0; j < SudokuStore.Instance.Sudoku.Grid.Size; j++)
                    {
                        SudokuStore.Instance.Solution.GivenNumbers[i, j] = finalSolution[i, j];
                    }
                }
            }
            ChangeGridVisibily();
            return count;
        }

        private void InvokeRightAlgo(int[,] grid, ref int count, int[,] finalSolution, CancellationToken token)
        {
            if (SudokuStore.Instance.Sudoku.Variants.Contains(Model.SudokuType.GreaterThan))
            {
                SolveSudoku.SolveGTWithAll(grid, SudokuStore.Instance.Sudoku.Grid.Size, ref count, finalSolution, token);
            }
            else
            {
                SolveSudoku.Solve(grid, SudokuStore.Instance.Sudoku.Grid.Size, 0, 0, ref count, finalSolution, token);
            }
        }

        private void DisplayMessage()
        {
            App.DialogService.ShowDialog(_solutionViewModel);
        }

        private void ChangeGridVisibily()
        {
            _solutionViewModel.CalculationGridVisibility = Visibility.Hidden;
            _solutionViewModel.SolutionGridVisibility = Visibility.Visible;
        }
    }
}
