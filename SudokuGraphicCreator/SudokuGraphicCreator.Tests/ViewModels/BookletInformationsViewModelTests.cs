using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class BookletInformationsViewModelTests
    {
        private BookletInformationsViewModel _viewModel;

        private Booklet _booklet;

        [SetUp]
        public void SetUp()
        {
            BookletStore.Instance.Booklet = new Booklet();
            _booklet = BookletStore.Instance.Booklet;
            _viewModel = new BookletInformationsViewModel();
        }

        [Test]
        public void SaveInfoIntoModel()
        {
            string name = "Czech sudoku competition 2022";
            _viewModel.TournamentName = name;

            var date = DateTime.Today;
            _viewModel.TournamentDate = date;

            string roundName = "Classic";
            _viewModel.RoundName = roundName;

            string roundNumber = "1";
            _viewModel.RoundNumber = roundNumber;

            var time = "30 minut";
            _viewModel.TimeForSolving = time;

            string location = "Brno";
            _viewModel.Location = location;

            Assert.That(name, Is.EqualTo(_booklet.TournamentName));
            Assert.That(date, Is.EqualTo(_booklet.TournamentDate));
            Assert.That(roundName, Is.EqualTo(_booklet.RoundName));
            Assert.That(roundNumber, Is.EqualTo(_booklet.RoundNumber));
            Assert.That(time, Is.EqualTo(_booklet.TimeForSolving));
            Assert.That(location, Is.EqualTo(_booklet.Location));
        }

        [Test]
        public void SaveInfoIntoModel_Change()
        {
            string name = "Czech sudoku competition 2022";
            _viewModel.TournamentName = name;

            Assert.That(name, Is.EqualTo(_booklet.TournamentName));

            string nameCorrect = "Czech sudoku competition 2023";
            _viewModel.TournamentName = nameCorrect;
            Assert.That(nameCorrect, Is.EqualTo(_booklet.TournamentName));
        }
    }
}
