using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class BookletEditCommandTests
    {
        private BookletEditCommand _command;

        private ICreatingBookletViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new CreatingBookletViewModel();
            _command = new BookletEditCommand(_viewModel);
            BookletStore.Instance.Booklet = new Model.Booklet();
        }

        [Test]
        public void CanExecute_ZeroPages_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_NotZeroPagesNoSudoku_False()
        {
            _viewModel.Pages.Add(new PageViewModel(1));
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_NotZeroPagesOneSudoku_False()
        {
            var page = new PageViewModel(1);
            _viewModel.Pages.Add(page);
            page.AddSudoku(new SudokuInBookletViewModel());
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_NotZeroPagesTwoSudoku_True()
        {
            var page = new PageViewModel(1);
            _viewModel.Pages.Add(page);
            page.AddSudoku(new SudokuInBookletViewModel());
            page.AddSudoku(new SudokuInBookletViewModel());
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_Changing_TrueFalse()
        {
            var page = new PageViewModel(1);
            _viewModel.Pages.Add(page);
            page.AddSudoku(new SudokuInBookletViewModel());
            page.AddSudoku(new SudokuInBookletViewModel());
            Assert.IsTrue(_command.CanExecute(null));

            page.Sudoku.Clear();
            Assert.IsFalse (_command.CanExecute(null));
        }
    }
}
