using SudokuGraphicCreator.Model;
using System;
using System.Collections.Generic;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by sequence rules.
    /// Rules: number on lines follow arithmetic sequence.
    /// </summary>
    public class SequencesRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by sequence rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by sequence rules.</returns>
        public static bool IsSequencesSafe(int[,] grid, int row, int col, int number)
        {
            List<Line> line = SudokuRules.FindLine(row, col, SudokuElementType.Sequences);
            if (line.Count == 0)
            {
                return true;
            }

            return CanBeOnSequence(grid, line, row, col, number);
        }

        private static bool CanBeOnSequence(int[,] grid, List<Line> lines, int row, int col, int number)
        {
            foreach (var line in lines)
            {
                if (!ValidateOneSequence(grid, line, number))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool ValidateOneSequence(int[,] grid, Line line, int number)
        {
            if (!SudokuRules.IsLastOnLine(grid, line))
            {
                return true;
            }

            List<int> numbers = new List<int>();

            foreach (var cell in line.Positions)
            {
                int actualNumber = grid[cell.Item1, cell.Item2];
                if (actualNumber == 0)
                {
                    actualNumber = number;
                }

                numbers.Add(actualNumber);
            }

            int step = Math.Abs(numbers[0] - numbers[1]);
            int firstNumber = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                int actualNumber = numbers[i];
                if (Math.Abs(firstNumber - actualNumber) != step)
                {
                    return false;
                }
                firstNumber = actualNumber;
            }
            return true;
        }
    }
}
