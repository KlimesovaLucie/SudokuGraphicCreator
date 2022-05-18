using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.Collections.ObjectModel;
using System.Windows;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="Character"/> class.
    /// </summary>
    public class CharacterViewModel : SudokuElementViewModel
    {
        private readonly Character _model;

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

        private double _rotateLeft;

        public double RotateLeft
        {
            get => _rotateLeft;
            set
            {
                _rotateLeft = value;
                OnPropertyChanged(nameof(RotateLeft));
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

        private double _rotateTop;

        public double RotateTop
        {
            get => _rotateTop;
            set
            {
                _rotateTop = value;
                OnPropertyChanged(nameof(RotateTop));
                OnPropertyChanged(nameof(Margin));
            }
        }

        /// <summary>
        /// Value of character.
        /// </summary>
        public string Text
        {
            get => _model.Text;
            set
            {
                _model.Text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public Thickness Margin
        {
            get => new Thickness(RotateLeft, RotateTop, 0, 0);
            set
            {
                OnPropertyChanged(nameof(Margin));
            }
        }

        /// <summary>
        /// Type of sudoku graphic element.
        /// </summary>
        public SudokuElementType SudokuElemType => _model.SudokuElemType;

        private double _textSize;

        public double TextSize
        {
            get => _textSize;
            set
            {
                _textSize = value;
                OnPropertyChanged(nameof(TextSize));
            }
        }

        public double Angle { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="CharacterViewModel"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="rowIndex">Index of row in grid.</param>
        /// <param name="colIndex">Index cf col in grid.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="text"></param>
        /// <param name="graphicElem">Orientation of element.</param>
        /// <param name="location">Location on grid.</param>
        public CharacterViewModel(double width, double height, double left, double top, int rowIndex, int colIndex, SudokuElementType type,
            string text, GraphicElementType graphicElem, ElementLocationType location)
        {
            Width = width;
            Height = height;
            var newElem = new Character(left, top, rowIndex, colIndex, text, type, location);
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(newElem);
            _model = newElem;
            SetTextSize(type);
            SetAngle(graphicElem);
            AddVariant(type);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CharacterViewModel"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="left">Left distance from left up corner of grid.</param>
        /// <param name="top">Top distance from left up corner of grid.</param>
        /// <param name="rowIndex">Index of row in grid.</param>
        /// <param name="colIndex">Index cf col in grid.</param>
        /// <param name="type">Type of graphic element.</param>
        /// <param name="graphicElem">Orientation of element.</param>
        /// <param name="location">Location on grid.</param>
        public CharacterViewModel(double width, double height, double left, double top, int rowIndex, int colIndex, SudokuElementType type,
            string text, GraphicElementType graphicElem, double textSize, ElementLocationType location)
        {
            Width = width;
            Height = height;
            var newElem = new Character(left, top, rowIndex, colIndex, text, type, location);
            SudokuStore.Instance.Sudoku.SudokuVariants.Add(newElem);
            _model = newElem;
            TextSize = textSize;
            SetAngle(graphicElem);
            AddVariant(type);
        }

        /// <summary>
        /// Resize this element by <paramref name="ratio"/>.
        /// </summary>
        /// <param name="ratio">Value for resizeing.</param>
        public void Resize(double ratio)
        {
            Width *= ratio;
            Height *= ratio;
            RotateLeft *= ratio;
            RotateTop *= ratio;
            TextSize *= ratio;
        }

        private void AddVariant(SudokuElementType sudokuElement)
        {
            if (sudokuElement == SudokuElementType.XvV || sudokuElement == SudokuElementType.XvX)
            {
                AddSudokuVariant(SudokuType.XV);
            }
            else if (sudokuElement == SudokuElementType.GreaterThanDown || sudokuElement == SudokuElementType.GreaterThanLeft ||
                sudokuElement == SudokuElementType.GreaterThanRight || sudokuElement == SudokuElementType.GreaterThanUp)
            {
                AddSudokuVariant(SudokuType.GreaterThan);
            }
        }

        private void AddSudokuVariant(SudokuType sudokuType)
        {
            if (!SudokuStore.Instance.Sudoku.Variants.Contains(sudokuType))
            {
                SudokuStore.Instance.Sudoku.Variants.Add(sudokuType);
            }
        }

        private void SetTextSize(SudokuElementType type)
        {
            if (type == SudokuElementType.Killer)
            {
                TextSize = GridSizeStore.KillerNumberTextSize;
            }
            else
            {
                TextSize = GridSizeStore.OnEdgeTextSize;
            }
        }

        private void SetAngle(GraphicElementType graphicElem)
        {
            RotateLeft = Left;
            RotateTop = Top;
            if (graphicElem == GraphicElementType.None)
            {
                return;
            }

            if (graphicElem == GraphicElementType.Down)
            {
                Angle = 0;
            }
            else if (graphicElem == GraphicElementType.Left)
            {
                RotateLeft += Width;
                Angle = 90;
            }
            else if (graphicElem == GraphicElementType.Right)
            {
                RotateTop += Height;
                Angle = 270;
            }
            else if (graphicElem == GraphicElementType.Up)
            {
                RotateLeft += Width;
                RotateTop += Height;
                Angle = 180;
            }
        }

        private void RemoveVariant(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType sudokuType)
        {
            if (!IsDeletingLastElemVariant(collection, sudokuType))
            {
                if ((sudokuType == SudokuElementType.XvX && CountSameTypeElems(collection, SudokuElementType.XvV) != 0) ||
                    (sudokuType == SudokuElementType.XvV && CountSameTypeElems(collection, SudokuElementType.XvX) != 0) ||
                    IsGreaterThanMoreElems(collection, sudokuType))
                {
                    return;
                }
            }
            if (sudokuType == SudokuElementType.XvX || sudokuType == SudokuElementType.XvV)
            {
                SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.XV);
            }
            else if (sudokuType == SudokuElementType.GreaterThanDown || sudokuType == SudokuElementType.GreaterThanLeft ||
                sudokuType == SudokuElementType.GreaterThanRight || sudokuType == SudokuElementType.GreaterThanUp)
            {
                AddSudokuVariant(SudokuType.GreaterThan);
            }
        }

        private bool IsGreaterThanMoreElems(ObservableCollection<SudokuElementViewModel> collection, SudokuElementType sudokuType)
        {
            return (sudokuType == SudokuElementType.GreaterThanDown || sudokuType == SudokuElementType.GreaterThanUp ||
                    sudokuType == SudokuElementType.GreaterThanRight || sudokuType == SudokuElementType.GreaterThanLeft) &&
                    (CountSameTypeElems(collection, SudokuElementType.GreaterThanDown) != 0 ||
                    CountSameTypeElems(collection, SudokuElementType.GreaterThanUp) != 0 ||
                    CountSameTypeElems(collection, SudokuElementType.GreaterThanLeft) != 0 ||
                    CountSameTypeElems(collection, SudokuElementType.GreaterThanRight) != 0);
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
                var elem = item as CharacterViewModel;
                if (elem != null && elem.SudokuElemType == elemType)
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
                var elem = item as CharacterViewModel;
                if (elem != null)
                {
                    if (elem.Left == left && elem.Top == top && elem.SudokuElemType == elemType)
                    {
                        collection.Remove(item);
                        elem.Remove(collection, elemType);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
