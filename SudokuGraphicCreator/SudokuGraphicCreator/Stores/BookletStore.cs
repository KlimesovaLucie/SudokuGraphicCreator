using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System;

namespace SudokuGraphicCreator.Stores
{
    /// <summary>
    /// Class for storing only one instance of booklet created by this app.
    /// </summary>
    public class BookletStore
    {
        private static BookletStore _instance = new BookletStore();

        /// <summary>
        /// Instance of this class.
        /// </summary>
        public static BookletStore Instance => _instance;

        /// <summary>
        /// Created booklet.
        /// </summary>
        public Booklet Booklet { get; set; }

        /// <summary>
        /// ViewModel clas for <see cref="CreatingBooklet"/> view.
        /// </summary>
        public CreatingBookletViewModel CreatingBookletViewModel { get; set; }

        private BookletStore()
        {
            Booklet = new Booklet("", "", DateTime.Now, "", "0", "", "");
        }

        static BookletStore()
        {

        }

        public void CreateNewBooklet()
        {
            Booklet = new Booklet();
        }
    }
}
