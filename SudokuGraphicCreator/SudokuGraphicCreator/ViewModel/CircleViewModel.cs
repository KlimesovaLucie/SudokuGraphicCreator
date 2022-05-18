using SudokuGraphicCreator.Model;
using System.Windows;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="Circle"/> class.
    /// </summary>
    public abstract class CircleViewModel : SudokuElementViewModel
    {
        protected Circle _model;

        private double _width;

        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        private double _height;

        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        /// <summary>
        /// Left distance from left up corner of grid.
        /// </summary>
        public double Left
        {
            get => _model.Left;
            set
            {
                _model.Left = value;
                OnPropertyChanged(nameof(Left));
                OnPropertyChanged(nameof(Margin));
            }
        }

        /// <summary>
        /// Top distance form left up corner of grid.
        /// </summary>
        public double Top
        {
            get => _model.Top;
            set
            {
                _model.Top = value;
                OnPropertyChanged(nameof(Top));
                OnPropertyChanged(nameof(Margin));
            }
        }

        public Thickness Margin
        {
            get => new Thickness(Left, Top, 0, 0);
            set
            {
                OnPropertyChanged(nameof(Margin));
            }
        }

        /// <summary>
        /// Color of element.
        /// </summary>
        public Brush Fill => _model.FillColor;

        /// <summary>
        /// Stroke color of element.
        /// </summary>
        public Brush Stroke => _model.BorderColor;

        public double StrokeThickness { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="CircleViewModel"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        public CircleViewModel(double width, double height, double left, double top)
        {
            Width = width;
            Height = height;
            Margin = new Thickness(left, top, 0, 0);
            StrokeThickness = 1;
        }

        /// <summary>
        /// Resize this element by <paramref name="ratio"/>.
        /// </summary>
        /// <param name="ratio">Value for resizeing.</param>
        public void Resize(double ratio)
        {
            Width *= ratio;
            Height *= ratio;
            Left *= ratio;
            Top *= ratio;
        }
    }
}
