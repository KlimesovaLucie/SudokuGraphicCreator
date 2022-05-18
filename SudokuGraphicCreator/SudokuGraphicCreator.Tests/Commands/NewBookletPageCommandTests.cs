using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class NewBookletPageCommandTests
    {
        private NewBookletPageCommand _command;

        private ICreatingBookletViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new CreatingBookletViewModel();
            _command = new NewBookletPageCommand(_viewModel);
        }

        [Test]
        public void CanExecute_EmptyBooklet_True()
        {
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_EmptyPage_False()
        {
            var page = new PageViewModel(1);
            _viewModel.Pages.Add(page);
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void Execute_Add_TrueFalse()
        {
            Assert.IsTrue(_command.CanExecute(null));
            Assert.IsTrue(_viewModel.Pages.Count == 0);
            _command.Execute(null);
            Assert.IsTrue(_viewModel.Pages.Count == 1);
            Assert.IsFalse (_command.CanExecute(null));
        }
    }
}
