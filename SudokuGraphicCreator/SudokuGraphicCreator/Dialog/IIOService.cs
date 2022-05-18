namespace SudokuGraphicCreator.Dialog
{
    /// <summary>
    /// Provides methods for file dialog.
    /// </summary>
    public interface IIOService
    {
        /// <summary>
        /// Open OpenFileDialog for select image.
        /// </summary>
        /// <returns>Full path to selected file.</returns>
        string OpenImage();

        /// <summary>
        /// Open OpenFileDialog for open saved work on booklet.
        /// </summary>
        /// <returns></returns>
        string OpenBooklet();

        /// <summary>
        /// Open OpenFileDialog for open text file.
        /// </summary>
        /// <returns></returns>
        string OpenTextFile();

        /// <summary>
        /// Open SaveFileDialog for export image.
        /// </summary>
        /// <returns></returns>
        string SaveImage();

        /// <summary>
        /// Open SaveFileDialog for export booklet.
        /// </summary>
        /// <returns></returns>
        string SaveBooklet();

        /// <summary>
        /// Open SaveFileDialog for saving work on booklet.
        /// </summary>
        /// <returns></returns>
        string SaveWorkOnBooklet();
    }
}
