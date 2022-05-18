using System.Windows;

namespace SudokuGraphicCreator.Dialog
{
    /// <summary>
    /// Informations about dialog window.
    /// </summary>
    public interface IDialog
    {
        /// <summary>
        /// DataContext of dialog.
        /// </summary>
        object DataContext { get; set; }

        /// <summary>
        /// Result after closing dialog.
        /// </summary>
        bool? DialogResult { get; set; }

        /// <summary>
        /// Owner of dialog.
        /// </summary>
        Window Owner { get; set; }

        /// <summary>
        /// Close dialog.
        /// </summary>
        void Close();

        /// <summary>
        /// Show dialog window.
        /// </summary>
        /// <returns></returns>
        bool? ShowDialog();
    }
}
