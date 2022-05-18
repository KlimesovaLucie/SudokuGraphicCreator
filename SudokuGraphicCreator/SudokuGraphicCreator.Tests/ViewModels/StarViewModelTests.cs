using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class StarViewModelTests
    {
        private ICreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
            var elem = new StarViewModel(GridSizeStore.XCellSize, GridSizeStore.XCellSize, 0, 0, 0, 0,
                SudokuElementType.StarProduct, ElementLocationType.Grid);
            _creatingViewModel.GraphicElements.Add(elem);
        }

        [Test]
        public void DeleteElement_True()
        {
            Assert.IsTrue(StarViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.StarProduct));
        }

        [Test]
        public void DeleteElementValidateVariant_TrueFalse()
        {
            Assert.IsTrue(StarViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.StarProduct));
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.StarProduct));
        }

        [Test]
        public void AddVariant_True()
        {
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.StarProduct));
        }

        [Test]
        public void DeleteElement_False()
        {
            Assert.IsFalse(StarViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.NoMeaning));
        }
    }
}
