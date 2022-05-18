using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.Windows;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Represents cells in grid with given numbers.
    /// </summary>
    public class CellNumberViewModel : BaseViewModel
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

        private Brush _background;

        /// <summary>
        /// Color of background of cell.
        /// </summary>
        public Brush Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        /// <summary>
        /// Default color of cell.
        /// </summary>
        public Brush DefaultBrush { get; private set; }

        private readonly int[,] _collectionInModel;

        /// <summary>
        /// Given number in cell.
        /// </summary>
        public int Number
        {
            get => _collectionInModel[RowIndex, ColumnIndex];
            set
            {
                _collectionInModel[RowIndex, ColumnIndex] = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        /// <summary>
        /// Index of row in grid.
        /// </summary>
        public int RowIndex { get; }

        /// <summary>
        /// Column index in grid.
        /// </summary>
        public int ColumnIndex { get; }

        private readonly SudokuElementType[,] _typeCollection;

        private SudokuElementType _type;

        /// <summary>
        /// Type of sudoku graphic element.
        /// </summary>
        public SudokuElementType Type
        {
            get => _type;
            set
            {
                _type = value;
                _typeCollection[RowIndex, ColumnIndex] = value;
                ChangeSudokuVariant(value);
            }
        }

        private double _textSize;

        /// <summary>
        /// Size of <see cref="Number"/>.
        /// </summary>
        public double TextSize
        {
            get => _textSize;
            set
            {
                _textSize = value;
                OnPropertyChanged(nameof(TextSize));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CellNumberViewModel"/> class.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="collectionInModel">Collection in model class in which is stored.</param>
        /// <param name="elementCollection">Collection with types of elements stored in <paramref name="collectionInModel"/></param>
        public CellNumberViewModel(double left, double top, double width, double height, int[,] collectionInModel, SudokuElementType[,] elementCollection)
        {
            Left = left;
            Top = top;
            RowIndex = (int)(top / GridSizeStore.YCellSize);
            ColumnIndex = (int)(left / GridSizeStore.XCellSize);
            Width = width;
            Height = height;
            Margin = new Thickness(left, top, 0, 0);
            Background = Brushes.Transparent;
            DefaultBrush = Brushes.Transparent;
            _collectionInModel = collectionInModel;
            _typeCollection = elementCollection;
            TextSize = GridSizeStore.InCellTextSize;
        }

        private void ChangeSudokuVariant(SudokuElementType type)
        {
            int count = TypeCounts(type);
            if (count == 0)
            {
                if (type == SudokuElementType.Outside)
                {
                    SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.Outside);
                }
                else if (type == SudokuElementType.NextToNine)
                {
                    SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.NextToNine);
                }
                else if (type == SudokuElementType.Skyscrapers)
                {
                    SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.Skyscraper);
                }
            }
            else if (count != 0)
            {
                if (type == SudokuElementType.Outside && !SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Outside))
                {
                    SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Outside);
                }
                else if (type == SudokuElementType.NextToNine && !SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.NextToNine))
                {
                    SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.NextToNine);
                }
                else if (type == SudokuElementType.Skyscrapers && !SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Skyscraper))
                {
                    SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Skyscraper);
                }
            }
        }

        private int TypeCounts(SudokuElementType type)
        {
            int count = 0;
            count += CountTypeInCollection(SudokuStore.Instance.Sudoku.LeftNumbersType, type);
            count += CountTypeInCollection(SudokuStore.Instance.Sudoku.RightNumbersType, type);
            count += CountTypeInCollection(SudokuStore.Instance.Sudoku.UpNumbersType, type);
            count += CountTypeInCollection(SudokuStore.Instance.Sudoku.BottomNumbersType, type);
            count += CountTypeInCollection(SudokuStore.Instance.Sudoku.GridNumbersType, type);
            return count;
        }

        private int CountTypeInCollection(SudokuElementType[,] collection, SudokuElementType type)
        {
            int count = 0;
            foreach (var cell in collection)
            {
                if (cell == type)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
