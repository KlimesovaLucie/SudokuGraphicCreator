using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.View;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Represents button for <see cref="CreatingSudoku"/> represents toggle button with graphic element with more look.
    /// </summary>
    public class ElementButtonOption : ElementControl
    {
        private readonly ICreatingSudokuViewModel _viewModel;

        /// <summary>
        /// Path to file in file system to icon of button.
        /// </summary>
        public string ImageSource { get; set; }

        /// <summary>
        /// Width of icon.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Height of icon.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Name of graphic element which represents.
        /// </summary>
        public string ElementName { get; private set; }

        /// <summary>
        /// Command for checking only one button of whole set.
        /// </summary>
        public ICommand ElementButtonSelectedCommand { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ElementButtonOption"/> class.
        /// </summary>
        /// <param name="imageSource">Path to file in file system to icon of button.</param>
        /// <param name="elementName">Name of graphic element which represents.</param>
        /// <param name="viewModel">ViewModel class of <see cref="CreatingSudoku"/> view.</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// 
        public ElementButtonOption(string imageSource, string elementName, ICreatingSudokuViewModel viewModel, double width = 15, double height = 15)
        {
            Checked = false;
            ImageSource = imageSource;
            Width = width;
            Height = height;
            ElementName = elementName;
            Text = elementName;
            _viewModel = viewModel;
            ElementButtonSelectedCommand = new ElementButtonCommand(_viewModel);
            NameOfElement = elementName;
        }

        public override string ToString()
        {
            return ElementName;
        }
    }
}
