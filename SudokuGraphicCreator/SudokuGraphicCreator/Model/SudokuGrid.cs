using System;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represent grid of sudoku.
    /// </summary>
    public class SudokuGrid
    {
        /// <summary>
        /// Count of cell in row / column.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Count of cells in box in x direction.
        /// </summary>
        public int XBoxCells { get; }

        /// <summary>
        /// Count of cells in box in y direction.
        /// </summary>
        public int YBoxCells { get; }

        /// <summary>
        /// Collection of boxes. One box is collection of cells defined by index of row and column.
        /// </summary>
        public ObservableCollection<ObservableCollection<Tuple<int, int>>> Boxes { get; set; }

        /// <summary>
        /// Default collection of boxes. One box is collection of cells defined by index of row and column.
        /// </summary>
        public ObservableCollection<ObservableCollection<Tuple<int, int>>> DefaultBoxes { get; set; }

        /// <summary>
        /// Collection of boxes represents extra regions. One box is collection of cells defined by index of row and column.
        /// </summary>
        public ObservableCollection<ObservableCollection<Tuple<int, int>>> ExtraRegions { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SudokuGrid"/> class.
        /// </summary>
        /// <param name="size">Of of cells in row / col of grid.</param>
        /// <param name="xBoxCells">Count of cells in box in x direction.</param>
        /// <param name="yBoxCells">Count of cells in box in y direction.</param>
        public SudokuGrid(int size, int xBoxCells, int yBoxCells)
        {
            Size = size;
            XBoxCells = xBoxCells;
            YBoxCells = yBoxCells;
            Boxes = new ObservableCollection<ObservableCollection<Tuple<int, int>>>();
            DefaultBoxes = new ObservableCollection<ObservableCollection<Tuple<int, int>>>();
            ExtraRegions = new ObservableCollection<ObservableCollection<Tuple<int, int>>>();
            CreateDefaultBoxes();
        }

        private void CreateDefaultBoxes()
        {
            for (int i = 0; i < Size; i++)
            {
                Boxes.Add(new ObservableCollection<Tuple<int, int>>());
                DefaultBoxes.Add(new ObservableCollection<Tuple<int, int>>());
            }

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    int x = col / XBoxCells;
                    int y = row / YBoxCells;
                    int index = x * XBoxCells + y;
                    Boxes[index].Add(new Tuple<int, int>(row, col));
                    DefaultBoxes[index].Add(new Tuple<int, int>(row, col));
                }
            }
        }
    }
}
