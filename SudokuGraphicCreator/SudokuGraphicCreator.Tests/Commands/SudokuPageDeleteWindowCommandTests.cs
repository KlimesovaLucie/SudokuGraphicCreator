using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class SudokuPageDeleteWindowCommandTests
    {
        private SudokuPageDeleteWindowCommand _command;

        private ICreatingBookletViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new CreatingBookletViewModel();
            _command = new SudokuPageDeleteWindowCommand(_viewModel);
        }

        [Test]
        public void CanExecute_NotZeroPages_True()
        {
            _viewModel.Pages.Add(new PageViewModel(1));
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_ZeroPages_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_ChangingValue_FalseTrue()
        {
            Assert.IsFalse(_command.CanExecute(null));
            _viewModel.Pages.Add(new PageViewModel(1));
            Assert.IsTrue(_command.CanExecute(null));
        }
    }
}
