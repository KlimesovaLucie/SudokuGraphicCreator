using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.View;
using SudokuGraphicCreator.Stores;
using System;
using System.IO;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class of <see cref="InsertGivenNumbers"/> view.
    /// </summary>
    public class InsertGivenNumbersViewModel : BaseViewModel, IDialogRequestClose
    {
        private readonly ICreatingSudokuViewModel _viewModel;

        private readonly int _gridSize = SudokuStore.Instance.Sudoku.Grid.Size;

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

        private int[] _insertedNumbers;

        /// <summary>
        /// Ok command for <see cref="InsertGivenNumbers"/> view.
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        /// Cancel command for <see cref="InsertGivenNumbers"/> view.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Command for selesting file for <see cref="InsertGivenNumbers"/> view.
        /// </summary>
        public ICommand SelectFileCommand { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="InsertGivenNumbersViewModel"/>
        /// </summary>
        /// <param name="viewModel">ViewModel for <see cref="CreatingSudoku"/> view.</param>
        public InsertGivenNumbersViewModel(ICreatingSudokuViewModel viewModel)
        {
            _insertedNumbers = new int[_gridSize * _gridSize];
            OkCommand = new ActionCommand(
                _ =>
                {
                    CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
                    SaveInsertedNumbers();
                },
                _ => IsCorrectFormat());
            CancelCommand = new ActionCommand(_ => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)), _ => true);
            _viewModel = viewModel;
            SelectFileCommand = new ActionCommand(_ => OpenStringForGivenNumbers(), _ => true);
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        /// <summary>
        /// Deside if <see cref="InputString"/> is in correct format.
        /// </summary>
        /// <returns></returns>
        public bool IsCorrectFormat()
        {
            if (InputString == null)
            {
                return false;
            }
            int stringLenght = InputString.Length;
            int tableCellCount = SudokuStore.Instance.Sudoku.Grid.Size * SudokuStore.Instance.Sudoku.Grid.Size;
            if (stringLenght != tableCellCount)
            {
                return false;
            }

            for (int i = 0; i < stringLenght; i++)
            {
                try
                {
                    _insertedNumbers[i] = Int32.Parse(InputString[i].ToString());
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        private void SaveInsertedNumbers()
        {
            for (int i = 0; i < _gridSize * _gridSize; i++)
            {
                int actualNumber = _insertedNumbers[i];
                if (actualNumber != 0)
                {
                    _viewModel.CellNumbers[i].Number = actualNumber;
                }
            }
        }

        private void OpenStringForGivenNumbers()
        {
            IIOService service = new IOService();
            string fileName = service.OpenTextFile();
            if (fileName == null || fileName == "")
            {
                return;
            }
            try
            {
                InputString = File.ReadAllText(fileName);
            }
            catch
            { }
        }
    }
}
