using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.View;
using SudokuGraphicCreator.Stores;
using System;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="CreateSudokuSize"/> view.
    /// </summary>
    public class CreateSudokuSizeViewModel : BaseViewModel, IDialogRequestClose
    {
        /// <summary>
        /// Ok command for <see cref="CreateSudokuSize"/> view.
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        /// Cancel command for <see cref="CreateSudokuSize"/> view.
        /// </summary>
        public ICommand CancelCommand { get; }

        private bool _isCheckedSixSizeTwoThree;

        /// <summary>
        /// true for create sudoku 6x6, box 2x3, otherwise false.
        /// </summary>
        public bool IsCheckedSixSizeTwoThree
        {
            get => _isCheckedSixSizeTwoThree;
            set
            {
                _isCheckedSixSizeTwoThree = value;
                OnPropertyChanged(nameof(IsCheckedSixSizeTwoThree));
            }
        }

        private bool _isCheckedSixSizeThreeTwo;

        /// <summary>
        /// true for create sudoku 6x6, box 3x2, otherwise false.
        /// </summary>
        public bool IsCheckedSixSizeThreeTwo
        {
            get => _isCheckedSixSizeThreeTwo;
            set
            {
                _isCheckedSixSizeThreeTwo = value;
                OnPropertyChanged(nameof(IsCheckedSixSizeThreeTwo));
            }
        }

        private bool _isCheckedNineSize = true;

        /// <summary>
        /// true for create sudoku 9x9, box 3x3, otherwise false.
        /// </summary>
        public bool IsCheckedNineSize
        {
            get => _isCheckedNineSize;
            set
            {
                _isCheckedNineSize = value;
                OnPropertyChanged(nameof(IsCheckedNineSize));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CreateSudokuSizeViewModel"/> class.
        /// </summary>
        public CreateSudokuSizeViewModel()
        {
            OkCommand = new ActionCommand(
                _ =>
                    {
                        int size = 9;
                        int xBox = 3;
                        int yBox = 3;
                        if (IsCheckedSixSizeTwoThree)
                        {
                            size = 6;
                            xBox = 2;
                        } else if (IsCheckedSixSizeThreeTwo)
                        {
                            size = 6;
                            yBox = 2;
                        }
                        SudokuStore.Instance.Sudoku = new Sudoku(size, xBox, yBox);
                        CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
                        NavigationStore.Instance.CurrentViewModel = new CreatingSudokuViewModel();
                    },
                _ => true);
            CancelCommand = new ActionCommand(_ => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)), _ => true);
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}