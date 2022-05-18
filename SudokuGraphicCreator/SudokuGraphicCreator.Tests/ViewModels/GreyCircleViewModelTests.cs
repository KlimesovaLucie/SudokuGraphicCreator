using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class GreyCircleViewModelTests
    {
        private ICreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
            var elem = new GreyCircleViewModel(GridSizeStore.XCellSize, GridSizeStore.XCellSize, 0, 0, 0, 0,
                SudokuElementType.Odd, ElementLocationType.Column);
            _creatingViewModel.GraphicElements.Add(elem);
        }

        [Test]
        public void DeleteElement_True()
        {
            Assert.IsTrue(GreyCircleViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.Odd));
        }

        [Test]
        public void DeleteElementValidateVariant_TrueFalse()
        {
            Assert.IsTrue(GreyCircleViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.Odd));
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Odd));
        }

        [Test]
        public void AddVariant_True()
        {
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Odd));
        }

        [Test]
        public void DeleteElement_False()
        {
            Assert.IsFalse(GreyCircleViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 30, SudokuElementType.NoMeaning));
        }
    }
}
