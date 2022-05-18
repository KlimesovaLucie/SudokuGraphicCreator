using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class LineViewModelTests
    {
        private ICreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
            var points = new ObservableCollection<Tuple<int, int>>();
            points.Add(new Tuple<int, int>(0, 0));
            points.Add(new Tuple<int, int>(0, 1));
            var elem = new LineViewModel(SudokuElementType.Sequences, points);
            _creatingViewModel.GraphicElements.Add(elem);
        }

        [Test]
        public void DeleteElement_True()
        {
            var points = new ObservableCollection<Tuple<int, int>>();
            points.Add(new Tuple<int, int>(0, 0));
            points.Add(new Tuple<int, int>(0, 1));

            Assert.IsTrue(LineViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                SudokuElementType.Sequences, points));
        }

        [Test]
        public void DeleteElementValidateVariant_TrueFalse()
        {
            var points = new ObservableCollection<Tuple<int, int>>();
            points.Add(new Tuple<int, int>(0, 0));
            points.Add(new Tuple<int, int>(0, 1));

            Assert.IsTrue(LineViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                SudokuElementType.Sequences, points));
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Sequence));
        }

        [Test]
        public void AddVariant_True()
        {
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Sequence));
        }

        [Test]
        public void DeleteElement_False()
        {
            var points = new ObservableCollection<Tuple<int, int>>();
            points.Add(new Tuple<int, int>(0, 0));
            points.Add(new Tuple<int, int>(0, 1));

            Assert.IsFalse(LineViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                SudokuElementType.NoMeaning, points));
        }
    }
}
