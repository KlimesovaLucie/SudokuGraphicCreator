using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// The viewModel class for <see cref="DeleteSudokuPage"/> view.
    /// </summary>
    public class DeleteSudokuPageViewModel : IDialogRequestClose, IDeleteSudokuPageViewModel
    {
        private int? _selectedPageOrder = 1;

        public int? SelectedPageOrder
        {
            get => _selectedPageOrder;
            set
            {
                _selectedPageOrder = value;
                OnPropertyChanged(nameof(SelectedPageOrder));
            }
        }

        public List<int?> Pages
        {
            get
            {
                List<int?> result = new List<int?>();
                foreach (var page in BookletStore.Instance.Booklet.Pages)
                {
                    result.Add(page.Order);
                }
                return result;
            }
        }

        /// <summary>
        /// Command for delete selected page.
        /// </summary>
        public ICommand DeleteCommand { get; }

        /// <summary>
        /// Cancel command for <see cref="DeleteSudokuPage"/> window.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="DeleteSudokuPageViewModel"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingBooklet"/> view.</param>
        public DeleteSudokuPageViewModel(ICreatingBookletViewModel viewModel)
        {
            DeleteCommand = new DeletePageCommand(viewModel, this);
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
