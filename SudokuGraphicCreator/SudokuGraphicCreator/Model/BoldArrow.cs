using System.Windows.Media;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// This class represents graphic element in shape of arrow. Typically is placing in cell. With meaning can be used for example for Search nine sudoku.
    /// </summary>
    public class BoldArrow : OneSpaceElement
    {
        /// <summary>
        /// Inside color of arrow.
        /// </summary>
        public Brush FillColor { get; }

        /// <summary>
        /// Type of orientation of arrow.
        /// </summary>
        public GraphicElementType ElementType { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="BoldArrow"/> class.
        /// </summary>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="row">Index of row in sudoku table.</param>
        /// <param name="col">Index of column in sudoku table.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="graphicElemType">Type of orientation of arrow.</param>
        public BoldArrow(double left, double top, int row, int col, SudokuElementType type, GraphicElementType graphicElemType)
        {
            Left = left;
            Top = top;
            RowIndex = row;
            ColIndex = col;
            FillColor = Brushes.DarkGray;
            SudokuElemType = type;
            Location = ElementLocationType.Cell;
            ElementType = graphicElemType;
        }
    }
}
