using NUnit.Framework;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System.Collections.Generic;

namespace SudokuGraphicCreator.Tests.ViewModels
{
    public class DeleteSudokuPageViewModelTests
    {
        private DeleteSudokuPageViewModel _pageViewModel;

        private ICreatingBookletViewModel _bookletViewModel;

        [SetUp]
        public void SetUp()
        {
            _bookletViewModel = new CreatingBookletViewModel();
            _pageViewModel = new DeleteSudokuPageViewModel(_bookletViewModel);
            BookletStore.Instance.Booklet = new Model.Booklet();
        }

        [Test]
        public void GetNumberOfPages()
        {
            _bookletViewModel.Pages.Add(new PageViewModel(1));
            _bookletViewModel.Pages.Add(new PageViewModel(2));
            _bookletViewModel.Pages.Add(new PageViewModel(3));
            
            Assert.That(GetNumbersOfPages, Is.EqualTo(_pageViewModel.Pages));
        }

        [Test]
        public void RemoveAndUpdateNumbersOfPages()
        {
            Assert.That(GetNumbersOfPages(), Is.EqualTo(_pageViewModel.Pages));

            _bookletViewModel.Pages.Add(new PageViewModel(1));
            _bookletViewModel.Pages.Add(new PageViewModel(2));
            _bookletViewModel.Pages.Add(new PageViewModel(3));

            Assert.IsTrue(_pageViewModel.DeleteCommand.CanExecute(null));
            _pageViewModel.SelectedPageOrder = 1;
            _pageViewModel.DeleteCommand.Execute(null);

            var pages = GetNumbersOfPages();
            Assert.That(pages, Is.EqualTo(_pageViewModel.Pages));
            Assert.IsTrue(pages.Count == 2);
        }

        private List<int?> GetNumbersOfPages()
        {
            List<int?> pages = new List<int?>();
            foreach (var page in BookletStore.Instance.Booklet.Pages)
            {
                pages.Add(page.Order);
            }
            return pages;
        }
    }
}
