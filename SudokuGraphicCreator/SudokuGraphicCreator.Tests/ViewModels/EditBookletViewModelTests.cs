using NUnit.Framework;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System.Collections.Generic;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class EditBookletViewModelTests
    {
        private EditBookletViewModel _viewModel;

        private ICreatingBookletViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            _creatingViewModel = new CreatingBookletViewModel();
            _viewModel = new EditBookletViewModel(_creatingViewModel);
            BookletStore.Instance.Booklet = new Model.Booklet();
        }

        [Test]
        public void GetPages()
        {
            _creatingViewModel.Pages.Add(new PageViewModel(1));
            _creatingViewModel.Pages.Add(new PageViewModel(2));
            _creatingViewModel.Pages.Add(new PageViewModel(3));

            Assert.That(GetNumbersOfPages(), Is.EqualTo(_viewModel.Pages));
        }

        [Test]
        public void GetAllSudoku()
        {
            var pageOne = new PageViewModel(1);
            _creatingViewModel.Pages.Add(pageOne);
            var sudokuOne = new SudokuInBookletViewModel();
            var sudokuTwo = new SudokuInBookletViewModel();
            pageOne.Sudoku.Add(sudokuOne);
            pageOne.Sudoku.Add(sudokuTwo);

            Assert.That(_creatingViewModel.ListOfSudoku, Is.EqualTo(_viewModel.AllSudoku));
        }

        private List<int?> GetNumbersOfPages()
        {
            List<int?> pages = new List<int?>();
            foreach (var page in BookletStore.Instance.Booklet.Pages)
            {
                pages.Add(page.Order);
            }
            return pages;
        }

        [Test]
        public void CanOkCommand_True()
        {
            var pageOne = new PageViewModel(1);
            _creatingViewModel.Pages.Add(pageOne);
            var sudokuOne = new SudokuInBookletViewModel();
            var sudokuTwo = new SudokuInBookletViewModel();
            pageOne.AddSudoku(sudokuOne);
            pageOne.AddSudoku(sudokuTwo);
            _viewModel.SelectedPageIndex = 0;
            _viewModel.SelectedPageOrder = 1;

            _viewModel.FirstSudoku = sudokuTwo;
            _viewModel.SecondSudoku = sudokuOne;

            Assert.IsTrue(_viewModel.OkCommand.CanExecute(null));
        }

        [Test]
        public void CanOkCommand_False()
        {
            var pageOne = new PageViewModel(1);
            _creatingViewModel.Pages.Add(pageOne);
            var sudokuOne = new SudokuInBookletViewModel();
            var sudokuTwo = new SudokuInBookletViewModel();
            pageOne.AddSudoku(sudokuOne);
            pageOne.AddSudoku(sudokuTwo);
            _viewModel.SelectedPageIndex = 0;
            _viewModel.SelectedPageOrder = 1;

            _viewModel.FirstSudoku = sudokuTwo;
            _viewModel.SecondSudoku = sudokuTwo;

            Assert.IsFalse(_viewModel.OkCommand.CanExecute(null));
        }

        [Test]
        public void CanOkCommandTwoPages_False()
        {
            var pageOne = new PageViewModel(1);
            _creatingViewModel.Pages.Add(pageOne);
            var sudokuOne = new SudokuInBookletViewModel();
            var sudokuTwo = new SudokuInBookletViewModel();
            pageOne.AddSudoku(sudokuOne);
            pageOne.AddSudoku(sudokuTwo);
            _viewModel.SelectedPageIndex = 0;
            _viewModel.SelectedPageOrder = 1;

            _viewModel.FirstSudoku = sudokuTwo;

            var pageTwo = new PageViewModel(2);
            _creatingViewModel.Pages.Add(pageTwo);
            var sudokuThree = new SudokuInBookletViewModel();
            pageTwo.AddSudoku(sudokuThree);

            _viewModel.SelectedPageIndex = 1;
            _viewModel.SelectedPageOrder = 2;
            _viewModel.FirstSudoku = sudokuOne;

            Assert.IsFalse(_viewModel.OkCommand.CanExecute(null));
        }

        [Test]
        public void CanOkCommandTwoPages_Execute_True()
        {
            var pageOne = new PageViewModel(1);
            _creatingViewModel.Pages.Add(pageOne);
            var sudokuOne = new SudokuInBookletViewModel();
            sudokuOne.OrderNumber = 1;
            var sudokuTwo = new SudokuInBookletViewModel();
            sudokuTwo.OrderNumber = 2;
            pageOne.AddSudoku(sudokuOne);
            pageOne.AddSudoku(sudokuTwo);
            _viewModel.SelectedPageIndex = 0;
            _viewModel.SelectedPageOrder = 1;

            _viewModel.FirstSudoku = sudokuTwo;

            var pageTwo = new PageViewModel(2);
            _creatingViewModel.Pages.Add(pageTwo);
            var sudokuThree = new SudokuInBookletViewModel();
            sudokuThree.OrderNumber = 3;
            pageTwo.AddSudoku(sudokuThree);

            _viewModel.SelectedPageIndex = 1;
            _viewModel.SelectedPageOrder = 2;
            _viewModel.FirstSudoku = sudokuOne;

            _viewModel.SelectedPageIndex = 0;
            _viewModel.SelectedPageOrder = 1;
            _viewModel.SecondSudoku = sudokuThree;

            Assert.IsTrue(_viewModel.OkCommand.CanExecute(null));

            _viewModel.OkCommand.Execute(null);
            Assert.That(_creatingViewModel.ListOfSudoku, Is.EqualTo(_viewModel.AllSudoku));

            Assert.IsTrue(_viewModel.AllSudoku.Count == 3);
            Assert.IsTrue(_viewModel.AllSudoku[0].OrderNumber == 1);
            Assert.IsTrue(_viewModel.AllSudoku[1].OrderNumber == 2);
            Assert.IsTrue(_viewModel.AllSudoku[2].OrderNumber == 3);
        }
    }
}
