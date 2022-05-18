using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class OkSudokuTableCommandTests
    {
        private OkSudokuTableCommand _command;

        private ISudokuInBookletViewModel _sudokuViewModel;

        private InsertNewSudokuTableViewModel _insertedSudokuTable;

        private ICreatingBookletViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            _creatingViewModel = new CreatingBookletViewModel();
            _sudokuViewModel = new SudokuInBookletViewModel();
            _insertedSudokuTable = new InsertNewSudokuTableViewModel(_sudokuViewModel, _sudokuViewModel.OrderNumber, _creatingViewModel);
            _command = new OkSudokuTableCommand(_sudokuViewModel, _insertedSudokuTable);
        }

        [Test]
        public void CanExecute_CorrectInfo_True()
        {
            _sudokuViewModel.Name = "Arrow sudoku";
            _sudokuViewModel.Rules = "Apply rules of Arrow sudoku";
            _sudokuViewModel.Points = 40;
            _sudokuViewModel.TableName = "arrow.jpg";
            _sudokuViewModel.SolutionName = "arrowSol.jpg";
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_InitialState_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_InCorrectInfo_False()
        {
            _sudokuViewModel.Name = "Arrow sudoku";
            _sudokuViewModel.Rules = "Apply rules of Arrow sudoku";
            _sudokuViewModel.Points = 0;
            _sudokuViewModel.TableName = "arrow.jpg";
            _sudokuViewModel.SolutionName = "arrowSol.jpg";
            Assert.IsFalse(_command.CanExecute(null));
        }
    }
}
