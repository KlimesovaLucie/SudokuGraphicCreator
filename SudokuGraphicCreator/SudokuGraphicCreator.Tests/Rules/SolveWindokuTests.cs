using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveWindokuTests
    {
        [Test]
        public void SolveWindoku_OneSolution()
        {
            string inputString = "000203080000000513004100060109040006000506000200030401010009800425000000060305000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Windoku);
            SudokuStore.Instance.Sudoku = sudoku;
            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "Count solutions was " + countSolution);
            string solutionString = "691253784872694513534178962159842376347516298286937451713469825425781639968325147";
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void SolveWindoku_ZeroSolution()
        {
            string inputString = "009203080000000513004100060109040006000506000200030401010009800425000000060305000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Windoku);
            SudokuStore.Instance.Sudoku = sudoku;
            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "Count solutions was " + countSolution);
        }

        [Test]
        public void SolveWindoku_MultipleSolution()
        {
            int[,] givenNumber = SudokuRulesUtilities.CreateEmptyGivenNumbers(9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Windoku);
            SudokuStore.Instance.Sudoku = sudoku;
            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 2, "Count solutions was " + countSolution);
        }
    }
}
