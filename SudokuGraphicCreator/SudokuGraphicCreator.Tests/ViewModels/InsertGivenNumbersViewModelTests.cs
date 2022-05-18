using NUnit.Framework;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class InsertGivenNumbersViewModelTests
    {
        private InsertGivenNumbersViewModel _viewModel;

        private ICreatingSudokuViewModel _createViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Model.Sudoku(9, 3, 3);
            _createViewModel = new CreatingSudokuViewModel();
            _viewModel = new InsertGivenNumbersViewModel(_createViewModel);
        }

        [Test]
        public void InputStringIncorrect_False()
        {
            _viewModel.InputString = "000000000";
            Assert.IsFalse(_viewModel.OkCommand.CanExecute(null));
        }

        [Test]
        public void InputStringCorrect_True()
        {
            _viewModel.InputString = "001802400060090010800000009100985006040307008900426001700000004080040060006208300";
            Assert.IsTrue(_viewModel.OkCommand.CanExecute(null));
        }

        [Test]
        public void InputStringToModelCorrect_True()
        {
            _viewModel.InputString = "001802400060090010800000009100985006040307008900426001700000004080040060006208300";
            _viewModel.IsCorrectFormat();
            _viewModel.OkCommand.Execute(null);
            Assert.That(SudokuStore.Instance.Sudoku.GivenNumbers,
                Is.EqualTo(Rules.SudokuRulesUtilities.CreateArrayFromInputString(_viewModel.InputString, 9, 9)));
        }
    }
}
