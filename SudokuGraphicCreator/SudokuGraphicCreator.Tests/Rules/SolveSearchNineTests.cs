using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveSearchNineTests
    {
        [Test]
        public void SolveSearchNine_OneSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.SearchNine);
            SudokuStore.Instance.Sudoku = sudoku;

            SudokuStore.Instance.Sudoku.SudokuVariants.Add(new BoldArrow(0, 0, 0, 2, SudokuElementType.SearchNineRight, GraphicElementType.Right));
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(new BoldArrow(0, 0, 1, 7, SudokuElementType.SearchNineRight, GraphicElementType.Right));

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            string solutionString = "162498537374256819985173462527641983639825741418739625741582396253964178896317254";
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void SolveSearchNine_ZeroSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.SearchNine);
            SudokuStore.Instance.Sudoku = sudoku;

            SudokuStore.Instance.Sudoku.SudokuVariants.Add(new BoldArrow(0, 0, 0, 2, SudokuElementType.SearchNineLeft, GraphicElementType.Left));
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(new BoldArrow(0, 0, 1, 7, SudokuElementType.SearchNineRight, GraphicElementType.Right));

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "count was " + countSolution);
        }
    }
}
