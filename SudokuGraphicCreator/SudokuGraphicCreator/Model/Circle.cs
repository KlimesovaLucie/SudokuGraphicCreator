using System.Windows.Media;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents graphic element in shape of circle.
    /// </summary>
    public abstract class Circle : OneSpaceElement
    {
        /// <summary>
        /// Inside color of circle.
        /// </summary>
        public Brush FillColor { get; protected set; }

        /// <summary>
        /// Color of border.
        /// </summary>
        public Brush BorderColor { get; protected set; }
    }
}
