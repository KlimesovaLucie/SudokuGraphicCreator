using SudokuGraphicCreator.Model;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by even rules.
    /// Rules: if in cell is placed grey square, must be in this cell even number.
    /// </summary>
    public class EvenRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in by even rules.
        /// </summary>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in grid.</param>
        /// <returns>true if <paramref name="number"/> can be placed by even rules.</returns>
        public static bool IsEvenSafe(int row, int col, int number)
        {
            if (IsEvenElem(row, col))
            {
                return number % 2 == 0;
            }
            return true;
        }

        private static bool IsEvenElem(int row, int col)
        {
            foreach (var element in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                GreySquare circle = element as GreySquare;
                if (circle != null && circle.RowIndex == row && circle.ColIndex == col && circle.SudokuElemType == SudokuElementType.Even)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
