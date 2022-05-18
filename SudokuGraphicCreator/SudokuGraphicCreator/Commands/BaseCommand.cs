using System;
using System.Windows.Input;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Basic behaviour of command.
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        /// <summary>
        /// Invoke <see cref="CanExecuteChanged"/>.
        /// </summary>
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
