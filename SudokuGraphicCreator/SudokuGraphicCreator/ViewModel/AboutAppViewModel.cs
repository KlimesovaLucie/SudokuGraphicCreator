using SudokuGraphicCreator.Dialog;
using System;

namespace SudokuGraphicCreator.ViewModel
{
    public class AboutAppViewModel : BaseViewModel, IDialogRequestClose
    {
        public void CLoseWindow()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}
