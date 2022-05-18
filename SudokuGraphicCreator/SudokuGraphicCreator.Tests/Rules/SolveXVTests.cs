using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveXVTests
    {
        [Test]
        public void SolveKropki_OneSolution()
        {
            string inputString = "300000000000000000187000000000000000000260000000000000000000000052300000000000003";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.XV);

            string solutionString = "394618725526974138187523964215497386843261597679835412738152649952346871461789253";
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
            string inputString = "300000000000000000187000000000000000000260000000000000000000000052300000000000003";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.XV);

            CreateSymbolsForOneSolution();

            // change one symbol
            Character elem = SudokuStore.Instance.Sudoku.SudokuVariants[0] as Character;
            elem.SudokuElemType = SudokuElementType.XvV;

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "count was " + countSolution);
        }

        private void CreateSymbolsForOneSolution()
        {
            CreateXInRow();
            CreateXInCol();
            CreateVInRow();
            CreateVInCol();
        }

        private void CreateXInRow()
        {
            List<Tuple<int, int>> location = new List<Tuple<int, int>>();
            location.Add(new Tuple<int, int>(0, 3));
            location.Add(new Tuple<int, int>(2, 8));
            location.Add(new Tuple<int, int>(3, 6));
            location.Add(new Tuple<int, int>(6, 1));
            location.Add(new Tuple<int, int>(6, 7));
            location.Add(new Tuple<int, int>(7, 5));
            location.Add(new Tuple<int, int>(8, 1));
            CreateXVSymbolsInSudoku(SudokuStore.Instance.Sudoku, "X", location, SudokuElementType.XvX, ElementLocationType.Row);
        }

        private void CreateXInCol()
        {
            List<Tuple<int, int>> location = new List<Tuple<int, int>>();
            location.Add(new Tuple<int, int>(1, 2));
            location.Add(new Tuple<int, int>(2, 1));
            location.Add(new Tuple<int, int>(2, 6));
            location.Add(new Tuple<int, int>(3, 5));
            location.Add(new Tuple<int, int>(3, 8));
            location.Add(new Tuple<int, int>(4, 0));
            location.Add(new Tuple<int, int>(5, 3));
            location.Add(new Tuple<int, int>(5, 7));
            location.Add(new Tuple<int, int>(6, 1));
            location.Add(new Tuple<int, int>(6, 6));
            location.Add(new Tuple<int, int>(7, 8));
            location.Add(new Tuple<int, int>(7, 2));
            location.Add(new Tuple<int, int>(8, 3));
            location.Add(new Tuple<int, int>(8, 6));
            CreateXVSymbolsInSudoku(SudokuStore.Instance.Sudoku, "X", location, SudokuElementType.XvX, ElementLocationType.Column);
        }

        private void CreateVInRow()
        {
            List<Tuple<int, int>> location = new List<Tuple<int, int>>();
            location.Add(new Tuple<int, int>(1, 6));
            location.Add(new Tuple<int, int>(2, 5));
            location.Add(new Tuple<int, int>(4, 3));
            location.Add(new Tuple<int, int>(5, 7));
            location.Add(new Tuple<int, int>(7, 3));
            CreateXVSymbolsInSudoku(SudokuStore.Instance.Sudoku, "V", location, SudokuElementType.XvV, ElementLocationType.Row);
        }

        private void CreateVInCol()
        {
            List<Tuple<int, int>> location = new List<Tuple<int, int>>();
            location.Add(new Tuple<int, int>(1, 7));
            location.Add(new Tuple<int, int>(4, 1));
            location.Add(new Tuple<int, int>(6, 7));
            CreateXVSymbolsInSudoku(SudokuStore.Instance.Sudoku, "V", location, SudokuElementType.XvV, ElementLocationType.Column);
        }

        private static void CreateXVSymbolsInSudoku(Sudoku sudoku, string value, List<Tuple<int, int>> location, SudokuElementType element, ElementLocationType locationType)
        {
            foreach (var indexes in location)
            {
                sudoku.SudokuVariants.Add(new Character(0, 0, indexes.Item1, indexes.Item2, value, element, locationType));
            }
        }
    }
}
