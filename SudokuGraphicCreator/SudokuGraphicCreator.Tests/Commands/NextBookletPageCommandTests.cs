using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.ViewModel;
using System.Windows;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class NextBookletPageCommandTests
    {
        private NextBookletPageCommand _command;

        private ICreatingBookletViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new CreatingBookletViewModel();
            _command = new NextBookletPageCommand(_viewModel);
        }

        [Test]
        public void CanExecute_NotNextPage_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_IsNextPage_True()
        {
            _viewModel.Pages.Add(new PageViewModel(1));
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_Changing_FalseTrueFalse()
        {
            Assert.IsFalse(_command.CanExecute(null));
            _viewModel.Pages.Add(new PageViewModel(1));
            Assert.IsTrue(_command.CanExecute(null));
            _viewModel.Pages.Clear();
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void Execute_NextEmptyPage()
        {
            var page = new PageViewModel(1);
            _viewModel.Pages.Add(page);
            _command.Execute(null);
            Assert.IsTrue(_viewModel.LeftSudokuVisibility == Visibility.Hidden);
            Assert.IsTrue(_viewModel.RightSudokuVisibility == Visibility.Hidden);
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void Execute_NextNotEmptyPage()
        {
            var page = new PageViewModel(1);
            var sudoku = new SudokuInBookletViewModel();
            sudoku.Points = 20;
            page.AddSudoku(sudoku);
            _viewModel.Pages.Add(page);
            _command.Execute(null);
            Assert.IsTrue(_viewModel.LeftSudokuVisibility == Visibility.Visible);
            Assert.IsTrue(_viewModel.RightSudokuVisibility == Visibility.Hidden);
            Assert.IsFalse(_command.CanExecute(null));
        }
    }
}
