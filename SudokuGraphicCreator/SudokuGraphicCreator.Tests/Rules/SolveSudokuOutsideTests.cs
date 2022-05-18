using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using System.Threading;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SolveSudokuOutsideTests
    {
        [Test]
        public void SolveOutside_OneSolution()
        {
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = SudokuRulesUtilities.CreateEmptyGivenNumbers(9, 9);
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Outside);

            sudoku.LeftNumbers = SudokuRulesUtilities.CreateArrayFromInputString("015007068007058046045016000", 9, 3);
            sudoku.LeftNumbersType = SudokuRulesUtilities.MapTypesByNumbers("015007068007058046045016000", 9, 3, SudokuElementType.Outside);

            sudoku.UpNumbers = SudokuRulesUtilities.CreateUpNumbers("000000000003060200124578945", 3, 9);
            sudoku.UpNumbersType = SudokuRulesUtilities.MapTypesUpNumbers("000000000003060200124578945", 3, 9, SudokuElementType.Outside);

            sudoku.RightNumbers = SudokuRulesUtilities.CreateArrayFromInputString("260100390300670290680240000", 9, 3);
            sudoku.RightNumbersType = SudokuRulesUtilities.MapTypesByNumbers("260100390300670290680240000", 9, 3, SudokuElementType.Outside);

            sudoku.BottomNumbers = SudokuRulesUtilities.CreateBottomNumbers("265912512309050608000000000", 3, 9);
            sudoku.BottomNumbersType = SudokuRulesUtilities.MapTypesByNumbers("265912512309050608000000000", 3, 9, SudokuElementType.Outside);
            string solutionString = "153479286927368145684521937792186354538294761416735829245913678361857492879642513";

            SudokuStore.Instance.Sudoku = sudoku;
            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(sudoku.GivenNumbers, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "Count was " + countSolution + ", expected 1.");
            Assert.True(SudokuRulesUtilities.IsCorrectResult(solution, SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9)));
        }

        [Test]
        public void SolveOutsideTwo_OneSolution()
        {
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = SudokuRulesUtilities.CreateEmptyGivenNumbers(9, 9);
            //sudoku.GivenNumbers = CreateArrayFromInputString("600340150500127600000000000800900420000500800160080000008700000710800000000010500", 9, 9);
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Outside);

            sudoku.LeftNumbers = SudokuRulesUtilities.CreateArrayFromInputString("067005034037002016045007239", 9, 3);
            sudoku.LeftNumbersType = SudokuRulesUtilities.MapTypesByNumbers("067005034037002016045007239", 9, 3, SudokuElementType.Outside);

            sudoku.UpNumbers = SudokuRulesUtilities.CreateUpNumbers("300000004501107108627658239", 3, 9);
            sudoku.UpNumbersType = SudokuRulesUtilities.MapTypesUpNumbers("300000004501107108627658239", 3, 9, SudokuElementType.Outside);

            sudoku.RightNumbers = SudokuRulesUtilities.CreateArrayFromInputString("150400280250680390160290578", 9, 3);
            sudoku.RightNumbersType = SudokuRulesUtilities.MapTypesByNumbers("150400280250680390160290578", 9, 3, SudokuElementType.Outside);

            sudoku.BottomNumbers = SudokuRulesUtilities.CreateBottomNumbers("412412516030030040050090080", 3, 9);
            sudoku.BottomNumbersType = SudokuRulesUtilities.MapTypesByNumbers("412412516030030040050090080", 3, 9, SudokuElementType.Outside);

            SudokuStore.Instance.Sudoku = sudoku;
            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(sudoku.GivenNumbers, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "Count was " + countSolution + ", expected 1.");
        }

        
        public void SolveOutside_ZeroSolution()
        {
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = SudokuRulesUtilities.CreateEmptyGivenNumbers(9, 9);
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Outside);

            sudoku.LeftNumbers = SudokuRulesUtilities.CreateArrayFromInputString("167005034037002016045007239", 9, 3);
            sudoku.LeftNumbersType = SudokuRulesUtilities.MapTypesByNumbers("167005034037002016045007239", 9, 3, SudokuElementType.Outside);

            sudoku.UpNumbers = SudokuRulesUtilities.CreateUpNumbers("300000004501107108627658239", 3, 9);
            sudoku.UpNumbersType = SudokuRulesUtilities.MapTypesUpNumbers("300000004501107108627658239", 3, 9, SudokuElementType.Outside);

            sudoku.RightNumbers = SudokuRulesUtilities.CreateArrayFromInputString("150400280250680390160290578", 9, 3);
            sudoku.RightNumbersType = SudokuRulesUtilities.MapTypesByNumbers("150400280250680390160290578", 9, 3, SudokuElementType.Outside);

            sudoku.BottomNumbers = SudokuRulesUtilities.CreateBottomNumbers("412412516030030040050090080", 3, 9);
            sudoku.BottomNumbersType = SudokuRulesUtilities.MapTypesByNumbers("412412516030030040050090080", 3, 9, SudokuElementType.Outside);
            string solutionString = "153479286927368145684521937792186354538294761416735829245913678361857492879642513";

            SudokuStore.Instance.Sudoku = sudoku;
            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(sudoku.GivenNumbers, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 0, "Count was " + countSolution + ", expected 0");
            Assert.True(SudokuRulesUtilities.IsCorrectResult(solution, SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9)));
        }
    }
}
