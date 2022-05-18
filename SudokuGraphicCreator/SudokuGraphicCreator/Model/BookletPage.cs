using SudokuGraphicCreator.ViewModel;
using System.Collections.Generic;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// The model class for <see cref="PageViewModel"/> model. Represents one page in booklet.
    /// </summary>
    public class BookletPage
    {
        /// <summary>
        /// Order number of page.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Collection of sudoku on this page in booklet.
        /// </summary>
        public List<SudokuInBooklet> SudokuOnPage { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="BookletPage"/> class. Used for xml deserialization.
        /// </summary>
        /// <param name="order"></param>
        public BookletPage(int order)
        {
            Order = order;
            SudokuOnPage = new List<SudokuInBooklet>();
        }

        /// <summary>
        /// Add <paramref name="sudoku"/> to <see cref="SudokuOnPage"/> collection.
        /// </summary>
        /// <param name="sudoku">Model class for <see cref="SudokuInBookletViewModel"/> viewModel.</param>
        public void AddSudokuOnPage(SudokuInBooklet sudoku)
        {
            SudokuOnPage.Add(sudoku);
        }
    }
}
