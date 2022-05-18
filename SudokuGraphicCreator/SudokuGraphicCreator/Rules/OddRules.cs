using SudokuGraphicCreator.Model;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by odd rules.
    /// Rules: if in cell is placed grey circle, must be in this cell odd number.
    /// </summary>
    public class OddRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in by odd rules.
        /// </summary>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in grid.</param>
        /// <returns>true if <paramref name="number"/> can be placed by odd rules.</returns>
        public static bool IsOddSafe(int row, int col, int number)
        {
            if (IsOddElem(row, col))
            {
                return number % 2 != 0;
            }
            return true;
        }

        private static bool IsOddElem(int row, int col)
        {
            foreach (var element in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                GreyCircle circle = element as GreyCircle;
                if (circle != null && circle.RowIndex == row && circle.ColIndex == col && circle.SudokuElemType == SudokuElementType.Odd)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
