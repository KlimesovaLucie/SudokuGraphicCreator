using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveArrowsTests
    {
        [Test]
        public void SolveArrow_OneSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Arrow);
            SudokuStore.Instance.Sudoku = sudoku;

            ObservableCollection<Tuple<int, int>> positions = new ObservableCollection<Tuple<int, int>>();
            positions.Add(new Tuple<int, int>(3, 0));
            positions.Add(new Tuple<int, int>(3, 1));
            positions.Add(new Tuple<int, int>(4, 1));
            var longArrow = new LongArrowWithCircle(SudokuElementType.Arrows);
            var circle = new CircleWithGreyEdge(0, 0, 3, 0, SudokuElementType.Arrows);
            longArrow.Circle = circle;
            var arrow = new LongArrow(SudokuElementType.Arrows, positions);
            longArrow.Arrow = arrow;
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(longArrow);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            string solutionString = "162498537374256819985173462527641983639825741418739625741582396253964178896317254";
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void SolveArrow_ZeroSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Arrow);
            SudokuStore.Instance.Sudoku = sudoku;

            ObservableCollection<Tuple<int, int>> positions = new ObservableCollection<Tuple<int, int>>();
            positions.Add(new Tuple<int, int>(3, 0));
            positions.Add(new Tuple<int, int>(3, 1));
            positions.Add(new Tuple<int, int>(4, 1));
            positions.Add(new Tuple<int, int>(4, 2));
            var longArrow = new LongArrowWithCircle(SudokuElementType.Arrows);
            var circle = new CircleWithGreyEdge(0, 0, 3, 0, SudokuElementType.Arrows);
            longArrow.Circle = circle;
            var arrow = new LongArrow(SudokuElementType.Arrows, positions);
            longArrow.Arrow = arrow;
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(longArrow);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "count was " + countSolution);
        }
    }
}
