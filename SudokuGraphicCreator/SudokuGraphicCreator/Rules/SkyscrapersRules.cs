using System.Collections.Generic;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by skyscrapers rules.
    /// Rules: number around grid says how many numbers in corresponding direction can be seen.
    /// </summary>
    public class SkyscrapersRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by skyscrapers rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by skyscrapers rules.</returns>
        public static bool IsSkyscrapersSafe(int[,] grid, int row, int col, int number)
        {
            return IsSkyscraperInRow(grid, row, number) && IsSkyscraperInCol(grid, col, number);
        }

        private static bool IsSkyscraperInRow(int[,] grid, int row, int number)
        {
            List<int> numbers = new List<int>();
            int missing = 0;
            for (int actualCol = 0; actualCol < Stores.SudokuStore.Instance.Sudoku.Grid.Size; actualCol++)
            {
                int actualNumber = grid[row, actualCol];
                if (actualNumber == 0)
                {
                    actualNumber = number;
                    missing++;
                }
                numbers.Add(actualNumber);
            }
            if (missing != 1)
            {
                return true;
            }

            int leftSkyscraper = Stores.SudokuStore.Instance.Sudoku.LeftNumbers[row, 2];
            if (leftSkyscraper != 0)
            {
                int count = 1;
                int previousNumber = numbers[0];
                for (int actualCol = 1; actualCol < Stores.SudokuStore.Instance.Sudoku.Grid.Size; actualCol++)
                {
                    if (previousNumber < numbers[actualCol])
                    {
                        count++;
                        previousNumber = numbers[actualCol];
                    }
                }
                if (count != leftSkyscraper)
                {
                    return false;
                }
            }

            int rightSkyscraper = Stores.SudokuStore.Instance.Sudoku.RightNumbers[row, 0];
            if (rightSkyscraper != 0)
            {
                int count = 1;
                int previousNumber = numbers[Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1];
                for (int actualCol = Stores.SudokuStore.Instance.Sudoku.Grid.Size - 2; actualCol >= 0; actualCol--)
                {
                    if (previousNumber < numbers[actualCol])
                    {
                        count++;
                        previousNumber = numbers[actualCol];
                    }
                }
                if (count != rightSkyscraper)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsSkyscraperInCol(int[,] grid, int col, int number)
        {
            List<int> numbers = new List<int>();
            int missing = 0;
            for (int actualRow = 0; actualRow < Stores.SudokuStore.Instance.Sudoku.Grid.Size; actualRow++)
            {
                int actualNumber = grid[actualRow, col];
                if (actualNumber == 0)
                {
                    actualNumber = number;
                    missing++;
                }
                numbers.Add(actualNumber);
            }
            if (missing != 1)
            {
                return true;
            }

            int upSkyscraper = Stores.SudokuStore.Instance.Sudoku.UpNumbers[2, col];
            if (upSkyscraper != 0)
            {
                int count = 1;
                int previousNumber = numbers[0];
                for (int actualRow = 1; actualRow < Stores.SudokuStore.Instance.Sudoku.Grid.Size; actualRow++)
                {
                    if (previousNumber < numbers[actualRow])
                    {
                        count++;
                        previousNumber = numbers[actualRow];
                    }
                }
                if (count != upSkyscraper)
                {
                    return false;
                }
            }

            int downSkyscraper = Stores.SudokuStore.Instance.Sudoku.BottomNumbers[0, col];
            if (downSkyscraper != 0)
            {
                int count = 1;
                int previousNumber = numbers[Stores.SudokuStore.Instance.Sudoku.Grid.Size - 1];
                for (int actualRow = Stores.SudokuStore.Instance.Sudoku.Grid.Size - 2; actualRow >= 0; actualRow--)
                {
                    if (previousNumber < numbers[actualRow])
                    {
                        count++;
                        previousNumber = numbers[actualRow];
                    }
                }
                if (count != downSkyscraper)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
