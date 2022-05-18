using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class LongArrowWithCircleViewModelTests
    {
        private ICreatingSudokuViewModel _creatingViewModel;

        private LongArrowWithCircleViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
            var points = new ObservableCollection<Tuple<int, int>>();
            points.Add(new Tuple<int, int>(0, 0));
            points.Add(new Tuple<int, int>(0, 1));
            var elem = new LongArrowWithCircleViewModel(SudokuElementType.Arrows, points);
            _viewModel = elem;
            _creatingViewModel.GraphicElements.Add(elem);
        }

        [Test]
        public void DeleteElement_True()
        {
            var points = new ObservableCollection<Tuple<int, int>>();
            points.Add(new Tuple<int, int>(0, 0));
            points.Add(new Tuple<int, int>(0, 1));

            Assert.IsTrue(LongArrowWithCircleViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                _viewModel.Circle.Left, _viewModel.Circle.Top, SudokuElementType.Arrows, points));
        }

        [Test]
        public void DeleteElementValidateVariant_TrueFalse()
        {
            var points = new ObservableCollection<Tuple<int, int>>();
            points.Add(new Tuple<int, int>(0, 0));
            points.Add(new Tuple<int, int>(0, 1));

            Assert.IsTrue(LongArrowWithCircleViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                _viewModel.Circle.Left, _viewModel.Circle.Top, SudokuElementType.Arrows, points));
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Arrow));
        }

        [Test]
        public void AddVariant_True()
        {
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Arrow));
        }

        [Test]
        public void DeleteElement_False()
        {
            var points = new ObservableCollection<Tuple<int, int>>();
            points.Add(new Tuple<int, int>(0, 0));
            points.Add(new Tuple<int, int>(0, 1));

            Assert.IsFalse(LongArrowWithCircleViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                0, 0, SudokuElementType.NoMeaning, points));
        }
    }
}
