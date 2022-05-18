using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="BoldArrow"/> class.
    /// </summary>
    public class BoldArrowViewModel : SudokuElementViewModel
    {
        private readonly BoldArrow _model;

        /// <summary>
        /// Type of sudoku graphic element.
        /// </summary>
        public SudokuElementType SudokuElemType => _model.SudokuElemType;

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
            }
        }

        /// <summary>
        /// Color of element, default <see cref="Brushes.DarkGray"/>.
        /// </summary>
        public Brush Fill => _model.FillColor;

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
        /// Initializes a new instance of <see cref="BoldArrowViewModel"/> class.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="graphicType">Orientation of element.</param>
        public BoldArrowViewModel(double left, double top, SudokuElementType type, GraphicElementType graphicType)
        {
            var newElem = new BoldArrow(left, top, (int)(top / GridSizeStore.XCellSize), (int)(left / GridSizeStore.YCellSize), type, graphicType);
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(newElem);
            _model = newElem;
            Points = new PointCollection();
            Points = CreatePoints(left, top, graphicType, true);
            AddVariant(type);
        }

        /// <summary>
        /// Resize this element by <paramref name="ratio"/>.
        /// </summary>
        /// <param name="ratio">Value for resizeing.</param>
        public void Resize(double ratio)
        {
            Left *= ratio;
            Top *= ratio;
            ResizePoints(ratio);
        }

        private void ResizePoints(double ratio)
        {
            PointCollection previous = Points;
            Points = new PointCollection();
            foreach (var elem in previous)
            {
                Points.Add(new Point(elem.X * ratio, elem.Y * ratio));
            }
        }

        private void AddVariant(SudokuElementType sudokuElement)
        {
            if (sudokuElement == SudokuElementType.SearchNineDown || sudokuElement == SudokuElementType.SearchNineLeft ||
                sudokuElement == SudokuElementType.SearchNineUp || sudokuElement == SudokuElementType.SearchNineRight)
            {
                AddSudokuVariant(SudokuType.SearchNine);
            }
        }

        private void AddSudokuVariant(SudokuType sudokuType)
        {
            if (!SudokuStore.Instance.Sudoku.Variants.Contains(sudokuType))
            {
                SudokuStore.Instance.Sudoku.Variants.Add(sudokuType);
            }
        }

        /// <summary>
        /// Create point for this element by <paramref name="type"/>.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="type">Type of orientation of arrow.</param>
        /// <param name="forGrid">true for grid, false for export.</param>
        /// <returns><see cref="PointCollection"/> of point of element.</returns>
        public static PointCollection CreatePoints(double left, double top, GraphicElementType type, bool forGrid = false)
        {
            PointCollection points = new PointCollection();
            if (type == GraphicElementType.Left)
            {
                LeftArrowPoints(left, top, points, forGrid);
            }
            else if (type == GraphicElementType.Right)
            {
                RightArrowPoints(left, top, points, forGrid);
            }
            else if (type == GraphicElementType.Up)
            {
                UpArrowPoints(left, top, points, forGrid);
            }
            else if (type == GraphicElementType.Down)
            {
                DownArrowPoints(left, top, points, forGrid);
            }
            return points;
        }

        private static PointCollection CreateDefaultValues(bool forgrid = false)
        {
            PointCollection defaultPoints = new PointCollection();
            if (forgrid)
            {
                defaultPoints.Add(new Point(GridSizeStore.YCellSize * 0.25, GridSizeStore.YCellSize / 2));
                defaultPoints.Add(new Point(GridSizeStore.XCellSize / 2, GridSizeStore.YCellSize * 0.25));
                defaultPoints.Add(new Point(GridSizeStore.XCellSize / 2, (GridSizeStore.YCellSize / 2) - (GridSizeStore.YCellSize / 7)));
                defaultPoints.Add(new Point(GridSizeStore.YCellSize * 0.7, (GridSizeStore.YCellSize / 2) - (GridSizeStore.YCellSize / 7)));
                defaultPoints.Add(new Point(GridSizeStore.YCellSize * 0.7, (GridSizeStore.YCellSize / 2) + (GridSizeStore.YCellSize / 7)));
                defaultPoints.Add(new Point(GridSizeStore.XCellSize / 2, (GridSizeStore.YCellSize / 2) + (GridSizeStore.YCellSize / 7)));
                defaultPoints.Add(new Point(GridSizeStore.XCellSize / 2, GridSizeStore.YCellSize - (GridSizeStore.YCellSize * 0.25)));
                return defaultPoints;
            }
            defaultPoints.Add(new Point(10, GridSizeStore.YCellSize / 2));
            defaultPoints.Add(new Point(GridSizeStore.XCellSize / 2, 10));
            defaultPoints.Add(new Point(GridSizeStore.XCellSize / 2, (GridSizeStore.YCellSize / 2) - 7));
            defaultPoints.Add(new Point(40, (GridSizeStore.YCellSize / 2) - 7));
            defaultPoints.Add(new Point(40, (GridSizeStore.YCellSize / 2) + 7));
            defaultPoints.Add(new Point(GridSizeStore.XCellSize / 2, (GridSizeStore.YCellSize / 2) + 7));
            defaultPoints.Add(new Point(GridSizeStore.XCellSize / 2, GridSizeStore.YCellSize - 10));
            return defaultPoints;
        }

        private static void LeftArrowPoints(double left, double top, PointCollection points, bool forGrid = false)
        {
            PointCollection defaultValues = CreateDefaultValues(forGrid);
            foreach (var item in defaultValues)
            {
                points.Add(new Point(item.X + left, item.Y + top));
            }
        }

        private static void RightArrowPoints(double left, double top, PointCollection points, bool forGrid = false)
        {
            PointCollection defaultValues = CreateDefaultValues(forGrid);
            foreach (var item in defaultValues)
            {
                points.Add(new Point(GridSizeStore.XCellSize - item.X + left, GridSizeStore.YCellSize - item.Y + top));
            }
        }

        private static void UpArrowPoints(double left, double top, PointCollection points, bool forGrid = false)
        {
            PointCollection defaultValues = CreateDefaultValues(forGrid);
            foreach (var item in defaultValues)
            {
               points.Add(new Point(item.Y + left, item.X + top));
            }
        }

        private static void DownArrowPoints(double left, double top, PointCollection points, bool forGrid = false)
        {
            PointCollection defaultValues = CreateDefaultValues(forGrid);
            foreach (var item in defaultValues)
            {
                points.Add(new Point(GridSizeStore.YCellSize - item.Y + left, GridSizeStore.XCellSize - item.X + top));
            }
        }

        private void RemoveVariant(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType sudokuType)
        {
            if (!IsDeletingLastElemVariant(collection, sudokuType))
            {
                return;
            }
            if (sudokuType == SudokuElementType.SearchNineDown || sudokuType == SudokuElementType.SearchNineLeft ||
                sudokuType == SudokuElementType.SearchNineUp || sudokuType == SudokuElementType.SearchNineRight)
            {
                SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.SearchNine);
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
                var elem = item as BoldArrowViewModel;
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
                var elem = item as BoldArrowViewModel;
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
