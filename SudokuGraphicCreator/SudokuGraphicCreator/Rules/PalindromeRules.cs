using SudokuGraphicCreator.Model;
using System.Collections.Generic;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by sequence rules.
    /// Rules: number on lines must form palindrome.
    /// </summary>
    public class PalindromeRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by palidrome rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by palindrome rules.</returns>
        public static bool IsPalindromesSafe(int[,] grid, int row, int col, int number)
        {
            List<Line> line = SudokuRules.FindLine(row, col, SudokuElementType.Palindromes);
            if (line.Count == 0)
            {
                return true;
            }
            return CanBeOnPalindrome(grid, line, number);
        }

        private static bool CanBeOnPalindrome(int[,] grid, List<Line> lines, int number)
        {
            foreach (var line in lines)
            {
                if (!ValidateOnePalindrome(grid, line, number))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool ValidateOnePalindrome(int[,] grid, Line line, int number)
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

            int index = 0;
            while (index <= numbers.Count / 2)
            {
                if (numbers[index] != numbers[numbers.Count - 1 - index])
                {
                    return false;
                }
                index++;
            }
            return true;
        }
    }
}
