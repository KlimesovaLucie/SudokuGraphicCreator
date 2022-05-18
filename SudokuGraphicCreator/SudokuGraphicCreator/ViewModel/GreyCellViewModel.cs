using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.Windows;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="GreyCell"/> class.
    /// </summary>
    public class GreyCellViewModel : SudokuElementViewModel
    {
        private readonly GreyCell _model;

        /// <summary>
        /// Type of sudoku graphic element.
        /// </summary>
        public SudokuElementType SudokuElemType => _model.SudokuElemType;

        /// <summary>
        /// Left distance from left up corner of grid.
        /// </summary>
        public double Left => _model.Left;

        /// <summary>
        /// Top distance form left up corner of grid.
        /// </summary>
        public double Top => _model.Top;

        /// <summary>
        /// Width of cell.
        /// </summary>
        public double Width => GridSizeStore.XCellSize;

        /// <summary>
        /// Height of cell.
        /// </summary>
        public double Height => GridSizeStore.XCellSize;

        /// <summary>
        /// Margin of cell.
        /// </summary>
        public Thickness Margin { get; }

        /// <summary>
        /// Color of arrow, default <see cref="Brushes.DarkGray"/>.
        /// </summary>
        public Brush Fill => _model.FillColor;

        /// <summary>
        /// Initializes a new instance of <see cref="GreyCellViewModel"/> class.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="type">Type of graphic element.</param>
        public GreyCellViewModel(double left, double top, SudokuElementType elementType)
        {
            GreyCell cell = new GreyCell(left, top, (int)(top / GridSizeStore.XCellSize), (int)(left / GridSizeStore.XCellSize), elementType);
            _model = cell;
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(cell);
            Margin = new Thickness(left, top, 0, 0);
        }
    }
}
