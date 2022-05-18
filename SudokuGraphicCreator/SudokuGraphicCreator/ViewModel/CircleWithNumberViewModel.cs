using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="CircleWithNumber"/> class.
    /// </summary>
    public class CircleWithNumberViewModel : SudokuElementViewModel
    {
        private readonly CircleWithNumber _model;

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
        /// Color of border of element, default <see cref="Brushes.DarkGray"/>.
        /// </summary>
        public Brush Border => _model.BorderColor;

        /// <summary>
        /// Number in circle.
        /// </summary>
        public int Value
        {
            get => _model.Value;
            set
            {
                _model.Value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        private double _textSize;

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
        /// Initializes a new instance of <see cref="CircleWithNumberViewModel"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="rowIndex">Index of row of cell in grid.</param>
        /// <param name="colIndex">Index of column of cell int grid.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="location">Location on grid.</param>
        public CircleWithNumberViewModel(double width, double height, double left, double top, int rowIndex, int colIndex,
            SudokuElementType type, ElementLocationType location)
        {
            Width = width;
            Height = height;
            Margin = new Thickness(left, top, 0, 0);
            var newElem = new CircleWithNumber(left, top, rowIndex, colIndex, type, location);
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(newElem);
            _model = newElem;
            AddVariant(type);
            TextSize = GridSizeStore.KillerNumberTextSize;
        }

        // <summary>
        /// Resize this element by <paramref name="ratio"/>.
        /// </summary>
        /// <param name="ratio">Value for resizeing.</param>
        public void Resize(double ratio)
        {
            Width *= ratio;
            Height *= ratio;
            Left *= ratio;
            Top *= ratio;
            TextSize *= ratio;
        }

        private void AddVariant(SudokuElementType sudokuElement)
        {
            if (sudokuElement == SudokuElementType.Sum)
            {
                AddSudokuVariant(SudokuType.Sum);
            }
            else if (sudokuElement == SudokuElementType.Difference)
            {
                AddSudokuVariant(SudokuType.Difference);
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
            if (sudokuType == SudokuElementType.Sum)
            {
                SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.Sum);
            }
            else if (sudokuType == SudokuElementType.Difference)
            {
                AddSudokuVariant(SudokuType.Difference);
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
                var elem = item as CircleWithNumberViewModel;
                if (elem != null && elem.SudokuElemType == elemType)
                {
                    count++;
                }
            }
            return count;
        }

        private void Remove(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType elemType)
        {
            SudokuStore.Instance.Sudoku.SudokuVariants.Remove(_model);
            RemoveVariant(collection, elemType);
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
                var elem = item as CircleWithNumberViewModel;
                if (elem != null)
                {
                    if (Math.Abs(elem.Left - left) < .0001 && Math.Abs(elem.Top - top) < .0001 && elem.SudokuElemType == elemType)
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
