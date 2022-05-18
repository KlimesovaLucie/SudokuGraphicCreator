using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="Line"/> model class.
    /// </summary>
    public class LineViewModel : SudokuElementViewModel
    {
        private readonly Line _model;

        /// <summary>
        /// Type of sudoku graphic element.
        /// </summary>
        public SudokuElementType SudokuElemType => _model.SudokuElemType;

        private PointCollection _points;

        /// <summary>
        /// Collection of points of elements.
        /// </summary>
        public PointCollection Points
        {
            get => _points;
            set
            {
                _points = value;
                OnPropertyChanged(nameof(Points));
            }
        }

        /// <summary>
        /// Color of cage, default <see cref="Brushes.DarkGray"/>
        /// </summary>
        public Brush Color => _model.FillColor;

        private double _strokeThickness;

        public double StrokeThickness
        {
            get => _strokeThickness;
            set
            {
                _strokeThickness = value;
                OnPropertyChanged(nameof(StrokeThickness));
            }
        }

        private ObservableCollection<Tuple<int, int>> _cells;

        private SudokuElementType _type;

        /// <summary>
        /// Initializes a new instance of <see cref="LineViewModel"/> class.
        /// </summary>
        /// <param name="type">Type of sudoku graphic element.</param>
        /// <param name="cells">Collection of cells in which this element lies.</param>
        public LineViewModel(SudokuElementType type, ObservableCollection<Tuple<int, int>> cells)
        {
            var newElem = new Line(type, cells);
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(newElem);
            _model = newElem;
            CreatePoints(cells);
            StrokeThickness = 4;
            if (type == SudokuElementType.Thermometers)
            {
                StrokeThickness = GridSizeStore.ThermometerThickness;
            }
            AddVariant(type);
            _cells = cells;
            _type = type;
        }

        /// <summary>
        /// Resize this element by <paramref name="ratio"/>.
        /// </summary>
        /// <param name="ratio">Value for resizeing.</param>
        public void Resize(double ratio)
        {
            CreatePoints(_cells);
            if (_type == SudokuElementType.Thermometers)
            {
                StrokeThickness *= ratio;
            }
        }

        private void AddVariant(SudokuElementType sudokuElement)
        {
            if (SudokuElementType.Palindromes == sudokuElement)
            {
                AddSudokuVariant(SudokuType.Palindrome);
            }
            else if (SudokuElementType.Sequences == sudokuElement)
            {
                AddSudokuVariant(SudokuType.Sequence);
            }
            else if (SudokuElementType.Thermometers == sudokuElement)
            {
                AddSudokuVariant(SudokuType.Thermometer);
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
            if (sudokuType == SudokuElementType.Palindromes)
            {
                SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.Palindrome);
            }
            else if (sudokuType == SudokuElementType.Sequences)
            {
                SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.Sequence);
            }
            else if (sudokuType == SudokuElementType.Thermometers)
            {
                SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.Thermometer);
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
                var elem = item as LineViewModel;
                if (elem != null && elem.SudokuElemType == elemType)
                {
                    count++;
                }
            }
            return count;
        }

        private void CreatePoints(ObservableCollection<Tuple<int, int>> cells)
        {
            Points = new PointCollection();

            double halfX = GridSizeStore.XCellSize / 2;
            double halfY = GridSizeStore.YCellSize / 2;

            foreach (var cell in cells)
            {
                double x = cell.Item2;
                double y = cell.Item1;

                Points.Add(new Point(x * GridSizeStore.XCellSize + halfX, y * GridSizeStore.YCellSize + halfY));
            }
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
        /// <param name="elemType">Type of graphic element.</param>
        /// <param name="cells">Collection of cells of element.</param>
        /// <returns>true if element was removed, otherwise false.</returns>
        public static bool RemoveFromCollection(ObservableCollection<SudokuElementViewModel> collection,
            SudokuElementType elemType, ObservableCollection<Tuple<int, int>> cells)
        {
            foreach (SudokuElementViewModel item in collection)
            {
                var elem = item as LineViewModel;
                if (elem != null)
                {
                    if (elem.SudokuElemType == elemType && IsSameSpace(cells, elem.Points))
                    {
                        elem.Remove(collection, elemType);
                        collection.Remove(item);
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IsSameSpace(ObservableCollection<Tuple<int, int>> cells, PointCollection points)
        {
            if (cells.Count != points.Count)
            {
                return false;
            }
            
            for (int i = 0; i < cells.Count; i++)
            {
                if (!FindSamePoint(points[i].X, points[i].Y, cells))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool FindSamePoint(double row, double column, ObservableCollection<Tuple<int, int>> cells)
        {
            double halfX = GridSizeStore.XCellSize / 2;
            double halfY = GridSizeStore.YCellSize / 2;

            foreach (var cell in cells)
            {
                if (cell.Item1 * GridSizeStore.XCellSize + halfX == column &&
                    cell.Item2 * GridSizeStore.YCellSize + halfY == row)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
