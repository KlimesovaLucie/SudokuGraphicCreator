using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System;

namespace SudokuGraphicCreator.Tests
{
    public class OkBookletInfoCommand_Tests
    {
        private OkBookletInfoCommand _command;

        private BookletInformationsViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new BookletInformationsViewModel();
            _command = new OkBookletInfoCommand(_viewModel);
            BookletStore.Instance.Booklet = new Booklet();
        }

        [Test]
        public void CanExecute_NotFill_False()
        {
            Assert.IsFalse(_command.CanExecute(null));
        }

        [Test]
        public void CanExecute_FillOk_True()
        {
            _viewModel.TournamentName = "Czech sudoku competition 2022";
            _viewModel.TournamentDate = DateTime.Today;
            _viewModel.RoundName = "Classic";
            _viewModel.RoundNumber = "1";
            _viewModel.TimeForSolving = "30 minut";
            _viewModel.Location = "Brno";
            Assert.IsTrue(_command.CanExecute(null));
        }

        [Test]
        public void Execute_ChangeView()
        {
            _command.Execute(null);
            Assert.That(NavigationStore.Instance.CurrentViewModel as CreatingBookletViewModel != null);
        }
    }
}
