using SudokuGraphicCreator.Model;
using System;
using System.Collections.Generic;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// Conntation helper methods for rules classes.
    /// </summary>
    public class SudokuRules
    {
        /// <summary>
        /// Decide if indexes are correct.
        /// </summary>
        /// <param name="gridSize">Size of grid.</param>
        /// <param name="row">Index of row.</param>
        /// <param name="col">Index fo column.</param>
        /// <returns>true if indexes are correct, otherwise false.</returns>
        public static bool AreIndexesInBound(int gridSize, int row, int col)
        {
            return 0 <= row && row < gridSize && 0 <= col && col < gridSize;
        }

        /// <summary>
        /// Decide if on <paramref name="row"/> and <paramref name="col"/> is instance <see cref="CircleWithNumber"/> class.
        /// </summary>
        /// <param name="row">Index of row in grid.</param>
        /// <param name="col">Index of col in grid.</param>
        /// <param name="elemType">Type of <see cref="CircleWithNumber"/>.</param>
        /// <param name="locationType">Location in grid.</param>
        /// <param name="circleValue">Value in circle.</param>
        /// <returns>true if grid contain on <paramref name="row"/> and <paramref name="col"/> instance of <see cref="CircleWithNumber"/> class.</returns>
        public static bool IsCircleWithNumberElem(int row, int col, SudokuElementType elemType, ElementLocationType locationType, out int circleValue)
        {
            foreach (var element in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                CircleWithNumber circle = element as CircleWithNumber;
                if (circle != null && circle.RowIndex == row && circle.ColIndex == col && circle.Location == locationType && circle.SudokuElemType == elemType)
                {
                    circleValue = circle.Value;
                    return true;
                }
            }
            circleValue = 0;
            return false;
        }

        /// <summary>
        /// Decide if on <paramref name="row"/> and <paramref name="col"/> is instance <see cref="Character"/> class.
        /// </summary>
        /// <param name="row">Index of row in grid.</param>
        /// <param name="col">Index of col in grid.</param>
        /// <param name="elemType">Type of <see cref="Character"/> element.</param>
        /// <param name="locationType">Location of <see cref="Character"/> in grid.</param>
        /// <returns>true if sudoku contains corresponding instance of <see cref="Character"/> class.</returns>
        public static bool IsCharacterElem(int row, int col, SudokuElementType elemType, ElementLocationType locationType)
        {
            foreach (var element in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                Character circle = element as Character;
                if (circle != null && circle.RowIndex == row && circle.ColIndex == col && circle.Location == locationType && circle.SudokuElemType == elemType)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Find <see cref="Line"/> in sudoku graphic elements base on <paramref name="row"/>, <paramref name="col"/> and <paramref name="type"/>.
        /// </summary>
        /// <param name="row">Rowindex in grid.</param>
        /// <param name="col">Col index in grid.</param>
        /// <param name="type">Type of finding <see cref="Line"/>.</param>
        /// <returns>collection of <see cref="Line"/></returns>
        public static List<Line> FindLine(int row, int col, SudokuElementType type)
        {
            List<Line> result = new List<Line>();
            foreach (var elem in Stores.SudokuStore.Instance.Sudoku.SudokuVariants)
            {
                Line line = elem as Line;
                if (line != null && line.SudokuElemType == type)
                {
                    Tuple<int, int> cell = new Tuple<int, int>(row, col);
                    if (line.Positions.Contains(cell))
                    {
                        result.Add(line);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Decides if on <paramref name="line"/> is only one number not filled.
        /// </summary>
        /// <param name="grid">Collection of given numbers in sudoku grid.</param>
        /// <param name="line">Line in grid.</param>
        /// <returns>true if on <paramref name="line"/> is only one number empty, otherwise false.</returns>
        public static bool IsLastOnLine(int[,] grid, Line line)
        {
            int notFill = 0;
            foreach (var cell in line.Positions)
            {
                if (grid[cell.Item1, cell.Item2] == 0)
                {
                    notFill++;
                }
            }
            return notFill == 1;
        }
    }
}
