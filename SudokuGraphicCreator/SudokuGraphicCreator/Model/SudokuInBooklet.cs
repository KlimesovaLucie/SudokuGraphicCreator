using SudokuGraphicCreator.ViewModel;
using SudokuGraphicCreator.Properties.Resources;
using System.Text;
using System.Windows.Media.Imaging;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// The model class for <see cref="SudokuInBookletViewModel"/> model. Represents one sudoku in booklet.
    /// </summary>
    public class SudokuInBooklet
    {
        /// <summary>
        /// Name of sudoku.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Points of sudoku.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Order number of sudoku in booklet.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Rules of sudoku.
        /// </summary>
        public string Rules { get; set; }

        /// <summary>
        /// Image of sudoku in booklet.
        /// </summary>
        public BitmapImage Table { get; set; }

        /// <summary>
        /// Name of file with image of sudoku in booklet.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Full path in file system to file with image of sudoku in booklet.
        /// </summary>
        public string TableFullPath { get; set; }

        /// <summary>
        /// Image with solution of sudoku in booklet.
        /// </summary>
        public BitmapImage Solution { get; set; }

        /// <summary>
        /// Name of file with image with solution of sudoku in booklet.
        /// </summary>
        public string SolutionName { get; set; }

        /// <summary>
        /// Full path in file system to file with image with solution of sudoku in booklet.
        /// </summary>
        public string SolutionFullPath { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SudokuInBooklet"/> class.
        /// </summary>
        /// <param name="name">Name of sudoku.</param>
        /// <param name="points">Points of sudoku.</param>
        /// <param name="order">Order number in booklet.</param>
        /// <param name="rules">Rules of sudoku in booklet.</param>
        public SudokuInBooklet(string name, int points, int order, string rules)
        {
            Name = name;
            Points = points;
            Order = order;
            Rules = rules;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SudokuInBooklet"/> class.
        /// </summary>
        public SudokuInBooklet() { }

        /// <summary>
        /// String representation with points.
        /// </summary>
        /// <returns>String format of the currect object.</returns>
        public string GetNameWithPoints()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Order)
                .Append(". ")
                .Append(Name)
                .Append(" (")
                .Append(Points)
                .Append(" ")
                .Append(GetCorrectPointsString())
                .Append(")");
            return stringBuilder.ToString();
        }

        private string GetCorrectPointsString()
        {
            if (Points == 1)
            {
                return Resources.Point;
            }
            else if (2 <= Points && Points <= 4)
            {
                return Resources.PointsMiddle;
            }
            return Resources.Points;
        }

        public override string ToString()
        {
            return GetNameWithPoints();
        }
    }
}
