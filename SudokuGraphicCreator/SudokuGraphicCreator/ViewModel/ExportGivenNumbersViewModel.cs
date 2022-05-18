using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.Stores;
using System;
using System.Text;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    public class ExportGivenNumbersViewModel : BaseViewModel, IDialogRequestClose
    {
        private string _inputString;

        /// <summary>
        /// Input string with given numbers.
        /// </summary>
        public string InputString
        {
            get => _inputString;
            set
            {
                _inputString = value;
                OnPropertyChanged(nameof(InputString));
            }
        }

        /// <summary>
        /// Ok command for <see cref="InsertGivenNumbers"/> view.
        /// </summary>
        public ICommand OkCommand { get; }

        public ExportGivenNumbersViewModel()
        {
            OkCommand = OkCommand = new ActionCommand(_ => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)), _ => true);
            CreateInputString();
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        private void CreateInputString()
        {
            StringBuilder str = new StringBuilder();
            foreach (var number in SudokuStore.Instance.Sudoku.GivenNumbers)
            {
                str.Append(number.ToString());
            }
            InputString = str.ToString();
        }
    }
}
