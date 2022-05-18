using NUnit.Framework;
using SudokuGraphicCreator.IO;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System.Xml.Serialization;

namespace SudokuGraphicCreator.Tests.IO
{
    public class OpenBookletSaveBookletTests
    {
        private CreatingBookletViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new CreatingBookletViewModel();

            var firstPage = new PageViewModel(1);
            _viewModel.Pages.Add(firstPage);

            var sudokuOne = new SudokuInBookletViewModel();
            FillFirstSudoku(sudokuOne);
            firstPage.AddSudoku(sudokuOne);

            var sudokuTwo = new SudokuInBookletViewModel();
            FillSecondSudoku(sudokuTwo);
            firstPage.AddSudoku(sudokuTwo);

            var secondPage = new PageViewModel(2);
            _viewModel.Pages.Add(secondPage);

            var sudokuThree = new SudokuInBookletViewModel();
            FillThirdSudoku(sudokuThree);
            secondPage.AddSudoku(sudokuThree);
        }

        private void FillFirstSudoku(SudokuInBookletViewModel sudoku)
        {
            sudoku.Name = "Classic sudoku";
            sudoku.Points = 20;
            sudoku.Rules = "Apply rules of classic sudoku";
            sudoku.OrderNumber = 1;
        }

        private void FillSecondSudoku(SudokuInBookletViewModel sudoku)
        {
            sudoku.Name = "Untouchable sudoku";
            sudoku.Points = 30;
            sudoku.Rules = "Apply rules of untouchable sudoku";
            sudoku.OrderNumber = 2;
        }

        private void FillThirdSudoku(SudokuInBookletViewModel sudoku)
        {
            sudoku.Name = "Killer sudoku";
            sudoku.Points = 40;
            sudoku.Rules = "Apply rules of killer sudoku";
            sudoku.OrderNumber = 3;
        }

        [Test]
        public void SaveAndOpen_ActualPage_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.ActualPage, Is.EqualTo(_viewModel.ActualPage));
        }

        [Test]
        public void SaveAndOpen_BookletWorkFileName_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.BookletWorkFileName, Is.EqualTo(_viewModel.BookletWorkFileName));
        }

        [Test]
        public void SaveAndOpen_IntroductionPageVisibility_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.IntroductionPageVisibility, Is.EqualTo(_viewModel.IntroductionPageVisibility));
        }

        [Test]
        public void SaveAndOpen_LeftSudokuVisibility_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.LeftSudokuVisibility, Is.EqualTo(_viewModel.LeftSudokuVisibility));
        }

        [Test]
        public void SaveAndOpen_Location_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.Location, Is.EqualTo(_viewModel.Location));
        }

        [Test]
        public void SaveAndOpen_LogoOneFullPath_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.LogoOneFullPath, Is.EqualTo(_viewModel.LogoOneFullPath));
        }

        [Test]
        public void SaveAndOpen_LogoOneImage_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.LogoOneImage, Is.EqualTo(_viewModel.LogoOneImage));
        }

        [Test]
        public void SaveAndOpen_LogoThreeFullPath_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.LogoThreeFullPath, Is.EqualTo(_viewModel.LogoThreeFullPath));
        }

        [Test]
        public void SaveAndOpen_LogoThreeImage_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.LogoThreeImage, Is.EqualTo(_viewModel.LogoThreeImage));
        }

        [Test]
        public void SaveAndOpen_LogoTwoFullPath_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.LogoTwoFullPath, Is.EqualTo(_viewModel.LogoTwoFullPath));
        }

        [Test]
        public void SaveAndOpen_LogoTwoImage_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.LogoTwoImage, Is.EqualTo(_viewModel.LogoTwoImage));
        }

        [Test]
        public void SaveAndOpen_RightSudokuVisibility_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.RightSudokuVisibility, Is.EqualTo(_viewModel.RightSudokuVisibility));
        }

        [Test]
        public void SaveAndOpen_RoundName_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.RoundName, Is.EqualTo(_viewModel.RoundName));
        }

        [Test]
        public void SaveAndOpen_RoundNumber_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.RoundNumber, Is.EqualTo(_viewModel.RoundNumber));
        }

        [Test]
        public void SaveAndOpen_SudokuPageVisibility_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.SudokuPageVisibility, Is.EqualTo(_viewModel.SudokuPageVisibility));
        }

        [Test]
        public void SaveAndOpen_TimeForSolving_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.TimeForSolving, Is.EqualTo(_viewModel.TimeForSolving));
        }

        [Test]
        public void SaveAndOpen_TotalPoints_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.TotalPoints, Is.EqualTo(_viewModel.TotalPoints));
        }

        [Test]
        public void SaveAndOpen_TournamentDate_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.TournamentDate, Is.EqualTo(_viewModel.TournamentDate));
        }

        [Test]
        public void SaveAndOpen_TournamentName_True()
        {
            XmlSerializer serializer = SaveBooklet.Save(_viewModel, "mockNameOfFile", false);
            BookletStore.Instance.CreatingBookletViewModel = new CreatingBookletViewModel();
            OpenBooklet.Deserializate("mockNameOfFile", serializer);
            CreatingBookletViewModel newViewModel = BookletStore.Instance.CreatingBookletViewModel;

            Assert.That(newViewModel.TournamentName, Is.EqualTo(_viewModel.TournamentName));
        }
    }
}
