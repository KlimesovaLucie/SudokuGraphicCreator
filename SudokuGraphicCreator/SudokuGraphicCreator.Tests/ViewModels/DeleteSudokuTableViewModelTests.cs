using NUnit.Framework;
using SudokuGraphicCreator.ViewModel;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class DeleteSudokuTableViewModelTests
    {
        private DeleteSudokuTableViewModel _viewModel;

        private ICreatingBookletViewModel _creatingViewModel;

        [SetUp]
        public void SetUp()
        {
            _creatingViewModel = new CreatingBookletViewModel();
            _viewModel = new DeleteSudokuTableViewModel(_creatingViewModel);
        }

        [Test]
        public void GetCorrectAllSudoku()
        {
            var page = new PageViewModel(1);
            _creatingViewModel.Pages.Add(page);
            var sudokuOne = new SudokuInBookletViewModel();
            var sudokuTwo = new SudokuInBookletViewModel();
            page.AddSudoku(sudokuOne);
            page.AddSudoku(sudokuTwo);

            Assert.That(GetAllSudoku(), Is.EqualTo(_viewModel.AllSudoku));
        }

        [Test]
        public void DeleteSelectedSudoku()
        {
            var page = new PageViewModel(1);
            _creatingViewModel.Pages.Add(page);
            var sudokuOne = new SudokuInBookletViewModel();
            var sudokuTwo = new SudokuInBookletViewModel();
            page.AddSudoku(sudokuOne);
            page.AddSudoku(sudokuTwo);

            _viewModel.SelectedSudoku = sudokuOne;
            _viewModel.DeleteCommand.Execute(null);

            Assert.That(GetAllSudoku(), Is.EqualTo(_viewModel.AllSudoku));
            Assert.IsFalse(_viewModel.AllSudoku.Contains(sudokuOne));
        }

        private ObservableCollection<SudokuInBookletViewModel> GetAllSudoku()
        {
            var result = new ObservableCollection<SudokuInBookletViewModel>();

            foreach (var page in _creatingViewModel.Pages)
            {
                foreach (var sudoku in page.Sudoku)
                {
                    result.Add(sudoku);
                }
            }

            return result;
        }
    }
}
