using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// The viewModel class for <see cref="EditBooklet"/> view. Responsible for swapping sudoku in booklet.
    /// </summary>
    public class EditBookletViewModel : BaseViewModel, IDialogRequestClose
    {
        private readonly ICreatingBookletViewModel _viewModel;

        /// <summary>
        /// Order numbers of pages in booklet.
        /// </summary>
        public List<int> Pages
        {
            get
            {
                List<int> result = new List<int>();
                foreach (var page in _viewModel.Pages)
                {
                    result.Add(page.PageNumber);
                }
                return result;
            }
        }

        private int _selectedPageOrder = 1;

        /// <summary>
        /// Order number of selected page in booklet.
        /// </summary>
        public int SelectedPageOrder
        {
            get => _selectedPageOrder;
            set
            {
                _selectedPageOrder = value;
                OnPropertyChanged(nameof(SelectedPageOrder));
                OnPropertyChanged(nameof(ActualPage));
                OnPropertyChanged(nameof(FirstSudoku));
                OnPropertyChanged(nameof(SecondSudoku));
            }
        }

        /// <summary>
        /// Index of selected page in booklet.
        /// </summary>
        public int SelectedPageIndex
        {
            get => SelectedPageOrder - 1;
            set
            {
                SelectedPageOrder = value + 1;
                OnPropertyChanged(nameof(SelectedPageIndex));
            }
        }

        /// <summary>
        /// <see cref="PageViewModel"/> of actual selected page.
        /// </summary>
        public PageViewModel ActualPage
        {
            get => _viewModel.Pages[SelectedPageIndex];
        }

        /// <summary>
        /// Left placed sudoku on selected page.
        /// </summary>
        public SudokuInBookletViewModel FirstSudoku
        {
            get => ActualPage.Sudoku[0];
            set
            {
                ActualPage.Sudoku[0] = value;
                ActualPage.GetForInsert().SudokuOnPage[0] = value.GetModel();
                OnPropertyChanged(nameof(FirstSudoku));
            }
        }

        /// <summary>
        /// Right placed sudoku on selected page.
        /// </summary>
        public SudokuInBookletViewModel SecondSudoku
        {
            get
            {
                if (ActualPage.Sudoku.Count != 2)
                {
                    return null;
                }
                return ActualPage.Sudoku[1];
            }
            set
            {
                if (ActualPage.Sudoku.Count == 2)
                {
                    ActualPage.Sudoku[1] = value;
                    ActualPage.GetForInsert().SudokuOnPage[1] = value.GetModel();
                    OnPropertyChanged(nameof(SecondSudoku));
                }
            }
        }

        /// <summary>
        /// All sudoku in booklet.
        /// </summary>
        public ObservableCollection<SudokuInBookletViewModel> AllSudoku
        {
            get => _viewModel.ListOfSudoku;
        }

        /// <summary>
        /// Ok command for <see cref="EditBooklet"/> window.
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="EditBookletViewModel"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingBooklet"/> view.</param>
        public EditBookletViewModel(ICreatingBookletViewModel viewModel)
        {
            _viewModel = viewModel;
            OkCommand = new ActionCommand(_ =>
            {
                DoCorrectionOrderNumbers();
                CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
            },
                _ => CanOkCommand());
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        private void DoCorrectionOrderNumbers()
        {
            for (int i = 0; i < AllSudoku.Count; i++)
            {
                AllSudoku[i].OrderNumber = i + 1;
            }
        }

        private bool CanOkCommand()
        {
            if (FirstSudoku.Equals(SecondSudoku))
            {
                return false;
            }

            return IsSudokuOrderInBookletCorrect() && IsSudokuOnlyOnce(FirstSudoku) &&
                (SecondSudoku == null || (SecondSudoku != null && IsSudokuOnlyOnce(SecondSudoku)));
        }

        private bool IsSudokuOnlyOnce(SudokuInBookletViewModel actualSudoku)
        {
            int count = 0;
            foreach (var sudoku in AllSudoku)
            {
                if (actualSudoku.Equals(sudoku))
                {
                    count++;
                }

                if (count > 1)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsSudokuOrderInBookletCorrect()
        {
            for (int i = 0; i < AllSudoku.Count; i++)
            {
                for (int j = 0; j < AllSudoku.Count; j++)
                {
                    if (i != j && AllSudoku[i] == AllSudoku[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
