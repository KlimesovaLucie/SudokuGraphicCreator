using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class SmallArrowViewModelTests
    {
        private ICreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
            var elem = new SmallArrowViewModel(GridSizeStore.XCellSize, GridSizeStore.XCellSize, 1,
                SudokuElementType.LittleKillerLeftDown, GraphicElementType.LeftDown, ElementLocationType.Grid);
            _creatingViewModel.GraphicElements.Add(elem);
        }

        [Test]
        public void DeleteElement_True()
        {
            Assert.IsTrue(SmallArrowViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                GridSizeStore.XCellSize, GridSizeStore.XCellSize, SudokuElementType.LittleKillerLeftDown));
        }

        [Test]
        public void DeleteElementValidateVariant_TrueFalse()
        {
            Assert.IsTrue(SmallArrowViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                GridSizeStore.XCellSize, GridSizeStore.XCellSize, SudokuElementType.LittleKillerLeftDown));
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.LittleKiller));
        }

        [Test]
        public void AddVariant_True()
        {
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.LittleKiller));
        }

        [Test]
        public void DeleteElement_False()
        {
            Assert.IsFalse(SmallArrowViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                GridSizeStore.XCellSize, GridSizeStore.XCellSize, SudokuElementType.LittleKillerLeftUp));
        }
    }
}
