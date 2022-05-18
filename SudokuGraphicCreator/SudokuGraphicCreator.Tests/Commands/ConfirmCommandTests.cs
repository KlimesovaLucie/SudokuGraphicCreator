using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class ConfirmCommandTests
    {
        private ConfirmCommand _command;

        private CreatingSudokuViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            SudokuStore.Instance.Sudoku = new Model.Sudoku(9, 3, 3);
            _creatingViewModel = new CreatingSudokuViewModel();
        }

        [Test]
        public void CanBeExecuteZeroCells_False()
        {
            _command = new ConfirmCommand(_creatingViewModel, _creatingViewModel.ButtonsConfirm[0].NameOfElement);
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanBeExecuteMoreCells_True()
        {
            _creatingViewModel.ButtonsConfirm[1].Checked = true;
            _command = new ConfirmCommand(_creatingViewModel, _creatingViewModel.ButtonsConfirm[1].NameOfElement);
            _creatingViewModel.SelectedCells.Add(_creatingViewModel.GridCells[0]);
            _creatingViewModel.SelectedCells.Add(_creatingViewModel.GridCells[1]);
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void ExecuteMoreCells_True()
        {
            _creatingViewModel.ButtonsConfirm[1].Checked = true;
            _command = new ConfirmCommand(_creatingViewModel, _creatingViewModel.ButtonsConfirm[1].NameOfElement);
            _creatingViewModel.SelectedCells.Add(_creatingViewModel.GridCells[0]);
            _creatingViewModel.SelectedCells.Add(_creatingViewModel.GridCells[1]);
            _command.Execute(null);

            Assert.IsTrue(_creatingViewModel.GraphicElements.Count == 1);
        }
    }
}
