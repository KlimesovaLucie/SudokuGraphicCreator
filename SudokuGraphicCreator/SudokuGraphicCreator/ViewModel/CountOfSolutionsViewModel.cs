using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.View;
using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.Properties.Resources;
using System;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="CountOfSolutions"/> view.
    /// </summary>
    public class CountOfSolutionsViewModel : BaseViewModel, IDialogRequestClose
    {
        private string _solutionMessage;

        /// <summary>
        /// Message with count of solutions.
        /// </summary>
        public string SolutionMessage
        {
            get => _solutionMessage;
            set
            {
                _solutionMessage = value;
                OnPropertyChanged(nameof(SolutionMessage));
            }
        }

        private Visibility _solutionGridVisibility;

        /// <summary>
        /// If computation of solution is done <see cref="Visibility.Visible"/>, otherwise <see cref="Visibility.Hidden"/>.
        /// </summary>
        public Visibility SolutionGridVisibility
        {
            get => _solutionGridVisibility;
            set
            {
                _solutionGridVisibility = value;
                OnPropertyChanged(nameof(SolutionGridVisibility));
            }
        }

        private Visibility _calculationGridVisibility;

        /// <summary>
        /// If computation of solution going <see cref="Visibility.Visible"/>, otherwise <see cref="Visibility.Hidden"/>.
        /// </summary>
        public Visibility CalculationGridVisibility
        {
            get => _calculationGridVisibility;
            set
            {
                _calculationGridVisibility = value;
                OnPropertyChanged(nameof(CalculationGridVisibility));
            }
        }

        private readonly CancellationTokenSource _ctSource;

        private int _solutionCount;

        /// <summary>
        /// Count of founded solutions.
        /// </summary>
        public int SolutionCount
        {
            get => _solutionCount;
            set
            {
                _solutionCount = value;
                OnPropertyChanged(nameof(SolutionCount));
                if (SolutionCount == 1)
                {
                    Enable = true;
                }
                else
                {
                    Enable = false;
                }
            }
        }

        /// <summary>
        /// Command for OK in <see cref="CountOfSolutions"/>.
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        /// Command for end computation of solutions in <see cref="CountOfSolutions"/>.
        /// </summary>
        public ICommand AbortCommand { get; }

        /// <summary>
        /// Command for export solution in <see cref="CountOfSolutions"/>.
        /// </summary>
        public ICommand ExportSolution { get; }

        private bool _enable;

        /// <summary>
        /// true if <see cref="ExportSolution"/> can be invoked, otherwise false.
        /// </summary>
        public bool Enable
        {
            get => _enable;
            set
            {
                _enable = value;
                OnPropertyChanged(nameof(Enable));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CountOfSolutionsViewModel"/> class.
        /// </summary>
        /// <param name="ctSource"><see cref="CancellationToken"/> for stoppin computation of solutions.</param>
        public CountOfSolutionsViewModel(CancellationTokenSource ctSource)
        {
            CalculationGridVisibility = Visibility.Visible;
            SolutionGridVisibility = Visibility.Hidden;
            OkCommand = new ActionCommand(_ => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)), _ => true);
            _ctSource = ctSource;
            AbortCommand = new ActionCommand(_ => { _ctSource.Cancel(); }, _ => true);
            ExportSolution = new ExportSolutionCount(this);
        }

        private void CreateSolutionMessage(int solutionCount)
        {
            SolutionCount = solutionCount;
            StringBuilder message = new StringBuilder(Resources.SudokuHas);
            message.Append(" ");
            if (solutionCount == 0|| solutionCount == 1)
            {
                message.Append(solutionCount.ToString())
                    .Append(" ")
                    .Append(Resources.SolutionSingular);
            }
            else
            {
                message.Append(Resources.More)
                    .Append(" ")
                    .Append(Resources.SolutionPlural);
            }
            SolutionMessage = message.ToString();
        }

        /// <summary>
        /// Set founded <paramref name="count"/> into <see cref="SolutionMessage"/>.
        /// </summary>
        /// <param name="count"></param>
        public void SetSolutionCount(int count)
        {
            CreateSolutionMessage(count);
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}
