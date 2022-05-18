using System;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// This class represents graphic element cage. With meaning can be used for example for Killer sudoku.
    /// </summary>
    public class Cage : MultipleSpaceElement
    {
        /// <summary>
        /// Text in left up corner of cage.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Number in left up corner of cage.
        /// </summary>
        public int? Number { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="Cage"/> class.
        /// </summary>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="positions">Indexes of cells in table where element lies.</param>
        public Cage(SudokuElementType type, ObservableCollection<Tuple<int, int>> positions)
        {
            SudokuElemType = type;
            Positions = positions;
        }
    }
}
