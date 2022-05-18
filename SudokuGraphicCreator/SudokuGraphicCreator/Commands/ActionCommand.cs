using System;
using System.Windows.Input;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command is universal approach for creating commands.
    /// </summary>
    public class ActionCommand : ICommand
    {
        private readonly Action<object> _execute;

        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// Initializes a new instance of <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="execute">Code to be executed, when this command is invoked.</param>
        /// <param name="canExecute">Predicate for deside, if command can be or cannot be invoked.</param>
        public ActionCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
