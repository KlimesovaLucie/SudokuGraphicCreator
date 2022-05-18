using System;

namespace SudokuGraphicCreator.Dialog
{
    /// <summary>
    /// Provides event for closing window.
    /// </summary>
    public interface IDialogRequestClose
    {
        /// <summary>
        /// Event for closing window with result.
        /// </summary>
        event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}
