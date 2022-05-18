using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// This class represents lines in grid.
    /// </summary>
    public class ClassicLineViewModel : SudokuElementViewModel
    {
        private double _x1;

        /// <summary>
        /// X coordinate of first point.
        /// </summary>
        public double X1
        {
            get => _x1;
            set
            {
                _x1 = value;
                OnPropertyChanged(nameof(X1));
            }
        }

        private double _x2;

        /// <summary>
        /// X coordinate of second point.
        /// </summary>
        public double X2
        {
            get => _x2;
            set
            {
                _x2 = value;
                OnPropertyChanged(nameof(X2));
            }
        }

        private double _y1;

        /// <summary>
        /// Y coordinate of first point.
        /// </summary>
        public double Y1
        {
            get => _y1;
            set
            {
                _y1 = value;
                OnPropertyChanged(nameof(Y1));
            }
        }

        private double _y2;

        /// <summary>
        /// Y coordinate of second point.
        /// </summary>
        public double Y2
        {
            get => _y2;
            set
            {
                _y2 = value;
                OnPropertyChanged(nameof(Y2));
            }
        }

        /// <summary>
        /// Thickness of line.
        /// </summary>
        public double StrokeThickness { get; set; }

        /// <summary>
        /// Color of line.
        /// </summary>
        public SolidColorBrush Brush { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="ClassicLineViewModel"/> class.
        /// </summary>
        /// <param name="x1">X coordinate of first point.</param>
        /// <param name="y1">Y coordinate of first point.</param>
        /// <param name="x2">X coordinate of second point.</param>
        /// <param name="y2">Y coordinate of second point.</param>
        /// <param name="strokeThickness">Thickness of line.</param>
        /// <param name="brush">Color of line.</param>
        public ClassicLineViewModel(double x1, double y1, double x2, double y2, double strokeThickness, SolidColorBrush brush)
        {
            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;
            StrokeThickness = strokeThickness;
            Brush = brush;
        }

        /// <summary>
        /// Resize this element by <paramref name="ratio"/>.
        /// </summary>
        /// <param name="ratio">Value for resizeing.</param>
        public void Resize(double ratio)
        {
            X1 *= ratio;
            X2 *= ratio;
            Y1 *= ratio;
            Y2 *= ratio;
        }
    }
}
