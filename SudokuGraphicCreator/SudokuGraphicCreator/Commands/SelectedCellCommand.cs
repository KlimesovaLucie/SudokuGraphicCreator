using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Properties.Resources;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command is responsible for selecting or placing elements based on actual variant.
    /// </summary>
    public class SelectedCellCommand : BaseCommand
    {
        private readonly ICreatingSudokuViewModel _viewModel;

        private readonly Brush _selectedCellColor = Brushes.LightBlue;

        private List<ClassicLineViewModel> _lines;

        /// <summary>
        /// Initializes a new instance of <see cref="SelectedCellCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel for <see cref="CreatingSudoku"/> view.</param>
        public SelectedCellCommand(ICreatingSudokuViewModel viewModel)
        {
            _viewModel = viewModel;
            _lines = new List<ClassicLineViewModel>();
        }

        /// <summary>
        /// Select cell in grid or placed element based on checked variant.
        /// </summary>
        /// <param name="parameter">Tuple of <see cref="Point"/> and clicked <see cref="Label"/>.</param>
        public override void Execute(object parameter)
        {
            Tuple<object, Point> param = (Tuple<object, Point>)parameter;
            Point point = param.Item2;
            Label label = (Label)param.Item1;
            if (label == null)
            {
                return;
            }

            var gridCell = VariantGraphicElementStore.Instance.GridCellPartOfGrid;
            var graphic = VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid;
            var number = VariantGraphicElementStore.Instance.NumberCellPartOfGrid;
            var location = VariantGraphicElementStore.Instance.LocationType;

            int row;
            int col;
            GridCellViewModel cell;
            FindGridCollection(point, label, out row, out col, out cell);
            if (VariantGraphicElementStore.Instance.IsSelectedBasicGrid)
            {
                bool changed = true;
                if (CanSellectCell(cell, ref changed))
                {
                    if (CanSelectMoreCells())
                    {
                        if (!IsAdjacentCell(cell))
                        {
                            return;
                        }
                    }
                    else
                    {
                        SetAllCellsTransparent();
                    }
                    ChangeColor(cell);
                }
                InsertingSudokuElement(cell, point, row, col);
                if (changed)
                {
                    VariantGraphicElementStore.Instance.GridCellPartOfGrid = gridCell;
                    VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid = graphic;
                    VariantGraphicElementStore.Instance.NumberCellPartOfGrid = number;
                    VariantGraphicElementStore.Instance.LocationType = location;
                }
            }
            else
            {
                SetAllCellsTransparent();
                if (CanSelectCellOutside(point, cell))
                {
                    ChangeColor(cell);
                }
            }
        }

        private void FindGridCollection(Point point, Label label, out int row, out int col, out GridCellViewModel cell)
        {
            double left = label.Margin.Left;
            double top = label.Margin.Top;

            col = (int)(Math.Round(left / GridSizeStore.XCellSize));
            row = (int)(Math.Round(top / GridSizeStore.YCellSize));
            VariantGraphicElementStore.Instance.IsSelectedBasicGrid = false;
            if (point.X < 0)
            {
                cell = FindCellInLeftGrid(row, ref col);
                VariantGraphicElementStore.Instance.GridCellPartOfGrid = _viewModel.LeftGridCells;
                VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid = _viewModel.LeftGraphicCells;
                VariantGraphicElementStore.Instance.NumberCellPartOfGrid = _viewModel.LeftNumberCells;
                VariantGraphicElementStore.Instance.LocationType = ElementLocationType.GridLeft;
            }
            else if (point.Y < 0)
            {
                cell = FindCellInUpGrid(ref row, col);
                VariantGraphicElementStore.Instance.GridCellPartOfGrid = _viewModel.UpGridCells;
                VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid = _viewModel.UpGraphicCells;
                VariantGraphicElementStore.Instance.NumberCellPartOfGrid = _viewModel.UpNumberCells;
                VariantGraphicElementStore.Instance.LocationType = ElementLocationType.GridUp;
            }
            else if (point.X > SudokuStore.Instance.Sudoku.Grid.Size * GridSizeStore.XCellSize)
            {
                cell = FindCellInRightGrid(row, col);
                VariantGraphicElementStore.Instance.GridCellPartOfGrid = _viewModel.RightGridCells;
                VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid = _viewModel.RightGraphicCells;
                VariantGraphicElementStore.Instance.NumberCellPartOfGrid = _viewModel.RightNumberCells;
                VariantGraphicElementStore.Instance.LocationType = ElementLocationType.GridRight;
            }
            else if (point.Y > SudokuStore.Instance.Sudoku.Grid.Size * GridSizeStore.YCellSize)
            {
                cell = FindCellInBottomGrid(row, col);
                VariantGraphicElementStore.Instance.GridCellPartOfGrid = _viewModel.BottomGridCells;
                VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid = _viewModel.BottomGraphicCells;
                VariantGraphicElementStore.Instance.NumberCellPartOfGrid = _viewModel.BottomNumberCells;
                VariantGraphicElementStore.Instance.LocationType = ElementLocationType.GridDown;
            }
            else
            {
                cell = FindCellInGrid(row, col);
                VariantGraphicElementStore.Instance.GridCellPartOfGrid = _viewModel.GridCells;
                VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid = _viewModel.GraphicElements;
                VariantGraphicElementStore.Instance.NumberCellPartOfGrid = _viewModel.CellNumbers;
                VariantGraphicElementStore.Instance.IsSelectedBasicGrid = true;
                VariantGraphicElementStore.Instance.LocationType = ElementLocationType.Grid;
            }
        }

        private GridCellViewModel FindCellInLeftGrid(int row, ref int col)
        {
            col = 2 - col;
            return _viewModel.LeftGridCells[row * 3 + col];
        }

        private GridCellViewModel FindCellInUpGrid(ref int row, int col)
        {
            row = 2 - row;
            return _viewModel.UpGridCells[row * SudokuStore.Instance.Sudoku.Grid.Size + col];
        }

        private GridCellViewModel FindCellInRightGrid(int row, int col)
        {
            return _viewModel.RightGridCells[row * 3 + col];
        }

        private GridCellViewModel FindCellInBottomGrid(int row, int col)
        {
            return _viewModel.BottomGridCells[row * SudokuStore.Instance.Sudoku.Grid.Size + col];
        }

        private GridCellViewModel FindCellInGrid(int row, int col)
        {
            return _viewModel.GridCells[row * SudokuStore.Instance.Sudoku.Grid.Size + col];
        }

        private void InsertingSudokuElement(GridCellViewModel cell, Point point, int row, int col)
        {
            if (VariantsButtons(cell, point))
            {
                return;
            }
            GraphicElementsButtons(cell, point, row, col);
        }

        private bool VariantsButtons(GridCellViewModel cell, Point point)
        {
            foreach (var button in _viewModel.SudokuVariantElementButton)
            {
                var buttonOptions = button as ElementButtonGraphicOptions;
                if (buttonOptions != null)
                {
                    foreach (var option in buttonOptions.ElementButtonOptions)
                    {
                        if (option.Checked)
                        {
                            if (option.ToString() == Resources.SudokuKropki + Resources.ElemBlackCircle)
                            {
                                PlaceElementOnEdge(cell, SudokuElementType.BlackKropki, point, GridSizeStore.SmallCircleSize,
                                    BlackCircleViewModel.RemoveFromCollection, CreateBlackCircle);
                                return true;
                            }

                            if (option.ToString() == Resources.SudokuXV + Resources.MenuV)
                            {
                                PlaceTextOnEdge(cell, SudokuElementType.XvV, point, GridSizeStore.OnEdgeTextElement,
                                    GraphicElementType.Down, CharacterViewModel.RemoveFromCollection, "V", CreateCharacter);
                                return true;
                            }

                            if (option.ToString() == Resources.SudokuGreaterThan + Resources.MenuRight)
                            {
                                PlaceTextOnEdge(cell, SudokuElementType.GreaterThanRight, point, GridSizeStore.OnEdgeTextElement,
                                    GraphicElementType.Right, CharacterViewModel.RemoveFromCollection, "V", CreateCharacter);
                                return true;
                            }

                            if (option.ToString() == Resources.SudokuGreaterThan + Resources.MenuUp)
                            {
                                PlaceTextOnEdge(cell, SudokuElementType.GreaterThanUp, point, GridSizeStore.OnEdgeTextElement,
                                    GraphicElementType.Up, CharacterViewModel.RemoveFromCollection, "V", CreateCharacter);
                                return true;
                            }

                            if (option.ToString() == Resources.SudokuGreaterThan + Resources.MenuDown)
                            {
                                PlaceTextOnEdge(cell, SudokuElementType.GreaterThanDown, point, GridSizeStore.OnEdgeTextElement,
                                    GraphicElementType.Down, CharacterViewModel.RemoveFromCollection, "V", CreateCharacter);
                                return true;
                            }

                            if (option.ToString() == Resources.SudokuSearchNine + Resources.MenuRight)
                            {
                                PlaceBoldArrow(cell, SudokuElementType.SearchNineRight, GraphicElementType.Right);
                                return true;
                            }

                            if (option.ToString() == Resources.SudokuSearchNine + Resources.MenuDown)
                            {
                                PlaceBoldArrow(cell, SudokuElementType.SearchNineDown, GraphicElementType.Down);
                                return true;
                            }

                            if (option.ToString() == Resources.SudokuSearchNine + Resources.MenuUp)
                            {
                                PlaceBoldArrow(cell, SudokuElementType.SearchNineUp, GraphicElementType.Up);
                                return true;
                            }
                        }
                    }
                }
                
                if (button.Checked)
                {
                    if (button.ToString() == Resources.SudokuSum)
                    {
                        PlaceElementOnEdge(cell, SudokuElementType.Sum, point, GridSizeStore.CircleSize,
                            CircleWithNumberViewModel.RemoveFromCollection, CreateCircleWithNumber);
                        return true;
                    }

                    if (button.ToString() == Resources.SudokuDifference)
                    {
                        PlaceElementOnEdge(cell, SudokuElementType.Difference, point, GridSizeStore.CircleSize,
                            CircleWithNumberViewModel.RemoveFromCollection, CreateCircleWithNumber);
                        return true;
                    }
                    
                    if (button.ToString() == Resources.SudokuConsecutive)
                    {
                        PlaceElementOnEdge(cell, SudokuElementType.Consecutive, point, GridSizeStore.SmallCircleSize,
                            WhiteCircleViewModel.RemoveFromCollection, CreateWhiteCircle);
                        return true;
                    }
                    
                    if (button.ToString() == Resources.SudokuKropki + Resources.ElemWhiteCircle)
                    {
                        PlaceElementOnEdge(cell, SudokuElementType.WhiteKropki, point, GridSizeStore.SmallCircleSize,
                            WhiteCircleViewModel.RemoveFromCollection, CreateWhiteCircle);
                        return true;
                    }

                    if (button.ToString() == Resources.SudokuGreaterThan + Resources.MenuLeft)
                    {
                        PlaceTextOnEdge(cell, SudokuElementType.GreaterThanLeft, point, GridSizeStore.OnEdgeTextElement,
                            GraphicElementType.Left, CharacterViewModel.RemoveFromCollection, "V", CreateCharacter);
                    }

                    if (button.ToString() == Resources.SudokuXV + Resources.MenuX)
                    {
                        PlaceTextOnEdge(cell, SudokuElementType.XvX, point, GridSizeStore.OnEdgeTextElement,
                            GraphicElementType.None, CharacterViewModel.RemoveFromCollection, "X", CreateCharacter);
                    }

                    if (button.ToString() == Resources.SudokuOdd)
                    {
                        PlaceElementInCell(cell, SudokuElementType.Odd, GreyCircleViewModel.RemoveFromCollection,
                            CreateGreyCircle);
                    }

                    if (button.ToString() == Resources.SudokuEven)
                    {
                        PlaceElementInCell(cell, SudokuElementType.Even, GreySquareViewModel.RemoveFromCollection,
                            CreateGreySquare);
                        return true;
                    }

                    if (button.ToString() == Resources.SudokuStarProducts)
                    {
                        PlaceElementWithPoints(cell, SudokuElementType.StarProduct, StarViewModel.RemoveFromCollection,
                            CreateStar);
                        return true;
                    }

                    if (button.ToString() == Resources.SudokuSearchNine + Resources.MenuLeft)
                    {
                        PlaceBoldArrow(cell, SudokuElementType.SearchNineLeft, GraphicElementType.Left);
                        return true;
                    }
                }
            }
            return false;
        }

        private void GraphicElementsButtons(GridCellViewModel cell, Point point, int row, int col)
        {
            foreach (var button in _viewModel.SudokuGraphicElementButton)
            {
                var buttonOptions = button as ElementButtonGraphicOptions;
                if (buttonOptions != null)
                {
                    foreach (var option in buttonOptions.ElementButtonOptions)
                    {
                        if (option.Checked)
                        {
                            if (option.ToString() == Resources.ElemBoldArrow + Resources.MenuRight)
                            {
                                PlaceBoldArrow(cell, SudokuElementType.NoMeaning, GraphicElementType.Right);
                                return;
                            }

                            if (option.ToString() == Resources.ElemBoldArrow + Resources.MenuUp)
                            {
                                PlaceBoldArrow(cell, SudokuElementType.NoMeaning, GraphicElementType.Up);
                                return;
                            }

                            if (option.ToString() == Resources.ElemBoldArrow + Resources.MenuDown)
                            {
                                PlaceBoldArrow(cell, SudokuElementType.NoMeaning, GraphicElementType.Down);
                                return;
                            }

                            if (option.ToString() == Resources.ElemSmallArrow + Resources.MenuLeftUp)
                            {
                                PlaceSmallArrow(cell, SudokuElementType.NoMeaning, GraphicElementType.LeftUp, point,
                                    GridSizeStore.XCellSize * SudokuStore.Instance.Sudoku.Grid.Size);
                                return;
                            }

                            if (option.ToString() == Resources.ElemSmallArrow + Resources.MenuRightUp)
                            {
                                PlaceSmallArrow(cell, SudokuElementType.NoMeaning, GraphicElementType.RightUp, point,
                                    GridSizeStore.XCellSize * SudokuStore.Instance.Sudoku.Grid.Size);
                                return;
                            }

                            if (option.ToString() == Resources.ElemSmallArrow + Resources.MenuRightDown)
                            {
                                PlaceSmallArrow(cell, SudokuElementType.NoMeaning, GraphicElementType.RightDown, point,
                                    GridSizeStore.XCellSize * SudokuStore.Instance.Sudoku.Grid.Size);
                                return;
                            }
                        }
                    }
                }

                if (button.Checked)
                {
                    if (button.ToString() == Resources.ElemWhiteCircle)
                    {
                        PlaceElementOnEdge(cell, SudokuElementType.NoMeaning, point, GridSizeStore.SmallCircleSize,
                            WhiteCircleViewModel.RemoveFromCollection, CreateWhiteCircle);
                        return;
                    }

                    if (button.ToString() == Resources.ElemBlackCircle)
                    {
                        PlaceElementOnEdge(cell, SudokuElementType.NoMeaning, point, GridSizeStore.SmallCircleSize,
                            BlackCircleViewModel.RemoveFromCollection, CreateBlackCircle);
                        return;
                    }

                    if (button.ToString() == Resources.ElemGreyCircle)
                    {
                        PlaceElementInCell(cell, SudokuElementType.NoMeaning,
                            GreyCircleViewModel.RemoveFromCollection, CreateGreyCircle);
                        return;
                    }

                    if (button.ToString() == Resources.ElemCircleWithGreyEdge)
                    {
                        PlaceElementInCell(cell, SudokuElementType.NoMeaning,
                            CircleWithGreyEdgeViewModel.RemoveFromCollection, CreateCircleWithEdge);
                    }

                    if (button.ToString() == Resources.ElemCircleWithNumber)
                    {
                        PlaceElementOnEdge(cell, SudokuElementType.NoMeaning, point, GridSizeStore.CircleSize,
                            CircleWithNumberViewModel.RemoveFromCollection, CreateCircleWithNumber);
                        return;
                    }

                    if (button.ToString() == Resources.ElemSquare)
                    {
                        PlaceElementInCell(cell, SudokuElementType.NoMeaning,
                            GreySquareViewModel.RemoveFromCollection, CreateGreySquare);
                        return;
                    }

                    if (button.ToString() == Resources.ElemStar)
                    {
                        PlaceElementWithPoints(cell, SudokuElementType.NoMeaning, StarViewModel.RemoveFromCollection,
                            CreateStar);
                        return;
                    }

                    if (button.ToString() == Resources.ElemBoldArrow + Resources.MenuLeft)
                    {
                        PlaceBoldArrow(cell, SudokuElementType.NoMeaning, GraphicElementType.Left);
                        return;
                    }

                    if (button.ToString() == Resources.ElemGreyCell)
                    {
                        var actualCell = _viewModel.GridCells[row * SudokuStore.Instance.Sudoku.Grid.Size + col];
                        if (actualCell.DefaultBrush == Brushes.Transparent)
                        {
                            actualCell.Background = Brushes.DarkGray;
                            actualCell.DefaultBrush = Brushes.DarkGray;
                            _viewModel.GraphicElements.Add(new GreyCellViewModel(actualCell.Left, actualCell.Top, SudokuElementType.NoMeaning));
                        }
                        else
                        {
                            actualCell.Background = Brushes.Transparent;
                            actualCell.DefaultBrush = Brushes.Transparent;
                            GreyCellViewModel finalCell = null;
                            foreach (var graphicElem in _viewModel.GraphicElements)
                            {
                                GreyCellViewModel greyCell = graphicElem as GreyCellViewModel;
                                if (greyCell != null && greyCell.Left == actualCell.Left && greyCell.Top == actualCell.Top)
                                {
                                    finalCell = greyCell;
                                }
                            }
                            if (finalCell != null)
                            {
                                _viewModel.GraphicElements.Remove(finalCell);
                            }
                        }
                        return;
                    }

                    if (button.ToString() == Resources.ElemSmallArrow + Resources.MenuLeftDown)
                    {
                        PlaceSmallArrow(cell, SudokuElementType.NoMeaning, GraphicElementType.LeftDown, point,
                            GridSizeStore.XCellSize * SudokuStore.Instance.Sudoku.Grid.Size);
                        return;
                    }
                }
            }
        }

        private void PlaceElementOnEdge(GridCellViewModel cell, SudokuElementType elemType, Point point, double elementSize,
            Func<ObservableCollection<SudokuElementViewModel>, double, double, SudokuElementType, bool> remove,
            Func<double, double, int, int, SudokuElementType, ElementLocationType, SudokuElementViewModel> createNewElement)
        {
            double left;
            double top;
            int rowIndex;
            int colIndex;
            ElementLocationType locationType;
            ElementOnEdge(cell, point, elementSize, out left, out top, out rowIndex, out colIndex, out locationType);
            if (remove(_viewModel.GraphicElements, left, top, elemType))
            {
                return;
            }
            bool isOnEdge = IsPlacingOnGridEdge(left, top, elementSize / 2);
            if ((isOnEdge && elemType == SudokuElementType.NoMeaning) || !isOnEdge)
            {
                _viewModel.GraphicElements.Add(createNewElement(left, top, rowIndex, colIndex, elemType, locationType));
            }
        }

        private CircleWithNumberViewModel CreateCircleWithNumber(double left, double top, int rowIndex, int colIndex, SudokuElementType elemType,
            ElementLocationType locationType)
        {
            return new CircleWithNumberViewModel(GridSizeStore.CircleSize, GridSizeStore.CircleSize, left, top, rowIndex, colIndex, elemType, locationType);
        }

        private WhiteCircleViewModel CreateWhiteCircle(double left, double top, int rowIndex, int colIndex, SudokuElementType elemType,
            ElementLocationType locationType)
        {
            return new WhiteCircleViewModel(GridSizeStore.SmallCircleSize, GridSizeStore.SmallCircleSize, left, top, rowIndex, colIndex, elemType, locationType);
        }

        private BlackCircleViewModel CreateBlackCircle(double left, double top, int rowIndex, int colIndex, SudokuElementType elemType,
            ElementLocationType locationType)
        {
            BlackCircleViewModel circle = new BlackCircleViewModel(GridSizeStore.SmallCircleSize, GridSizeStore.SmallCircleSize, left, top, rowIndex, colIndex, elemType, locationType);
            return circle;
        }

        private void PlaceElementInCell(GridCellViewModel cell, SudokuElementType elemType,
            Func<ObservableCollection<SudokuElementViewModel>, double, double, SudokuElementType, bool> remove,
            Func<double, double, int, int, SudokuElementType, ElementLocationType, SudokuElementViewModel> createNewElement)
        {
            double left;
            double top;
            CalculateLeftTopInCell(cell, out left, out top);
            if (remove(_viewModel.GraphicElements, left, top, elemType))
            {
                return;
            }
            _viewModel.GraphicElements.Add(createNewElement(left, top, cell.RowIndex, cell.ColIndex, elemType, ElementLocationType.Cell));
        }

        private GreyCircleViewModel CreateGreyCircle(double left, double top, int rowIndex, int colIndex, SudokuElementType elemType, ElementLocationType location)
        {
            return new GreyCircleViewModel(GridSizeStore.InCellElementSize, GridSizeStore.InCellElementSize, left, top, rowIndex, colIndex, elemType, location);
        }

        private CircleWithGreyEdgeViewModel CreateCircleWithEdge(double left, double top, int rowIndex, int colIndex, SudokuElementType elemType, ElementLocationType location)
        {
            return new CircleWithGreyEdgeViewModel(GridSizeStore.InCellElementSize, GridSizeStore.InCellElementSize, left, top, elemType,
                new CircleWithGreyEdge(left, top, rowIndex, colIndex, elemType));
        }

        private GreySquareViewModel CreateGreySquare(double left, double top, int rowIndex, int colIndex, SudokuElementType elemType, ElementLocationType location)
        {
            return new GreySquareViewModel(GridSizeStore.InCellElementSize, GridSizeStore.InCellElementSize, left, top, rowIndex, colIndex, elemType, location);
        }

        private void PlaceElementWithPoints(GridCellViewModel cell, SudokuElementType elemType,
            Func<ObservableCollection<SudokuElementViewModel>, double, double, SudokuElementType, bool> remove,
            Func<double, double, int, int, SudokuElementType, ElementLocationType, SudokuElementViewModel> createNewElement)
        {
            double left = cell.Left;
            double top = cell.Top;
            if (remove(_viewModel.GraphicElements, left, top, elemType))
            {
                return;
            }
            _viewModel.GraphicElements.Add(createNewElement(left, top, cell.RowIndex, cell.ColIndex, elemType, ElementLocationType.Cell));
        }

        private void PlaceBoldArrow(GridCellViewModel cell, SudokuElementType elemType, GraphicElementType graphicType)
        {
            double left = cell.Left;
            double top = cell.Top;
            if (BoldArrowViewModel.RemoveFromCollection(_viewModel.GraphicElements, left, top, elemType))
            {
                return;
            }
            _viewModel.GraphicElements.Add(CreateBoldArrow(left, top, elemType, graphicType));
        }

        private BoldArrowViewModel CreateBoldArrow(double left, double top, SudokuElementType elemType,
            GraphicElementType graphicType)
        {
            return new BoldArrowViewModel(left, top, elemType, graphicType);
        }

        private StarViewModel CreateStar(double left, double top, int rowIndex, int colIndex, SudokuElementType elemType, ElementLocationType location)
        {
            return new StarViewModel(GridSizeStore.InCellElementSize,
                GridSizeStore.InCellElementSize, left, top, rowIndex, colIndex, elemType, location);
        }

        private void PlaceTextOnEdge(GridCellViewModel cell, SudokuElementType elemType, Point point, double elementSize,
            GraphicElementType graphicType,
            Func<ObservableCollection<SudokuElementViewModel>, double, double, SudokuElementType, bool> remove,
            string text, Func<double, double, int, int, SudokuElementType, string, GraphicElementType, ElementLocationType, SudokuElementViewModel> createNewElement)
        {
            double left;
            double top;
            int rowIndex;
            int colIndex;
            ElementLocationType locationType;
            ElementOnEdge(cell, point, elementSize, out left, out top, out rowIndex, out colIndex, out locationType);
            if (remove(_viewModel.GraphicElements, left, top, elemType))
            {
                return;
            }
            if (!IsCorrectGreaterThanSymbol(locationType, elemType))
            {
                return;
            }
            bool isOnEdge = IsPlacingOnGridEdge(left, top, elementSize / 2);
            if ((isOnEdge && elemType == SudokuElementType.NoMeaning) || !isOnEdge)
            {
                _viewModel.GraphicElements.Add(createNewElement(left, top, rowIndex, colIndex, elemType, text, graphicType, locationType));
            }
        }

        private bool IsCorrectGreaterThanSymbol(ElementLocationType locationType, SudokuElementType elemType)
        {
            if (((elemType == SudokuElementType.GreaterThanDown || elemType == SudokuElementType.GreaterThanUp) &&
                locationType == ElementLocationType.Row) || locationType == ElementLocationType.Column &&
                (elemType == SudokuElementType.GreaterThanLeft || elemType == SudokuElementType.GreaterThanRight))
            {
                return false;
            }
            return true;
        }

        private CharacterViewModel CreateCharacter(double left, double top, int rowIndex, int colIndex, SudokuElementType elemType, string value,
            GraphicElementType graphicElem, ElementLocationType location)
        {
            return new CharacterViewModel(GridSizeStore.OnEdgeTextElement, GridSizeStore.OnEdgeTextElement, left, top, rowIndex, colIndex,
                elemType, value, graphicElem, location);
        }

        private void CalculateLeftTopInCell(GridCellViewModel cell, out double left, out double top)
        {
            left = cell.Left + ((GridSizeStore.XCellSize - GridSizeStore.InCellElementSize) / 2);
            top = cell.Top + ((GridSizeStore.YCellSize - GridSizeStore.InCellElementSize) / 2);
        }

        private bool IsPlacingOnGridEdge(double left, double top, double elemhalfSize)
        {
            return left + elemhalfSize == 0 || top + elemhalfSize == 0 ||
                left + elemhalfSize == SudokuStore.Instance.Sudoku.Grid.Size * GridSizeStore.XCellSize ||
                top + elemhalfSize == SudokuStore.Instance.Sudoku.Grid.Size * GridSizeStore.YCellSize;
        }

        private void ElementOnEdge(GridCellViewModel cell, Point point, double elementSize, out double elementLeft, out double elementTop,
            out int rowIndex, out int colIndex, out ElementLocationType locationType)
        {
            double xOnPositive = CalculateXOnPositiveDiagonal(cell, point.Y);
            double xOnNegative = CalculateXOnNegativeDiagonal(cell, point.Y);

            double halfX = cell.Left + (GridSizeStore.XCellSize / 2);
            double halfY = cell.Top + (GridSizeStore.YCellSize / 2);

            double halfCircleSize = elementSize / 2;

            if ((IsLeftUp(point, halfX, halfY) && xOnPositive <= point.X) ||
                (IsRightUp(point, halfX, halfY) && point.X <= xOnNegative))
            {
                rowIndex = cell.RowIndex;
                colIndex = cell.ColIndex;
                locationType = ElementLocationType.Column;
                TopBorderElement(cell, out elementLeft, out elementTop, halfCircleSize);
            }
            else if ((IsLeftUp(point, halfX, halfY) && xOnPositive > point.X) ||
                (IsLeftDown(point, halfX, halfY) && point.X <= xOnNegative))
            {
                rowIndex = cell.RowIndex;
                colIndex = cell.ColIndex;
                locationType = ElementLocationType.Row;
                LeftBorderElement(cell, out elementLeft, out elementTop, halfCircleSize);
            }
            else if ((IsRightUp(point, halfX, halfY) && point.X > xOnNegative) ||
                (IsRightDown(point, halfX, halfY) && point.X > xOnPositive))
            {
                rowIndex = cell.RowIndex;
                colIndex = cell.ColIndex + 1;
                locationType = ElementLocationType.Row;
                RightBorderElement(cell, out elementLeft, out elementTop, halfCircleSize);
            }
            else
            {
                rowIndex = cell.RowIndex + 1;
                colIndex = cell.ColIndex;
                locationType = ElementLocationType.Column;
                BottomBorderElement(cell, out elementLeft, out elementTop, halfCircleSize);
            }
        }

        private static bool IsRightDown(Point point, double halfX, double halfY)
        {
            return point.X > halfX && point.Y > halfY;
        }

        private static bool IsLeftDown(Point point, double halfX, double halfY)
        {
            return point.X <= halfX && point.Y > halfY;
        }

        private static bool IsRightUp(Point point, double halfX, double halfY)
        {
            return point.X > halfX && point.Y <= halfY;
        }

        private static bool IsLeftUp(Point point, double halfX, double halfY)
        {
            return point.X <= halfX && point.Y <= halfY;
        }

        private static void BottomBorderElement(GridCellViewModel cell, out double elementLeft, out double elementTop, double halfCircleSize)
        {
            elementLeft = cell.Left + (GridSizeStore.XCellSize / 2) - halfCircleSize;
            elementTop = cell.Top + GridSizeStore.YCellSize - halfCircleSize;
        }

        private static void RightBorderElement(GridCellViewModel cell, out double elementLeft, out double elementTop, double halfCircleSize)
        {
            elementLeft = cell.Left + GridSizeStore.XCellSize - halfCircleSize;
            elementTop = cell.Top + (GridSizeStore.YCellSize / 2) - halfCircleSize;
        }

        private static void LeftBorderElement(GridCellViewModel cell, out double elementLeft, out double elementTop, double halfCircleSize)
        {
            elementLeft = cell.Left - halfCircleSize;
            elementTop = cell.Top + (GridSizeStore.YCellSize / 2) - halfCircleSize;
        }

        private static void TopBorderElement(GridCellViewModel cell, out double elementLeft, out double elementTop, double halfCircleSize)
        {
            elementLeft = cell.Left + (GridSizeStore.XCellSize / 2) - halfCircleSize;
            elementTop = cell.Top - halfCircleSize;
        }

        private double CalculateXOnPositiveDiagonal(GridCellViewModel cell, double y)
        {
            return (GridSizeStore.XCellSize * y +
                (-1) * GridSizeStore.XCellSize * cell.Top + GridSizeStore.YCellSize * cell.Left) /
                GridSizeStore.YCellSize;
        }

        private double CalculateXOnNegativeDiagonal(GridCellViewModel cell, double y)
        {
            return ((-1) * GridSizeStore.XCellSize * y + GridSizeStore.YCellSize * (cell.Left +
                GridSizeStore.XCellSize) +
                GridSizeStore.XCellSize * cell.Top) / GridSizeStore.YCellSize;
        }

        private bool IsAdjacentCell(GridCellViewModel cell)
        {
            if (_viewModel.SelectedCells.Count == 0 || VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.SudokuExtraRegions)
            {
                return true;
            }

            foreach (var elem in _viewModel.SelectedCells)
            {
                if (IsLabelLeft(cell, elem) || IsLabelRight(cell, elem) || IsLabelUp(cell, elem) || IsLabelDown(cell, elem))
                {
                    return true;
                }
                if ((VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.SudokuPalindrome ||
                    VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.SudokuSequence ||
                    VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.SudokuArrow ||
                    VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.ElemLine||
                    VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.ElemLongArrow||
                    VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.ElemArrowWithCircle ||
                    VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.SudokuThermometer) &&
                    (IsActualCellRightDown(cell, elem) || IsActualCellLeftUp(cell, elem) || IsActualLeftDown(cell, elem) ||
                    IsActualRightUp(cell, elem)))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsLabelLeft(GridCellViewModel label, GridCellViewModel elem)
        {
            return Math.Abs(elem.Margin.Left - label.Margin.Left - GridSizeStore.XCellSize) <= .0001 &&
                Math.Abs(elem.Margin.Top - label.Margin.Top) <= .0001;
        }

        private bool IsLabelRight(GridCellViewModel label, GridCellViewModel elem)
        {
            return Math.Abs(elem.Margin.Left - label.Margin.Left + GridSizeStore.XCellSize) <= .0001 &&
                Math.Abs(elem.Margin.Top - label.Margin.Top) <= .0001;
        }

        private bool IsLabelUp(GridCellViewModel label, GridCellViewModel elem)
        {
            return Math.Abs(elem.Margin.Top - label.Margin.Top - GridSizeStore.YCellSize) <= .0001 &&
                Math.Abs(elem.Margin.Left - label.Margin.Left) <= .0001;
        }

        private bool IsLabelDown(GridCellViewModel label, GridCellViewModel elem)
        {
            return Math.Abs(elem.Margin.Top - label.Margin.Top + GridSizeStore.YCellSize) <= .0001 &&
                Math.Abs(elem.Margin.Left - label.Margin.Left) <= .0001;
        }

        private bool IsActualCellRightDown(GridCellViewModel cell, GridCellViewModel actualCell)
        {
            return (cell.RowIndex - 1) >= 0 && (cell.RowIndex - 1) == actualCell.RowIndex &&
                (cell.ColIndex - 1) >= 0 && (cell.ColIndex - 1) == actualCell.ColIndex;
        }

        private bool IsActualCellLeftUp(GridCellViewModel cell, GridCellViewModel actualCell)
        {
            return (cell.RowIndex + 1) >= 0 && (cell.RowIndex + 1) == actualCell.RowIndex &&
                (cell.ColIndex + 1) >= 0 && (cell.ColIndex + 1) == actualCell.ColIndex;
        }

        private bool IsActualLeftDown(GridCellViewModel cell, GridCellViewModel actualCell)
        {
            return (cell.RowIndex + 1) >= 0 && (cell.RowIndex + 1) == actualCell.RowIndex &&
                (cell.ColIndex - 1) >= 0 && (cell.ColIndex - 1) == actualCell.ColIndex;
        }

        private bool IsActualRightUp(GridCellViewModel cell, GridCellViewModel actualCell)
        {
            return (cell.RowIndex - 1) >= 0 && (cell.RowIndex - 1) == actualCell.RowIndex &&
                (cell.ColIndex + 1) >= 0 && (cell.ColIndex + 1) == actualCell.ColIndex;
        }

        private bool CanSelectMoreCells()
        {
            foreach (var elem in _viewModel.SudokuVariantElementButton)
            {
                if (elem.Checked)
                {
                    if (elem.ToString() == Resources.SudokuPalindrome ||
                        elem.ToString() == Resources.SudokuSequence ||
                        elem.ToString() == Resources.SudokuArrow ||
                        elem.ToString() == Resources.SudokuThermometer ||
                        elem.ToString() == Resources.SudokuKiller ||
                        elem.ToString() == Resources.SudokuExtraRegions)
                    {
                        return true;
                    }
                }
            }
            foreach (var elem in _viewModel.SudokuGraphicElementButton)
            {
                if (elem.Checked)
                {
                    if (elem.ToString() == Resources.ElemLongArrow ||
                        elem.ToString() == Resources.ElemArrowWithCircle ||
                        elem.ToString() == Resources.ElemLine ||
                        elem.ToString() == Resources.ElemCage)
                    {
                        return true;
                    }
                }
            }
            return _viewModel.GridButton.Checked;
        }

        private void ChangeColor(GridCellViewModel cell)
        {
            if (cell.Background == _selectedCellColor)
            {
                _viewModel.SelectedCells.Remove(cell);
                cell.Background = Brushes.Transparent;
                if (_viewModel.SelectedVariantsName.Contains(Resources.SudokuWindoku))
                {
                    cell.Background = cell.DefaultBrush;
                }
            }
            else
            {
                cell.Background = _selectedCellColor;
                _viewModel.SelectedCells.Add(cell);
            }
        }

        private bool CanSellectCell(GridCellViewModel cell, ref bool changedBack)
        {
            changedBack = false;
            foreach (var elem in _viewModel.SudokuVariantElementButton)
            {
                if (elem.Checked)
                {
                    VariantGraphicElementStore.Instance.VariantGraphicElem = elem.ToString();
                    if (elem.ToString() == Resources.SudokuPalindrome ||
                        elem.ToString() == Resources.SudokuSequence ||
                        elem.ToString() == Resources.SudokuArrow ||
                        elem.ToString() == Resources.SudokuThermometer ||
                        elem.ToString() == Resources.SudokuKiller ||
                        elem.ToString() == Resources.SudokuExtraRegions)
                    {
                        return cell.Background != _selectedCellColor;
                    }
                    if (elem.ToString() == Resources.SudokuSkyscrapers ||
                        elem.ToString() == Resources.SudokuNextToNine ||
                        elem.ToString() == Resources.SudokuOutside)
                    {
                        changedBack = true;
                        return false;
                    }
                }
            }
            foreach (var elem in _viewModel.SudokuGraphicElementButton)
            {
                if (elem.Checked)
                {
                    VariantGraphicElementStore.Instance.VariantGraphicElem = elem.ToString();
                    if (elem.ToString() == Resources.Number ||
                        elem.ToString() == Resources.ElemLongArrow ||
                        elem.ToString() == Resources.ElemArrowWithCircle ||
                        elem.ToString() == Resources.ElemLine ||
                        elem.ToString() == Resources.ElemCage ||
                        elem.ToString() == Resources.ElemCharacters ||
                        elem.ToString() == Resources.ElemGreyCell)
                    {
                        return cell.Background != _selectedCellColor;
                    }
                }
            }

            if (_viewModel.GridButton.Checked)
            {
                VariantGraphicElementStore.Instance.VariantGraphicElem = Resources.SudokuIrregular;
                return true;
            }

            return _viewModel.GivenNumberButton.Checked;
        }

        public void SetAllCellsTransparent()
        {
            foreach (var elem in _viewModel.SelectedCells)
            {
                elem.Background = elem.DefaultBrush;
            }
            _viewModel.SelectedCells.Clear();
        }

        private bool CanSelectCellOutside(Point point, GridCellViewModel cell)
        {
            return VariantPlacedOutside(point, cell) || GraphicElemPlacedOutside(point, cell);
        }

        private bool VariantPlacedOutside(Point point, GridCellViewModel cell)
        {
            double sizeGrid = GridSizeStore.XCellSize * SudokuStore.Instance.Sudoku.Grid.Size;
            foreach (var variant in _viewModel.SudokuVariantElementButton)
            {
                ElementButtonGraphicOptions button = variant as ElementButtonGraphicOptions;
                if (button != null)
                {
                    foreach (var graphicElem in button.ElementButtonOptions)
                    {
                        if (graphicElem.Checked)
                        {
                            VariantGraphicElementStore.Instance.VariantGraphicElem = variant.ToString();
                            if (graphicElem.ToString() == Resources.SudokuLittleKiller + Resources.MenuLeftUp)
                            {
                                PlaceSmallArrow(cell, SudokuElementType.LittleKillerLeftUp, GraphicElementType.LeftUp, point, sizeGrid);
                                return false;
                            }

                            if (graphicElem.ToString() == Resources.SudokuLittleKiller + Resources.MenuRightDown)
                            {
                                PlaceSmallArrow(cell, SudokuElementType.LittleKillerRightDown, GraphicElementType.RightDown, point, sizeGrid);
                                return false;
                            }

                            if (graphicElem.ToString() == Resources.SudokuLittleKiller + Resources.MenuRightUp)
                            {
                                PlaceSmallArrow(cell, SudokuElementType.LittleKillerRightUp, GraphicElementType.RightUp, point, sizeGrid);
                                return false;
                            }
                        }
                    }
                }

                if (variant.Checked)
                {
                    VariantGraphicElementStore.Instance.VariantGraphicElem = variant.ToString();
                    if (variant.ToString() == Resources.SudokuLittleKiller + Resources.MenuLeftDown)
                    {
                        PlaceSmallArrow(cell, SudokuElementType.LittleKillerLeftDown, GraphicElementType.LeftDown, point, sizeGrid);
                        return false;
                    }

                    if (variant.ToString() == Resources.SudokuSkyscrapers)
                    {
                        return IsFirstAroundGrid(point, sizeGrid);
                    }

                    if (variant.ToString() == Resources.SudokuNextToNine)
                    {
                        return IsFirstSecondAroundGrid(point, sizeGrid);
                    }

                    if (variant.ToString() == Resources.SudokuOutside)
                    {
                        return true;
                    }

                    if (variant.ToString() == Resources.SudokuStarProducts)
                    {
                        return IsFirstAroundGrid(point, sizeGrid);
                    }
                }
            }
            return false;
        }

        private bool GraphicElemPlacedOutside(Point point, GridCellViewModel cell)
        {
            double sizeGrid = GridSizeStore.XCellSize * SudokuStore.Instance.Sudoku.Grid.Size;
            foreach (var variant in _viewModel.SudokuGraphicElementButton)
            {
                ElementButtonGraphicOptions button = variant as ElementButtonGraphicOptions;
                if (button != null)
                {
                    foreach (var graphicElem in button.ElementButtonOptions)
                    {
                        if (graphicElem.Checked)
                        {
                            VariantGraphicElementStore.Instance.VariantGraphicElem = variant.ToString();
                            if (graphicElem.ToString() == Resources.ElemSmallArrow + Resources.MenuLeftUp)
                            {
                                PlaceSmallArrow(cell, SudokuElementType.NoMeaning, GraphicElementType.LeftUp, point, sizeGrid);
                                return false;
                            }

                            if (graphicElem.ToString() == Resources.ElemSmallArrow + Resources.MenuRightDown)
                            {
                                PlaceSmallArrow(cell, SudokuElementType.NoMeaning, GraphicElementType.RightDown, point, sizeGrid);
                                return false;
                            }

                            if (graphicElem.ToString() == Resources.ElemSmallArrow + Resources.MenuRightUp)
                            {
                                PlaceSmallArrow(cell, SudokuElementType.NoMeaning, GraphicElementType.RightUp, point, sizeGrid);
                                return false;
                            }
                        }
                    }
                }

                if (variant.Checked)
                {
                    VariantGraphicElementStore.Instance.VariantGraphicElem = variant.ToString();
                    if (variant.ToString() == Resources.Number || variant.ToString() == Resources.ElemCharacters)
                    {
                        return true;
                    }
                    
                    if (variant.ToString() == Resources.ElemSmallArrow + Resources.MenuLeftDown)
                    {
                        PlaceSmallArrow(cell, SudokuElementType.NoMeaning, GraphicElementType.LeftDown, point, sizeGrid);
                        return false;
                    }
                }
            }
            return false;
        }

        private bool IsFirstAroundGrid(Point point, double sizeGrid)
        {
            return -GridSizeStore.XCellSize <= point.X && point.X <= sizeGrid + GridSizeStore.XCellSize &&
                -GridSizeStore.YCellSize <= point.Y && point.Y <= sizeGrid + GridSizeStore.YCellSize;
        }

        private bool IsFirstSecondAroundGrid(Point point, double sizeGrid)
        {
            return -2 * GridSizeStore.XCellSize <= point.X && point.X <= sizeGrid + 2 * GridSizeStore.XCellSize &&
                -2 * GridSizeStore.YCellSize <= point.Y && point.Y <= sizeGrid + 2 * GridSizeStore.YCellSize;
        }

        private void PlaceSmallArrow(GridCellViewModel cell, SudokuElementType type, GraphicElementType graphicType, Point point, double sizeGrid)
        {
            if ((type != SudokuElementType.NoMeaning && !IsFirstAroundGrid(point, sizeGrid)) ||
                (type == SudokuElementType.NoMeaning && !IsInGrid(point, sizeGrid)))
            {
                return;
            }
            double left = cell.Left;
            double top = cell.Top;
            if (SmallArrowViewModel.RemoveFromCollection(VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid,
                left, top, type))
            {
                VariantGraphicElementStore.Instance.NumberCellPartOfGrid[cell.Order].Number = 0;
                if (SudokuElementType.LittleKillerLeftDown == type || SudokuElementType.LittleKillerLeftUp == type ||
                SudokuElementType.LittleKillerRightDown == type || SudokuElementType.LittleKillerRightUp == type)
                {
                    if (CountSudokuTypes(SudokuType.LittleKiller) == 0)
                    {
                        LittleKillerDiagonalLines(false);
                    }
                }
                return;
            }
            int previousCount = CountSudokuTypes(SudokuType.LittleKiller);
            VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid.Add(new SmallArrowViewModel(left, top, cell.Order,
                type, graphicType, LocationGrid(point)));
            int actualCount = CountSudokuTypes(SudokuType.LittleKiller);
            if (SudokuElementType.LittleKillerLeftDown == type || SudokuElementType.LittleKillerLeftUp == type ||
                SudokuElementType.LittleKillerRightDown == type || SudokuElementType.LittleKillerRightUp == type)
            {
                if (previousCount == 0 && actualCount != 0)
                {
                    LittleKillerDiagonalLines(true);
                }
            }
        }

        private int CountSudokuTypes(SudokuType type)
        {
            int count = 0;
            foreach (var elem in SudokuStore.Instance.Sudoku.Variants)
            {
                if (elem == type)
                {
                    count++;
                }
            }
            return count;
        }

        private void LittleKillerDiagonalLines(bool show)
        {
            if (show)
            {
                int gridSize = SudokuStore.Instance.Sudoku.Grid.Size;
                ClassicLineViewModel leftLine = new ClassicLineViewModel(0, 0,
                GridSizeStore.XCellSize * gridSize,
                GridSizeStore.YCellSize * gridSize,
                2.0, Brushes.Gray);
                _viewModel.GraphicElements.Add(leftLine);
                _lines.Add(leftLine);

                ClassicLineViewModel rightLine = new ClassicLineViewModel(GridSizeStore.XCellSize * gridSize, 0,
                    0, GridSizeStore.YCellSize * gridSize,
                    2.0, Brushes.Gray);
                _viewModel.GraphicElements.Add(rightLine);
                _lines.Add(rightLine);
            }
            else
            {
                foreach (var elem in _lines)
                {
                    _viewModel.GraphicElements.Remove(elem);
                }
                _lines.Clear();
            }
        }

        private bool IsInGrid(Point point, double sizeGrid)
        {
            return 0 <= point.X && point.X <= sizeGrid && 0 <= point.Y && point.Y <= sizeGrid;
        }

        private ElementLocationType LocationGrid(Point point)
        {
            if (point.X <= 0)
            {
                return ElementLocationType.GridLeft;
            }
            else if (point.X >= GridSizeStore.XCellSize * SudokuStore.Instance.Sudoku.Grid.Size)
            {
                return ElementLocationType.GridRight;
            }
            else if (point.Y <= 0)
            {
                return ElementLocationType.GridUp;
            }
            return ElementLocationType.GridDown;
        }
    }
}
