using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.View;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// The ViewModel class for <see cref="BookletInformations"/>.
    /// </summary>
    public class BookletInformationsViewModel : BaseViewModel, IDialogRequestClose, IBookletInformationsViewModel
    {
        private static Booklet _booklet;

        public string TournamentName
        {
            get => _booklet.TournamentName;
            set
            {
                _booklet.TournamentName = value;
                OnPropertyChanged(nameof(TournamentName));
            }
        }

        public DateTime TournamentDate
        {
            get => _booklet.TournamentDate;
            set
            {
                _booklet.TournamentDate = value;
                OnPropertyChanged(nameof(TournamentDate));
            }
        }
        
        public string Location
        {
            get => _booklet.Location;
            set
            {
                _booklet.Location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public string RoundNumber
        {
            get => _booklet.RoundNumber;
            set
            {
                _booklet.RoundNumber = value;
                OnPropertyChanged(nameof(RoundNumber));
            }
        }

        public string RoundName
        {
            get => _booklet.RoundName;
            set
            {
                _booklet.RoundName = value;
                OnPropertyChanged(nameof(RoundName));
            }
        }

        public string TimeForSolving
        {
            get => _booklet.TimeForSolving;
            set
            {
                _booklet.TimeForSolving = value;
                OnPropertyChanged(nameof(TimeForSolving));
            }
        }

        public string LogoOne
        {
            get => _booklet.LogoOne;
            set
            {
                _booklet.LogoOne = value;
                OnPropertyChanged(nameof(LogoOne));
            }
        }

        public string LogoOneFullPath
        {
            get => _booklet.LogoOneFullPath;
            set
            {
                _booklet.LogoOneFullPath = value;
                 var elems = value.Split("\\");
                LogoOne = elems[elems.Length - 1];
                OnPropertyChanged(nameof(LogoOneFullPath));
            }
        }

        public BitmapImage LogoOneImage
        {
            get => _booklet.LogoOneImage;
            set
            {
                _booklet.LogoOneImage = value;
                OnPropertyChanged(nameof(LogoOneImage));
            }
        }

        public string LogoTwo
        {
            get => _booklet.LogoTwo;
            set
            {
                _booklet.LogoTwo = value;
                OnPropertyChanged(nameof(LogoTwo));
            }
        }

        public string LogoTwoFullPath
        {
            get => _booklet.LogoTwoFullPath;
            set
            {
                _booklet.LogoTwoFullPath = value;
                var elems = value.Split("\\");
                LogoTwo = elems[elems.Length - 1];
                OnPropertyChanged(nameof(LogoTwoFullPath));
            }
        }

        public BitmapImage LogoTwoImage
        {
            get => _booklet.LogoTwoImage;
            set
            {
                _booklet.LogoTwoImage = value;
                OnPropertyChanged(nameof(LogoTwoImage));
            }
        }

        public string LogoThree
        {
            get => _booklet.LogoThree;
            set
            {
                _booklet.LogoThree = value;
                OnPropertyChanged(nameof(LogoThree));
            }
        }

        public string LogoThreeFullPath
        {
            get => _booklet.LogoThreeFullPath;
            set
            {
                _booklet.LogoThreeFullPath = value;
                var elems = value.Split("\\");
                LogoThree = elems[elems.Length - 1];
                OnPropertyChanged(nameof(LogoThreeFullPath));
            }
        }

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
        /// Ok command for <see cref="BookletInformations"/> window.
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        /// Cancel command for <see cref="BookletInformations"/> window.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Command for selecting and saving logos for booklet.
        /// </summary>
        public ICommand SelectNewFileCommand { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="BookletInformationsViewModel"/> class.
        /// </summary>
        public BookletInformationsViewModel()
        {
            _booklet = BookletStore.Instance.Booklet;
            OkCommand = new OkBookletInfoCommand(this);
            CancelCommand = new ActionCommand(_ => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)), _ => true);
            SelectNewFileCommand = new SelectLogoCommand(this);
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public void CloseWindowWithOk()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }
    }
}
