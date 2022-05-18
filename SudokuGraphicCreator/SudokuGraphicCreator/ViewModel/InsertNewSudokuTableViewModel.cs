using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// The viewModel class for <see cref="InsertNewSudokuTable"/> view. It is responsible for inserting new sudoku into booklet.
    /// </summary>
    public class InsertNewSudokuTableViewModel : BaseViewModel, IDialogRequestClose, ISudokuImageBooklet, IInsertedSudokuTable
    {
        protected ISudokuInBookletViewModel _sudokuViewModel;

        /// <summary>
        /// Name of inserted sudoku.
        /// </summary>
        public string Name
        {
            get => _sudokuViewModel.Name;
            set
            {
                _sudokuViewModel.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Points of inserted sudoku.
        /// </summary>
        public int Points
        {
            get => _sudokuViewModel.Points;
            set
            {
                _sudokuViewModel.Points = value;
                OnPropertyChanged(nameof(Points));
            }
        }

        public string Rules
        {
            get => _sudokuViewModel.Rules;
            set
            {
                _sudokuViewModel.Rules = value;
                OnPropertyChanged(nameof(Rules));
            }
        }

        /// <summary>
        /// Image of inserted sudoku.
        /// </summary>
        public BitmapImage SudokuTable
        {
            get => _sudokuViewModel.Table;
            set
            {
                _sudokuViewModel.Table = value;
                OnPropertyChanged(nameof(SudokuTable));
            }
        }

        /// <summary>
        /// File name of image of inserted sudoku.
        /// </summary>
        public string SudokuTableName
        {
            get => _sudokuViewModel.TableName;
            set
            {
                _sudokuViewModel.TableName = value;
                OnPropertyChanged(nameof(SudokuTableName));
            }
        }

        /// <summary>
        /// Full path in file system to file with name of inserted sudoku.
        /// </summary>
        public string SudokuTableFullPath
        {
            get => _sudokuViewModel.TableFullPath;
            set
            {
                _sudokuViewModel.TableFullPath = value;
                OnPropertyChanged(nameof(SudokuTableFullPath));
            }
        }

        /// <summary>
        /// Image with solution of inserted sudoku.
        /// </summary>
        public BitmapImage SudokuSolution
        {
            get => _sudokuViewModel.Solution;
            set
            {
                _sudokuViewModel.Solution = value;
                OnPropertyChanged(nameof(SudokuSolution));
            }
        }

        /// <summary>
        /// File name of image with solution of inserted sudoku.
        /// </summary>
        public string SudokuSolutionName
        {
            get => _sudokuViewModel.SolutionName;
            set
            {
                _sudokuViewModel.SolutionName = value;
                OnPropertyChanged(nameof(SudokuSolutionName));
            }
        }

        /// <summary>
        /// Full path in file system to image file with solution of inserted sudoku.
        /// </summary>
        public string SudokuSolutionFullPath
        {
            get => _sudokuViewModel.SolutionFullPath;
            set
            {
                _sudokuViewModel.SolutionFullPath = value;
                OnPropertyChanged(nameof(SudokuSolutionFullPath));
            }
        }

        private bool _isSelectedSupported;

        /// <summary>
        /// true if inserted sudoku in supported, otherwise false.
        /// </summary>
        public bool IsSelectedSupported
        {
            get => _isSelectedSupported;
            set
            {
                _isSelectedSupported = value;
                OnPropertyChanged(nameof(IsSelectedSupported));
            }
        }

        private bool _isSelectedUnsupported;

        /// <summary>
        /// true if inserted sudoku in unsupported, otherwise false.
        /// </summary>
        public bool IsSelectedUnsupported
        {
            get => _isSelectedUnsupported;
            set
            {
                _isSelectedUnsupported = value;
                GenerateSolution = false;
                OnPropertyChanged(nameof(IsSelectedUnsupported));
            }
        }

        private bool _generateSolution;

        public bool GenerateSolution
        {
            get => _generateSolution;
            set
            {
                _generateSolution = value;
                OnPropertyChanged(nameof(GenerateSolution));
            }
        }

        private Visibility _selectVariantsVisibility = Visibility.Hidden;
        
        /// <summary>
        /// <see cref="Visibility"/> of block for selecting sudoku variant type.
        /// </summary>
        public Visibility SelectVariantsVisibility
        {
            get => _selectVariantsVisibility;
            set
            {
                _selectVariantsVisibility = value;
                OnPropertyChanged(nameof(SelectVariantsVisibility));
            }
        }

        #region COMMANDS
        /// <summary>
        /// Command for changing value of <see cref="SelectVariantsVisibility"/>.
        /// </summary>
        public ICommand ChangeSelectVariantVisibilityCommand { get; }

        /// <summary>
        /// Command for selecting files from file system.
        /// </summary>
        public ICommand SelectFileCommand { get; }

        /// <summary>
        /// Ok command for <see cref="InsertNewSudokuTable"/> view.
        /// </summary>
        public ICommand OkCommand { get; protected set; }

        /// <summary>
        /// Cancel command for <see cref="InsertNewSudokuTable"/> view.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Command for creating appropriate format of rules.
        /// </summary>
        public ICommand VariantCommand { get; }
        #endregion

        /// <summary>
        /// All sudoku in booklet.
        /// </summary>
        public List<SudokuInBookletViewModel> AllSudoku
        {
            get
            {
                List<SudokuInBookletViewModel> result = new List<SudokuInBookletViewModel>();
                foreach (var page in _bookletPages)
                {
                    foreach (var sudoku in page.Sudoku)
                    {
                        result.Add(sudoku);
                    }
                }
                return result;
            }
        }

        private readonly ObservableCollection<PageViewModel> _bookletPages;

        /// <summary>
        /// Initializes a new instance of <see cref="InsertNewSudokuTableViewModel"/> class.
        /// </summary>
        /// <param name="sudokuViewModel">ViewModel of <see cref="SudokuInBooklet"/> model.</param>
        /// <param name="tableOrder">Order number of sudoku in booklet.</param>
        public InsertNewSudokuTableViewModel(ISudokuInBookletViewModel sudokuViewModel, int tableOrder, ICreatingBookletViewModel viewModel)
        {
            _bookletPages = viewModel.Pages;
            OkCommand = new OkSudokuTableCommand(sudokuViewModel, this);
            CancelCommand = new ActionCommand(_ => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)), _ => true);
            _sudokuViewModel = sudokuViewModel;
            ChangeSelectVariantVisibilityCommand = new ActionCommand(param => ChangeVisibility(), param => true);
            SelectFileCommand = new SelectFileNewSudokuCommand(this);
            SetCorrectOrderNumber(tableOrder);
            VariantCommand = new RulesCommand(this);
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        /// <summary>
        /// 
        /// </summary>
        public void CloseWindowWithOk()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        private void SetCorrectOrderNumber(int order)
        {
            _sudokuViewModel.OrderNumber = order;
        }

        private void ChangeVisibility()
        {
            SelectVariantsVisibility = SelectVariantsVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
