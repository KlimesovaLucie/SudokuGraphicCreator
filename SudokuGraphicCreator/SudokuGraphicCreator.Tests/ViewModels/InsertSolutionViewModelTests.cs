using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Rules;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.Tests.Rules;
using System.Threading;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class InsertSolutionViewModelTests
    {
        [Test]
        public void GenerateAllSymbolsConsecutiveAndSolve()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            string inputString = "000000007000000000010000000000070000000000070000000000700000000000000000000000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Consecutive);

            var creatingSudoku = new ViewModel.CreatingSudokuViewModel();
            var insertSolution = new ViewModel.InsertSolutionViewModel(creatingSudoku);
            insertSolution.InputString = "945683217286417359317952486861574923423869175579321648794135862632748591158296734";
            insertSolution.IsConsecutiveChecked = true;
            insertSolution.AllGraphicElemChecked = true;
            insertSolution.WithoutNumberChecked = true;
            insertSolution.IsCorrectFormat();
            insertSolution.OkCommand.Execute(null);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "Count was " + countSolution);
            string solutionString = "945683217286417359317952486861574923423869175579321648794135862632748591158296734";
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void GenerateAllSymbolsGreaterThanAndSolve()
        {
            string inputString = "000000000000000000084000016060000030000000000040050020000000095059000000870000040";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.GreaterThan);

            string solutionString = "621538974795146382384297516567429138932681457148753629216374895459812763873965241";
            var creatingSudoku = new ViewModel.CreatingSudokuViewModel();
            var insertSolution = new ViewModel.InsertSolutionViewModel(creatingSudoku);
            insertSolution.InputString = solutionString;
            insertSolution.IsGreaterThanCkecked = true;
            insertSolution.AllGraphicElemChecked = true;
            insertSolution.WithoutNumberChecked = true;
            insertSolution.IsCorrectFormat();
            insertSolution.OkCommand.Execute(null);


            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1);
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void GenerateAllSymbolsKropkiAndSolve()
        {
            int[,] givenNumber = SudokuRulesUtilities.CreateEmptyGivenNumbers(9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Kropki);

            string solutionString = "853712694469835712217649853386271549721594386945368127138426975572983461694157238";
            var creatingSudoku = new ViewModel.CreatingSudokuViewModel();
            var insertSolution = new ViewModel.InsertSolutionViewModel(creatingSudoku);
            insertSolution.InputString = solutionString;
            insertSolution.IsKropkiChecked = true;
            insertSolution.AllGraphicElemChecked = true;
            insertSolution.WithoutNumberChecked = true;
            insertSolution.IsCorrectFormat();
            insertSolution.OkCommand.Execute(null);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void GenerateAllSymbolsAndSolveKropki()
        {
            string inputString = "300000000000000000187000000000000000000260000000000000000000000052300000000000003";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.XV);

            string solutionString = "394618725526974138187523964215497386843261597679835412738152649952346871461789253";
            var creatingSudoku = new ViewModel.CreatingSudokuViewModel();
            var insertSolution = new ViewModel.InsertSolutionViewModel(creatingSudoku);
            insertSolution.InputString = solutionString;
            insertSolution.IsXVChecked = true;
            insertSolution.AllGraphicElemChecked = true;
            insertSolution.WithoutNumberChecked = true;
            insertSolution.IsCorrectFormat();
            insertSolution.OkCommand.Execute(null);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void GenerateAllSymbolsAndSolveEven()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Even);

            string solutionString = "162498537374256819985173462527641983639825741418739625741582396253964178896317254";
            var creatingSudoku = new ViewModel.CreatingSudokuViewModel();
            var insertSolution = new ViewModel.InsertSolutionViewModel(creatingSudoku);
            insertSolution.InputString = solutionString;
            insertSolution.IsEvenChecked = true;
            insertSolution.AllGraphicElemChecked = true;
            insertSolution.WithoutNumberChecked = true;
            insertSolution.IsCorrectFormat();
            insertSolution.OkCommand.Execute(null);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void GenerateAllSymbolsAndSolveOdd()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            SudokuStore.Instance.Sudoku.GivenNumbers = givenNumber;
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Classic);
            SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Odd);

            string solutionString = "162498537374256819985173462527641983639825741418739625741582396253964178896317254";
            var creatingSudoku = new ViewModel.CreatingSudokuViewModel();
            var insertSolution = new ViewModel.InsertSolutionViewModel(creatingSudoku);
            insertSolution.InputString = solutionString;
            insertSolution.IsOddChecked = true;
            insertSolution.AllGraphicElemChecked = true;
            insertSolution.WithoutNumberChecked = true;
            insertSolution.IsCorrectFormat();
            insertSolution.OkCommand.Execute(null);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void GenerateAllSymbolsAndSolveSkyscrapers()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.Skyscraper);
            SudokuStore.Instance.Sudoku = sudoku;

            string solutionString = "162498537374256819985173462527641983639825741418739625741582396253964178896317254";
            var creatingSudoku = new ViewModel.CreatingSudokuViewModel();
            var insertSolution = new ViewModel.InsertSolutionViewModel(creatingSudoku);
            insertSolution.InputString = solutionString;
            insertSolution.IsSkyscrapersChecked = true;
            insertSolution.AllGraphicElemChecked = true;
            insertSolution.WithoutNumberChecked = true;
            insertSolution.IsCorrectFormat();
            insertSolution.OkCommand.Execute(null);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }

        [Test]
        public void GenerateAllSymbolsAndSolveNextToNine()
        {
            string inputString = "100000000000200810005103002020600003000800740000700005000502006000900170800000000";
            int[,] givenNumber = SudokuRulesUtilities.CreateArrayFromInputString(inputString, 9, 9);
            Sudoku sudoku = new Sudoku(9, 3, 3);
            sudoku.GivenNumbers = givenNumber;
            sudoku.Variants.Add(SudokuType.Classic);
            sudoku.Variants.Add(SudokuType.NextToNine);
            SudokuStore.Instance.Sudoku = sudoku;

            string solutionString = "162498537374256819985173462527641983639825741418739625741582396253964178896317254";
            var creatingSudoku = new ViewModel.CreatingSudokuViewModel();
            var insertSolution = new ViewModel.InsertSolutionViewModel(creatingSudoku);
            insertSolution.InputString = solutionString;
            insertSolution.IsSkyscrapersChecked = true;
            insertSolution.AllGraphicElemChecked = true;
            insertSolution.WithoutNumberChecked = true;
            insertSolution.IsCorrectFormat();
            insertSolution.OkCommand.Execute(null);

            int countSolution = 0;
            int[,] solution = new int[9, 9];
            using var ctSource = new CancellationTokenSource();
            SolveSudoku.Solve(givenNumber, 9, 0, 0, ref countSolution, solution, ctSource.Token);
            Assert.That(countSolution == 1, "count was " + countSolution);
            Assert.That(SudokuRulesUtilities.CreateArrayFromInputString(solutionString, 9, 9), Is.EqualTo(solution));
        }
    }
}
