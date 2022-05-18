using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class OkSudokuTableEditCommandTests
    {
        private OkSudokuTableEditCommand _command;

        private ISudokuInBookletViewModel _sudokuViewModel;

        private EditSudokuTableViewModel _editViewModel;

        private ICreatingBookletViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            _creatingViewModel = new CreatingBookletViewModel();
            var sudoku = new SudokuInBookletViewModel();
            _sudokuViewModel = sudoku;
            var page = new PageViewModel(1);
            _creatingViewModel.Pages.Add(page);
            page.AddSudoku(sudoku);
            _editViewModel = new EditSudokuTableViewModel(_creatingViewModel);
            _command = new OkSudokuTableEditCommand(_sudokuViewModel, _editViewModel);
        }

        [Test]
        public void CanExecute_CorrectInfo_True()
        {
            _editViewModel.Name = "Arrow sudoku";
            _editViewModel.Rules = "Apply rules of Arrow sudoku";
            _editViewModel.Points = 40;
            _editViewModel.SudokuTableName = "arrow.jpg";
            _editViewModel.SudokuSolutionName = "arrowSol.jpg";
            Assert.IsTrue(_command.CanExecute(null));
            Assert.IsTrue(_editViewModel.IsSelectedEnable);
        }

        [Test]
        public void CanExecute_InitialState_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
            Assert.IsFalse(_editViewModel.IsSelectedEnable);
        }

        [Test]
        public void CanExecute_InCorrectInfo_False()
        {
            _editViewModel.Name = "Arrow sudoku";
            _editViewModel.Rules = "Apply rules of Arrow sudoku";
            _editViewModel.Points = 0;
            _editViewModel.SudokuTableName = "arrow.jpg";
            _editViewModel.SudokuSolutionName = "arrowSol.jpg";
            Assert.IsFalse(_command.CanExecute(null));
            Assert.IsFalse(_editViewModel.IsSelectedEnable);
        }
    }
}
