using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class SudokuDeleteCommandTests
    {
        private SudokuDeleteCommand _command;

        private ICreatingBookletViewModel _viewModelCreate;

        private IDeleteSudokuTableViewModel _viewModelDelete;

        [SetUp]
        public void SetUp()
        {
            _viewModelCreate = new CreatingBookletViewModel();
            _viewModelDelete = new DeleteSudokuTableViewModel(_viewModelCreate);
            _command = new SudokuDeleteCommand(_viewModelCreate, _viewModelDelete);
        }

        [Test]
        public void CanExecute_ZeroSudoku_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_NotZeroSudoku_True()
        {
            var page = new PageViewModel(1);
            page.AddSudoku(new SudokuInBookletViewModel());
            _viewModelCreate.Pages.Add(page);
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_Changing_TrueFalse()
        {
            var page = new PageViewModel(1);
            page.AddSudoku(new SudokuInBookletViewModel());
            _viewModelCreate.Pages.Add(page);
            Assert.IsTrue(_command.CanExecute(null));
            _viewModelCreate.Pages.Clear();
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void Execute_DeleteLast()
        {
            var page = new PageViewModel(1);
            page.AddSudoku(new SudokuInBookletViewModel());
            _viewModelCreate.Pages.Add(page);
            Assert.IsTrue(_command.CanExecute(null));
            _command.Execute(null);
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void Execute_DeleteNotLast()
        {
            var page = new PageViewModel(1);
            var firstSudoku = new SudokuInBookletViewModel();
            firstSudoku.Points = 20;
            firstSudoku.Name = "Classic";
            firstSudoku.OrderNumber = 1;
            page.AddSudoku(firstSudoku);

            var secondSudoku = new SudokuInBookletViewModel();
            secondSudoku.Points = 30;
            secondSudoku.Name = "Classic";
            secondSudoku.OrderNumber = 2;
            page.AddSudoku(secondSudoku);

            _viewModelCreate.Pages.Add(page);
            _viewModelDelete.SelectedSudoku = firstSudoku;
            Assert.IsTrue(_command.CanExecute(null));
            _command.Execute(null);
            Assert.IsTrue(_command.CanExecute(null));
            Assert.IsTrue(_viewModelCreate.ListOfSudoku.Count == 1);
            Assert.IsFalse(_viewModelCreate.ListOfSudoku.Contains(firstSudoku));
        }
    }
}
