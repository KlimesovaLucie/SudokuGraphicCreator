using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class CircleWithGreyEdgeViewModelTests
    {
        private ICreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
            var elem = new CircleWithGreyEdgeViewModel(GridSizeStore.XCellSize, GridSizeStore.XCellSize, 0, 0,
                SudokuElementType.NoMeaning, new CircleWithGreyEdge(0, 0, 0, 0, SudokuElementType.NoMeaning));
            _creatingViewModel.GraphicElements.Add(elem);
        }

        [Test]
        public void DeleteElement_True()
        {
            Assert.IsTrue(CircleWithGreyEdgeViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.NoMeaning));
        }

        [Test]
        public void DeleteElement_False()
        {
            Assert.IsFalse(CircleWithGreyEdgeViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 30, SudokuElementType.NoMeaning));
        }
    }
}
