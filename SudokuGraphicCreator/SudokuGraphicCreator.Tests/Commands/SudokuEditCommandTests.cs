using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class SudokuEditCommandTests
    {
        private SudokuEditCommand _command;

        private ICreatingBookletViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new CreatingBookletViewModel();
            _command = new SudokuEditCommand(_viewModel);
        }

        [Test]
        public void CanExecute_InicialState_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_NotZeroPages_False()
        {
            _viewModel.Pages.Add(new PageViewModel(1));
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_NotZeroPagesNotZeroSudoku_True()
        {
            var page = new PageViewModel(1);
            _viewModel.Pages.Add(page);
            page.Sudoku.Add(new SudokuInBookletViewModel());
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_ChangingValues_FalseTrue()
        {
            Assert.IsFalse(_command.CanExecute(null));

            var page = new PageViewModel(1);
            _viewModel.Pages.Add(page);
            page.Sudoku.Add(new SudokuInBookletViewModel());
            Assert.IsTrue(_command.CanExecute(null));
        }
    }
}
