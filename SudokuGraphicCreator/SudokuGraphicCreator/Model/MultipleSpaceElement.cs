using System;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents sudoku graphic element which in many spaces in sudoku table.
    /// </summary>
    public abstract class MultipleSpaceElement : SudokuElement
    {
        private ObservableCollection<Tuple<int, int>> _positions = new ObservableCollection<Tuple<int, int>>();

        /// <summary>
        /// Indexes of cells in table where element lies.
        /// </summary>
        public ObservableCollection<Tuple<int, int>> Positions
        {
            get => _positions;
            set
            {
                _positions = value;
            }
        }
    }
}
