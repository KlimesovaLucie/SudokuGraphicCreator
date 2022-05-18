using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.IO;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.View;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// The viewModel class for <see cref="CreatingBooklet"/> view. It is responsible to creating and editing booklet.
    /// </summary>
    public class CreatingBookletViewModel : BaseViewModel, ICreatingBookletViewModel
    {
        private static readonly Booklet _booklet = BookletStore.Instance.Booklet;

        #region VISIBILITY
        private Visibility _introductionPageVisibility = Visibility.Visible;

        public Visibility IntroductionPageVisibility
        {
            get => _introductionPageVisibility;
            set
            {
                _introductionPageVisibility = value;
                OnPropertyChanged(nameof(IntroductionPageVisibility));
            }
        }

        private Visibility _sudokuPageVisibility = Visibility.Hidden;

        public Visibility SudokuPageVisibility
        {
            get => _sudokuPageVisibility;
            set
            {
                _sudokuPageVisibility = value;
                OnPropertyChanged(nameof(SudokuPageVisibility));
            }
        }

        private Visibility _leftSudokuVisibility = Visibility.Hidden;

        public Visibility LeftSudokuVisibility
        {
            get => _leftSudokuVisibility;
            set
            {
                _leftSudokuVisibility = value;
                OnPropertyChanged(nameof(LeftSudokuVisibility));
            }
        }

        private Visibility _rightSudokuVisibility = Visibility.Hidden;

        public Visibility RightSudokuVisibility
        {
            get => _rightSudokuVisibility;
            set
            {
                _rightSudokuVisibility = value;
                OnPropertyChanged(nameof(RightSudokuVisibility));
            }
        }
        #endregion

        private ObservableCollection<PageViewModel> _pages = new ObservableCollection<PageViewModel>();

        public ObservableCollection<PageViewModel> Pages
        {
            get => _pages;
            set
            {
                _pages = value;
                OnPropertyChanged(nameof(Pages));
            }
        }

        private PageViewModel _actualPage;

        public PageViewModel ActualPage
        {
            get => _actualPage;
            set
            {
                _actualPage = value;
                OnPropertyChanged(nameof(ActualPage));
            }
        }

        /// <summary>
        /// Name of competition.
        /// </summary>
        public string TournamentName
        {
            get => _booklet.TournamentName;
            set
            {
                _booklet.TournamentName = value;
                OnPropertyChanged(nameof(TournamentName));
            }
        }

        /// <summary>
        /// Date of competition.
        /// </summary>
        public DateTime TournamentDate
        {
            get => _booklet.TournamentDate;
            set
            {
                _booklet.TournamentDate = value;
                OnPropertyChanged(nameof(TournamentDate));
            }
        }

        /// <summary>
        /// Name of the city where the competition takes place.
        /// </summary>
        public string Location
        {
            get => _booklet.Location;
            set
            {
                _booklet.Location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        /// <summary>
        /// Number of the round of the competition.
        /// </summary>
        public string RoundNumber
        {
            get => _booklet.RoundNumber;
            set
            {
                _booklet.RoundNumber = value;
                OnPropertyChanged(nameof(RoundNumber));
            }
        }

        /// <summary>
        /// Name of the actual round of the competition.
        /// </summary>
        public string RoundName
        {
            get => _booklet.RoundName;
            set
            {
                _booklet.RoundName = value;
                OnPropertyChanged(nameof(RoundName));
            }
        }

        /// <summary>
        /// Time for solving the round.
        /// </summary>
        public string TimeForSolving
        {
            get => _booklet.TimeForSolving;
            set
            {
                _booklet.TimeForSolving = value;
                OnPropertyChanged(nameof(TimeForSolving));
            }
        }

        /// <summary>
        /// Sum of all points for every sudoku in booklet.
        /// </summary>
        public int TotalPoints
        {
            get => _booklet.TotalPoints;
        }

        /// <summary>
        /// First logo bitmap image, which is placed in the top left corner of first booklet page.
        /// </summary>
        [XmlIgnore]
        public BitmapImage LogoOneImage
        {
            get => _booklet.LogoOneImage;
            set
            {
                _booklet.LogoOneImage = value;
                OnPropertyChanged(nameof(LogoOneImage));
            }
        }

        /// <summary>
        /// Second logo bitmap image, which is placed in the top right corner of first booklet page.
        /// </summary>
        [XmlIgnore]
        public BitmapImage LogoTwoImage
        {
            get => _booklet.LogoTwoImage;
            set
            {
                _booklet.LogoTwoImage = value;
                OnPropertyChanged(nameof(LogoTwoImage));
            }
        }

        /// <summary>
        /// Third logo bitmap image, which is placed in the left bottom corner of first booklet page.
        /// </summary>
        [XmlIgnore]
        public BitmapImage LogoThreeImage
        {
            get => _booklet.LogoThreeImage;
            set
            {
                _booklet.LogoThreeImage = value;
                OnPropertyChanged(nameof(LogoThreeImage));
            }
        }

        /// <summary>
        /// Full file path in file system for first logo, which is placed in the top left corner of first booklet page.
        /// </summary>
        public string LogoOneFullPath
        {
            get => _booklet.LogoOneFullPath;
            set
            {
                try
                {
                    _booklet.LogoOneFullPath = value;
                    OnPropertyChanged(nameof(LogoOneFullPath));
                    LogoOneImage = new BitmapImage(new Uri(value));
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Full file path in file system for second logo, which is placed in the top right corner of first booklet page.
        /// </summary>
        public string LogoTwoFullPath
        {
            get => _booklet.LogoTwoFullPath;
            set
            {
                try
                {
                    _booklet.LogoTwoFullPath = value;
                    OnPropertyChanged(nameof(LogoTwoFullPath));
                    LogoTwoImage = new BitmapImage(new Uri(value));
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Full file path in file system for third logo, which is placed in the left bottom corner of first booklet page.
        /// </summary>
        public string LogoThreeFullPath
        {
            get => _booklet.LogoThreeFullPath;
            set
            {
                try
                {
                    _booklet.LogoThreeFullPath = value;
                    OnPropertyChanged(nameof(LogoThreeFullPath));
                    LogoThreeImage = new BitmapImage(new Uri(value));
                }
                catch
                { }
            }
        }

        public ObservableCollection<SudokuInBookletViewModel> ListOfSudoku
        {
            get
            {
                ObservableCollection<SudokuInBookletViewModel> result = new ObservableCollection<SudokuInBookletViewModel>();
                foreach (var page in Pages)
                {
                    foreach (var sudoku in page.Sudoku)
                    {
                        result.Add(sudoku);
                    }
                }
                return result;
            }
        }

        private string _bookletWorkFileName;

        public string BookletWorkFileName
        {
            get => _bookletWorkFileName;
            set
            {
                _bookletWorkFileName = value;
                OnPropertyChanged(nameof(BookletWorkFileName));
            }
        }

        #region COMMANDS
        /// <summary>
        /// Command for showing next page in booklet.
        /// </summary>
        public ICommand NextPageCommand { get; }

        /// <summary>
        /// Command for showing previous page in booklet.
        /// </summary>
        public ICommand PreviousPageCommand { get; }

        /// <summary>
        /// Command for creating new page in booklet.
        /// </summary>
        public ICommand CreateNewPageCommand { get; }

        /// <summary>
        /// Command for creating new sudoku in booklet.
        /// </summary>
        public ICommand NewTableCommand { get; }

        /// <summary>
        /// Command for nagivate to <see cref="StartScreen"/> view.
        /// </summary>
        public ICommand GoStartScreenCommand { get; }

        /// <summary>
        /// Command for creating new booklet.
        /// </summary>
        public ICommand CreateNewBookletCommand { get; }

        /// <summary>
        /// Command for deleting sudoku from booklet.
        /// </summary>
        public ICommand DeleteSudokuTableCommand { get; }

        /// <summary>
        /// Command for deleting page from booklet.
        /// </summary>
        public ICommand DeleteSudokuPage { get; }

        /// <summary>
        /// Command for editing basic information about competition.
        /// </summary>
        public ICommand EditBookletInformationsCommand { get; }

        /// <summary>
        /// Command for editing booklet - swapping sudoku.
        /// </summary>
        public ICommand EditBookletCommand { get; }

        /// <summary>
        /// Command for editing information about sudoku in booklet.
        /// </summary>
        public ICommand EditSudokuCommand { get; }

        /// <summary>
        /// Command for exporting booklet into PDF.
        /// </summary>
        public ICommand ExportBookletCommand { get; }

        /// <summary>
        /// Command for save as unfinished work.
        /// </summary>
        public ICommand SaveAsCommand { get; }

        public ICommand ExportFileWithSolution { get; }

        public ICommand SaveCommand { get; }

        /// <summary>
        /// End of application.
        /// </summary>
        public ICommand EndCommand { get; }

        /// <summary>
        /// Command for showing OpenFielDialog for open booklet.
        /// </summary>
        public ICommand OpenBookletCommand { get; }

        /// <summary>
        /// Show <see cref="AboutApp"/> view.
        /// </summary>
        public ICommand ShowAboutAppWindow { get; }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="CreatingBookletViewModel"/> class.
        /// </summary>
        public CreatingBookletViewModel()
        {
            NextPageCommand = new NextBookletPageCommand(this);
            PreviousPageCommand = new PreviousBookletPageCommand(this);
            CreateNewPageCommand = new NewBookletPageCommand(this);
            NewTableCommand = new NewSudokuTableBookletCommand(this);
            GoStartScreenCommand = new NavigateCommand(CreateStartScreenViewModel);
            CreateNewBookletCommand = new CreateNewBookletCommand();
            DeleteSudokuTableCommand = new SudokuDeleteWindowCommand(this);
            DeleteSudokuPage = new SudokuPageDeleteWindowCommand(this);
            EditBookletInformationsCommand = new BookletInfoWindowCommand(false);
            EditBookletCommand = new BookletEditCommand(this);
            EditSudokuCommand = new SudokuEditCommand(this);
            ExportBookletCommand = new BookletExportCommand();
            SaveAsCommand = new ActionCommand(_ => SaveAsBooklet(), _ => true);
            BookletStore.Instance.CreatingBookletViewModel = this;
            ExportFileWithSolution = new ActionCommand(_ => ExportSolution(), _ => true);
            SaveCommand = new ActionCommand(_ => SaveUnfinishedBooklet(), _ => BookletWorkFileName != "" && BookletWorkFileName != null);
            EndCommand = new ActionCommand(_ => { Application.Current.Shutdown(); }, _ => true);
            OpenBookletCommand = new ActionCommand(_ => IO.OpenBooklet.Open(), _ => true);
            ShowAboutAppWindow = new ActionCommand(_ => { App.DialogService.ShowDialog(new AboutAppViewModel()); }, _ => true);
        }

        private void SaveAsBooklet()
        {
            IIOService service = new IOService();
            string name = service.SaveWorkOnBooklet();
            if (name != null && name != "")
            {
                BookletWorkFileName = name;
                SaveBooklet.Save(this, name);
            }
        }

        private void SaveUnfinishedBooklet()
        {
            if (BookletWorkFileName != null && BookletWorkFileName != "")
            {
                SaveBooklet.Save(this, BookletWorkFileName);
            }
        }

        private void ExportSolution()
        {
            IIOService service = new IOService();
            string name = service.SaveBooklet();
            if (name != null && name != "")
            {
                PdfSolution pdfSol = new PdfSolution();
                pdfSol.CreatePdfWithSolutions(name);
            }
        }

        /// <summary>
        /// Notify that total point has changed.
        /// </summary>
        public void SudokuTotalPointsChanged()
        {
            OnPropertyChanged(nameof(TotalPoints));
        }

        /// <summary>
        /// Nofity that basic info has changed.
        /// </summary>
        public void BasicInfoChanged()
        {
            OnPropertyChanged(nameof(TournamentName));
            OnPropertyChanged(nameof(TournamentDate));
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(RoundName));
            OnPropertyChanged(nameof(TimeForSolving));
            OnPropertyChanged(nameof(LogoOneImage));
            OnPropertyChanged(nameof(LogoTwoImage));
            OnPropertyChanged(nameof(LogoThreeImage));
            OnPropertyChanged(nameof(LogoOneFullPath));
            OnPropertyChanged(nameof(LogoTwoFullPath));
            OnPropertyChanged(nameof(LogoThreeFullPath));
        }

        private StartScreenViewModel CreateStartScreenViewModel()
        {
            return new StartScreenViewModel();
        }
    }
}
