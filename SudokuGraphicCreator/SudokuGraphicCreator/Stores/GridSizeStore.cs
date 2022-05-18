namespace SudokuGraphicCreator.Stores
{
    /// <summary>
    /// Stores sizes of sudoku graphic elements in view.
    /// </summary>
    public class GridSizeStore
    {
        private static GridSizeStore _instance = new GridSizeStore();

        /// <summary>
        /// Instance of this class.
        /// </summary>
        public static GridSizeStore Instance => _instance;

        /// <summary>
        /// Size of cell.
        /// </summary>
        public static double XCellSize = 35;

        /// <summary>
        /// Size of cell.
        /// </summary>
        public static double YCellSize = 35;

        /// <summary>
        /// Thickness of normal line.
        /// </summary>
        public static double NormalLine = 0.5;

        /// <summary>
        /// Thickness of bold line.
        /// </summary>
        public static double BoldLine = 1.5;

        /// <summary>
        /// Size of element placed in cell.
        /// </summary>
        public static double InCellElementSize = 28;

        /// <summary>
        /// Innner radius of star element.
        /// </summary>
        public static double InnerRadiusStar = 7;

        /// <summary>
        /// Size of text element of edge.
        /// </summary>
        public static double OnEdgeTextElement = 40;

        /// <summary>
        /// Text ize of text element of edge.
        /// </summary>
        public static double OnEdgeTextSize = 17;

        /// <summary>
        /// Offset of cage from border od cell.
        /// </summary>
        public static double CageOffset = 3;

        /// <summary>
        /// Text size of killer value.
        /// </summary>
        public static double KillerNumberTextSize = 10;

        /// <summary>
        /// Size of killer value.
        /// </summary>
        public static double KillerNumberSize = 21;

        /// <summary>
        /// Size of small circle.
        /// </summary>
        public static double SmallCircleSize = 10;

        /// <summary>
        /// Size of text in cell.
        /// </summary>
        public static double InCellTextSize = 21;

        /// <summary>
        /// Size of circle.
        /// </summary>
        public static double CircleSize = 17;

        /// <summary>
        /// Thickness of thermometer.
        /// </summary>
        public static double ThermometerThickness = 14;

        private GridSizeStore()
        {
        }

        static GridSizeStore()
        {
        }
    }
}
