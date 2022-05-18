using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Properties.Resources;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This class confirm selected region in sudoku grid and add graphic elements corresponding by checked variant.
    /// </summary>
    public class ConfirmCommand : BaseCommand
    {
        private readonly CreatingSudokuViewModel _viewModel;

        private readonly Brush _selectedCellBrush = Brushes.LightBlue;

        private readonly string _nameOfButton;

        /// <summary>
        /// Initializes a new instance of <see cref="ConfirmCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingSudoku"/> view.</param>
        /// <param name="nameOfButton">Name of variant.</param>
        public ConfirmCommand(CreatingSudokuViewModel viewModel, string nameOfButton)
        {
            _viewModel = viewModel;
            _nameOfButton = nameOfButton;
            _viewModel.SelectedCells.CollectionChanged += SelectedCellsCollectionChanged;
        }

        private void SelectedCellsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        /// <summary>
        /// This command can be execute only if is selected one or two cells based on variant.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if is selected one or two cells based on variant, otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            string name = FindCheckedButton();
            if (name != _nameOfButton)
            {
                return false;
            }
            if (name == Resources.SudokuPalindrome || name == Resources.SudokuSequence || name == Resources.SudokuArrow ||
                name == Resources.SudokuThermometer || name == Resources.ElemArrowWithCircle || name == Resources.ElemLongArrow ||
                name == Resources.ElemLine)
            {
                return _viewModel.SelectedCells.Count > 1;
            }
            if (name == Resources.SudokuExtraRegions)
            {
                return _viewModel.SelectedCells.Count == SudokuStore.Instance.Sudoku.Grid.Size;
            }
            return _viewModel.SelectedCells.Count > 0;
        }

        /// <summary>
        /// Add graphic element into selected cells based on corresponding variant and unselect all selected cells.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            CheckSudokuButton();
            ClearSelectedCells();
        }

        private string FindCheckedButton()
        {
            foreach (var button in _viewModel.ButtonsConfirm)
            {
                if (button.Checked && button.NameOfElement == _nameOfButton)
                {
                    return _nameOfButton;
                }
            }
            return "";
        }

        private void ClearSelectedCells()
        {
            foreach (var elem in _viewModel.SelectedCells)
            {
                elem.Background = elem.DefaultBrush;
            }
            _viewModel.SelectedCells.Clear();
        }

        private void CheckSudokuButton()
        {
            if (VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.SudokuIrregular)
            {
                IrregularBoxConfirmed();
                return;
            }
            foreach (var elem in _viewModel.SudokuVariantElementButton)
            {
                if (elem.Checked)
                {
                    VariantGraphicElementStore.Instance.VariantGraphicElem = elem.ToString();
                    if (elem.ToString() == Resources.SudokuPalindrome)
                    {
                        CreateLine(LineViewModel.RemoveFromCollection, SudokuElementType.Palindromes);
                        return;
                    }

                    if (elem.ToString() == Resources.SudokuSequence)
                    {
                        CreateLine(LineViewModel.RemoveFromCollection, SudokuElementType.Sequences);
                        return;
                    }

                    if (elem.ToString() == Resources.SudokuArrow)
                    {
                        PlaceLongArrowWithCircle(SudokuElementType.Arrows);
                        return;
                    }

                    if (elem.ToString() == Resources.SudokuThermometer)
                    {
                        PlaceThermometer();
                        return;
                    }

                    if (elem.ToString() == Resources.SudokuExtraRegions)
                    {
                        PlaceExtraRegion();
                        return;
                    }

                    if (elem.ToString() == Resources.SudokuKiller)
                    {
                        PlaceCage(SudokuElementType.Killer);
                        return;
                    }
                }
            }

            foreach (var elem in _viewModel.SudokuGraphicElementButton)
            {
                if (elem.Checked)
                {
                    if (elem.ToString() == Resources.ElemLine)
                    {
                        CreateLine(LineViewModel.RemoveFromCollection, SudokuElementType.NoMeaning);
                        return;
                    }

                    if (elem.ToString() == Resources.ElemArrowWithCircle)
                    {
                        PlaceLongArrowWithCircle(SudokuElementType.NoMeaning);
                        return;
                    }

                    if (elem.ToString() == Resources.ElemLongArrow)
                    {
                        PlaceLongArrow(SudokuElementType.NoMeaning);
                        return;
                    }

                    if (elem.ToString() == Resources.ElemCage)
                    {
                        PlaceCage(SudokuElementType.NoMeaning);
                        return;
                    }
                }
            }
        }

        #region IRREGULAR
        private void IrregularBoxConfirmed()
        {
            foreach (var actual in _viewModel.SelectedCells)
            {
                foreach (var nextCell in _viewModel.GridCells)
                {
                    Thickness newBorder = actual.BorderThickness;
                    bool isNext = IsLabelLeft(actual, nextCell);

                    if (isNext && nextCell.Background == _selectedCellBrush)
                    {
                        newBorder.Left = GridSizeStore.NormalLine;
                    }
                    else if (isNext && nextCell.Background != _selectedCellBrush)
                    {
                        newBorder.Left = GridSizeStore.BoldLine;
                        Thickness thick = nextCell.BorderThickness;
                        thick.Right = GridSizeStore.BoldLine;
                        nextCell.BorderThickness = thick;
                    }

                    isNext = IsLabelTop(actual, nextCell);
                    if (isNext && nextCell.Background == _selectedCellBrush)
                    {
                        newBorder.Top = GridSizeStore.NormalLine;
                    }
                    else if (isNext && nextCell.Background != _selectedCellBrush)
                    {
                        newBorder.Top = GridSizeStore.BoldLine;
                        Thickness thick = nextCell.BorderThickness;
                        thick.Bottom = GridSizeStore.BoldLine;
                        nextCell.BorderThickness = thick;
                    }

                    isNext = IsLabelRight(actual, nextCell);
                    if (isNext && nextCell.Background == _selectedCellBrush)
                    {
                        newBorder.Right = GridSizeStore.NormalLine;
                    }
                    else if (isNext && nextCell.Background != _selectedCellBrush)
                    {
                        newBorder.Right = GridSizeStore.BoldLine;
                        Thickness thick = nextCell.BorderThickness;
                        thick.Left = GridSizeStore.BoldLine;
                        nextCell.BorderThickness = thick;
                    }

                    isNext = IsLabelBottom(actual, nextCell);
                    if (isNext && nextCell.Background == _selectedCellBrush)
                    {
                        newBorder.Bottom = GridSizeStore.NormalLine;
                    }
                    else if (isNext && nextCell.Background != _selectedCellBrush)
                    {
                        newBorder.Bottom = GridSizeStore.BoldLine;
                        Thickness thick = nextCell.BorderThickness;
                        thick.Top = GridSizeStore.BoldLine;
                        nextCell.BorderThickness = thick;
                    }

                    actual.BorderThickness = newBorder;
                }
            }
            TransformBoxes();
            if (SudokuStore.Instance.Sudoku.Grid.Boxes.Count == SudokuStore.Instance.Sudoku.Grid.DefaultBoxes.Count)
            {
                SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.Irregular);
            }
            else if (!SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Irregular))
            {
                SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Irregular);
            }
        }

        private void TransformBoxes()
        {
            var newBox = new ObservableCollection<Tuple<int, int>>();
            foreach (var cell in _viewModel.SelectedCells)
            {
                RemoveCellFromBoxes(cell);
                newBox.Add(new Tuple<int, int>(cell.RowIndex, cell.ColIndex));
            }
            _viewModel.Boxes.Add(newBox);
        }

        private void RemoveCellFromBoxes(GridCellViewModel cell)
        {
            foreach (var box in _viewModel.Boxes)
            {
                for (int i = 0; i < box.Count; i++)
                {
                    if (box[i].Item1 == cell.RowIndex && box[i].Item2 == cell.ColIndex)
                    {
                        box.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        private bool IsLabelLeft(GridCellViewModel label, GridCellViewModel elem)
        {
            return (elem.Margin.Left == label.Margin.Left - GridSizeStore.XCellSize) && (elem.Margin.Top == label.Margin.Top);
        }

        private bool IsLabelRight(GridCellViewModel label, GridCellViewModel elem)
        {
            return (elem.Margin.Left == label.Margin.Left + GridSizeStore.XCellSize) && (elem.Margin.Top == label.Margin.Top);
        }

        private bool IsLabelTop(GridCellViewModel label, GridCellViewModel elem)
        {
            return (elem.Margin.Top == label.Margin.Top - GridSizeStore.YCellSize) && (elem.Margin.Left == label.Margin.Left);
        }

        private bool IsLabelBottom(GridCellViewModel label, GridCellViewModel elem)
        {
            return (elem.Margin.Top == label.Margin.Top + GridSizeStore.YCellSize) && (elem.Margin.Left == label.Margin.Left);
        }
        #endregion

        #region LINE
        private void CreateLine(Func<ObservableCollection<SudokuElementViewModel>, SudokuElementType,
            ObservableCollection<Tuple<int, int>>, bool> remove,
            SudokuElementType elemType)
        {
            ObservableCollection<Tuple<int, int>> cellIndexes = new ObservableCollection<Tuple<int, int>>();
            foreach (var cell in _viewModel.SelectedCells)
            {
                cellIndexes.Add(new Tuple<int, int>(cell.RowIndex, cell.ColIndex));
            }

            if (!remove(_viewModel.GraphicElements, elemType, cellIndexes))
            {
                _viewModel.GraphicElements.Add(new LineViewModel(elemType, cellIndexes));
            }
        }
        #endregion

        private void PlaceThermometer()
        {
            CreateLine(LineViewModel.RemoveFromCollection, SudokuElementType.Thermometers);
            PlaceCircle(SudokuElementType.Thermometers);
        }

        private void PlaceCircle(SudokuElementType elemType)
        {
            double left;
            double top;
            CalculateLeftTopInCell(_viewModel.SelectedCells[0], out left, out top);
            if (!GreyCircleViewModel.RemoveFromCollection(_viewModel.GraphicElements, left, top, elemType))
            {
                _viewModel.GraphicElements.Add(new GreyCircleViewModel(GridSizeStore.InCellElementSize, GridSizeStore.InCellElementSize,
                    left, top, (int)(top / GridSizeStore.YCellSize), (int)(left / GridSizeStore.XCellSize), elemType, ElementLocationType.Cell));
            }
        }

        private void CalculateLeftTopInCell(GridCellViewModel cell, out double left, out double top)
        {
            left = cell.Left + ((GridSizeStore.XCellSize - GridSizeStore.InCellElementSize) / 2);
            top = cell.Top + ((GridSizeStore.YCellSize - GridSizeStore.InCellElementSize) / 2);
        }

        private void PlaceCage(SudokuElementType type)
        {
            ObservableCollection<Tuple<double, double>> cellIndexes = new ObservableCollection<Tuple<double, double>>();
            foreach (var cell in _viewModel.SelectedCells)
            {
                cellIndexes.Add(new Tuple<double, double>(cell.Left, cell.Top));
            }
            CageViewModel newCage = new CageViewModel(type, cellIndexes);
            if (!CageViewModel.RemoveFromCollection(_viewModel.GraphicElements, newCage.Points, type))
            {
                _viewModel.GraphicElements.Add(newCage);
            }
            else
            {
                newCage.Remove(_viewModel.GraphicElements, type);
            }
        }

        private void PlaceExtraRegion()
        {
            if (_viewModel.SelectedCells.Count != SudokuStore.Instance.Sudoku.Grid.Size)
            {
                return;
            }

            if (!IsCreatingNewRegion())
            {
                foreach (var cell in _viewModel.SelectedCells)
                {
                    cell.Background = Brushes.Transparent;
                    cell.DefaultBrush = Brushes.Transparent;
                }
                if (SudokuStore.Instance.Sudoku.Grid.Boxes.Count == SudokuStore.Instance.Sudoku.Grid.Size)
                {
                    SudokuStore.Instance.Sudoku.Variants.Remove(SudokuType.Extraregion);
                }
                return;
            }

            foreach (var cell in _viewModel.SelectedCells)
            {
                cell.Background = Brushes.DarkGray;
                cell.DefaultBrush = Brushes.DarkGray;
                if (!SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Extraregion))
                {
                    SudokuStore.Instance.Sudoku.Variants.Add(SudokuType.Extraregion);
                }
            }

            CreateNewRegion();
        }

        private void CreateNewRegion()
        {
            ObservableCollection<Tuple<int, int>> newBox = new ObservableCollection<Tuple<int, int>>();

            foreach (var cell in _viewModel.SelectedCells)
            {
                newBox.Add(new Tuple<int, int>(cell.RowIndex, cell.ColIndex));
            }

            SudokuStore.Instance.Sudoku.Grid.ExtraRegions.Add(newBox);
        }

        private bool IsCreatingNewRegion()
        {
            foreach (var box in SudokuStore.Instance.Sudoku.Grid.ExtraRegions)
            {
                int sameCells = 0;
                foreach (var cell in _viewModel.SelectedCells)
                {
                    if (ContainsCell(box, cell))
                    {
                        sameCells++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (sameCells == SudokuStore.Instance.Sudoku.Grid.Size)
                {
                    SudokuStore.Instance.Sudoku.Grid.ExtraRegions.Remove(box);
                    return false;
                }
            }
            return true;
        }

        private bool ContainsCell(ObservableCollection<Tuple<int, int>> box, GridCellViewModel cell)
        {
            foreach (var cellInBox in box)
            {
                if (cellInBox.Item1 == cell.RowIndex && cellInBox.Item2 == cell.ColIndex)
                {
                    return true;
                }
            }
            return false;
        }

        private void PlaceLongArrow(SudokuElementType type)
        {
            ObservableCollection<Tuple<int, int>> cellIndexes = new ObservableCollection<Tuple<int, int>>();
            foreach (var cell in _viewModel.SelectedCells)
            {
                cellIndexes.Add(new Tuple<int, int>(cell.RowIndex, cell.ColIndex));
            }
            if (!LongArrowViewModel.RemoveFromCollection(_viewModel.GraphicElements, type, cellIndexes))
            {
                _viewModel.GraphicElements.Add(new LongArrowViewModel(type, cellIndexes, new LongArrow(type, cellIndexes)));
            }
        }

        private void PlaceLongArrowWithCircle(SudokuElementType type)
        {
            ObservableCollection<Tuple<int, int>> cellIndexes = new ObservableCollection<Tuple<int, int>>();
            foreach (var cell in _viewModel.SelectedCells)
            {
                cellIndexes.Add(new Tuple<int, int>(cell.RowIndex, cell.ColIndex));
            }

            double left = cellIndexes[0].Item2 * GridSizeStore.XCellSize +
                (GridSizeStore.XCellSize - GridSizeStore.InCellElementSize) / 2;
            double top = cellIndexes[0].Item1 * GridSizeStore.YCellSize +
                (GridSizeStore.YCellSize - GridSizeStore.InCellElementSize) / 2;

            if (!LongArrowWithCircleViewModel.RemoveFromCollection(_viewModel.GraphicElements, left, top,
                type, cellIndexes))
            {
                _viewModel.GraphicElements.Add(new LongArrowWithCircleViewModel(type, cellIndexes));
            }
            else
            {
                _viewModel.CellNumbers[cellIndexes[0].Item2 * SudokuStore.Instance.Sudoku.Grid.Size + cellIndexes[0].Item1].Number = 0;
            }
        }
    }
}
