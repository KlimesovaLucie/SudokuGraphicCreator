using System.Collections.Generic;
using System.ComponentModel;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Provides informations for deleting booklet's page.
    /// </summary>
    public interface IDeleteSudokuPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Order number of actual selected booklet's page.
        /// </summary>
        int? SelectedPageOrder { get; set; }

        /// <summary>
        /// Invoke <see cref="PropertyChanged"/> for <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Name of changed property.</param>
        void OnPropertyChanged(string name = null);

        /// <summary>
        /// Order numbers of pages of booklet.
        /// </summary>
        List<int?> Pages { get; }
    }
}
