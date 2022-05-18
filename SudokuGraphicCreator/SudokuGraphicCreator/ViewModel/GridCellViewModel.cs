using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Represents cells which create sudoku grid.
    /// </summary>
    public class GridCellViewModel : BaseViewModel
    {
        private double _left;

        /// <summary>
        /// Left distance from left up corner of grid.
        /// </summary>
        public double Left
        {
            get => _left;
            set
            {
                _left = value;
                OnPropertyChanged(nameof(Left));
                OnPropertyChanged(nameof(Margin));
            }
        }

        private double _top;

        /// <summary>
        /// Top distance form left up corner of grid.
        /// </summary>
        public double Top
        {
            get => _top;
            set
            {
                _top = value;
                OnPropertyChanged(nameof(Top));
                OnPropertyChanged(nameof(Margin));
            }
        }

        /// <summary>
        /// Index of row in grid.
        /// </summary>
        public int RowIndex { get; }

        /// <summary>
        /// Column index in grid.
        /// </summary>
        public int ColIndex { get; }

        /// <summary>
        /// Order of cell in grid.
        /// </summary>
        public int Order { get; }

        public Thickness _margin;

        public Thickness Margin
        {
            get => new Thickness(Left, Top, 0, 0);
            set
            {
                _margin = value;
                OnPropertyChanged(nameof(Margin));
            }
        }

        private Brush _brush;

        /// <summary>
        /// Background color of cell.
        /// </summary>
        public Brush Background
        {
            get => _brush;
            set
            {
                _brush = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        private double _width;

        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
                OnPropertyChanged(nameof(Margin));
                OnPropertyChanged(nameof(Top));
            }
        }

        private double _height;

        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
                OnPropertyChanged(nameof(Margin));
                OnPropertyChanged(nameof(Left));
            }
        }

        private Thickness _borderThickness;

        /// <summary>
        /// Thickness of grid.
        /// </summary>
        public Thickness BorderThickness
        {
            get => _borderThickness;
            set
            {
                _borderThickness = value;
                OnPropertyChanged(nameof(BorderThickness));
            }
        }

        /// <summary>
        /// Default background of cell.
        /// </summary>
        public Brush DefaultBrush { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="GridCellViewModel"/> class.
        /// </summary>
        /// <param name="rowIndex">Index of row in grid.</param>
        /// <param name="colIndex">Index of col in grid.</param>
        /// <param name="order"></param>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="background">Color of background of cell.</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderThickness">Thickeness of grid.</param>
        public GridCellViewModel(int rowIndex, int colIndex, int order, double left, double top, Brush background, double width, double height, Thickness borderThickness)
        {
            RowIndex = rowIndex;
            ColIndex = colIndex;
            Order = order;
            Left = left;
            Top = top;
            Background = background;
            Width = width;
            Height = height;
            BorderThickness = borderThickness;
            Margin = new Thickness(left, top, 0, 0);
            DefaultBrush = Brushes.Transparent;
        }
    }
}
