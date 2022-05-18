using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.Collections.ObjectModel;
using System.Windows;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="Number"/> class.
    /// </summary>
    public class NumberViewModel : SudokuElementViewModel
    {
        private readonly Number _model;

        /// <summary>
        /// Width of number.
        /// </summary>
        public double Width { get; }

        /// <summary>
        /// Heigth of number.
        /// </summary>
        public double Height { get; }

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
            }
        }

        /// <summary>
        /// Value of number.
        /// </summary>
        public int? Value
        {
            get => _model.Value;
            set
            {
                _model.Value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        /// <summary>
        /// Margin in UI.
        /// </summary>
        public Thickness Margin => new Thickness(Left, Top, 0, 0);

        /// <summary>
        /// Type of sudoku graphic element.
        /// </summary>
        public SudokuElementType SudokuElemType => _model.SudokuElemType;

        /// <summary>
        /// Size of text.
        /// </summary>
        public double TextSize => 36;

        /// <summary>
        /// Initializes a new instance of <see cref="NumberViewModel"/> class.
        /// </summary>
        /// <param name="width">Width of number.</param>
        /// <param name="height">Height of number.</param>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="value">Value of number.</param>
        /// <param name="type">Type of graphic element.</param>
        public NumberViewModel(double width, double height, double left, double top, int? value,
            SudokuElementType type)
        {
            Width = width;
            Height = height;
            var newElem = new Number(left, top, value, type);
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(newElem);
            _model = newElem;
        }

        private void Remove()
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
                var elem = item as NumberViewModel;
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
