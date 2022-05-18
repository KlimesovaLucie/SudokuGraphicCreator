namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by noncinsecutive rules.
    /// Rules: all adjancent numbers must be nonconsecutive.
    /// </summary>
    public class NonconsecutiveRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by nonconsecutive rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by nonconsecutive rules.</returns>
        public static bool IsNonconsecutiveSafe(int[,] grid, int row, int col, int number)
        {
            return IsCorrectNonconsecutive(grid, row - 1, col, number) &&
                IsCorrectNonconsecutive(grid, row + 1, col, number) &&
                IsCorrectNonconsecutive(grid, row, col - 1, number) &&
                IsCorrectNonconsecutive(grid, row, col + 1, number);
        }

        private static bool IsCorrectNonconsecutive(int[,] grid, int row, int col, int number)
        {
            if (SudokuRules.AreIndexesInBound(grid.GetLength(0), row, col))
            {
                if (!AreNonconsecutiveNumbers(grid[row, col], number))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool AreNonconsecutiveNumbers(int givenNumber, int secondNumber)
        {
            if (givenNumber == 0)
            {
                return true;
            }
            return givenNumber + 1 != secondNumber && givenNumber - 1 != secondNumber;
        }
    }
}
