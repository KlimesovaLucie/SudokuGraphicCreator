using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class CancelCommandTests
    {
        private CancelCommand _command;

        private CreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Model.Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
            _command = new CancelCommand(_creatingViewModel);
        }

        [Test]
        public void CanExecuteZeroSelected_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecuteMoreSelected_True()
        {
            _command = new CancelCommand(_creatingViewModel, _creatingViewModel.ButtonsConfirm[0].NameOfElement);
            _creatingViewModel.ButtonsConfirm[0].Checked = true;
            _creatingViewModel.SelectedCells.Add(_creatingViewModel.GridCells[0]);
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void Execute()
        {
            _command = new CancelCommand(_creatingViewModel, _creatingViewModel.ButtonsConfirm[0].NameOfElement);
            _creatingViewModel.ButtonsConfirm[0].Checked = true;
            _creatingViewModel.SelectedCells.Add(_creatingViewModel.GridCells[0]);
            Assert.IsTrue(_command.CanExecute(null));
            _command.Execute(null);
            Assert.IsTrue(_creatingViewModel.SelectedCells.Count == 0);
            Assert.IsFalse(_command.CanExecute(null));
        }
    }
}
