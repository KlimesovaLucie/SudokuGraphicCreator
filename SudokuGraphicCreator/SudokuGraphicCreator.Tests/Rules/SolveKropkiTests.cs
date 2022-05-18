using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveKropkiTests
    {
        [Test]
        public void SolveKropki_OneSolution()
        {
            int[,] givenNumber = SudokuRulesUtilities.CreateEmptyGivenNumbers(9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Kropki);

            string solutionString = "853712694469835712217649853386271549721594386945368127138426975572983461694157238";
            CreateSymbolsForOneSolution();

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void SolveKropki_ZeroSolution()
        {
            int[,] givenNumber = SudokuRulesUtilities.CreateEmptyGivenNumbers(9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Kropki);

            CreateSymbolsForOneSolution();

            // remove one symbol
            SudokuStore.Instance.Sudoku.SudokuVariants.RemoveAt(0);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "count was " + countSolution);
        }

        private void CreateSymbolsForOneSolution()
        {
            CreateWhiteSymbolsInRowOneSolution();
            CreateBlackSymbolsInRowOneSolution();
            CreateWhiteSymbolsInColOneSolution();
            CreateBlackSymbolsInColOneSolution();
        }

        private static void CreateWhiteSymbolsInRowOneSolution()
        {
            List<Tuple<int, int>> rowWhite = new List<Tuple<int, int>>();
            rowWhite.Add(new Tuple<int, int>(1, 3));
            rowWhite.Add(new Tuple<int, int>(2, 1));
            rowWhite.Add(new Tuple<int, int>(2, 3));
            rowWhite.Add(new Tuple<int, int>(2, 6));
            rowWhite.Add(new Tuple<int, int>(3, 7));
            rowWhite.Add(new Tuple<int, int>(4, 6));
            rowWhite.Add(new Tuple<int, int>(5, 2));
            rowWhite.Add(new Tuple<int, int>(7, 4));
            rowWhite.Add(new Tuple<int, int>(7, 6));
            rowWhite.Add(new Tuple<int, int>(8, 7));
            CreateWhiteSymbolsInSudoku(SudokuStore.Instance.Sudoku, rowWhite, ElementLocationType.Row);
        }

        private static void CreateBlackSymbolsInRowOneSolution()
        {
            List<Tuple<int, int>> rowBlack = new List<Tuple<int, int>>();
            rowBlack.Add(new Tuple<int, int>(0, 5));
            rowBlack.Add(new Tuple<int, int>(1, 8));
            rowBlack.Add(new Tuple<int, int>(4, 2));
            rowBlack.Add(new Tuple<int, int>(5, 4));
            rowBlack.Add(new Tuple<int, int>(5, 7));
            rowBlack.Add(new Tuple<int, int>(6, 3));
            rowBlack.Add(new Tuple<int, int>(6, 4));
            CreateBlackSymbolsInSudoku(SudokuStore.Instance.Sudoku, rowBlack, ElementLocationType.Row);
        }

        private static void CreateWhiteSymbolsInColOneSolution()
        {
            List<Tuple<int, int>> colWhite = new List<Tuple<int, int>>();
            colWhite.Add(new Tuple<int, int>(3, 0));
            colWhite.Add(new Tuple<int, int>(8, 0));
            colWhite.Add(new Tuple<int, int>(1, 1));
            colWhite.Add(new Tuple<int, int>(6, 1));
            colWhite.Add(new Tuple<int, int>(3, 2));
            colWhite.Add(new Tuple<int, int>(1, 3));
            colWhite.Add(new Tuple<int, int>(6, 3));
            colWhite.Add(new Tuple<int, int>(2, 4));
            colWhite.Add(new Tuple<int, int>(1, 6));
            colWhite.Add(new Tuple<int, int>(2, 6));
            colWhite.Add(new Tuple<int, int>(3, 7));
            colWhite.Add(new Tuple<int, int>(7, 7));
            colWhite.Add(new Tuple<int, int>(2, 8));
            colWhite.Add(new Tuple<int, int>(5, 8));
            CreateWhiteSymbolsInSudoku(SudokuStore.Instance.Sudoku, colWhite, ElementLocationType.Column);
        }

        private static void CreateBlackSymbolsInColOneSolution()
        {
            List<Tuple<int, int>> colBlack = new List<Tuple<int, int>>();
            colBlack.Add(new Tuple<int, int>(1, 0));
            colBlack.Add(new Tuple<int, int>(2, 0));
            colBlack.Add(new Tuple<int, int>(5, 1));
            colBlack.Add(new Tuple<int, int>(8, 2));
            colBlack.Add(new Tuple<int, int>(5, 5));
            colBlack.Add(new Tuple<int, int>(7, 5));
            colBlack.Add(new Tuple<int, int>(8, 6));
            colBlack.Add(new Tuple<int, int>(4, 7));
            colBlack.Add(new Tuple<int, int>(8, 7));
            colBlack.Add(new Tuple<int, int>(1, 8));
            CreateBlackSymbolsInSudoku(SudokuStore.Instance.Sudoku, colBlack, ElementLocationType.Column);
        }

        private static void CreateWhiteSymbolsInSudoku(Sudoku sudoku, List<Tuple<int, int>> location, ElementLocationType locationType)
        {
            foreach (var indexes in location)
            {
                sudoku.SudokuVariants.Add(new WhiteCircle(0, 0, indexes.Item1, indexes.Item2, SudokuElementType.Consecutive, locationType));
            }
        }

        private static void CreateBlackSymbolsInSudoku(Sudoku sudoku, List<Tuple<int, int>> location, ElementLocationType locationType)
        {
            foreach (var indexes in location)
            {
                sudoku.SudokuVariants.Add(new BlackCircle(0, 0, indexes.Item1, indexes.Item2, SudokuElementType.Consecutive, locationType));
            }
        }
    }
}
