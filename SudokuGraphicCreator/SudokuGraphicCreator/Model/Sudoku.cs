using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents creating sudoku.
    /// </summary>
    public class Sudoku
    {
        /// <summary>
        /// Grid of sudoku.
        /// </summary>
        public SudokuGrid Grid { get; }
        
        /// <summary>
        /// Collection to given numbers.
        /// </summary>
        public int[,] GivenNumbers { get; set; }

        /// <summary>
        /// Collections of types of <see cref="GivenNumbers"/>.
        /// </summary>
        public SudokuElementType[,] GridNumbersType { get; set; }

        /// <summary>
        /// Collection of number of left side of the grid.
        /// </summary>
        public int[,] LeftNumbers { get; set; }

        /// <summary>
        /// Collection of types of <see cref="LeftNumbers"/>.
        /// </summary>
        public SudokuElementType[,] LeftNumbersType { get; set; }

        /// <summary>
        /// Collection of number of right side of the grid.
        /// </summary>
        public int[,] RightNumbers { get; set; }

        /// <summary>
        /// Collection of types of <see cref="RightNumbers"/>.
        /// </summary>
        public SudokuElementType[,] RightNumbersType { get; set; }

        /// <summary>
        /// Collection of number of up side of the grid.
        /// </summary>
        public int[,] UpNumbers { get; set; }

        /// <summary>
        /// Collection of types of <see cref="UpNumbers"/>.
        /// </summary>
        public SudokuElementType[,] UpNumbersType { get; set; }

        /// <summary>
        /// Collection of number of down side of the grid.
        /// </summary>
        public int[,] BottomNumbers { get; set; }

        /// <summary>
        /// Collection of types of <see cref="BottomNumbersType"/>.
        /// </summary>
        public SudokuElementType[,] BottomNumbersType { get; set; }

        /// <summary>
        /// Collection of graphic elements of sudoku.
        /// </summary>
        public ObservableCollection<SudokuElement> SudokuVariants { get; }

        /// <summary>
        /// List of variant that sudoku represents.
        /// </summary>
        public List<SudokuType> Variants { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Sudoku"/> class.
        /// </summary>
        /// <param name="size">Size of grid.</param>
        /// <param name="xbox">Count of cells in box in x direction.</param>
        /// <param name="ybox">Count of cells in box in y direction.</param>
        public Sudoku(int size, int xbox, int ybox)
        {
            Grid = new SudokuGrid(size, xbox, ybox);
            SudokuVariants = new ObservableCollection<SudokuElement>();
            Variants = new List<SudokuType>();
            GivenNumbers = new int[size, size];
            LeftNumbers = new int[size, 3];
            RightNumbers = new int[size, 3];
            UpNumbers = new int[3, size];
            BottomNumbers = new int[3, size];
            GridNumbersType = new SudokuElementType[size, size];
            LeftNumbersType = new SudokuElementType[size, 3];
            RightNumbersType = new SudokuElementType[size, 3];
            UpNumbersType = new SudokuElementType[3, size];
            BottomNumbersType = new SudokuElementType[3, size];
        }
    }
}
