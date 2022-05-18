using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="CircleWithGreyEdge"/> class.
    /// </summary>
    public class CircleWithGreyEdgeViewModel : CircleViewModel
    {
        /// <summary>
        /// Type of sudoku graphic element.
        /// </summary>
        public SudokuElementType SudokuElemType => _model.SudokuElemType;

        /// <summary>
        /// Initializes a new instance of <see cref="CircleWithGreyEdgeViewModel"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="model">Instance of <see cref="CircleWithGreyEdge"/> model.</param>
        public CircleWithGreyEdgeViewModel(double width, double height, double left, double top, SudokuElementType type,
            CircleWithGreyEdge model) :
            base(width, height, left, top)
        {
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(model);
            _model = model;
            StrokeThickness = 4;
        }

        /// <summary>
        /// Remove from sudoku model graphic elements.
        /// </summary>
        public void Remove()
        {
            SudokuStore.Instance.Sudoku.SudokuVariants.Remove(_model);
        }

        /// <summary>
        /// Remove this element from elements in sudoku and if possible, change type of variant.
        /// </summary>
        /// <param name="collection">Collection of elements.</param>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="elemType">Type of graphic element.</param>
        /// <returns>true if element was removed, otherwise false.</returns>
        public static bool RemoveFromCollection(ObservableCollection<SudokuElementViewModel> collection,
            double left, double top, SudokuElementType elemType)
        {
            foreach (SudokuElementViewModel item in collection)
            {
                var elem = item as CircleWithGreyEdgeViewModel;
                if (elem != null)
                {
                    if (elem.Left == left && elem.Top == top && elem.SudokuElemType == elemType)
                    {
                        elem.Remove();
                        collection.Remove(item);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
