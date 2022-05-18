using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using SudokuGraphicCreator.View;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Show dialog window for <see cref="BookletInformationsViewModel"/>. Can create new booklet.
    /// </summary>
    public class BookletInfoWindowCommand : BaseCommand
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BookletInfoWindowCommand"/> class.
        /// </summary>
        /// <param name="createNewBooklet">True for creating a new instance of booklet, otherwise false.</param>
        public BookletInfoWindowCommand(bool createNewBooklet)
        {
            if (createNewBooklet)
            {
                BookletStore.Instance.CreateNewBooklet();
            }
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked and show dialog window <see cref="BookletInformations"/>.
        /// </summary>
        /// <param name="parameter">Data used by the command. This command does not require any data, this object can be set to null.</param>
        public override void Execute(object parameter)
        {
            App.DialogService.ShowDialog(new BookletInformationsViewModel());
        }
    }
}
