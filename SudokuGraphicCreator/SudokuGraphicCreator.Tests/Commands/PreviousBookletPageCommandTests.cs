using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.ViewModel;
using System.Windows;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class PreviousBookletPageCommandTests
    {
        private PreviousBookletPageCommand _command;

        private ICreatingBookletViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new CreatingBookletViewModel();
            _command = new PreviousBookletPageCommand(_viewModel);
        }

        [Test]
        public void CanExecute_ViewFirstPage_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_ViewNotFirstPage_True()
        {
            var page = new PageViewModel(1);
            _viewModel.Pages.Add(page);
            _viewModel.ActualPage = page;
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void Execute_ViewIntroPage()
        {
            var page = new PageViewModel(1);
            _viewModel.Pages.Add(page);
            _viewModel.ActualPage = page;
            _viewModel.IntroductionPageVisibility = Visibility.Hidden;
            _viewModel.SudokuPageVisibility = Visibility.Visible;

            _command.Execute(null);
            Assert.IsNull(_viewModel.ActualPage);
            Assert.IsTrue(_viewModel.IntroductionPageVisibility == Visibility.Visible);
            Assert.IsTrue(_viewModel.SudokuPageVisibility == Visibility.Hidden);
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void Execute_ViewNotIntroPage()
        {
            var page = new PageViewModel(1);
            var firstSudoku = new SudokuInBookletViewModel();
            firstSudoku.Points = 20;
            page.Sudoku.Add(firstSudoku);
            var secondSudoku = new SudokuInBookletViewModel();
            page.Sudoku.Add(secondSudoku);
            secondSudoku.Points = 30;
            var page2 = new PageViewModel(2);
            _viewModel.Pages.Add(page);
            _viewModel.Pages.Add(page2);
            _viewModel.ActualPage = page2;
            _viewModel.IntroductionPageVisibility = Visibility.Hidden;
            _viewModel.SudokuPageVisibility = Visibility.Visible;

            _command.Execute(null);
            Assert.IsNotNull(_viewModel.ActualPage);
            Assert.IsTrue(_viewModel.ActualPage.PageNumber == 1);
            Assert.IsTrue(_viewModel.LeftSudokuVisibility == Visibility.Visible);
            Assert.IsTrue(_viewModel.RightSudokuVisibility == Visibility.Visible);
            Assert.IsTrue(_viewModel.IntroductionPageVisibility == Visibility.Hidden);
            Assert.IsTrue(_viewModel.SudokuPageVisibility == Visibility.Visible);
            Assert.IsTrue(_command.CanExecute(null));
        }
    }
}
