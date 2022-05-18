using SudokuGraphicCreator.Commands;
using System.Windows.Input;

using SudokuGraphicCreator.View;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Represents basic button for <see cref="CreatingSudoku"/> represents toggle button with graphic element with one look.
    /// </summary>
    public class ElementButton : ElementControl
    {
        private readonly ICreatingSudokuViewModel _viewModel;

        /// <summary>
        /// Path to file in file system to icon of button.
        /// </summary>
        public string ImageSource { get; set; }

        public double ImageWidth { get; set; }

        public double ImageHeight { get; set; }

        /// <summary>
        /// Command for checking only one button of whole set.
        /// </summary>
        public ICommand ElementButtonSelectedCommand { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ElementButton"/> class.
        /// </summary>
        /// <param name="text">Text of button.</param>
        /// <param name="imageSource">Path to file in file system to icon of button.</param>
        /// <param name="viewModel">ViewModel class of <see cref="CreatingSudoku"/> view.</param>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        public ElementButton(string text, string imageSource, ICreatingSudokuViewModel viewModel, double imageWidth = 30, double imageHeight = 30)
        {
            Checked = false;
            Text = text;
            ImageSource = imageSource;
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
            _viewModel = viewModel;
            NameOfElement = text;
            ElementButtonSelectedCommand = new ElementButtonCommand(_viewModel);
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
