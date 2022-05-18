using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class NewSudokuTableBookletCommandTests
    {
        private NewSudokuTableBookletCommand _command;

        private ICreatingBookletViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new CreatingBookletViewModel();
            _command = new NewSudokuTableBookletCommand(_viewModel);
        }

        [Test]
        public void CanExecute_NoPages_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_EmptyPage_True()
        {
            _viewModel.Pages.Add(new PageViewModel(1));
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_FullPage_False()
        {
            var page = new PageViewModel(1);
            _viewModel.Pages.Add(page);
            var firstSudoku = new SudokuInBookletViewModel();
            firstSudoku.Points = 20;
            page.Sudoku.Add(firstSudoku);
            var secondSudoku = new SudokuInBookletViewModel();
            secondSudoku.Points = 30;
            page.Sudoku.Add(secondSudoku);
            Assert.IsFalse(_command.CanExecute(null));
        }
    }
}
