using NUnit.Framework;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class CreatingBookletViewModelTests
    {
        private CreatingBookletViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new CreatingBookletViewModel();
        }

        [Test]
        public void CanExecuteExportBookletCommand_True()
        {
            Assert.IsTrue(_viewModel.ExportBookletCommand.CanExecute(null));
        }

        [Test]
        public void CanExecuteSaveAsCommand_True()
        {
            Assert.IsTrue(_viewModel.SaveAsCommand.CanExecute(null));
        }

        [Test]
        public void CanExecuteSaveCommand_False()
        {
            Assert.IsFalse(_viewModel.SaveCommand.CanExecute(null));
        }

        [Test]
        public void CanExecuteSaveCommand_True()
        {
            _viewModel.BookletWorkFileName = "someMockFile.xml";
            Assert.IsTrue(_viewModel.SaveCommand.CanExecute(null));
        }

        [Test]
        public void CanExecuteExportFileWithSolution_True()
        {
            Assert.IsTrue(_viewModel.ExportFileWithSolution.CanExecute(null));
        }
    }
}
