using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveEvenTests
    {
        [Test]
        public void SolveEven_OneSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Even);

            string solutionString = "162498537374256819985173462527641983639825741418739625741582396253964178896317254";

            List<Tuple<int, int>> locationCol = new List<Tuple<int, int>>();
            locationCol.Add(new Tuple<int, int>(0, 1));
            locationCol.Add(new Tuple<int, int>(1, 2));
            CreateEvenSymbolsInSudoku(SudokuStore.Instance.Sudoku, locationCol, ElementLocationType.Cell);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void SolveEven_ZeroSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Even);

            List<Tuple<int, int>> locationCol = new List<Tuple<int, int>>();
            locationCol.Add(new Tuple<int, int>(1, 0));
            locationCol.Add(new Tuple<int, int>(1, 2));
            CreateEvenSymbolsInSudoku(SudokuStore.Instance.Sudoku, locationCol, ElementLocationType.Cell);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "count was " + countSolution);
        }

        private static void CreateEvenSymbolsInSudoku(Sudoku sudoku, List<Tuple<int, int>> location, ElementLocationType locationType)
        {
            foreach (var indexes in location)
            {
                sudoku.SudokuVariants.Add(new GreySquare(0, 0, indexes.Item1, indexes.Item2, SudokuElementType.Even, locationType));
            }
        }
    }
}
