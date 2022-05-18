using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewMode class for <see cref="LongArrowWithCircle"/> model.
    /// </summary>
    public class LongArrowWithCircleViewModel : SudokuElementViewModel
    {
        private readonly LongArrowWithCircle _model;

        private readonly LongArrow _arrowModel;

        private readonly CircleWithGreyEdge _circleModel;

        /// <summary>
        /// Type of sudoku graphic element.
        /// </summary>
        public SudokuElementType SudokuElemType => _model.SudokuElemType;

        /// <summary>
        /// ViewModel class of <see cref="LongArrow"/> model.
        /// </summary>
        public LongArrowViewModel Arrow { get; }

        /// <summary>
        /// ViewModel class of <see cref="CircleWithGreyEdge"/> model.
        /// </summary>
        public CircleWithGreyEdgeViewModel Circle { get; }

        /// <summary>
        /// Value in circle.
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
        /// Initializes a new instance of <see cref="LongArrowWithCircleViewModel"/> class.
        /// </summary>
        /// <param name="type">Type of sudoku graphic element.</param>
        /// <param name="cells">Collection of cells in which this element lies.</param>
        public LongArrowWithCircleViewModel(SudokuElementType type, ObservableCollection<Tuple<int, int>> cells)
        {
            var newElem = new LongArrowWithCircle(type);
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(newElem);
            _model = newElem;

            _arrowModel = new LongArrow(type, cells);
            Arrow = new LongArrowViewModel(type, cells, _arrowModel);

            double left = cells[0].Item2 * GridSizeStore.XCellSize +
                (GridSizeStore.XCellSize - GridSizeStore.InCellElementSize) / 2;
            double top = cells[0].Item1 * GridSizeStore.YCellSize +
                (GridSizeStore.YCellSize - GridSizeStore.InCellElementSize) / 2;

            _circleModel = new CircleWithGreyEdge(left, top, (int)(top / GridSizeStore.XCellSize), (int)(left / GridSizeStore.XCellSize), type);
            _model.Circle = _circleModel;
            _model.Arrow = _arrowModel;
            Circle = new CircleWithGreyEdgeViewModel(GridSizeStore.InCellElementSize, GridSizeStore.InCellElementSize,
                left, top, type, _circleModel);
            AddVariant(type);
        }

        /// <summary>
        /// Resize this element by <paramref name="ratio"/>.
        /// </summary>
        /// <param name="ratio">Value for resizeing.</param>
        public void Resize(double ratio)
        {
            Arrow.Resize();
            Circle.Resize(ratio);
        }

        private void AddVariant(SudokuElementType sudokuElement)
        {
            if (sudokuElement == SudokuElementType.Arrows)
            {
                AddSudokuVariant(SudokuType.Arrow);
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
            if (sudokuType == SudokuElementType.Arrows)
            {
                SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.Arrow);
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
                var elem = item as LongArrowWithCircleViewModel;
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
            Circle.Remove();
            Arrow.Remove();
            RemoveVariant(collection, type);
        }

        /// <summary>
        /// Remove this element from elements in sudoku and if possible, change type of variant.
        /// </summary>
        /// <param name="collection">Collection of elements.</param>
        /// <param name="circleLeft">Left distance from left up corner for circle of arrow.</param>
        /// <param name="circleTop">Top distance from left up corner for circle of arrow.</param>
        /// <param name="elemType"></param>
        /// <param name="cells">Collection of cells of element.</param>
        /// <returns>true if element was removed, otherwise false.</returns>
        public static bool RemoveFromCollection(ObservableCollection<SudokuElementViewModel> collection,
            double circleLeft, double circleTop, SudokuElementType elemType, ObservableCollection<Tuple<int, int>> cells)
        {
            foreach (SudokuElementViewModel item in collection)
            {
                var elem = item as LongArrowWithCircleViewModel;
                if (elem != null)
                {
                    if (elem.SudokuElemType == elemType && Math.Abs(elem.Circle.Left - circleLeft) < .0001 &&
                        Math.Abs(elem.Circle.Top - circleTop) < .0001 && IsSameSpace(cells, elem.Arrow.Points))
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
                if (Math.Abs(cell.Item1 * GridSizeStore.XCellSize + halfX - column) < .0001 &&
                    Math.Abs(cell.Item2 * GridSizeStore.YCellSize + halfY - row) < .0001)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
