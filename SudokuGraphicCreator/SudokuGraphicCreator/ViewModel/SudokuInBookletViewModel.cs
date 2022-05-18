using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// The viewModel class for <see cref="SudokuInBooklet"/> model. Represents one sudoku in booklet.
    /// </summary>
    public class SudokuInBookletViewModel : BaseViewModel, ISudokuInBookletViewModel
    {
        private readonly SudokuInBooklet _sudokuInBooklet;

        public string Name
        {
            get => _sudokuInBooklet.Name;
            set
            {
                _sudokuInBooklet.Name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(StringRepresentation));
            }
        }

        public int OrderNumber
        {
            get => _sudokuInBooklet.Order;
            set
            {
                _sudokuInBooklet.Order = value;
                OnPropertyChanged(nameof(OrderNumber));
                OnPropertyChanged(nameof(StringRepresentation));
            }
        }

        public int Points
        {
            get => _sudokuInBooklet.Points;
            set
            {
                _sudokuInBooklet.Points = value;
                OnPropertyChanged(nameof(Points));
                BookletStore.Instance.CreatingBookletViewModel.SudokuTotalPointsChanged();
                OnPropertyChanged(nameof(StringRepresentation));
            }
        }

        public string Rules
        {
            get => _sudokuInBooklet.Rules;
            set
            {
                _sudokuInBooklet.Rules = value;
                OnPropertyChanged(nameof(Rules));
            }
        }

        [XmlIgnore]
        public BitmapImage Table
        {
            get => _sudokuInBooklet.Table;
            set
            {
                _sudokuInBooklet.Table = value;
                OnPropertyChanged(nameof(Table));
            }
        }

        public string TableName
        {
            get => _sudokuInBooklet.TableName;
            set
            {
                _sudokuInBooklet.TableName = value;
                OnPropertyChanged(nameof(TableName));
            }
        }

        public string TableFullPath
        {
            get => _sudokuInBooklet.TableFullPath;
            set
            {
                try
                {
                    _sudokuInBooklet.TableFullPath = value;
                    OnPropertyChanged(nameof(TableFullPath));
                    Table = new BitmapImage(new System.Uri(value));
                }
                catch
                { }
            }
        }

        public string TableFullPathSvg
        {
            get
            {
                if (_sudokuInBooklet.TableFullPath.EndsWith(".svg"))
                {
                    return _sudokuInBooklet.TableFullPath;
                }
                return "";
            }
            set
            {
                OnPropertyChanged(nameof(TableFullPathSvg));
            }
        }

        [XmlIgnore]
        public BitmapImage Solution
        {
            get => _sudokuInBooklet.Solution;
            set
            {
                _sudokuInBooklet.Solution = value;
                OnPropertyChanged(nameof(Solution));
            }
        }

        public string SolutionName
        {
            get => _sudokuInBooklet.SolutionName;
            set
            {
                _sudokuInBooklet.SolutionName = value;
                OnPropertyChanged(nameof(SolutionName));
            }
        }

        public string SolutionFullPath
        {
            get => _sudokuInBooklet.SolutionFullPath;
            set
            {
                _sudokuInBooklet.SolutionFullPath = value;
                OnPropertyChanged(nameof(SolutionFullPath));
            }
        }

        public string StringRepresentation
        {
            get => _sudokuInBooklet.ToString();
            set
            {
                OnPropertyChanged(nameof(StringRepresentation));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SudokuInBookletViewModel"/> class.
        /// </summary>
        /// <param name="name">Name of sudoku in booklet.</param>
        /// <param name="orderNumber">Order number in booklet.</param>
        /// <param name="points">Points of sudoku.</param>
        /// <param name="rules">Rules of sudoku.</param>
        public SudokuInBookletViewModel(string name, int orderNumber, int points, string rules)
        {
            _sudokuInBooklet = new SudokuInBooklet(name, points, orderNumber, rules);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SudokuInBookletViewModel"/> class.
        /// </summary>
        public SudokuInBookletViewModel()
        {
            _sudokuInBooklet = new SudokuInBooklet();
        }

        public override string ToString()
        {
            return _sudokuInBooklet.ToString();
        }

        /// <summary>
        /// Get <see cref="SudokuInBooklet"/> model for <see cref="PageViewModel"/>.
        /// </summary>
        /// <returns></returns>
        public SudokuInBooklet GetModel()
        {
            return _sudokuInBooklet;
        }
    }
}
