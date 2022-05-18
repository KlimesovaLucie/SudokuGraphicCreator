using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class GreySquareViewModelTests
    {
        private ICreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
            var elem = new GreySquareViewModel(GridSizeStore.XCellSize, GridSizeStore.XCellSize, 0, 0,
                0, 0, SudokuElementType.Even, ElementLocationType.Cell);
            _creatingViewModel.GraphicElements.Add(elem);
        }

        [Test]
        public void DeleteElement_True()
        {
            Assert.IsTrue(GreySquareViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.Even));
        }

        [Test]
        public void DeleteElementValidateVariant_TrueFalse()
        {
            Assert.IsTrue(GreySquareViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.Even));
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Even));
        }

        [Test]
        public void AddVariant_True()
        {
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Even));
        }

        [Test]
        public void DeleteElement_False()
        {
            Assert.IsFalse(GreySquareViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.NoMeaning));
        }
    }
}
