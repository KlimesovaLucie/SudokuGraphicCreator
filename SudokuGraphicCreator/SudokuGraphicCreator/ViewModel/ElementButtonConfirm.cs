using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.View;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Represents button for <see cref="CreatingSudoku"/> represents toggle button with graphic element which lies in ore cells in grid.
    /// </summary>
    public class ElementButtonConfirm : ElementButton
    {
        /// <summary>
        /// Command for confirm selected cells and place or delete element.
        /// </summary>
        public ICommand ConfirmSudokuButtonCommand { get; }

        /// <summary>
        /// Command for unselect selected cells.
        /// </summary>
        public ICommand CancelSudokuButtonCommand { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ElementButtonConfirm"/> class.
        /// </summary>
        /// <param name="text">Text of button.</param>
        /// <param name="imageSource">Path to file in file system to icon of button.</param>
        /// <param name="viewModel">ViewModel class of <see cref="CreatingSudoku"/> view.</param>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        public ElementButtonConfirm(string text, string imageSource, CreatingSudokuViewModel viewModel, double imageWidth = 30, double imageHeight = 30)
            : base(text, imageSource, viewModel, imageWidth, imageHeight)
        {
            ConfirmSudokuButtonCommand = new ConfirmCommand(viewModel, text);
            CancelSudokuButtonCommand = new CancelCommand(viewModel, text);
            NameOfElement = text;
        }
    }
}
