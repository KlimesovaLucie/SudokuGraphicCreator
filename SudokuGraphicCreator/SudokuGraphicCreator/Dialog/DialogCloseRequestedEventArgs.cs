using System;

namespace SudokuGraphicCreator.Dialog
{
    /// <summary>
    /// Contains and provides result of closed window.
    /// </summary>
    public class DialogCloseRequestedEventArgs : EventArgs
    {
        /// <summary>
        /// Result of closed dialog. true if window was closed by OK button, false by cancel button or closed window.
        /// </summary>
        public bool? DialogResult { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="DialogCloseRequestedEventArgs"/> class.
        /// </summary>
        /// <param name="dialogResult">true if window was closed by OK button, false by cancel button or closed window.</param>
        public DialogCloseRequestedEventArgs(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}
