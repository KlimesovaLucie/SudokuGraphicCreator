using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// The viewModel class for <see cref="DeleteSudokuTable"/> view.
    /// </summary>
    public class DeleteSudokuTableViewModel : IDialogRequestClose, IDeleteSudokuTableViewModel
    {
        private readonly ICreatingBookletViewModel _viewModel;

        private SudokuInBookletViewModel _selectedSudoku;
        
        public SudokuInBookletViewModel SelectedSudoku
        {
            get => _selectedSudoku;
            set
            {
                _selectedSudoku = value;
                OnPropertyChanged(nameof(SelectedSudoku));
            }
        }

        public ObservableCollection<SudokuInBookletViewModel> AllSudoku => _viewModel.ListOfSudoku;

        /// <summary>
        /// Command for delete selected sudoku.
        /// </summary>
        public ICommand DeleteCommand { get; }

        /// <summary>
        /// Cancel command for <see cref="DeleteSudokuTable"/> window.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="DeleteSudokuTableViewModel"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class if <see cref="CreatingBooklet"/> view.</param>
        public DeleteSudokuTableViewModel(ICreatingBookletViewModel viewModel)
        {
            _viewModel = viewModel;
            SelectedSudoku = _viewModel.ListOfSudoku.Count == 0 ? null : _viewModel.ListOfSudoku[0];
            DeleteCommand = new SudokuDeleteCommand(_viewModel, this);
            CancelCommand = new ActionCommand(_ => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)), _ => true);
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
