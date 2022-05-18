using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveDifferenceTests
    {
        [Test]
        public void SolveDifference_OneSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Difference);

            string solutionString = "162498537374256819985173462527641983639825741418739625741582396253964178896317254";

            List<Tuple<int, int, int>> locationRow = new List<Tuple<int, int, int>>();
            locationRow.Add(new Tuple<int, int, int>(0, 2, 4));
            locationRow.Add(new Tuple<int, int, int>(4, 4, 6));
            CreateSumSymbolsInSudoku(SudokuStore.Instance.Sudoku, locationRow, ElementLocationType.Row);

            List<Tuple<int, int, int>> locationCol = new List<Tuple<int, int, int>>();
            locationCol.Add(new Tuple<int, int, int>(1, 0, 2));
            locationCol.Add(new Tuple<int, int, int>(4, 1, 1));
            CreateSumSymbolsInSudoku(SudokuStore.Instance.Sudoku, locationCol, ElementLocationType.Column);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void SolveDifference_ZeroSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Difference);

            List<Tuple<int, int, int>> locationRow = new List<Tuple<int, int, int>>();
            locationRow.Add(new Tuple<int, int, int>(0, 2, 5));
            locationRow.Add(new Tuple<int, int, int>(4, 4, 6));
            CreateSumSymbolsInSudoku(SudokuStore.Instance.Sudoku, locationRow, ElementLocationType.Row);

            List<Tuple<int, int, int>> locationCol = new List<Tuple<int, int, int>>();
            locationCol.Add(new Tuple<int, int, int>(1, 0, 2));
            locationCol.Add(new Tuple<int, int, int>(4, 1, 1));
            CreateSumSymbolsInSudoku(SudokuStore.Instance.Sudoku, locationCol, ElementLocationType.Column);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "count was " + countSolution);
        }

        private static void CreateSumSymbolsInSudoku(Sudoku sudoku, List<Tuple<int, int, int>> location, ElementLocationType locationType)
        {
            foreach (var indexes in location)
            {
                var circle = new CircleWithNumber(0, 0, indexes.Item1, indexes.Item2, SudokuElementType.Difference, locationType);
                circle.Value = indexes.Item3;
                sudoku.SudokuVariants.Add(circle);
            }
        }
    }
}
