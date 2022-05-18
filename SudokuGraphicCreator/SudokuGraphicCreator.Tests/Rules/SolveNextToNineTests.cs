using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveNextToNineTests
    {
        [Test]
        public void SolveNextToNine_OneSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.NextToNine);
            SudokuStore.Instance.Sudoku = sudoku;

            sudoku.LeftNumbers = SudokuRulesUtilities.CreateArrayFromInputString("0480010080000000000000000000", 9, 3);
            sudoku.LeftNumbersType = SudokuRulesUtilities.MapTypesByNumbers("0480010080000000000000000000", 9, 3, SudokuElementType.NextToNine);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            string solutionString = "162498537374256819985173462527641983639825741418739625741582396253964178896317254";
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void SolveNextToNine_ZeroSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.NextToNine);
            SudokuStore.Instance.Sudoku = sudoku;

            sudoku.LeftNumbers = SudokuRulesUtilities.CreateArrayFromInputString("0080010080000000000000000000", 9, 3);
            sudoku.LeftNumbersType = SudokuRulesUtilities.MapTypesByNumbers("0080010080000000000000000000", 9, 3, SudokuElementType.NextToNine);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "count was " + countSolution);
        }
    }
}
