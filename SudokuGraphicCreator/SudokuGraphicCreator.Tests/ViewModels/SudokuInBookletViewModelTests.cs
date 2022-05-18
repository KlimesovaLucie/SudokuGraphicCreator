using NUnit.Framework;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class SudokuInBookletViewModelTests
    {
        private SudokuInBookletViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new SudokuInBookletViewModel();
        }

        [Test]
        public void PropagateNameToModel()
        {
            string name = "Killer";
            _viewModel.Name = name;

            Assert.That(name, Is.EqualTo(_viewModel.GetModel().Name));
        }

        [Test]
        public void PropagateOrderNumberToModel()
        {
            int order = 1;
            _viewModel.OrderNumber = order;

            Assert.That(order, Is.EqualTo(_viewModel.GetModel().Order));
        }

        [Test]
        public void PropagatePointsToModel()
        {
            int points = 50;
            _viewModel.Points = points;

            Assert.That(points, Is.EqualTo(_viewModel.GetModel().Points));
        }

        [Test]
        public void PropagateRulesToModel()
        {
            string rules = "Apply rules of Classic sudoku.";
            _viewModel.Rules = rules;

            Assert.That(rules, Is.EqualTo(_viewModel.GetModel().Rules));
        }

        [Test]
        public void PropagateTableNameToModel()
        {
            string name = "classic.jpg";
            _viewModel.TableName = name;

            Assert.That(name, Is.EqualTo(_viewModel.GetModel().TableName));
        }

        [Test]
        public void PropagateSolutionNameToModel()
        {
            string name = "classicSol.jpg";
            _viewModel.SolutionName = name;

            Assert.That(name, Is.EqualTo(_viewModel.GetModel().SolutionName));
        }

        [Test]
        public void PropagateStringRepresentationToModel()
        {
            int order = 1;
            _viewModel.OrderNumber = order;

            string name = "Outside sudoku";
            _viewModel.Name = name;

            int points = 30;
            _viewModel.Points = points;

            Assert.That(_viewModel.StringRepresentation,
                Is.EqualTo(order + ". " + name + " (" + points + " " + Properties.Resources.Resources.Points + ")"));
        }

        [Test]
        public void PropagateNameAndChangeToModel()
        {
            string name = "Killer";
            _viewModel.Name = name;

            Assert.That(name, Is.EqualTo(_viewModel.GetModel().Name));

            string newName = "Odd sudoku";
            _viewModel.Name = newName;

            Assert.That(newName, Is.EqualTo(_viewModel.GetModel().Name));
        }
    }
}
