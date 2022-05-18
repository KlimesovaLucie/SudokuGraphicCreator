using SudokuGraphicCreator.Model;
using System.Collections.Generic;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// This class deside if number can be placed in given row and col by thermometer rules.
    /// Rules: numbers on thermometer have to increase in direction from bulb.
    /// </summary>
    public class ThermometerRules
    {
        /// <summary>
        /// Deside if <paramref name="number"/> can be placed in given <paramref name="row"/> and <paramref name="col"/> in <paramref name="grid"/> by thermometer rules.
        /// </summary>
        /// <param name="grid">Grid of sudoku.</param>
        /// <param name="row">Row in which is <paramref name="number"/> placing.</param>
        /// <param name="col">Col in which is <paramref name="number"/> placing.</param>
        /// <param name="number">Value which is placing in <paramref name="grid"/>.</param>
        /// <returns>true if <paramref name="number"/> can be placed in <paramref name="grid"/> by thermometer rules.</returns>
        public static bool IsThermometerSafe(int[,] grid, int row, int col, int number)
        {
            List<Line> thermometer = SudokuRules.FindLine(row, col, SudokuElementType.Thermometers);
            if (thermometer.Count == 0)
            {
                return true;
            }
            return CanBePlacedOnThermometer(grid, thermometer, number);
        }

        private static bool CanBePlacedOnThermometer(int[,] grid, List<Line> thermometers, int number)
        {
            foreach (var thermometer in thermometers)
            {
                if (!ValidateOneThermometer(grid, thermometer, number))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool ValidateOneThermometer(int[,] grid, Line thermometer, int number)
        {
            if (!SudokuRules.IsLastOnLine(grid, thermometer))
            {
                return true;
            }

            List<int> numbers = new List<int>();

            foreach (var cell in thermometer.Positions)
            {
                int actualNumber = grid[cell.Item1, cell.Item2];
                if (actualNumber == 0)
                {
                    actualNumber = number;
                }

                numbers.Add(actualNumber);
            }

            int previousNumber = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                int actualNumber = numbers[i];
                if (actualNumber <= previousNumber)
                {
                    return false;
                }
                previousNumber = actualNumber;
            }
            return true;
        }
    }
}
