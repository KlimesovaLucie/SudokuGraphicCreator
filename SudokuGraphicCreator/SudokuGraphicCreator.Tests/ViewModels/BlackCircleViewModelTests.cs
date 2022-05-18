using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class BlackCircleViewModelTests
    {
        private ICreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
            var elem = new BlackCircleViewModel(GridSizeStore.XCellSize, GridSizeStore.XCellSize, 0, 0, 0, 0,
                SudokuElementType.BlackKropki, ElementLocationType.Column);
            _creatingViewModel.GraphicElements.Add(elem);
        }

        [Test]
        public void DeleteElement_True()
        {
            Assert.IsTrue(BlackCircleViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.BlackKropki));
        }

        [Test]
        public void DeleteElementValidateVariant_TrueFalse()
        {
            Assert.IsTrue(BlackCircleViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.BlackKropki));
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Kropki));
        }

        [Test]
        public void AddVariant_True()
        {
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Kropki));
        }

        [Test]
        public void DeleteElement_False()
        {
            Assert.IsFalse(BlackCircleViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 30, SudokuElementType.BlackKropki));
        }
    }
}
