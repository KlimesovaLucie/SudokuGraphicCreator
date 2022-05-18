using System;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by rules of Classic sudoku.
    /// </summary>
    public class ClassicRules
    {
        /// <summary>
        /// Deside if numbers in table are correct by rules od Classic sudoku.
        /// </summary>
        /// <param name="grid">Collection with given numbers.</param>
        /// <param name="gridSize">Size of grid.</param>
        /// <returns></returns>
        public static bool IsCorrectByClassic(int[,] grid, int gridSize)
        {
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    if (grid[row, col] != 0 && !IsOnceByClassic(grid, gridSize, row, col, grid[row, col]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsOnceByClassic(int[,] grid, int gridSize, int actualRow, int actualCol, int number)
        {
            for (int row = 0; row < gridSize; row++)
            {
                if (row != actualRow && grid[row, actualCol] == number)
                {
                    return false;
                }
            }

            for (int col = 0; col < gridSize; col++)
            {
                if (col != actualCol && grid[actualRow, col] == number)
                {
                    return false;
                }
            }

            var box = FindActualBox(Stores.SudokuStore.Instance.Sudoku.Grid.Boxes, actualRow, actualCol);
            foreach (var cell in box)
            {
                if (cell.Item1 != actualRow && cell.Item2 != actualCol && grid[cell.Item1, cell.Item2] == number)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Deside it <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by rules of Classic sudoku.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="gridSize">Szie of grid.</param>
        /// <param name="boxes">Boxes of sudoku grid.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns></returns>
        public static bool IsClassicRules(int[,] grid, int gridSize,
            ObservableCollection<ObservableCollection<Tuple<int, int>>> boxes, int row, int col,
            int number)
        {
            return IsRowSafe(grid, gridSize, row, number) && IsColumnSafe(grid, gridSize, col, number) &&
                IsBoxSafe(grid, boxes, row, col, number);
        }

        private static bool IsRowSafe(int[,] grid, int gridSize, int row, int number)
        {
            for (int actualCol = 0; actualCol < gridSize; actualCol++)
            {
                if (grid[row, actualCol] == number)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsColumnSafe(int[,] grid, int gridSize, int col, int number)
        {
            for (int actualRow = 0; actualRow < gridSize; actualRow++)
            {
                if (grid[actualRow, col] == number)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsBoxSafe(int[,] grid, ObservableCollection<ObservableCollection<Tuple<int, int>>> boxes,
            int row, int col, int number)
        {
            ObservableCollection<Tuple<int, int>> actualBox = FindActualBox(boxes, row, col);
            foreach (var cell in actualBox)
            {
                if (grid[cell.Item1, cell.Item2] == number)
                {
                    return false;
                }
            }
            return true;
        }

        private static ObservableCollection<Tuple<int, int>> FindActualBox(ObservableCollection<ObservableCollection<Tuple<int, int>>> boxes,
            int row, int col)
        {
            foreach (var actualBox in boxes)
            {
                foreach (var cell in actualBox)
                {
                    if (cell.Item1 == row && cell.Item2 == col)
                    {
                        return actualBox;
                    }
                }
            }
            return new ObservableCollection<Tuple<int, int>>();
        }
    }
}
