using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace SudokuGraphicCreator.Rules
{
    /// <summary>
    /// Solve sudoku by corresponding variants.
    /// </summary>
    public class SolveSudoku
    {
        /// <summary>
        /// Solve algorithm for finding solutions of sudoku.
        /// </summary>
        /// <param name="grid">Collection with given numbers.</param>
        /// <param name="gridSize">Zise of grid.</param>
        /// <param name="row">Actual row in grid.</param>
        /// <param name="col">Actual col in grid.</param>
        /// <param name="countSolutions">Total count of founded solutions.</param>
        /// <param name="firstSolution">Collection with first founded solution.</param>
        /// <param name="token"><see cref="CancellationToken"/> for stopping algorithm.</param>
        /// <returns>true if was founded solution, otherwise false.</returns>
        public static bool Solve(int[,] grid, int gridSize, int row, int col, ref int countSolutions, int[,] firstSolution, CancellationToken token)
        {
            if (row == gridSize - 1 && col == gridSize)
            {
                countSolutions++;
                return true;
            }

            if (col == gridSize)
            {
                row++;
                col = 0;
            }

            if (grid[row, col] != 0)
            {
                if (!token.IsCancellationRequested)
                {
                    return Solve(grid, gridSize, row, col + 1, ref countSolutions, firstSolution, token);
                }
                return true;
            }

            for (int number = 1; number < gridSize + 1; number++)
            {
                if (CanPlaceNumber(grid, gridSize, row, col, number))
                {
                    grid[row, col] = number;
                    if (token.IsCancellationRequested)
                    {
                        return true;
                    }
                    if (Solve(grid, gridSize, row, col + 1, ref countSolutions, firstSolution, token))
                    {
                        if (countSolutions == 1)
                        {
                            SafeIntoFirstSolution(grid, firstSolution, gridSize);
                        }
                        if (countSolutions == 2)
                        {
                            return true;
                        }
                    }
                }
                grid[row, col] = 0;
            }
            return false;
        }

        /// <summary>
        /// Before starts <see cref="Solve(int[,], int, int, int, ref int, int[,], CancellationToken)"/>, fill some numbers by Greater than rules.
        /// </summary>
        /// <param name="grid">Collection with given numbers.</param>
        /// <param name="gridSize">Zise of grid.</param>
        /// <param name="countSolutions">Total count of founded solutions.</param>
        /// <param name="firstSolution">Collection with first founded solution.</param>
        /// <param name="token"><see cref="CancellationToken"/> for stopping algorithm.</param>
        public static void SolveGTWithAll(int[,] grid, int gridSize, ref int countSolutions, int[,] firstSolution, CancellationToken token)
        {
            PlaceOneByGT(grid, gridSize, 1);
            PlaceOneByGT(grid, gridSize, 1);
            PlaceOneByGT(grid, gridSize, 9);
            PlaceOneByGT(grid, gridSize, 9);
            Solve(grid, gridSize, 0, 0, ref countSolutions, firstSolution, token);
        }

        private static void PlaceOneByGT(int[,] grid, int gridSize, int number)
        {
            for (int row = 0; row < gridSize; row++)
            {
                int countCanPlace = 0;
                int finalCol = 0;
                for (int col = 0; col < gridSize; col++)
                {
                    if (CanPlaceNumber(grid, gridSize, row, col, number))
                    {
                        countCanPlace++;
                        finalCol = col;
                    }
                }
                if (countCanPlace == 1)
                {
                    grid[row, finalCol] = number;
                }
            }

            for (int col = 0; col < gridSize; col++)
            {
                int countCanPlace = 0;
                int finalRow = 0;
                for (int row = 0; row < gridSize; row++)
                {
                    if (CanPlaceNumber(grid, gridSize, row, col, number))
                    {
                        countCanPlace++;
                        finalRow = row;
                    }
                }
                if (countCanPlace == 1)
                {
                    grid[finalRow, col] = number;
                }
            }

            foreach (var box in SudokuStore.Instance.Sudoku.Grid.Boxes)
            {
                int countCanPlace = 0;
                int finalRow = 0;
                int finalCol = 0;
                foreach (var cell in box)
                {
                    if (CanPlaceNumber(grid, gridSize, cell.Item1, cell.Item2, number))
                    {
                        countCanPlace++;
                        finalRow = cell.Item1;
                        finalCol = cell.Item2;
                    }
                }
                if (countCanPlace == 1)
                {
                    grid[finalRow, finalCol] = number;
                }
            }
        }

        private static void Propagation(List<List<int>> candidates, int row, int col, int number)
        {
            // remove from row
            for (int actualCol = 0; actualCol < 9; actualCol++)
            {
                if (actualCol != col && candidates[row * 9 + actualCol].Count != 1)
                {
                    candidates[row * 9 + actualCol].Remove(number);
                    if (candidates[row * 9 + actualCol].Count == 1)
                    {
                        Propagation(candidates, row, actualCol, candidates[row * 9 + actualCol][0]);
                    }
                }
            }

            //remove from col
            for (int actualRow = 0; actualRow < 9; actualRow++)
            {
                if (actualRow != row && candidates[actualRow * 9 + col].Count != 1)
                {
                    candidates[actualRow * 9 + col].Remove(number);
                    if (candidates[actualRow * 9 + col].Count == 1)
                    {
                        Propagation(candidates, actualRow, col, candidates[actualRow * 9 + col][0]);
                    }
                }
            }

            foreach (var cell in FindBox(row, col))
            {
                if (cell.Item1 != row && cell.Item2 != col && candidates[cell.Item1 * 9 + cell.Item2].Count != 1)
                {
                    candidates[cell.Item1 * 9 + cell.Item2].Remove(number);
                    if (candidates[cell.Item1 * 9 + cell.Item2].Count == 1)
                    {
                        Propagation(candidates, cell.Item1, cell.Item2, candidates[cell.Item1 * 9 + cell.Item2][0]);
                    }
                }
            }
        }

        private static ObservableCollection<Tuple<int, int>> FindBox(int row, int col)
        {
            foreach (var box in SudokuStore.Instance.Sudoku.Grid.Boxes)
            {
                foreach (var cell in box)
                {
                    if (cell.Item1 == row && cell.Item2 == col)
                    {
                        return box;
                    }
                }
            }
            return new ObservableCollection<Tuple<int, int>>();
        }

        private static void SafeIntoFirstSolution(int[,] foundedSolution, int[,] finalSolution, int gridSize)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    finalSolution[i, j] = foundedSolution[i, j];
                }
            }
        }

        private static bool CanPlaceNumber(int[,] grid, int gridSize, int row, int col, int number)
        {
            foreach (var variant in SudokuStore.Instance.Sudoku.Variants)
            {
                if (variant == SudokuType.Diagonal && !DiagonalRules.IsDiagonalSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Windoku && !WindokuRules.IsWindokuRules(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Antiknight && !AntiknightRules.IsAntiknightSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Nonconsecutive && !NonconsecutiveRules.IsNonconsecutiveSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Untouchable && !UntouchableRules.IsUntouchableSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.DisjointGroups && !DisjointGroupsRules.IsDisjointGroupsSafe(grid, SudokuStore.Instance.Sudoku.Grid.Boxes,
                    SudokuStore.Instance.Sudoku.Grid.XBoxCells, SudokuStore.Instance.Sudoku.Grid.YBoxCells, row, col, number,
                    !SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Irregular)))
                {
                    return false;
                }
                else if (variant == SudokuType.Consecutive && !ConsecutiveRules.IsConsecutiveSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Kropki && !KropkiRules.IsKropkiSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Sum && !SumRules.IsSumSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Difference && !DifferenceRules.IsDifferenceSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.GreaterThan && !GreaterThanRules.IsGreaterThanSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.XV && !XVRules.IsXVSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Odd && !OddRules.IsOddSafe(row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Even && !EvenRules.IsEvenSafe(row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.StarProduct && !StarProductRules.IsStarProductSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.SearchNine && !SearchNineRules.IsSearchNineSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Palindrome && !PalindromeRules.IsPalindromesSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Sequence && !SequencesRules.IsSequencesSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Arrow && !ArrowsRules.IsArrowsSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Thermometer && !ThermometerRules.IsThermometerSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Extraregion && !ExtraRegionsRules.IsExtraRegionsSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Killer && !KillerRules.IsKillerSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.LittleKiller && !LittleKillerRules.IsLittleKillerSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Skyscraper && !SkyscrapersRules.IsSkyscrapersSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.NextToNine && !NextToNineRules.IsNextToNineSafe(grid, row, col, number))
                {
                    return false;
                }
                else if (variant == SudokuType.Outside && !OutsideRule.IsOutsideSafe(grid, row, col, number))
                {
                    return false;
                }
            }
            return ClassicRules.IsClassicRules(grid, gridSize, SudokuStore.Instance.Sudoku.Grid.Boxes,
                row, col, number);
        }
    }
}
