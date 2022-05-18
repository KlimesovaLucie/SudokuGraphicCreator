using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class BoldArrowViewModelTests
    {
        private ICreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
            var elem = new BoldArrowViewModel(0, 0, SudokuElementType.SearchNineLeft,
                GraphicElementType.Left);
            _creatingViewModel.GraphicElements.Add(elem);
        }

        [Test]
        public void DeleteElement_True()
        {
            Assert.IsTrue(BoldArrowViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.SearchNineLeft));
        }

        [Test]
        public void DeleteElementValidateVariant_TrueFalse()
        {
            Assert.IsTrue(BoldArrowViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.SearchNineLeft));
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.SearchNine));
        }

        [Test]
        public void AddVariant_True()
        {
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.SearchNine));
        }

        [Test]
        public void DeleteElement_False()
        {
            Assert.IsFalse(BoldArrowViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 50, SudokuElementType.SearchNineLeft));
        }
    }
}
