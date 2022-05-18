using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class SudokuDeleteWindowCommandTests
    {
        private ICreatingBookletViewModel _viewModel;

        private SudokuDeleteWindowCommand _command;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new CreatingBookletViewModel();
            _command = new SudokuDeleteWindowCommand(_viewModel);
        }

        [Test]
        public void CanExecute_Initial_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_OnePage_False()
        {
            _viewModel.Pages.Add(new PageViewModel(1));
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_OneSudoku_True()
        {
            var page = new PageViewModel(1);
            _viewModel.Pages.Add(page);
            page.Sudoku.Add(new SudokuInBookletViewModel());
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_Changing_FalseTrue()
        {
            Assert.IsFalse(_command.CanExecute(null));
            var page = new PageViewModel(1);
            _viewModel.Pages.Add(page);
            page.Sudoku.Add(new SudokuInBookletViewModel());
            Assert.IsTrue(_command.CanExecute(null));
        }
    }
}
