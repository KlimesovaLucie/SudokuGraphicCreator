using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="LongArrow"/> model.
    /// </summary>
    public class LongArrowViewModel : SudokuElementViewModel
    {
        private readonly LongArrow _model;

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

        public PointCollection _headPoints;

        /// <summary>
        /// Collection of points of head of arrow.
        /// </summary>
        public PointCollection HeadPoints
        {
            get => _headPoints;
            set
            {
                _headPoints = value;
                OnPropertyChanged(nameof(HeadPoints));
            }
        }

        /// <summary>
        /// Color of cage, default <see cref="Brushes.DarkGray"/>
        /// </summary>
        public Brush Color => _model.FillColor;

        public double StrokeThickness { get; }

        private ObservableCollection<Tuple<int, int>> _cells;

        /// <summary>
        /// Initializes a new instance of <see cref="LongArrowViewModel"/> class.
        /// </summary>
        /// <param name="type">Type of sudoku graphic element.</param>
        /// <param name="cells">Collection of cells in which this element lies.</param>
        /// <param name="model">Model of this viewModel.</param>
        public LongArrowViewModel(SudokuElementType type, ObservableCollection<Tuple<int, int>> cells, LongArrow model)
        {
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(model);
            _model = model;
            _model.Positions = cells;
            StrokeThickness = 4;
            CreatePoints(cells);
            _cells = cells;
        }

        /// <summary>
        /// Resize this element by <paramref name="ratio"/>.
        /// </summary>
        /// <param name="ratio">Value for resizeing.</param>
        public void Resize()
        {
            CreatePoints(_cells);
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

            CreatePointsForHead();
        }

        private void CreatePointsForHead()
        {
            HeadPoints = new PointCollection();
            int pointsCount = Points.Count;
            double quaterCell = GridSizeStore.XCellSize / 4;
            double headX = Points[pointsCount - 1].X;
            double headY = Points[pointsCount - 1].Y;
            if (headY == Points[pointsCount - 2].Y)
            {
                if (headX > Points[pointsCount - 2].X)
                {
                    HeadPoints.Add(new Point(headX - quaterCell, headY - quaterCell));
                    HeadPoints.Add(new Point(headX, headY));
                    HeadPoints.Add(new Point(headX - quaterCell, headY + quaterCell));
                }
                else
                {
                    HeadPoints.Add(new Point(headX + quaterCell, headY - quaterCell));
                    HeadPoints.Add(new Point(headX, headY));
                    HeadPoints.Add(new Point(headX + quaterCell, headY + quaterCell));
                }
            }
            else if (headX == Points[pointsCount - 2].X)
            {
                if (headY > Points[pointsCount - 2].Y)
                {
                    HeadPoints.Add(new Point(headX - quaterCell, headY - quaterCell));
                    HeadPoints.Add(new Point(headX, headY));
                    HeadPoints.Add(new Point(headX + quaterCell, headY - quaterCell));
                }
                else
                {
                    HeadPoints.Add(new Point(headX - quaterCell, headY + quaterCell));
                    HeadPoints.Add(new Point(headX, headY));
                    HeadPoints.Add(new Point(headX + quaterCell, headY + quaterCell));
                }
            }
            else if (headX < Points[pointsCount - 2].X && headY < Points[pointsCount - 2].Y)
            {
                HeadPoints.Add(new Point(headX + quaterCell, headY));
                HeadPoints.Add(new Point(headX, headY));
                HeadPoints.Add(new Point(headX, headY + quaterCell));
            }
            else if (headX > Points[pointsCount - 2].X && headY < Points[pointsCount - 2].Y)
            {
                HeadPoints.Add(new Point(headX - quaterCell, headY));
                HeadPoints.Add(new Point(headX, headY));
                HeadPoints.Add(new Point(headX, headY + quaterCell));
            }
            else if (headX > Points[pointsCount - 2].X && headY > Points[pointsCount - 2].Y)
            {
                HeadPoints.Add(new Point(headX, headY - quaterCell));
                HeadPoints.Add(new Point(headX, headY));
                HeadPoints.Add(new Point(headX - quaterCell, headY));
            }
            else
            {
                HeadPoints.Add(new Point(headX, headY - quaterCell));
                HeadPoints.Add(new Point(headX, headY));
                HeadPoints.Add(new Point(headX + quaterCell, headY));
            }
        }

        public void Remove()
        {
            SudokuStore.Instance.Sudoku.SudokuVariants.Remove(_model);
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
                var elem = item as LongArrowViewModel;
                if (elem != null)
                {
                    if (elem.SudokuElemType == elemType && IsSameSpace(cells, elem._model.Positions))
                    {
                        elem.Remove();
                        collection.Remove(item);
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IsSameSpace(ObservableCollection<Tuple<int, int>> cells, ObservableCollection<Tuple<int, int>> cellsSecond)
        {
            if (cells.Count != cellsSecond.Count)
            {
                return false;
            }

            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].Item1 != cellsSecond[i].Item1 || cells[i].Item2 != cellsSecond[i].Item2)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
