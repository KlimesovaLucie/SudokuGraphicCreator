using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class of <see cref="SmallArrow"/> class.
    /// </summary>
    public class SmallArrowViewModel : SudokuElementViewModel
    {
        private readonly SmallArrow _model;

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
        /// Order in grid.
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Color of elemnent, default <see cref="Brushes.Black"/>.
        /// </summary>
        public Brush Fill => _model.FillColor;

        private PointCollection _points;
        
        /// <summary>
        /// Point of element in grid.
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
        /// Value that arrow represents.
        /// </summary>
        public int? Value
        {
            get => _model.Value;
            set
            {
                _model.Value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SmallArrowViewModel"/> class.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="order">Order in grid.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="graphicType">Type of orientation of arrow.</param>
        /// <param name="location">Location on grid.</param>
        public SmallArrowViewModel(double left, double top, int order, SudokuElementType type, GraphicElementType graphicType, ElementLocationType location)
        {
            Order = order;
            var newElem = new SmallArrow(left, top, (int)(top / GridSizeStore.XCellSize),
                (int)(left / GridSizeStore.XCellSize), order, type, location, graphicType);
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(newElem);
            _model = newElem;
            PointCollection absolutePoints = CreatePoints(graphicType);
            Points = new PointCollection();
            foreach (var point in absolutePoints)
            {
                Points.Add(new Point(point.X + left, point.Y + top));
            }
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
            if (sudokuElement == SudokuElementType.LittleKillerLeftDown || sudokuElement == SudokuElementType.LittleKillerLeftUp ||
                sudokuElement == SudokuElementType.LittleKillerRightDown || sudokuElement == SudokuElementType.LittleKillerRightUp)
            {
                AddSudokuVariant(SudokuType.LittleKiller);
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
        /// <param name="type">Type of orientation of arrow.</param>
        /// <returns><see cref="PointCollection"/> of point of element.</returns>
        public static PointCollection CreatePoints(GraphicElementType type)
        {
            PointCollection points = new PointCollection();
            if (type == GraphicElementType.LeftDown)
            {
                LeftDownArrowPoints(points);
            }
            else if (type == GraphicElementType.RightUp)
            {
                RightUpArrowPoints(points);
            }
            else if (type == GraphicElementType.RightDown)
            {
                RightDownArrowPoints(points);
            }
            else if (type == GraphicElementType.LeftUp)
            {
                LeftUpArrowPoints(points);
            }
            return points;
        }

        private static PointCollection CreateDefaultValues()
        {
            PointCollection defaultPoints = new PointCollection();
            defaultPoints.Add(new Point(GridSizeStore.YCellSize * 0.1, GridSizeStore.YCellSize - (GridSizeStore.YCellSize * 0.1)));
            defaultPoints.Add(new Point(GridSizeStore.YCellSize * 0.3, GridSizeStore.YCellSize - (GridSizeStore.YCellSize * 0.1)));
            defaultPoints.Add(new Point(GridSizeStore.YCellSize * 0.22, GridSizeStore.YCellSize - (GridSizeStore.YCellSize * 0.18)));
            defaultPoints.Add(new Point(GridSizeStore.YCellSize * 0.36, GridSizeStore.YCellSize - (GridSizeStore.YCellSize * 0.3)));
            defaultPoints.Add(new Point(GridSizeStore.YCellSize * 0.3, GridSizeStore.YCellSize - (GridSizeStore.YCellSize * 0.36)));
            defaultPoints.Add(new Point(GridSizeStore.YCellSize * 0.18, GridSizeStore.YCellSize - GridSizeStore.YCellSize * 0.22));
            defaultPoints.Add(new Point(GridSizeStore.YCellSize * 0.1, GridSizeStore.YCellSize - (GridSizeStore.YCellSize * 0.3)));
            return defaultPoints;
        }

        private static void LeftDownArrowPoints(PointCollection points)
        {
            PointCollection defaultValues = CreateDefaultValues();
            foreach (var item in defaultValues)
            {
                points.Add(new Point(item.X, item.Y));
            }
        }

        private static void RightUpArrowPoints(PointCollection points)
        {
            PointCollection defaultValues = CreateDefaultValues();
            foreach (var item in defaultValues)
            {
                points.Add(new Point(GridSizeStore.XCellSize - item.X, GridSizeStore.YCellSize - item.Y));
            }
        }

        private static void RightDownArrowPoints(PointCollection points)
        {
            PointCollection defaultValues = CreateDefaultValues();
            foreach (var item in defaultValues)
            {
                points.Add(new Point(GridSizeStore.XCellSize - item.X, item.Y));
            }
        }

        private static void LeftUpArrowPoints(PointCollection points)
        {
            PointCollection defaultValues = CreateDefaultValues();
            foreach (var item in defaultValues)
            {
                points.Add(new Point(GridSizeStore.XCellSize - item.Y, item.X));
            }
        }

        private void RemoveVariant(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType sudokuType)
        {
            if (!IsDeletingLastElemVariant(collection, sudokuType))
            {
                return;
            }
            if (sudokuType == SudokuElementType.LittleKillerLeftDown || sudokuType == SudokuElementType.LittleKillerLeftUp ||
                sudokuType == SudokuElementType.LittleKillerRightDown || sudokuType == SudokuElementType.LittleKillerRightUp)
            {
                SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.LittleKiller);
            }
        }

        private bool IsDeletingLastElemVariant(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType elemType)
        {
            if (elemType == SudokuElementType.LittleKillerLeftDown || elemType == SudokuElementType.LittleKillerLeftUp ||
                elemType == SudokuElementType.LittleKillerRightDown || elemType == SudokuElementType.LittleKillerRightUp)
            {
                return (CountSameTypeElems(collection, SudokuElementType.LittleKillerLeftDown) +
                    CountSameTypeElems(collection, SudokuElementType.LittleKillerLeftUp) +
                    CountSameTypeElems(collection, SudokuElementType.LittleKillerRightDown) +
                    CountSameTypeElems(collection, SudokuElementType.LittleKillerRightUp)) == 1;
            }
            return CountSameTypeElems(collection, elemType) == 1;
        }

        private int CountSameTypeElems(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType elemType)
        {
            int count = 0;
            foreach (SudokuElementViewModel item in collection)
            {
                var elem = item as SmallArrowViewModel;
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
        /// <param name="type">Type of graphic element.</param>
        /// <returns>true if element was removed, otherwise false.</returns>
        public static bool RemoveFromCollection(ObservableCollection<SudokuElementViewModel> collection,
            double left, double top, SudokuElementType elemType)
        {
            foreach (SudokuElementViewModel item in collection)
            {
                var elem = item as SmallArrowViewModel;
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
