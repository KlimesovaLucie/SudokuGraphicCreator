using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.View;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Represents basic button for <see cref="CreatingSudoku"/> represents checkbox with variant which applies rules in whole table.
    /// </summary>
    public class ElementCheckBox : ElementControl
    {
        private readonly ICreatingSudokuViewModel _viewModel;

        /// <summary>
        /// true if checkox is enabled, otherwise false
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Command to be invoked if command is checked / unchecked
        /// </summary>
        public ICommand SudokuCheckBoxCommand { get; }

        /// <summary>
        /// Initiliazes a new instance of <see cref="ElementCheckBox"/> class.
        /// </summary>
        /// <param name="text">Text on checkbox.</param>
        /// <param name="creatingSudokuViewModel">ViewModel class of <see cref="CreatingSudoku"/> view.</param>
        /// <param name="enable">true if checkox is enabled, otherwise false</param>
        public ElementCheckBox(string text, ICreatingSudokuViewModel creatingSudokuViewModel, bool enable = true)
        {
            Checked = false;
            Text = text;
            Enabled = enable;
            _viewModel = creatingSudokuViewModel;
            NameOfElement = text;
            SudokuCheckBoxCommand = new VariantCheckBoxCommand(_viewModel);
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
