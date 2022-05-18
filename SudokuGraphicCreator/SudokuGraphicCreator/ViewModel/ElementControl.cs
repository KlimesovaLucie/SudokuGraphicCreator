using SudokuGraphicCreator.View;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Abstract button for <see cref="CreatingSudoku"/> represents toggle button with graphic element.
    /// </summary>
    public abstract class ElementControl : BaseViewModel
    {
        private bool _checked;

        /// <summary>
        /// true of button is checked, otherwise false.
        /// </summary>
        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;
                OnPropertyChanged(nameof(Checked));
            }
        }

        /// <summary>
        /// Text on button.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Name of element which represents.
        /// </summary>
        public string NameOfElement { get; protected set; }
    }
}
