using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="BlackCircle"/> class.
    /// </summary>
    public class BlackCircleViewModel : CircleViewModel
    {
        /// <summary>
        /// Type of sudoku graphic element.
        /// </summary>
        public SudokuElementType SudokuElemType => _model.SudokuElemType;

        /// <summary>
        /// Initializes a new instance of <see cref="BlackCircleViewModel"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="rowIndex">Index of row of cell in grid.</param>
        /// <param name="colIndex">Index of column of cell int grid.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="location">Location on grid.</param>
        public BlackCircleViewModel(double width, double height, double left, double top, int rowIndex, int colIndex, SudokuElementType type,
            ElementLocationType location) :
            base(width, height, left, top)
        {
            var newElem = new BlackCircle(left, top, rowIndex, colIndex, type, location);
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(newElem);
            _model = newElem;
            AddVariant(type);
        }

        private void AddVariant(SudokuElementType sudokuElement)
        {
            if (sudokuElement == SudokuElementType.BlackKropki)
            {
                AddSudokuVariant(SudokuType.Kropki);
            }
        }

        private void AddSudokuVariant(SudokuType sudokuType)
        {
            if (!SudokuStore.Instance.Sudoku.Variants.Contains(sudokuType))
            {
                SudokuStore.Instance.Sudoku.Variants.Add(sudokuType);
            }
        }

        private void RemoveVariant(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType sudokuType)
        {
            if (!IsDeletingLastElemVariant(collection, sudokuType))
            {
                if (sudokuType == SudokuElementType.BlackKropki && CountWhiteKropkiElems(collection) != 0)
                {
                    return;
                }
            }
            if (sudokuType == SudokuElementType.BlackKropki)
            {
                SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.Kropki);
            }
        }

        private bool IsDeletingLastElemVariant(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType elemType)
        {
            return CountSameTypeElems(collection, elemType) == 1;
        }

        private int CountSameTypeElems(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType elemType)
        {
            int count = 0;
            foreach (SudokuElementViewModel item in collection)
            {
                var elem = item as BlackCircleViewModel;
                if (elem != null && elem.SudokuElemType == elemType)
                {
                    count++;
                }
            }
            return count;
        }

        private int CountWhiteKropkiElems(ObservableCollection<SudokuElementViewModel> collection)
        {
            int count = 0;
            foreach (var item in collection)
            {
                var elem = item as WhiteCircleViewModel;
                if (elem != null && elem.SudokuElemType == SudokuElementType.BlackKropki)
                {
                    count++;
                }
            }
            return count;
        }

        private void Remove(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType elemType)
        {
            SudokuStore.Instance.Sudoku.SudokuVariants.Remove(_model);
            RemoveVariant(collection, elemType);
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
                var elem = item as BlackCircleViewModel;
                if (elem != null)
                {
                    if (elem.Left == left && elem.Top == top && elem.SudokuElemType == elemType)
                    {
                        elem.Remove(collection, elemType);
                        collection.Remove(item);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
