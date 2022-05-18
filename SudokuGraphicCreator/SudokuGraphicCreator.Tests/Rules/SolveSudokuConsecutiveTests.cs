using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveSudokuConsecutiveTests
    {
        [Test]
        public void SolveConsecutive_OneSolution()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            string inputString = "000000007000000000010000000000070000000000070000000000700000000000000000000000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Consecutive);

            CreateSymbolsOneSolution(SudokuStore.Instance.Sudoku);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "Count was " + countSolution);
            string solutionString = "945683217286417359317952486861574923423869175579321648794135862632748591158296734";
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        private void CreateSymbolsOneSolution(Sudoku sudoku)
        {
            List<Tuple<int, int>> rowSymbols = new List<Tuple<int, int>>();
            rowSymbols.Add(new Tuple<int, int>(0, 2));
            rowSymbols.Add(new Tuple<int, int>(0, 3));
            rowSymbols.Add(new Tuple<int, int>(0, 6));
            rowSymbols.Add(new Tuple<int, int>(0, 7));
            rowSymbols.Add(new Tuple<int, int>(3, 8));
            rowSymbols.Add(new Tuple<int, int>(4, 2));
            rowSymbols.Add(new Tuple<int, int>(5, 4));
            rowSymbols.Add(new Tuple<int, int>(5, 5));
            rowSymbols.Add(new Tuple<int, int>(7, 2));
            rowSymbols.Add(new Tuple<int, int>(8, 6));
            rowSymbols.Add(new Tuple<int, int>(8, 8));
            CreateSymbolsInSudoku(sudoku, rowSymbols, ElementLocationType.Row);

            List<Tuple<int, int>> colSymbols = new List<Tuple<int, int>>();
            colSymbols.Add(new Tuple<int, int>(2, 0));
            colSymbols.Add(new Tuple<int, int>(5, 0));
            colSymbols.Add(new Tuple<int, int>(7, 0));
            colSymbols.Add(new Tuple<int, int>(1, 2));
            colSymbols.Add(new Tuple<int, int>(2, 2));
            colSymbols.Add(new Tuple<int, int>(4, 4));
            colSymbols.Add(new Tuple<int, int>(6, 4));
            colSymbols.Add(new Tuple<int, int>(7, 4));
            colSymbols.Add(new Tuple<int, int>(1, 6));
            colSymbols.Add(new Tuple<int, int>(2, 6));
            colSymbols.Add(new Tuple<int, int>(7, 8));
            CreateSymbolsInSudoku(sudoku, colSymbols, ElementLocationType.Column);
        }

        [Test]
        public void SolveConsecutive_ZeroSolution()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            string inputString = "000000007000000000010000000000070000000000070000000000700000000000000000000000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Consecutive);

            CreateSymbolsZeroSolution(SudokuStore.Instance.Sudoku);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "Count was " + countSolution);
        }

        private void CreateSymbolsZeroSolution(Sudoku sudoku)
        {
            List<Tuple<int, int>> rowSymbols = new List<Tuple<int, int>>();
            rowSymbols.Add(new Tuple<int, int>(0, 2));
            rowSymbols.Add(new Tuple<int, int>(0, 3));
            rowSymbols.Add(new Tuple<int, int>(0, 6));
            rowSymbols.Add(new Tuple<int, int>(0, 7));
            rowSymbols.Add(new Tuple<int, int>(3, 8));
            rowSymbols.Add(new Tuple<int, int>(4, 2));
            rowSymbols.Add(new Tuple<int, int>(5, 4));
            rowSymbols.Add(new Tuple<int, int>(5, 5));
            rowSymbols.Add(new Tuple<int, int>(7, 2));
            rowSymbols.Add(new Tuple<int, int>(8, 6));
            rowSymbols.Add(new Tuple<int, int>(8, 8));
            CreateSymbolsInSudoku(sudoku, rowSymbols, ElementLocationType.Row);

            List<Tuple<int, int>> colSymbols = new List<Tuple<int, int>>();
            colSymbols.Add(new Tuple<int, int>(2, 0));
            colSymbols.Add(new Tuple<int, int>(5, 0));
            colSymbols.Add(new Tuple<int, int>(7, 0));
            colSymbols.Add(new Tuple<int, int>(1, 2));
            colSymbols.Add(new Tuple<int, int>(2, 2));
            colSymbols.Add(new Tuple<int, int>(4, 4));
            colSymbols.Add(new Tuple<int, int>(6, 4));
            colSymbols.Add(new Tuple<int, int>(1, 6));
            colSymbols.Add(new Tuple<int, int>(2, 6));
            colSymbols.Add(new Tuple<int, int>(7, 8));
            CreateSymbolsInSudoku(sudoku, colSymbols, ElementLocationType.Column);
        }

        private static void CreateSymbolsInSudoku(Sudoku sudoku, List<Tuple<int, int>> location, ElementLocationType locationType)
        {
            foreach (var indexes in location)
            {
                sudoku.SudokuVariants.Add(new WhiteCircle(0, 0, indexes.Item1, indexes.Item2, SudokuElementType.Consecutive, locationType));
            }
        }
    }
}
