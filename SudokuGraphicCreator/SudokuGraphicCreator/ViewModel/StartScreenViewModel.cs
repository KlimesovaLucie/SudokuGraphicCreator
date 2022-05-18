using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.View;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="StartScreen"/> view.
    /// </summary>
    public class StartScreenViewModel : BaseViewModel
    {
        /// <summary>
        /// Command for showing <see cref="CreateSudokuSize"/> view.
        /// </summary>
        public ICommand DisplaySudokuSizeCommand { get; }

        /// <summary>
        /// Command for showing <see cref="BookletInformations"/> view.
        /// </summary>
        public ICommand DisplayBookletInfoCommand { get; }

        /// <summary>
        /// Command for showing OpenFielDialog for open booklet.
        /// </summary>
        public ICommand OpenBookletCommand { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="StartScreenViewModel"/> class.
        /// </summary>
        public StartScreenViewModel()
        {
            DisplaySudokuSizeCommand = new ActionCommand(_ => DisplaySizeSudokuWindow(), _ => true);
            DisplayBookletInfoCommand = new BookletInfoWindowCommand(true);
            OpenBookletCommand = new ActionCommand(_ => IO.OpenBooklet.Open(), _ => true);
        }

        private void DisplaySizeSudokuWindow()
        {
            App.DialogService.ShowDialog(new CreateSudokuSizeViewModel());
        }
    }
}
