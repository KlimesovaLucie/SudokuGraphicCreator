using NUnit.Framework;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class InsertNewSudokuTableViewModelTests
    {
        private InsertNewSudokuTableViewModel _viewModel;

        private ISudokuInBookletViewModel _sudokuViewModel;

        private ICreatingBookletViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            _creatingViewModel = new CreatingBookletViewModel();
            _sudokuViewModel = new SudokuInBookletViewModel();
            _viewModel = new InsertNewSudokuTableViewModel(_sudokuViewModel, _sudokuViewModel.OrderNumber, _creatingViewModel);
        }

        [Test]
        public void PropagateName()
        {
            string name = "Windoku";
            _viewModel.Name = name;
            Assert.That(_sudokuViewModel.Name, Is.EqualTo(name));
        }

        [Test]
        public void PropagatePoints()
        {
            int points = 10;
            _viewModel.Points = points;
            Assert.That(_sudokuViewModel.Points, Is.EqualTo(points));
        }

        [Test]
        public void PropagateRules()
        {
            string rules = "Apply rules of Windoku";
            _viewModel.Rules = rules;
            Assert.That(_sudokuViewModel.Rules, Is.EqualTo(rules));
        }

        [Test]
        public void PropagateTableName()
        {
            string name = "windoku.jpg";
            _viewModel.SudokuTableName = name;
            Assert.That(_sudokuViewModel.TableName, Is.EqualTo(name));
        }

        [Test]
        public void PropagateTableFull()
        {
            string path = "/some/mock/of/file/to/windoku.jpg";
            _viewModel.SudokuTableFullPath = path;
            Assert.That(_sudokuViewModel.TableFullPath, Is.EqualTo(path));
        }

        [Test]
        public void PropagateSolutionName()
        {
            string name = "windokuSol.jpg";
            _viewModel.SudokuSolutionName = name;
            Assert.That(_sudokuViewModel.SolutionName, Is.EqualTo(name));
        }

        [Test]
        public void PropagateSolutionPath()
        {
            string solutionPath = "/some/mock/path/to/windokuSol.jpg";
            _viewModel.SudokuSolutionFullPath = solutionPath;
            Assert.That(_sudokuViewModel.SolutionFullPath, Is.EqualTo(solutionPath));
        }

        [Test]
        public void FillCorrectInfo_OkExecute_True()
        {
            string name = "Windoku";
            _viewModel.Name = name;

            int points = 10;
            _viewModel.Points = points;

            string rules = "Apply rules of Windoku";
            _viewModel.Rules = rules;

            string tableName = "windoku.jpg";
            _viewModel.SudokuTableName = tableName;

            string path = "/some/mock/of/file/to/windoku.jpg";
            _viewModel.SudokuTableFullPath = path;

            string solName = "windokuSol.jpg";
            _viewModel.SudokuSolutionName = solName;

            string solutionPath = "/some/mock/path/to/windokuSol.jpg";
            _viewModel.SudokuSolutionFullPath = solutionPath;

            Assert.IsTrue(_viewModel.OkCommand.CanExecute(null));
        }

        [Test]
        public void FillInCorrectInfo_OkExecute_False()
        {
            string name = "Windoku";
            _viewModel.Name = name;

            int points = 10;
            _viewModel.Points = points;

            string rules = "Apply rules of Windoku";
            _viewModel.Rules = rules;

            string tableName = "";
            _viewModel.SudokuTableName = tableName;

            string path = "";
            _viewModel.SudokuTableFullPath = path;

            string solName = "windokuSol.jpg";
            _viewModel.SudokuSolutionName = solName;

            string solutionPath = "/some/mock/path/to/windokuSol.jpg";
            _viewModel.SudokuSolutionFullPath = solutionPath;

            Assert.IsFalse(_viewModel.OkCommand.CanExecute(null));
        }

        [Test]
        public void CancelButtonEnable_True()
        {
            Assert.IsTrue(_viewModel.CancelCommand.CanExecute(null));
        }
    }
}
