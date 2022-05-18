using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class PageViewModelTests
    {
        private PageViewModel _viewModel;

        private ICreatingBookletViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            BookletStore.Instance.Booklet = new Booklet();
            _creatingViewModel = new CreatingBookletViewModel();
            _viewModel = new PageViewModel(1);
            _creatingViewModel.Pages.Add(_viewModel);
        }

        [Test]
        public void AddSudoku()
        {
            var sudokuFirst = new SudokuInBookletViewModel();
            var sudokuSecond = new SudokuInBookletViewModel();
            _viewModel.AddSudoku(sudokuFirst);
            _viewModel.AddSudoku(sudokuSecond);

            var collection = new ObservableCollection<SudokuInBookletViewModel>();
            collection.Add(sudokuFirst);
            collection.Add(sudokuSecond);
            Assert.That(collection, Is.EqualTo(_viewModel.Sudoku));
        }

        [Test]
        public void AddSudokuCorrectModel()
        {
            var sudokuFirst = new SudokuInBookletViewModel();
            var sudokuSecond = new SudokuInBookletViewModel();
            _viewModel.AddSudoku(sudokuFirst);
            _viewModel.AddSudoku(sudokuSecond);

            List<SudokuInBooklet> collection = new List<SudokuInBooklet>();
            collection.Add(sudokuFirst.GetModel());
            collection.Add(sudokuSecond.GetModel());

            Assert.That(collection, Is.EqualTo(BookletStore.Instance.Booklet.Pages[0].SudokuOnPage));
        }

        [Test]
        public void CorrectNumberPage()
        {
            Assert.IsTrue(BookletStore.Instance.Booklet.Pages[0].Order == 1);
        }

        [Test]
        public void ChangeNumberPage()
        {
            _viewModel.PageNumber = 2;
            Assert.IsTrue(BookletStore.Instance.Booklet.Pages[0].Order == 2);
        }
    }
}
