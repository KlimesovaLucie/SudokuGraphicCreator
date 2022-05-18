using System;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by dijsoint group rules.
    /// Rule: extra boxes from cells in all boxes in corresponding positions must have different numbers.
    /// </summary>
    public class DisjointGroupsRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by disjoint groups rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="boxes">Boxes in grid.</param>
        /// <param name="xBoxCells">Size of box in x direction.</param>
        /// <param name="yBoxCells">Size of box in y direction.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <param name="isClassicGrid">This variant is only for grid corresponding classic sudoku.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by disjoint groups rules.</returns>
        public static bool IsDisjointGroupsSafe(int[,] grid, ObservableCollection<ObservableCollection<Tuple<int, int>>> boxes,
            int xBoxCells, int yBoxCells, int row, int col, int number, bool isClassicGrid)
        {
            if (!isClassicGrid)
            {
                return true;
            }

            int rowBox = row % yBoxCells;
            int colBox = col % xBoxCells;
            foreach (var box in boxes)
            {
                Tuple<int, int> actualBox = box[rowBox * xBoxCells + colBox];
                if (actualBox.Item1 != row && actualBox.Item2 != col && grid[actualBox.Item1, actualBox.Item2] == number)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
