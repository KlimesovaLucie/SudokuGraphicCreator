using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveSequencesTests
    {
        [Test]
        public void SolveSequence_OneSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Sequence);
            SudokuStore.Instance.Sudoku = sudoku;

            ObservableCollection<Tuple<int, int>> positions = new ObservableCollection<Tuple<int, int>>();
            positions.Add(new Tuple<int, int>(1, 7));
            positions.Add(new Tuple<int, int>(2, 8));
            positions.Add(new Tuple<int, int>(3, 8));
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(new Line(SudokuElementType.Sequences, positions));

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            string solutionString = "162498537374256819985173462527641983639825741418739625741582396253964178896317254";
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void SolveSequence_ZeroSolution()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Sequence);
            SudokuStore.Instance.Sudoku = sudoku;

            ObservableCollection<Tuple<int, int>> positions = new ObservableCollection<Tuple<int, int>>();
            positions.Add(new Tuple<int, int>(1, 7));
            positions.Add(new Tuple<int, int>(2, 8));
            positions.Add(new Tuple<int, int>(3, 8));
            positions.Add(new Tuple<int, int>(3, 7));
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(new Line(SudokuElementType.Sequences, positions));

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "count was " + countSolution);
        }
    }
}
