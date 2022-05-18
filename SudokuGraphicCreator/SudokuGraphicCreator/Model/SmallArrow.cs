using System.Windows.Media;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents black small arrow in director to corner of cell.
    /// </summary>
    public class SmallArrow : OneSpaceElement
    {
        /// <summary>
        /// Color of arrow, default <see cref="Brushes.Black"/>.
        /// </summary>
        public Brush FillColor { get; }

        /// <summary>
        /// Value that arrow represents.
        /// </summary>
        public int? Value { get; set; }

        /// <summary>
        /// Order in grid.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Type of orientation of arrow.
        /// </summary>
        public GraphicElementType GraphicType { get; set; }

        /// <summary>
        /// Initiliazes a new instance of <see cref="SmallArrow"/> class.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="row">Index of row in sudoku table.</param>
        /// <param name="col">Index of column in sudoku table.</param>
        /// <param name="order">Order in grid.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="location">Location on grid.</param>
        /// <param name="graphicType">Type of orientation of arrow.</param>
        public SmallArrow(double left, double top, int row, int col, int order, SudokuElementType type, ElementLocationType location, GraphicElementType graphicType)
        {
            Left = left;
            Top = top;
            Order = order;
            RowIndex = row;
            ColIndex = col;
            FillColor = Brushes.Black;
            SudokuElemType = type;
            Location = location;
            GraphicType = graphicType;
        }
    }
}
