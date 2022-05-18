using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class CellNumberViewModelTests
    {
        private ICreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
        }

        [Test]
        public void ChangeTypeToOutside()
        {
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Outside));
            var cell = _creatingViewModel.BottomNumberCells[0];
            cell.Number = 3;
            cell.Type = SudokuElementType.Outside;
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Outside));
        }

        [Test]
        public void ChangeTypeToNextToNine()
        {
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.NextToNine));
            var cell = _creatingViewModel.BottomNumberCells[0];
            cell.Number = 3;
            cell.Type = SudokuElementType.NextToNine;
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.NextToNine));
        }

        [Test]
        public void ChangeTypeSkyscrapers()
        {
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Skyscraper));
            var cell = _creatingViewModel.BottomNumberCells[0];
            cell.Number = 3;
            cell.Type = SudokuElementType.Skyscrapers;
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Skyscraper));
        }
    }
}
