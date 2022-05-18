using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class CageViewModelTests
    {
        private ICreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
            var points = new ObservableCollection<Tuple<double, double>>();
            points.Add(new Tuple<double, double>(0, 0));
            points.Add(new Tuple<double, double>(0, 1));
            var elem = new CageViewModel(SudokuElementType.Killer, points);
            _creatingViewModel.GraphicElements.Add(elem);
        }

        [Test]
        public void DeleteElement_True()
        {
            var points = new ObservableCollection<Tuple<double, double>>();
            points.Add(new Tuple<double, double>(0, 0));
            points.Add(new Tuple<double, double>(0, 1));

            PointCollection pointCollection = new PointCollection();
            double left = 0;
            CageViewModel.FindPoints(points, ref left, ref left, pointCollection);
            Assert.IsTrue(CageViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                pointCollection, SudokuElementType.Killer));
        }

        [Test]
        public void DeleteElementValidateVariant_TrueFalse()
        {
            var points = new ObservableCollection<Tuple<double, double>>();
            points.Add(new Tuple<double, double>(0, 0));
            points.Add(new Tuple<double, double>(0, 1));

            PointCollection pointCollection = new PointCollection();
            double left = 0;
            CageViewModel.FindPoints(points, ref left, ref left, pointCollection);
            Assert.IsTrue(CageViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                pointCollection, SudokuElementType.Killer));
            Assert.IsFalse(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Killer));
        }

        [Test]
        public void AddVariant_True()
        {
            Assert.IsTrue(SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Killer));
        }

        [Test]
        public void DeleteElement_False()
        {
            var points = new ObservableCollection<Tuple<double, double>>();
            points.Add(new Tuple<double, double>(0, 0));
            points.Add(new Tuple<double, double>(0, 1));

            PointCollection pointCollection = new PointCollection();
            double left = 0;
            CageViewModel.FindPoints(points, ref left, ref left, pointCollection);
            Assert.IsFalse(CageViewModel.RemoveFromCollection(_creatingViewModel.GraphicElements,
                pointCollection, SudokuElementType.NoMeaning));
        }
    }
}
