using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents line in sudoku table with <see cref="Brushes.DarkGray"/> color.
    /// </summary>
    public class Line : MultipleSpaceElement
    {
        /// <summary>
        /// <see cref="Brushes.DarkGray"/> color of line.
        /// </summary>
        public Brush FillColor { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Line"/> class.
        /// </summary>
        /// <param name="type">Type of graphic element.</param>
        public Line(SudokuElementType type, ObservableCollection<Tuple<int, int>> positions)
        {
            SudokuElemType = type;
            FillColor = Brushes.DarkGray;
            Positions = positions;
        }
    }
}
