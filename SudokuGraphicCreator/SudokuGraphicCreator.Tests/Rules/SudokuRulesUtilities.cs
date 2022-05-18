using SudokuGraphicCreator.Model;
using System;
using System.Collections.Generic;

namespace SudokuGraphicCreator.Tests.Rules
{
    public class SudokuRulesUtilities
    {
        public static int[,] CreateArrayFromInputString(string str, int row, int col)
        {
            int[,] result = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    result[i, j] = Int32.Parse(str[i * col + j].ToString());
                }
            }
            return result;
        }

        public static int[,] CreateEmptyGivenNumbers(int row, int col)
        {
            int[,] result = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    result[i, j] = 0;
                }
            }
            return result;
        }

        public static SudokuElementType[,] MapTypesByNumbers(string str, int row, int col, SudokuElementType type)
        {
            SudokuElementType[,] result = new SudokuElementType[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (str[i * col + j].ToString() != "0")
                    {
                        result[i, j] = type;
                    }
                    else
                    {
                        result[i, j] = SudokuElementType.NoMeaning;
                    }
                }
            }
            return result;
        }

        public static int[,] CreateUpNumbers(string str, int row, int col)
        {
            int[,] result = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    result[Math.Abs(2 - i), j] = Int32.Parse(str[col * i + j].ToString());
                }
            }
            return result;
        }

        public static SudokuElementType[,] MapTypesUpNumbers(string str, int row, int col, SudokuElementType type)
        {
            SudokuElementType[,] result = new SudokuElementType[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (str[i * col + j].ToString() != "0")
                    {
                        result[i, j] = type;
                    }
                    else
                    {
                        result[i, j] = SudokuElementType.NoMeaning;
                    }
                }
            }
            return result;
        }

        public static int[,] CreateBottomNumbers(string str, int row, int col)
        {
            int[,] result = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    result[i, j] = Int32.Parse(str[i * col + j].ToString());
                }
            }
            return result;
        }

        public static bool IsCorrectResult(int[,] solution, int[,] founded)
        {
            for (int i = 0; i < solution.GetLength(0); i++)
            {
                for (int j = 0; j < solution.GetLength(0); j++)
                {
                    if (solution[i, j] != founded[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
