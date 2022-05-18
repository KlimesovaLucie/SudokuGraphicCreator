namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents long arrow which starts with circle as graphic element.
    /// </summary>
    public class LongArrowWithCircle : SudokuElement
    {
        /// <summary>
        /// Long arrow of this element.
        /// </summary>
        public LongArrow Arrow { get; set; }

        /// <summary>
        /// Circle of this element.
        /// </summary>
        public CircleWithGreyEdge Circle { get; set; }

        /// <summary>
        /// Value in circle.
        /// </summary>
        public int? Value { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="LongArrowWithCircle"/> class.
        /// </summary>
        /// <param name="type">Type of graphic element.</param>
        public LongArrowWithCircle(SudokuElementType type)
        {
            SudokuElemType = type;
        }
    }
}
