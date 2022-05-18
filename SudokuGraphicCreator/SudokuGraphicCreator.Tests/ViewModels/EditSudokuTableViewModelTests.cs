using NUnit.Framework;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class EditSudokuTableViewModelTests
    {
        private EditSudokuTableViewModel _viewModel;

        private ICreatingBookletViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            _creatingViewModel = new CreatingBookletViewModel();
            PrepareTwoSudoku();
            _viewModel = new EditSudokuTableViewModel(_creatingViewModel);
        }

        [Test]
        public void SelectedFirstSudoku_CorrectName()
        {
            SudokuInBookletViewModel actual = _creatingViewModel.Pages[0].Sudoku[0];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.Name, Is.EqualTo(actual.Name));
        }

        [Test]
        public void SelectedFirstAndSecondSudoku_CorrectName()
        {
            SudokuInBookletViewModel actual = _creatingViewModel.Pages[0].Sudoku[0];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.Name, Is.EqualTo(actual.Name));

            actual = _creatingViewModel.Pages[0].Sudoku[1];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.Name, Is.EqualTo(actual.Name));
        }

        [Test]
        public void SelectedFirstSudoku_CorrectPoints()
        {
            SudokuInBookletViewModel actual = _creatingViewModel.Pages[0].Sudoku[0];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.Points, Is.EqualTo(actual.Points));
        }

        [Test]
        public void SelectedFirstAndSecondSudoku_CorrectPoints()
        {
            SudokuInBookletViewModel actual = _creatingViewModel.Pages[0].Sudoku[0];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.Points, Is.EqualTo(actual.Points));

            actual = _creatingViewModel.Pages[0].Sudoku[1];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.Points, Is.EqualTo(actual.Points));
        }

        [Test]
        public void SelectedFirstSudoku_CorrectRules()
        {
            SudokuInBookletViewModel actual = _creatingViewModel.Pages[0].Sudoku[0];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.Rules, Is.EqualTo(actual.Rules));
        }

        [Test]
        public void SelectedFirstAndSecondSudoku_CorrectRules()
        {
            SudokuInBookletViewModel actual = _creatingViewModel.Pages[0].Sudoku[0];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.Rules, Is.EqualTo(actual.Rules));

            actual = _creatingViewModel.Pages[0].Sudoku[1];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.Rules, Is.EqualTo(actual.Rules));
        }

        [Test]
        public void SelectedFirstSudoku_CorrectTableName()
        {
            SudokuInBookletViewModel actual = _creatingViewModel.Pages[0].Sudoku[0];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.SudokuTableName, Is.EqualTo(actual.TableName));
        }

        [Test]
        public void SelectedFirstAndSecondSudoku_CorrectTableName()
        {
            SudokuInBookletViewModel actual = _creatingViewModel.Pages[0].Sudoku[0];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.SudokuTableName, Is.EqualTo(actual.TableName));

            actual = _creatingViewModel.Pages[0].Sudoku[1];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.SudokuTableName, Is.EqualTo(actual.TableName));
        }

        [Test]
        public void SelectedFirstSudoku_CorrectSolutionName()
        {
            SudokuInBookletViewModel actual = _creatingViewModel.Pages[0].Sudoku[0];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.SudokuSolutionName, Is.EqualTo(actual.SolutionName));
        }

        [Test]
        public void SelectedFirstAndSecondSudoku_CorrectSolutionName()
        {
            SudokuInBookletViewModel actual = _creatingViewModel.Pages[0].Sudoku[0];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.SudokuSolutionName, Is.EqualTo(actual.SolutionName));

            actual = _creatingViewModel.Pages[0].Sudoku[1];
            _viewModel.SelectedSudoku = actual;
            Assert.That(_viewModel.SudokuSolutionName, Is.EqualTo(actual.SolutionName));
        }

        [Test]
        public void SelectedFirstSudoku_IncorrectName()
        {
            SudokuInBookletViewModel actual = _creatingViewModel.Pages[0].Sudoku[0];
            _viewModel.SelectedSudoku = actual;
            _viewModel.Name = "";
            Assert.IsFalse(_viewModel.IsSelectedEnable);
        }

        [Test]
        public void SelectedFirstSudoku_IncorrectNameThenCorrect()
        {
            SudokuInBookletViewModel actual = _creatingViewModel.Pages[0].Sudoku[0];
            _viewModel.SelectedSudoku = actual;
            _viewModel.Name = "";
            Assert.IsFalse(_viewModel.OkCommand.CanExecute(null));
            Assert.IsFalse(_viewModel.IsSelectedEnable);

            _viewModel.Name = "Even sudoku";
            Assert.IsTrue(_viewModel.OkCommand.CanExecute(null));
            Assert.IsTrue(_viewModel.IsSelectedEnable);
        }

        private void PrepareTwoSudoku()
        {
            var page = new PageViewModel(1);
            _creatingViewModel.Pages.Add(page);

            // fill info for fisrt sudoku
            var sudokuOne = new SudokuInBookletViewModel();

            string nameOne = "Classic sudoku";
            sudokuOne.Name = nameOne;

            int pointsOne = 30;
            sudokuOne.Points = pointsOne;

            string rulesOne = "Apply rules of Classic sudoku";
            sudokuOne.Rules = rulesOne;

            string tableNameOne = "classic.jpg";
            sudokuOne.TableName = tableNameOne;

            string solutionNameOne = "classicSol.jpg";
            sudokuOne.SolutionName = solutionNameOne;

            page.AddSudoku(sudokuOne);


            // fill info for second sudoku
            var sudokuTwo = new SudokuInBookletViewModel();

            string nameTwo = "Consecutive sudoku";
            sudokuTwo.Name = nameTwo;

            int pointsTwo = 50;
            sudokuTwo.Points = pointsTwo;

            string rulesTwo = "Apply rules of Consecutive sudoku";
            sudokuTwo.Rules = rulesTwo;

            string tableNameTwo = "consecutive.jpg";
            sudokuTwo.TableName = tableNameTwo;

            string solutionNameTwo = "consecutive.jpg";
            sudokuTwo.SolutionName = solutionNameTwo;

            page.AddSudoku(sudokuTwo);
        }
    }
}
