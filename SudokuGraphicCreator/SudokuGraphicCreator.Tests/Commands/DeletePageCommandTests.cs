using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class DeletePageCommandTests
    {
        private DeletePageCommand _command;

        private ICreatingBookletViewModel _viewModelCreate;

        private IDeleteSudokuPageViewModel _viewModelDelete;

        [SetUp]
        public void SetUp()
        {
            _viewModelCreate = new CreatingBookletViewModel();
            _viewModelDelete = new DeleteSudokuPageViewModel(_viewModelCreate);
            _command = new DeletePageCommand(_viewModelCreate, _viewModelDelete);
            BookletStore.Instance.Booklet = new Model.Booklet();
        }

        [Test]
        public void CanExecute_NoPages_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_Pages_True()
        {
            _viewModelCreate.Pages.Add(new PageViewModel(1));
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void Execute_DeleteFirstPage()
        {
            _viewModelCreate.Pages.Add(new PageViewModel(1));
            _viewModelDelete.SelectedPageOrder = 1;
            _command.Execute(null);
            Assert.IsTrue(_viewModelCreate.Pages.Count == 0);
            Assert.IsFalse(_command.CanExecute(null));
        }
    }
}
