using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class CharacterViewModelTests
    {
        private ICreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
            var elem = new CharacterViewModel(GridSizeStore.XCellSize, GridSizeStore.XCellSize, 0, 0, 0, 0,
                SudokuElementType.XvV, "V", GraphicElementType.Down, ElementLocationType.Column);
            _creatingViewModel.GraphicElements.Add(elem);
        }

        [Test]
        public void DeleteElement_True()
        {
            Assert.IsTrue(CharacterViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.XvV));
        }

        [Test]
        public void DeleteElementValidateVariant_TrueFalse()
        {
            Assert.IsTrue(CharacterViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.XvV));
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.XV));
        }

        [Test]
        public void AddVariant_True()
        {
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.XV));
        }

        [Test]
        public void DeleteElement_False()
        {
            Assert.IsFalse(CharacterViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.XvX));
        }
    }
}
