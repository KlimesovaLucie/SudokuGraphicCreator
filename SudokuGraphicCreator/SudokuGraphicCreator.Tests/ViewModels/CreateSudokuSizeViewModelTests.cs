using NUnit.Framework;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class CreateSudokuSizeViewModelTests
    {
        private CreateSudokuSizeViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new CreateSudokuSizeViewModel();
        }

        [Test]
        public void CreateNineXNine()
        {
            _viewModel.IsCheckedNineSize = true;
            _viewModel.OkCommand.Execute(null);
            Assert.That(SudokuStore.Instance.Sudoku.Grid.Size == 9);
            Assert.That(SudokuStore.Instance.Sudoku.Grid.XBoxCells == 3);
            Assert.That(SudokuStore.Instance.Sudoku.Grid.YBoxCells == 3);
        }

        [Test]
        public void CreateTwoXThree()
        {
            _viewModel.IsCheckedSixSizeTwoThree = true;
            _viewModel.OkCommand.Execute(null);
            Assert.That(SudokuStore.Instance.Sudoku.Grid.Size == 6);
            Assert.That(SudokuStore.Instance.Sudoku.Grid.XBoxCells == 2);
            Assert.That(SudokuStore.Instance.Sudoku.Grid.YBoxCells == 3);
        }

        [Test]
        public void CreateThreeXTwo()
        {
            _viewModel.IsCheckedSixSizeThreeTwo = true;
            _viewModel.OkCommand.Execute(null);
            Assert.That(SudokuStore.Instance.Sudoku.Grid.Size == 6);
            Assert.That(SudokuStore.Instance.Sudoku.Grid.XBoxCells == 3);
            Assert.That(SudokuStore.Instance.Sudoku.Grid.YBoxCells == 2);
        }
    }
}
