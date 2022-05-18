using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveLittleKillerTests
    {
        [Test]
        public void SolveNextToNine_OneSolution()
        {
            string inputString = "903000010000107009400500000045070090000209000080060140000002004300406000050000902";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.LittleKiller);
            var arrow = new SmallArrow(0, 0, 1, 1, 3, SudokuElementType.LittleKillerRightUp, ElementLocationType.GridLeft, GraphicElementType.RightUp);
            arrow.Value = 9;
            sudoku.SudokuVariants.Add(arrow);

            sudoku.LeftNumbers = SudokuRulesUtilities.CreateArrayFromInputString("0000090000000000000000000000", 9, 3);
            sudoku.LeftNumbersType = SudokuRulesUtilities.MapTypesByNumbers("0000090000000000000000000000", 9, 3, SudokuElementType.LittleKillerRightUp);

            SudokuStore.Instance.Sudoku = sudoku;
            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            string solutionString = "973624815568137429421598736645871293137249658289365147716952384392486571854713962";
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void SolveNextToNine_ZeroSolution()
        {
            string inputString = "903000010000107009400500000045070090000209000080060140000002004300406000050000902";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.LittleKiller);
            var arrow = new SmallArrow(0, 0, 1, 1, 3, SudokuElementType.LittleKillerRightUp, ElementLocationType.GridLeft, GraphicElementType.RightUp);
            arrow.Value = 7;
            sudoku.SudokuVariants.Add(arrow);

            sudoku.LeftNumbers = SudokuRulesUtilities.CreateArrayFromInputString("0000070000000000000000000000", 9, 3);
            sudoku.LeftNumbersType = SudokuRulesUtilities.MapTypesByNumbers("0000070000000000000000000000", 9, 3, SudokuElementType.LittleKillerRightUp);

            SudokuStore.Instance.Sudoku = sudoku;
            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "count was " + countSolution);
        }
    }
}
