using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="Cage"/> model class.
    /// </summary>
    public class CageViewModel : SudokuElementViewModel
    {
        private readonly Cage _model;

        /// <summary>
        /// Type of sudoku graphic element.
        /// </summary>
        public SudokuElementType SudokuElemType => _model.SudokuElemType;

        /// <summary>
        /// Color of cage, default <see cref="Brushes.Black"/>
        /// </summary>
        public Brush Color { get; }

        private CharacterViewModel _text;

        /// <summary>
        /// Value in left up corner of cage.
        /// </summary>
        public CharacterViewModel Text
        {
            get => _text;
            set
            {
                _text = value;
                _model.Text = value.Text;
                try
                {
                    int number = Int32.Parse(_text.Text);
                    _model.Number = number;
                }
                catch
                {
                    _model.Number = 0;
                }
            }
        }

        /// <summary>
        /// Left distance from left up corner of grid for the most left cell.
        /// </summary>
        public double LeftTopX { get; private set; }

        /// <summary>
        /// Top distance from left up corner of grid for the most left cell.
        /// </summary>
        public double LeftTopY { get; private set; }

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
        /// Initializes a new instance of <see cref="CageViewModel"/> class.
        /// </summary>
        /// <param name="type">Type of element.s</param>
        /// <param name="cells">Cell in which element takes place.</param>
        public CageViewModel(SudokuElementType type, ObservableCollection<Tuple<double, double>> cells)
        {
            var newElem = new Cage(type, GetPositions(cells));
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(newElem);
            _model = newElem;
            Points = new PointCollection();
            double topX = 0;
            double topY = 0;
            FindPoints(cells, ref topX, ref topY, Points);
            LeftTopX = topX;
            LeftTopY = topY;
            AddVariant(type);
        }

        /// <summary>
        /// Resize this element by <paramref name="ratio"/>.
        /// </summary>
        /// <param name="ratio">Value for resizeing.</param>
        public void Resize(double ratio)
        {
            ResizePoints(ratio);
            LeftTopX *= ratio;
            LeftTopY *= ratio;
        }

        private void ResizePoints(double ratio)
        {
            PointCollection previousPoints = Points;
            Points = new PointCollection();
            foreach (var elem in previousPoints)
            {
                Points.Add(new Point(elem.X * ratio, elem.Y * ratio));
            }
        }

        private ObservableCollection<Tuple<int, int>> GetPositions(ObservableCollection<Tuple<double, double>> cells)
        {
            ObservableCollection<Tuple<int, int>> positions = new ObservableCollection<Tuple<int, int>>();
            foreach (var item in cells)
            {
                positions.Add(new Tuple<int, int>((int)(item.Item2 / GridSizeStore.XCellSize), (int)(item.Item1 / GridSizeStore.YCellSize)));
            }
            return positions;
        }

        private void AddVariant(SudokuElementType sudokuElement)
        {
            if (SudokuElementType.Killer == sudokuElement)
            {
                AddSudokuVariant(SudokuType.Killer);
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

        /// <summary>
        /// Find point for cage.
        /// </summary>
        /// <param name="cells">Collection of cell in which cage lies.</param>
        /// <param name="leftTopX">Left distance from left up corner for the most left cell of cage.</param>
        /// <param name="leftTopY">Top distance from left up corner for the most left cell of cage.</param>
        /// <param name="points">Collection of points to fill.</param>
        public static void FindPoints(ObservableCollection<Tuple<double, double>> cells, ref double leftTopX, ref double leftTopY,
            PointCollection points)
        {
            var startCell = FindStartCell(cells);
            double startX = startCell.Item1;
            double startY = startCell.Item2;

            leftTopX = startX;
            leftTopY = startY;

            double actualX = startCell.Item1;
            double actualY = startCell.Item2;
            points.Add(new Point(actualX + GridSizeStore.CageOffset, actualY + GridSizeStore.CageOffset));
            
            double previousX = actualX;
            double previousY = actualY;

            actualX += GridSizeStore.XCellSize;
            points.Add(new Point(actualX, actualY));
            do
            {
                Tuple<double, double> newPoint = FindNewPoint(actualX, actualY, previousX, previousY, cells, points);
                previousX = actualX;
                previousY = actualY;
                actualX = newPoint.Item1;
                actualY = newPoint.Item2;
            } while (Math.Abs(actualX - startX) > .0001 || Math.Abs(actualY - startY) > .0001);
            AddOffsetToLastPoint(GridSizeStore.CageOffset, GridSizeStore.CageOffset, points);
        }

        private static Tuple<double, double> FindNewPoint(double actualX, double actualY, double previousX, double previousY,
            ObservableCollection<Tuple<double, double>> cells, PointCollection points)
        {
            double xOffset;
            double yOffset;
            Tuple<double, double> newPoint;
            if (previousY == actualY && previousX < actualX)
            {
                newPoint = IsInCollectionFromLeft(actualX, actualY, out xOffset, out yOffset, cells);
            }
            else if (previousX == actualX && previousY < actualY)
            {
                newPoint = IsInCollectionFromUp(actualX, actualY, out xOffset, out yOffset, cells);
            }
            else if (previousY == actualY && previousX > actualX)
            {
                newPoint = IsInCollectionFromRight(actualX, actualY, out xOffset, out yOffset, cells);
            }
            else
            {
                newPoint = IsInCollectionFromDown(actualX, actualY, out xOffset, out yOffset, cells);
            }
            AddOffsetToLastPoint(xOffset, yOffset, points);
            points.Add(new Point(newPoint.Item1, newPoint.Item2));
            return newPoint;
        }

        private static void AddOffsetToLastPoint(double xOffset, double yOffset, PointCollection points)
        {
            var previousPoint = points[points.Count - 1];
            previousPoint.X += xOffset;
            previousPoint.Y += yOffset;
            points[points.Count - 1] = previousPoint;
        }

        private static Tuple<double, double> FindStartCell(ObservableCollection<Tuple<double, double>> cells)
        {
            var result = cells[0];

            foreach (var cell in cells)
            {
                if (cell.Item1 <= result.Item1 && cell.Item2 <= result.Item2)
                {
                    result = cell;
                }
            }

            return result;
        }

        private static Tuple<double, double> IsInCollectionFromLeft(double x, double y, out double xOffset, out double yOffset,
            ObservableCollection<Tuple<double, double>> cells)
        {
            var first = new Tuple<double, double>(x, y - GridSizeStore.XCellSize);
            var second = new Tuple<double, double>(x + GridSizeStore.XCellSize, y);
            var third = new Tuple<double, double>(x, y + GridSizeStore.XCellSize);

            xOffset = 0;
            yOffset = 0;

            if (IsInCellCollection(first.Item1, first.Item2, cells))
            {
                xOffset = GridSizeStore.CageOffset;
                yOffset = GridSizeStore.CageOffset;
                return first;
            }
            
            if (IsInCellCollection(second.Item1, second.Item2, cells))
            {
                yOffset = GridSizeStore.CageOffset;
                return second;
            }

            if (IsInCellCollection(third.Item1, third.Item2, cells))
            {
                xOffset = -GridSizeStore.CageOffset;
                yOffset = GridSizeStore.CageOffset;
                return third;
            }

            return new Tuple<double, double>(0, 0);
        }

        private static Tuple<double, double> IsInCollectionFromUp(double x, double y, out double xOffset, out double yOffset,
            ObservableCollection<Tuple<double, double>> cells)
        {
            var first = new Tuple<double, double>(x + GridSizeStore.XCellSize, y);
            var second = new Tuple<double, double>(x, y + GridSizeStore.YCellSize);
            var third = new Tuple<double, double>(x - GridSizeStore.XCellSize, y);

            xOffset = 0;
            yOffset = 0;

            if (IsInCellCollection(first.Item1, first.Item2, cells))
            {
                xOffset = -GridSizeStore.CageOffset;
                yOffset = GridSizeStore.CageOffset;
                return first;
            }

            if (IsInCellCollection(second.Item1, second.Item2, cells))
            {
                xOffset = -GridSizeStore.CageOffset;
                return second;
            }

            if (IsInCellCollection(third.Item1, third.Item2, cells))
            {
                xOffset = -GridSizeStore.CageOffset;
                yOffset = -GridSizeStore.CageOffset;
                return third;
            }

            return new Tuple<double, double>(0, 0);
        }

        private static Tuple<double, double> IsInCollectionFromDown(double x, double y, out double xOffset, out double yOffset,
            ObservableCollection<Tuple<double, double>> cells)
        {
            var first = new Tuple<double, double>(x - GridSizeStore.XCellSize, y);
            var second = new Tuple<double, double>(x, y - GridSizeStore.YCellSize);
            var third = new Tuple<double, double>(x + GridSizeStore.XCellSize, y);

            xOffset = 0;
            yOffset = 0;

            if (IsInCellCollection(first.Item1, first.Item2, cells))
            {
                xOffset = GridSizeStore.CageOffset;
                yOffset = -GridSizeStore.CageOffset;
                return first;
            }

            if (IsInCellCollection(second.Item1, second.Item2, cells))
            {
                xOffset = GridSizeStore.CageOffset;
                return second;
            }

            if (IsInCellCollection(third.Item1, third.Item2, cells))
            {
                xOffset = GridSizeStore.CageOffset;
                yOffset = GridSizeStore.CageOffset;
                return third;
            }

            return new Tuple<double, double>(0, 0);
        }

        private static Tuple<double, double> IsInCollectionFromRight(double x, double y, out double xOffset, out double yOffset,
            ObservableCollection<Tuple<double, double>> cells)
        {
            var first = new Tuple<double, double>(x, y + GridSizeStore.YCellSize);
            var second = new Tuple<double, double>(x - GridSizeStore.XCellSize, y);
            var third = new Tuple<double, double>(x, y - GridSizeStore.YCellSize);

            xOffset = 0;
            yOffset = 0;

            if (IsInCellCollection(first.Item1, first.Item2, cells))
            {
                xOffset = -GridSizeStore.CageOffset;
                yOffset = -GridSizeStore.CageOffset;
                return first;
            }

            if (IsInCellCollection(second.Item1, second.Item2, cells))
            {
                yOffset = -GridSizeStore.CageOffset;
                return second;
            }

            if (IsInCellCollection(third.Item1, third.Item2, cells))
            {
                xOffset = GridSizeStore.CageOffset;
                yOffset = -GridSizeStore.CageOffset;
                return third;
            }

            return new Tuple<double, double>(0, 0);
        }

        private static bool IsInCellCollection(double x, double y, ObservableCollection<Tuple<double, double>> cells)
        {
            foreach (var cell in cells)
            {
                if ((Math.Abs(x - cell.Item1) < .0001 && Math.Abs(y - cell.Item2) < .0001) ||
                    (Math.Abs(x - (cell.Item1 + GridSizeStore.XCellSize)) < .0001 && Math.Abs(y - cell.Item2) < .0001) ||
                    (Math.Abs(x - cell.Item1) < .0001 && Math.Abs(y - (cell.Item2 + GridSizeStore.YCellSize)) < .0001) ||
                    (Math.Abs(x - (cell.Item1 + GridSizeStore.XCellSize)) < .0001 && Math.Abs(y - (cell.Item2 + GridSizeStore.YCellSize)) < .0001))
                {
                    return true;
                }
            }
            return false;
        }

        public void Remove(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType type)
        {
            SudokuStore.Instance.Sudoku.SudokuVariants.Remove(_model);
            RemoveVariant(collection, type);
        }

        private void RemoveVariant(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType sudokuType)
        {
            if (!IsDeletingLastElemVariant(collection, sudokuType))
            {
                return;
            }
            if (sudokuType == SudokuElementType.Killer)
            {
                SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.Killer);
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
                var elem = item as CageViewModel;
                if (elem != null && elem.SudokuElemType == elemType)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Remove this element from elements in sudoku and if possible, change type of variant.
        /// </summary>
        /// <param name="collection">Collection of elements.</param>
        /// <param name="points">Collection of points of element.</param>
        /// <param name="elemType">Type of graphic element.</param>
        /// <returns>true if element was removed, otherwise false.</returns>
        public static bool RemoveFromCollection(ObservableCollection<SudokuElementViewModel> collection,
            PointCollection points, SudokuElementType elemType)
        {
            foreach (SudokuElementViewModel item in collection)
            {
                var elem = item as CageViewModel;
                if (elem != null)
                {
                    if (elem.SudokuElemType == elemType && elem.Points.Count == points.Count)
                    {
                        if (IsSamePoints(points, elem.Points))
                        {
                            elem.Remove(collection, elemType);
                            collection.Remove(elem.Text);
                            collection.Remove(item);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static bool IsSamePoints(PointCollection firstPoints, PointCollection secondPoints)
        {
            for (int i = 0; i < firstPoints.Count; i++)
            {
                if (firstPoints[i].X != secondPoints[i].X || firstPoints[i].Y != secondPoints[i].Y)
                {
                    return false;
                }
            }
            return true;
        }

        public void SetNewNumber(int? number)
        {
            _model.Number = number;
        }

        public int? GetCageNumber()
        {
            return _model.Number;
        }
    }
}
