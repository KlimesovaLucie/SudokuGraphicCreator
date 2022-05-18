using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class LongArrowViewModelTests
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
            var elem = new LongArrowViewModel(SudokuElementType.NoMeaning, points,
                new LongArrow(SudokuElementType.NoMeaning, points));
            _creatingViewModel.GraphicElements.Add(elem);
        }

        [Test]
        public void DeleteElement_True()
        {
            var points = new ObservableCollection<Tuple<int, int>>();
            points.Add(new Tuple<int, int>(0, 0));
            points.Add(new Tuple<int, int>(0, 1));

            Assert.IsTrue(LongArrowViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                SudokuElementType.NoMeaning, points));
        }

        [Test]
        public void DeleteElement_False()
        {
            var points = new ObservableCollection<Tuple<int, int>>();
            points.Add(new Tuple<int, int>(0, 0));
            points.Add(new Tuple<int, int>(0, 2));

            Assert.IsFalse(LongArrowViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                SudokuElementType.NoMeaning, points));
        }
    }
}
