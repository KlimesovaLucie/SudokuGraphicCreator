using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveGreaterThanSudokuTests
    {
        
        [Test]
        public void SolveGreaterThan_OneSolution()
        {
            string inputString = "000000000000000000084000016060000030000000000040050020000000095059000000870000040";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.GreaterThan);

            string solutionString = "621538974795146382384297516567429138932681457148753629216374895459812763873965241";
            var creatingSudoku = new ViewModel.CreatingSudokuViewModel();
            var insertSolution = new ViewModel.InsertSolutionViewModel(creatingSudoku);
            insertSolution.InputString = solutionString;
            insertSolution.IsGreaterThanCkecked = true;
            insertSolution.AllGraphicElemChecked = true;
            insertSolution.WithoutNumberChecked = true;
            insertSolution.IsCorrectFormat();
            insertSolution.OkCommand.Execute(null);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1);
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void SolveGreaterThan_ZeroSolution()
        {
            string inputString = "000000000000000000084000016060000030000000000040050020000000095059000000870000040";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.GreaterThan);

            string solutionString = "621538974795146382384297516567429138932681457148753629216374895459812763873965241";
            var creatingSudoku = new ViewModel.CreatingSudokuViewModel();
            var insertSolution = new ViewModel.InsertSolutionViewModel(creatingSudoku);
            insertSolution.InputString = solutionString;
            insertSolution.IsGreaterThanCkecked = true;
            insertSolution.AllGraphicElemChecked = true;
            insertSolution.WithoutNumberChecked = true;
            insertSolution.IsCorrectFormat();
            insertSolution.OkCommand.Execute(null);

            // modify some element
            Character elem = SudokuStore.Instance.Sudoku.SudokuVariants[0] as Character;
            elem.SudokuElemType = SudokuElementType.GreaterThanLeft;

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0);
        }
    }
}
