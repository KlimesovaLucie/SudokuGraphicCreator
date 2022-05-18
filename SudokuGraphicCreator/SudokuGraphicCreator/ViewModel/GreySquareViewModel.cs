using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class of <see cref="GreySquare"/> class.
    /// </summary>
    public class GreySquareViewModel : SudokuElementViewModel
    {
        private readonly GreySquare _model;

        /// <summary>
        /// Type of sudoku graphic element.
        /// </summary>
        public SudokuElementType SudokuElemType => _model.SudokuElemType;

        private double _width;
        
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
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
            }
        }

        /// <summary>
        /// Left distance from left up corner of grid.
        /// </summary>
        public double Left
        {
            get => _model.Left;
            set
            {
                _model.Left = value;
                OnPropertyChanged(nameof(Left));
                OnPropertyChanged(nameof(Margin));
            }
        }

        /// <summary>
        /// Top distance form left up corner of grid.
        /// </summary>
        public double Top
        {
            get => _model.Top;
            set
            {
                _model.Top = value;
                OnPropertyChanged(nameof(Top));
                OnPropertyChanged(nameof(Margin));
            }
        }

        public Thickness Margin
        {
            get => new Thickness(Left, Top, 0, 0);
            set
            {
                OnPropertyChanged(nameof(Margin));
            }
        }

        /// <summary>
        /// Color of element, default <see cref="Brushes.DarkGray"/>.
        /// </summary>
        public Brush Fill => _model.FillColor;

        /// <summary>
        /// Initializes a new instance of <see cref="GreySquareViewModel"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="rowIndex">Index of row in grid.</param>
        /// <param name="colIndex">Index cf col in grid.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="location">Location on grid.</param>
        public GreySquareViewModel(double width, double height, double left, double top, int rowIndex, int colIndex, SudokuElementType type, ElementLocationType location)
        {
            Width = width;
            Height = height;
            Margin = new Thickness(left, top, 0, 0);
            var newElem = new GreySquare(left, top, rowIndex, colIndex, type, location);
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(newElem);
            _model = newElem;
            AddVariant(type);
        }

        /// <summary>
        /// Resize this element by <paramref name="ratio"/>.
        /// </summary>
        /// <param name="ratio">Value for resizeing.</param>
        public void Resize(double ratio)
        {
            Width *= ratio;
            Height *= ratio;
            Left *= ratio;
            Top *= ratio;
        }

        private void AddVariant(SudokuElementType sudokuElement)
        {
            if (sudokuElement == SudokuElementType.Even)
            {
                AddSudokuVariant(SudokuType.Even);
            }
        }

        private void AddSudokuVariant(SudokuType sudokuType)
        {
            if (!SudokuStore.Instance.Sudoku.Variants.Contains(sudokuType))
            {
                SudokuStore.Instance.Sudoku.Variants.Add(sudokuType);
            }
        }

        private void RemoveVariant(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType sudokuType)
        {
            if (!IsDeletingLastElemVariant(collection, sudokuType))
            {
                return;
            }
            if (sudokuType == SudokuElementType.Even)
            {
                SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.Even);
            }
        }

        private bool IsDeletingLastElemVariant(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType elemType)
        {
            return CountSameTypeElems(collection, elemType) == 1;
        }

        private int CountSameTypeElems(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType elemType)
        {
            int count = 0;
            foreach (SudokuElementViewModel item in collection)
            {
                var elem = item as GreySquareViewModel;
                if (elem != null && elem.SudokuElemType == elemType)
                {
                    count++;
                }
            }
            return count;
        }

        private void Remove(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType type)
        {
            SudokuStore.Instance.Sudoku.SudokuVariants.Remove(_model);
            RemoveVariant(collection, type);
        }

        /// <summary>
        /// Remove this element from elements in sudoku and if possible, change type of variant.
        /// </summary>
        /// <param name="collection">Collection of elements.</param>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="elemType">Type of graphic element.</param>
        /// <returns>true if element was removed, otherwise false.</returns>
        public static bool RemoveFromCollection(ObservableCollection<SudokuElementViewModel> collection,
            double left, double top, SudokuElementType elemType)
        {
            foreach (SudokuElementViewModel item in collection)
            {
                var elem = item as GreySquareViewModel;
                if (elem != null)
                {
                    if (elem.Left == left && elem.Top == top && elem.SudokuElemType == elemType)
                    {
                        elem.Remove(collection, elemType);
                        collection.Remove(item);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
