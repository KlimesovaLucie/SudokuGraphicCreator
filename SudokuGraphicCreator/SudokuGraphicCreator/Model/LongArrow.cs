using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents long arrow as graphic element with <see cref="Brushes.DarkGray"/>. Consist of line and end with arrow.
    /// </summary>
    public class LongArrow : MultipleSpaceElement
    {
        /// <summary>
        /// <see cref="Brushes.DarkGray"/> color of element.
        /// </summary>
        public Brush FillColor { get; }

        /// <summary>
        /// Inilializes a new instance of <see cref="LongArrow"/> class.
        /// </summary>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="positions">Indexes of cells in table where element lies.</param>
        public LongArrow(SudokuElementType type, ObservableCollection<Tuple<int, int>> positions)
        {
            SudokuElemType = type;
            FillColor = Brushes.DarkGray;
            Positions = positions;
        }
    }
}
