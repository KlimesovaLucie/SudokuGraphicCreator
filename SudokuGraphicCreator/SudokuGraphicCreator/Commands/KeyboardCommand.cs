using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Properties.Resources;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command is responsible for insertin numbers or characters form keyboard in corresponding graphic elements.
    /// </summary>
    public class KeyboardCommand : BaseCommand
    {
        private readonly ICreatingSudokuViewModel _viewModel;

        private const string BackSpace = "Backspace";

        /// <summary>
        /// Initializes a new instance of <see cref="KeyboardCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingSudoku"/> view.</param>
        public KeyboardCommand(ICreatingSudokuViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        /// <summary>
        /// This command is execute arried input from keyboard.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            KeyEventArgs pressedKey = (KeyEventArgs)parameter;
            KeyConverter kc = new KeyConverter();
            string str = kc.ConvertToString(pressedKey.Key);
            int? number = ConvertNumber(str);

            if (NotGivenNumberChecked(number, str, str == BackSpace))
            {
                return;
            }

            if (!_viewModel.GivenNumberButton.Checked && (VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.SudokuArrow ||
                VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.ElemArrowWithCircle))
            {
                PlaceNumberForArrowWithCircle(number, str);
                return;
            }

            if (!_viewModel.GivenNumberButton.Checked && (VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.SudokuOutside))
            {
                PlaceNumberAroundGrid(number, str, SudokuElementType.Outside);
                return;
            }

            if (!_viewModel.GivenNumberButton.Checked && (VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.SudokuNextToNine))
            {
                PlaceNumberAroundGrid(number, str, SudokuElementType.NextToNine);
                return;
            }

            if (!_viewModel.GivenNumberButton.Checked && (VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.SudokuSkyscrapers))
            {
                PlaceNumberAroundGrid(number, str, SudokuElementType.Skyscrapers);
                return;
            }

            if (!_viewModel.GivenNumberButton.Checked && VariantGraphicElementStore.Instance.VariantGraphicElem.Contains(Resources.SudokuLittleKiller))
            {
                PlaceNumberLittleKiller(number, str);
                return;
            }

            if (!_viewModel.GivenNumberButton.Checked && VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.Number)
            {
                PlaceNumber(number, str);
                return;
            }

            if (!_viewModel.GivenNumberButton.Checked && VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.ElemCharacters)
            {
                PlaceCharacter(str);
                return;
            }

            if (!_viewModel.GivenNumberButton.Checked && VariantGraphicElementStore.Instance.VariantGraphicElem == Resources.SudokuStarProducts)
            {
                PlaceJoinNumberAroundGrid(number, str, SudokuElementType.StarProduct);
                return;
            }

            if ((number == null && str != BackSpace) || _viewModel.SelectedCells.Count == 0 || !_viewModel.GivenNumberButton.Checked)
            {
                return;
            }

            PlaceGiveNumber(str, number);
        }

        private void PlaceNumberForArrowWithCircle(int? number, string str)
        {
            SetDefaultBackgrounds();
            if (str == BackSpace)
            {
                DeleteNumber();
                return;
            }

            if (!CorrectNumberValue((int)number))
            {
                return;
            }

            int order = 0;
            LongArrowWithCircleViewModel arrow = null;

            for (int i = _viewModel.GraphicElements.Count - 1; i > -1; i--)
            {
                arrow = _viewModel.GraphicElements[i] as LongArrowWithCircleViewModel;
                if (arrow != null)
                {
                    break;
                }
            }

            if (arrow == null)
            {
                return;
            }

            FindCircleCell(arrow, out order).Number = (int)number;
        }

        private void PlaceNumber(int? number, string str)
        {
            SetDefaultBackgrounds();
            var selectedCell = _viewModel.SelectedCells[0];
            if (number == null && str != BackSpace)
            {
                return;
            }
            if (str == BackSpace)
            {
                VariantGraphicElementStore.Instance.NumberCellPartOfGrid[selectedCell.Order].Number = 0;
                return;
            }
            int newNumber = (int)number;
            newNumber = VariantGraphicElementStore.Instance.NumberCellPartOfGrid[selectedCell.Order].Number * 10 + newNumber;
            VariantGraphicElementStore.Instance.NumberCellPartOfGrid[selectedCell.Order].Number = newNumber;
        }

        private void PlaceCharacter(string str)
        {
            SetDefaultBackgrounds();
            var selectedCell = _viewModel.SelectedCells[0];
            if (str == BackSpace)
            {
                CharacterViewModel.RemoveFromCollection(VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid, selectedCell.Left,
                selectedCell.Top, SudokuElementType.NoMeaning);
                return;
            }

            CharacterViewModel character = null;
            foreach (var element in VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid)
            {
                var actualCharacter = element as CharacterViewModel;
                if (actualCharacter != null && actualCharacter.Left == selectedCell.Left && actualCharacter.Top == selectedCell.Top)
                {
                    character = actualCharacter;
                    break;
                }
            }

            if (character == null)
            {
                character = new CharacterViewModel(GridSizeStore.XCellSize, GridSizeStore.YCellSize,
                    selectedCell.Left, selectedCell.Top, selectedCell.RowIndex, selectedCell.ColIndex, SudokuElementType.NoMeaning, str,
                    GraphicElementType.None, GridSizeStore.InCellTextSize, VariantGraphicElementStore.Instance.LocationType);
                VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid.Add(character);
                return;
            }
            character.Text += str;
        }

        private void PlaceNumberAroundGrid(int? number, string str, SudokuElementType type = SudokuElementType.NoMeaning)
        {
            SetDefaultBackgrounds();
            if (str == BackSpace)
            {
                DeleteNumber();
                return;
            }

            if (number == null || !CorrectNumberValue((int)number))
            {
                return;
            }

            int order = 0;
            CellNumberViewModel cell = FindSellectedCell(out order);
            cell.Number = (int)number;
            cell.Type = type;
        }

        private void PlaceJoinNumberAroundGrid(int? number, string str, SudokuElementType elemType = SudokuElementType.NoMeaning)
        {
            SetDefaultBackgrounds();
            if (str == BackSpace)
            {
                DeleteNumber();
                return;
            }

            if (number == null)
            {
                return;
            }

            int order = 0;
            CellNumberViewModel cell = FindSellectedCell(out order);
            cell.Number = (10 * cell.Number) + (int)number;
            cell.Type = elemType;
        }

        private void PlaceNumberLittleKiller(int? number, string str)
        {
            SetDefaultBackgrounds();
            SmallArrowViewModel arrow = null;

            for (int i = VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid.Count - 1; i > -1; i--)
            {
                arrow = VariantGraphicElementStore.Instance.GraphicCellsPartOfGrid[i] as SmallArrowViewModel;
                if (arrow != null)
                {
                    break;
                }
            }

            if (arrow == null)
            {
                return;
            }

            int? previousNumberInArrow = arrow.Value;
            if (arrow.Value == null)
            {
                arrow.Value = number;
            }
            else
            {
                int previousNumber = (int)arrow.Value;
                arrow.Value = previousNumber * 10 + number;
            }
            CellNumberViewModel cell = VariantGraphicElementStore.Instance.NumberCellPartOfGrid[arrow.Order];
            cell.Type = arrow.SudokuElemType;
            if (number != null)
            {
                if (previousNumberInArrow == null)
                {
                    cell.Number = (int)number;
                }
                else
                {
                    cell.Number = ((int)previousNumberInArrow) * 10 + (int)number;
                }
            }
            else
            {
                cell.Number = 0;
            }
        }

        private CellNumberViewModel FindCircleCell(LongArrowWithCircleViewModel arrow, out int order)
        {
            int leftIndex = (int)(arrow.Circle.Left / GridSizeStore.XCellSize);
            int topIndex = (int)(arrow.Circle.Top / GridSizeStore.YCellSize);
            order = topIndex * SudokuStore.Instance.Sudoku.Grid.Size + leftIndex;
            return _viewModel.CellNumbers[order];
        }

        private void PlaceGiveNumber(string str, int? number)
        {
            SetDefaultBackgrounds();
            if (str == BackSpace)
            {
                DeleteNumber();
                return;
            }

            int order = 0;
            FindSellectedCell(out order).Number = (int)number;
        }
        
        private void DeleteNumber()
        {
            int order = 0;
            CellNumberViewModel cell = FindSellectedCell(out order);
            cell.Number = 0;
            cell.Type = SudokuElementType.NoMeaning;
        }

        private bool NotGivenNumberChecked(int? number, string inputValue, bool delete)
        {
            if (NotGivenNumberInButtonCollection(number, inputValue, delete, _viewModel.SudokuVariantElementButton))
            {
                return true;
            }
            return NotGivenNumberInButtonCollection(number, inputValue, delete, _viewModel.SudokuGraphicElementButton);
        }

        private bool NotGivenNumberInButtonCollection(int? number, string inputValue, bool delete,
            ObservableCollection<ElementControl> collection)
        {
            foreach (var button in collection)
            {
                if (button.Checked)
                {
                    if (button.ToString() == Resources.SudokuSum ||
                        button.ToString() == Resources.SudokuDifference ||
                        button.ToString() == Resources.ElemCircleWithNumber ||
                        button.ToString() == Resources.SudokuKiller ||
                        button.ToString() == Resources.ElemCage)
                    {
                        SetNumber(number, inputValue, delete);
                        return true;
                    }
                }
            }
            return false;

        }

        private void SetNumber(int? number, string inputValue, bool delete)
        {
            for (int i = _viewModel.GraphicElements.Count - 1; i > -1; i--)
            {
                CircleWithNumberViewModel circle = _viewModel.GraphicElements[i] as CircleWithNumberViewModel;
                if (circle != null)
                {
                    if (number != null || delete)
                    {
                        SetNumberToCircle(number, delete, circle);
                    }
                    return;
                }

                CageViewModel cage = _viewModel.GraphicElements[i] as CageViewModel;
                if (cage != null)
                {
                    SetNumberToCage(number, inputValue, delete, cage);
                    return;
                }
            }
        }

        private void SetNumberToCage(int? number, string inputValue, bool delete, CageViewModel cage)
        {
            string actualValue;
            if (cage.SudokuElemType == SudokuElementType.Killer)
            {
                actualValue = number.ToString();
            }
            else
            {
                actualValue = inputValue;
            }

            if (cage.Text == null)
            {
                CharacterViewModel text = new CharacterViewModel(GridSizeStore.KillerNumberSize,
                        GridSizeStore.KillerNumberSize, cage.LeftTopX, cage.LeftTopY, (int)(cage.LeftTopY / GridSizeStore.YCellSize),
                        (int)(cage.LeftTopX / GridSizeStore.XCellSize), SudokuElementType.Killer,
                        actualValue, GraphicElementType.None, ElementLocationType.Cell);
                cage.Text = text;
                cage.SetNewNumber(number);
                _viewModel.GraphicElements.Add(text);
            }
            else
            {
                string value = "";
                int? newValue = null;
                if (!delete)
                {
                    value = cage.Text.Text + actualValue;
                    int? previous = cage.GetCageNumber();
                    if (previous == null)
                    {
                        previous = 0;
                    }
                    newValue = previous * 10 + number;
                }
                cage.Text.Text = value;
                int killerValue = 0;
                try
                {
                    killerValue = Int32.Parse(value);
                }
                catch
                { }
                cage.SetNewNumber(killerValue);
            }
        }

        private void SetNumberToCircle(int? number, bool delete, CircleWithNumberViewModel circle)
        {
            int value = 0;
            if (!delete)
            {
                value = JointNumber(circle.Value, (int)number);
            }
            circle.Value = value;
        }

        private int JointNumber(int? value, int enteredNumber)
        {
            if (value == null)
            {
                return enteredNumber;
            }
            return (int)value * 10 + enteredNumber;
        }

        private void SetDefaultBackgrounds()
        {
            foreach (var cell in _viewModel.CellNumbers)
            {
                cell.Background = cell.DefaultBrush;
            }
        }

        private int? ConvertNumber(string str)
        {
            if (str.Length == 0)
            {
                return null;
            }
            str = str[str.Length - 1].ToString();
            try
            {
                return Int32.Parse(str);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        private bool CorrectNumberValue(int number)
        {
            return 0 < number && number < SudokuStore.Instance.Sudoku.Grid.Size + 1;
        }

        private CellNumberViewModel FindSellectedCell(out int order)
        {
            int index = _viewModel.SelectedCells[0].Order;
            order = index;
            return VariantGraphicElementStore.Instance.NumberCellPartOfGrid[index];
        }
    }
}
