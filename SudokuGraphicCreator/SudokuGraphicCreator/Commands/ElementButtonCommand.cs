using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Allows to be checked zero or one of toggle buttons from given number, irregular grid, variant and
    /// graphic element panel.
    /// If appropiate button is checked, user can select cell of sudoku grid.
    /// </summary>
    public class ElementButtonCommand : BaseCommand
    {
        private readonly ICreatingSudokuViewModel _viewModel;

        private readonly SelectedCellCommand _selectedCellCommand;

        /// <summary>
        /// Initializes a new instance of <see cref="ElementButtonCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingSudoku"/> view.</param>
        public ElementButtonCommand(ICreatingSudokuViewModel viewModel)
        {
            _viewModel = viewModel;
            _selectedCellCommand = new SelectedCellCommand(viewModel);
        }

        /// <summary>
        /// Checked only one or zero button from element buttons.
        /// </summary>
        /// <param name="parameter">Actual clicked button.</param>
        public override void Execute(object parameter)
        {
            ElementControl button = FindButton(parameter as string);
            if (button.Checked)
            {
                UncheckAllButtons(_viewModel.SudokuVariantElementButton);
                UncheckAllButtons(_viewModel.SudokuGraphicElementButton);
                _viewModel.GivenNumberButton.Checked = false;
                _viewModel.GridButton.Checked = false;
                button.Checked = true;
                return;
            }
            else
            {
                ActualButtonUnchecked(button);
            }
        }

        private ElementControl FindButton(string text)
        {
            if (text == _viewModel.GivenNumberButton.NameOfElement)
            {
                return _viewModel.GivenNumberButton;
            }

            foreach (var button in _viewModel.SudokuVariantElementButton)
            {
                if (button.NameOfElement == text)
                {
                    return button;
                }

                ElementButtonGraphicOptions option = button as ElementButtonGraphicOptions;
                if (option != null)
                {
                    foreach (var graphicOption in option.ElementButtonOptions)
                    {
                        if (graphicOption.NameOfElement == text)
                        {
                            return graphicOption;
                        }
                    }
                }
            }

            foreach (var button in _viewModel.SudokuGraphicElementButton)
            {
                if (button.NameOfElement == text)
                {
                    return button;
                }

                ElementButtonGraphicOptions option = button as ElementButtonGraphicOptions;
                if (option != null)
                {
                    foreach (var graphicOption in option.ElementButtonOptions)
                    {
                        if (graphicOption.NameOfElement == text)
                        {
                            return graphicOption;
                        }
                    }
                }
            }

            return _viewModel.GridButton;
        }

        private void ActualButtonUnchecked(ElementControl button)
        {
            string name = button.Text;
            if (name == Properties.Resources.Resources.InsertNumber)
            {
                _selectedCellCommand.SetAllCellsTransparent();
            }
        }

        private void UncheckAllButtons(ObservableCollection<ElementControl> elementControls)
        {
            _selectedCellCommand.SetAllCellsTransparent();
            foreach (var button in elementControls)
            {
                if (typeof(ElementButton).IsInstanceOfType(button))
                {
                    button.Checked = false;
                }

                if (typeof(ElementButtonGraphicOptions).IsInstanceOfType(button))
                {
                    ElementButtonGraphicOptions buttons = (ElementButtonGraphicOptions)button;
                    foreach (var item in buttons.ElementButtonOptions)
                    {
                        item.Checked = false;
                    }
                }
            }
        }
    }
}
