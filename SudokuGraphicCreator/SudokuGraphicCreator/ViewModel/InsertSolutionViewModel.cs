using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.View;
using System;
using System.IO;
using System.Windows.Input;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel for <see cref="InsertSolution"/> view.
    /// </summary>
    public class InsertSolutionViewModel : BaseViewModel, IDialogRequestClose
    {
        private readonly ICreatingSudokuViewModel _viewModel;

        private readonly int _gridSize = SudokuStore.Instance.Sudoku.Grid.Size;

        private string _inputString;

        /// <summary>
        /// Input string with numbers of solution.
        /// </summary>
        public string InputString
        {
            get => _inputString;
            set
            {
                _inputString = value;
                OnPropertyChanged(nameof(InputString));
            }
        }

        private int[,] _givenNumbers;

        private bool _allGraphicElemChecked;

        /// <summary>
        /// true for generating all graphic elements, otherwise false.
        /// </summary>
        public bool AllGraphicElemChecked
        {
            get => _allGraphicElemChecked;
            set
            {
                _allGraphicElemChecked = value;
                OnPropertyChanged(nameof(AllGraphicElemChecked));
            }
        }

        private bool _withNumbersChecked;

        /// <summary>
        /// true for inserting number into table, otherwise false.
        /// </summary>
        public bool WithNumbersChecked
        {
            get => _withNumbersChecked;
            set
            {
                _withNumbersChecked = value;
                OnPropertyChanged(nameof(WithNumbersChecked));
            }
        }

        private bool _withoutNumberChecked;

        /// <summary>
        /// true for not inserting numbers into table, otherwise false.
        /// </summary>
        public bool WithoutNumberChecked
        {
            get => _withoutNumberChecked;
            set
            {
                _withoutNumberChecked = value;
                OnPropertyChanged(nameof(WithNumbersChecked));
            }
        }

        #region VARIANTCHECKED
        private bool _isConsecutiveChecked;

        /// <summary>
        /// true for generate all graphic elements for Consecutive, otherwise false.
        /// </summary>
        public bool IsConsecutiveChecked
        {
            get => _isConsecutiveChecked;
            set
            {
                _isConsecutiveChecked = value;
                OnPropertyChanged(nameof(IsConsecutiveChecked));
            }
        }

        private bool _isGreaterThanCkecked;

        /// <summary>
        /// true for generate all graphic elements for Greater Than, otherwise false.
        /// </summary>
        public bool IsGreaterThanCkecked
        {
            get => _isGreaterThanCkecked;
            set
            {
                _isGreaterThanCkecked = value;
                OnPropertyChanged(nameof(IsGreaterThanCkecked));
            }
        }

        private bool _isXVChecked;

        /// <summary>
        /// true for generate all graphic elements for XV, otherwise false.
        /// </summary>
        public bool IsXVChecked
        {
            get => _isXVChecked;
            set
            {
                _isXVChecked = value;
                OnPropertyChanged(nameof(IsXVChecked));
            }
        }

        private bool _isKropkiChecked;

        /// <summary>
        /// true for generate all graphic elements for Kropki, otherwise false.
        /// </summary>
        public bool IsKropkiChecked
        {
            get => _isKropkiChecked;
            set
            {
                _isKropkiChecked = value;
                OnPropertyChanged(nameof(IsKropkiChecked));
            }
        }

        private bool _isEvenChecked;

        /// <summary>
        /// true for generate all graphic elements for Even, otherwise false.
        /// </summary>
        public bool IsEvenChecked
        {
            get => _isEvenChecked;
            set
            {
                _isEvenChecked = value;
                OnPropertyChanged(nameof(IsEvenChecked));
            }
        }

        private bool _isOddChecked;

        /// <summary>
        /// true for generate all graphic elements for Odd, otherwise false.
        /// </summary>
        public bool IsOddChecked
        {
            get => _isOddChecked;
            set
            {
                _isOddChecked = value;
                OnPropertyChanged(nameof(IsOddChecked));
            }
        }

        private bool _isNextToNineChecked;

        /// <summary>
        /// true for generate all graphic elements for Next to nine, otherwise false.
        /// </summary>
        public bool IsNextToNineChecked
        {
            get => _isNextToNineChecked;
            set
            {
                _isNextToNineChecked = value;
                OnPropertyChanged(nameof(IsNextToNineChecked));
            }
        }

        private bool _isSkyscrapersChecked;

        /// <summary>
        /// true for generate all graphic elements for Skyscrapers, otherwise false.
        /// </summary>
        public bool IsSkyscrapersChecked
        {
            get => _isSkyscrapersChecked;
            set
            {
                _isSkyscrapersChecked = value;
                OnPropertyChanged(nameof(IsSkyscrapersChecked));
            }
        }
        #endregion

        /// <summary>
        /// Ok command for <see cref="InsertSolution"/> view.
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        /// Cancel command for <see cref="InsertSolution"/> view.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Command for selection file for <see cref="InsertSolution"/> view.
        /// </summary>
        public ICommand SelectFileCommand { get; }

        /// <summary>
        /// Initializes a new instace of <see cref="InsertSolutionViewModel"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel for <see cref="CreatingSudoku"/> view.</param>
        public InsertSolutionViewModel(ICreatingSudokuViewModel viewModel)
        {
            _viewModel = viewModel;
            _givenNumbers = new int[_gridSize, _gridSize];
            OkCommand = new ActionCommand(
                _ =>
                {
                    CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
                    CreateGraphicElems();
                },
                _ => IsCorrectFormat());
            CancelCommand = new ActionCommand(_ => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)), _ => true);
            SelectFileCommand = new ActionCommand(
                _ => OpenStringForGivenNumbers(),
                _ => true);
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        private void OpenStringForGivenNumbers()
        {
            IIOService service = new IOService();
            string fileName = service.OpenTextFile();
            if (fileName == null || fileName == "")
            {
                return;
            }
            try
            {
                InputString = File.ReadAllText(fileName);
            }
            catch
            { }
        }

        /// <summary>
        /// Deside if <see cref="InputString"/> is in correct format a save number to <see cref="_givenNumbers"/>.
        /// </summary>
        /// <returns>true if <see cref="InputString"/> is in correct format, otherwise false.</returns>
        public bool IsCorrectFormat()
        {
            if (InputString == null)
            {
                return false;
            }
            int stringLenght = InputString.Length;
            int tableCellCount = SudokuStore.Instance.Sudoku.Grid.Size * SudokuStore.Instance.Sudoku.Grid.Size;
            if (stringLenght != tableCellCount)
            {
                return false;
            }

            for (int i = 0; i < stringLenght; i++)
            {
                try
                {
                    _givenNumbers[i / _gridSize, i % _gridSize] = Int32.Parse(InputString[i].ToString());
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        private void SaveInsertedNumbers()
        {
            for (int row = 0; row < _gridSize; row++)
            {
                for (int col = 0; col < _gridSize; col++)
                {
                    int actualNumber = _givenNumbers[row, col];
                    if (actualNumber != 0)
                    {
                        _viewModel.CellNumbers[row * _gridSize + col].Number = actualNumber;
                    }
                }
            }
        }

        private void CreateGraphicElems()
        {
            if (!AllGraphicElemChecked)
            {
                SaveInsertedNumbers();
                return;
            }

            if (WithNumbersChecked)
            {
                SaveInsertedNumbers();
            }

            if (IsConsecutiveChecked)
            {
                CreateConsecutiveElems();
            }

            if (IsGreaterThanCkecked)
            {
                CreateGreaterThanElems();
            }

            if (IsXVChecked)
            {
                CreateXVElems();
            }

            if (IsKropkiChecked)
            {
                CreateKropkiElems();
            }

            if (IsEvenChecked)
            {
                CreateEvenElems();
            }

            if (IsOddChecked)
            {
                CreateOddElems();
            }

            if (IsNextToNineChecked)
            {
                CreateNextToNineElems();
            }

            if (IsSkyscrapersChecked)
            {
                CreateSkyscrapersElems();
            }
        }

        private void CreateConsecutiveElems()
        {
            for (int row = 0; row < _gridSize; row++)
            {
                for (int col = 0; col < _gridSize - 1; col++)
                {
                    if (_givenNumbers[row, col] + 1 == _givenNumbers[row, col + 1] || _givenNumbers[row, col] - 1 == _givenNumbers[row, col + 1])
                    {
                        double left;
                        double top;
                        CalculateLeftOnEdge(row, col + 1, out left, out top, GridSizeStore.SmallCircleSize / 2);
                        PlaceElementOnEdge(left, top, SudokuElementType.Consecutive, ElementLocationType.Row, row, col + 1, CreateWhiteCircle);
                    }
                }
            }

            for (int col = 0; col < _gridSize; col++)
            {
                for (int row = 0; row < _gridSize - 1; row++)
                {
                    if (_givenNumbers[row, col] + 1 == _givenNumbers[row + 1, col] || _givenNumbers[row, col] - 1 == _givenNumbers[row + 1, col])
                    {
                        double left;
                        double top;
                        CalculateTopOnEdge(row + 1, col, out left, out top, GridSizeStore.SmallCircleSize / 2);
                        PlaceElementOnEdge(left, top, SudokuElementType.Consecutive, ElementLocationType.Column, row + 1, col, CreateWhiteCircle);
                    }
                }
            }
        }

        private void CalculateLeftOnEdge(double rightCellRow, double rightCellCol, out double left, out double top, double elemSizeHalf)
        {
            left = rightCellCol * GridSizeStore.XCellSize - elemSizeHalf;
            top = rightCellRow * GridSizeStore.YCellSize + (GridSizeStore.YCellSize / 2) - elemSizeHalf;
        }

        private void CalculateTopOnEdge(double bottomCellRow, double bottomCellCol, out double left, out double top, double elemSizeHalf)
        {
            left = bottomCellCol * GridSizeStore.XCellSize + (GridSizeStore.XCellSize / 2) - elemSizeHalf;
            top = bottomCellRow * GridSizeStore.YCellSize - elemSizeHalf;
        }

        private void PlaceElementOnEdge(double left, double top, SudokuElementType elemType, ElementLocationType locationType, int rowIndex, int colIndex,
            Func<double, double, int, int, SudokuElementType, ElementLocationType, SudokuElementViewModel> createNewElement)
        {
            _viewModel.GraphicElements.Add(createNewElement(left, top, rowIndex, colIndex, elemType, locationType));
        }

        private WhiteCircleViewModel CreateWhiteCircle(double left, double top, int rowIndex, int colIndex, SudokuElementType elemType,
            ElementLocationType locationType)
        {
            return new WhiteCircleViewModel(GridSizeStore.SmallCircleSize, GridSizeStore.SmallCircleSize, left, top, rowIndex, colIndex, elemType, locationType);
        }

        private void CreateKropkiElems()
        {
            CheckKropkiInRows();
            CheckKropkiInCols();
        }

        private void CheckKropkiInCols()
        {
            for (int col = 0; col < _gridSize; col++)
            {
                for (int row = 0; row < _gridSize - 1; row++)
                {
                    if (_givenNumbers[row, col] * 2 == _givenNumbers[row + 1, col] || _givenNumbers[row, col] == _givenNumbers[row + 1, col] * 2)
                    {
                        double left;
                        double top;
                        CalculateTopOnEdge(row + 1, col, out left, out top, GridSizeStore.SmallCircleSize / 2);
                        PlaceElementOnEdge(left, top, SudokuElementType.BlackKropki, ElementLocationType.Column, row + 1, col, CreateBlackCircle);
                    }
                    else if (_givenNumbers[row, col] + 1 == _givenNumbers[row + 1, col] || _givenNumbers[row, col] - 1 == _givenNumbers[row + 1, col])
                    {
                        double left;
                        double top;
                        CalculateTopOnEdge(row + 1, col, out left, out top, GridSizeStore.SmallCircleSize / 2);
                        PlaceElementOnEdge(left, top, SudokuElementType.WhiteKropki, ElementLocationType.Column, row + 1, col, CreateWhiteCircle);
                    }
                }
            }
        }

        private void CheckKropkiInRows()
        {
            for (int row = 0; row < _gridSize; row++)
            {
                for (int col = 0; col < _gridSize - 1; col++)
                {
                    if (_givenNumbers[row, col] * 2 == _givenNumbers[row, col + 1] || _givenNumbers[row, col] == _givenNumbers[row, col + 1] * 2)
                    {
                        double left;
                        double top;
                        CalculateLeftOnEdge(row, col + 1, out left, out top, GridSizeStore.SmallCircleSize / 2);
                        PlaceElementOnEdge(left, top, SudokuElementType.BlackKropki, ElementLocationType.Row, row, col + 1, CreateBlackCircle);
                    }
                    else if (_givenNumbers[row, col] + 1 == _givenNumbers[row, col + 1] || _givenNumbers[row, col] - 1 == _givenNumbers[row, col + 1])
                    {
                        double left;
                        double top;
                        CalculateLeftOnEdge(row, col + 1, out left, out top, GridSizeStore.SmallCircleSize / 2);
                        PlaceElementOnEdge(left, top, SudokuElementType.WhiteKropki, ElementLocationType.Row, row, col + 1, CreateWhiteCircle);
                    }
                }
            }
        }

        private BlackCircleViewModel CreateBlackCircle(double left, double top, int rowIndex, int colIndex, SudokuElementType elemType,
            ElementLocationType locationType)
        {
            BlackCircleViewModel circle = new BlackCircleViewModel(GridSizeStore.SmallCircleSize, GridSizeStore.SmallCircleSize, left, top, rowIndex, colIndex, elemType, locationType);
            return circle;
        }

        private void CreateGreaterThanElems()
        {
            CheckGreaterThanElemInRows();
            CheckGreaterThanElemInCols();
        }

        private void CheckGreaterThanElemInRows()
        {
            for (int row = 0; row < _gridSize; row++)
            {
                for (int col = 0; col < _gridSize - 1; col++)
                {
                    if ((col + 1) % 3 != 0)
                    {
                        if (_givenNumbers[row, col] > _givenNumbers[row, col + 1])
                        {
                            double left;
                            double top;
                            CalculateLeftOnEdge(row, col + 1, out left, out top, GridSizeStore.OnEdgeTextElement / 2);
                            _viewModel.GraphicElements.Add(new CharacterViewModel(GridSizeStore.OnEdgeTextElement, GridSizeStore.OnEdgeTextElement,
                                left, top, row, col + 1, SudokuElementType.GreaterThanRight, "V", GraphicElementType.Right, ElementLocationType.Row));
                        }
                        else if (_givenNumbers[row, col] < _givenNumbers[row, col + 1])
                        {
                            double left;
                            double top;
                            CalculateLeftOnEdge(row, col + 1, out left, out top, GridSizeStore.OnEdgeTextElement / 2);
                            _viewModel.GraphicElements.Add(new CharacterViewModel(GridSizeStore.OnEdgeTextElement, GridSizeStore.OnEdgeTextElement,
                                left, top, row, col + 1, SudokuElementType.GreaterThanLeft, "V", GraphicElementType.Left, ElementLocationType.Row));
                        }
                    }
                }
            }
        }

        private void CheckGreaterThanElemInCols()
        {
            for (int col = 0; col < _gridSize; col++)
            {
                for (int row = 0; row < _gridSize - 1; row++)
                {
                    if ((row + 1) % 3 != 0)
                    {
                        if (_givenNumbers[row, col] > _givenNumbers[row + 1, col])
                        {
                            double left;
                            double top;
                            CalculateTopOnEdge(row + 1, col, out left, out top, GridSizeStore.OnEdgeTextElement / 2);
                            _viewModel.GraphicElements.Add(new CharacterViewModel(GridSizeStore.OnEdgeTextElement, GridSizeStore.OnEdgeTextElement,
                                left, top, row + 1, col, SudokuElementType.GreaterThanDown, "V", GraphicElementType.Down, ElementLocationType.Column));
                        }
                        else if (_givenNumbers[row, col] < _givenNumbers[row + 1, col])
                        {
                            double left;
                            double top;
                            CalculateTopOnEdge(row + 1, col, out left, out top, GridSizeStore.OnEdgeTextElement / 2);
                            _viewModel.GraphicElements.Add(new CharacterViewModel(GridSizeStore.OnEdgeTextElement, GridSizeStore.OnEdgeTextElement,
                                left, top, row + 1, col, SudokuElementType.GreaterThanUp, "V", GraphicElementType.Up, ElementLocationType.Column));
                        }
                    }
                }
            }
        }

        private void CreateXVElems()
        {
            CheckXVElemsInRows();
            CheckXVElemsInCols();
        }

        private void CheckXVElemsInRows()
        {
            for (int row = 0; row < _gridSize; row++)
            {
                for (int col = 0; col < _gridSize - 1; col++)
                {
                    if (_givenNumbers[row, col] + _givenNumbers[row, col + 1] == 5)
                    {
                        double left;
                        double top;
                        CalculateLeftOnEdge(row, col + 1, out left, out top, GridSizeStore.OnEdgeTextElement / 2);
                        _viewModel.GraphicElements.Add(new CharacterViewModel(GridSizeStore.OnEdgeTextElement, GridSizeStore.OnEdgeTextElement,
                            left, top, row, col + 1, SudokuElementType.XvV, "V", GraphicElementType.None, ElementLocationType.Row));
                    }
                    else if (_givenNumbers[row, col] + _givenNumbers[row, col + 1] == 10)
                    {
                        double left;
                        double top;
                        CalculateLeftOnEdge(row, col + 1, out left, out top, GridSizeStore.OnEdgeTextElement / 2);
                        _viewModel.GraphicElements.Add(new CharacterViewModel(GridSizeStore.OnEdgeTextElement, GridSizeStore.OnEdgeTextElement,
                            left, top, row, col + 1, SudokuElementType.XvX, "X", GraphicElementType.None, ElementLocationType.Row));
                    }
                }
            }
        }

        private void CheckXVElemsInCols()
        {
            for (int col = 0; col < _gridSize; col++)
            {
                for (int row = 0; row < _gridSize - 1; row++)
                {
                    if (_givenNumbers[row, col] + _givenNumbers[row + 1, col] == 5)
                    {
                        double left;
                        double top;
                        CalculateTopOnEdge(row + 1, col, out left, out top, GridSizeStore.OnEdgeTextElement / 2);
                        _viewModel.GraphicElements.Add(new CharacterViewModel(GridSizeStore.OnEdgeTextElement, GridSizeStore.OnEdgeTextElement,
                            left, top, row + 1, col, SudokuElementType.XvV, "V", GraphicElementType.None, ElementLocationType.Column));
                    }
                    else if (_givenNumbers[row, col] + _givenNumbers[row + 1, col] == 10)
                    {
                        double left;
                        double top;
                        CalculateTopOnEdge(row + 1, col, out left, out top, GridSizeStore.OnEdgeTextElement / 2);
                        _viewModel.GraphicElements.Add(new CharacterViewModel(GridSizeStore.OnEdgeTextElement, GridSizeStore.OnEdgeTextElement,
                            left, top, row + 1, col, SudokuElementType.XvX, "X", GraphicElementType.None, ElementLocationType.Column));
                    }
                }
            }
        }

        private void CreateEvenElems()
        {
            EvenElemInRows();
        }

        private void EvenElemInRows()
        {
            for (int row = 0; row < _gridSize; row++)
            {
                for (int col = 0; col < _gridSize; col++)
                {
                    if (_givenNumbers[row, col] % 2 == 0)
                    {
                        double left;
                        double top;
                        CalculateElemInCell(row, col, out left, out top, GridSizeStore.InCellElementSize);
                        _viewModel.GraphicElements.Add(new GreySquareViewModel(GridSizeStore.InCellElementSize, GridSizeStore.InCellElementSize,
                            left, top, row, col, SudokuElementType.Even, ElementLocationType.Cell));
                    }
                }
            }
        }

        private void CalculateElemInCell(double row, double col, out double left, out double top, double elemSize)
        {
            left = col * GridSizeStore.XCellSize + ((GridSizeStore.XCellSize - elemSize) / 2);
            top = row * GridSizeStore.YCellSize + ((GridSizeStore.YCellSize - elemSize) / 2);
        }

        private void CreateOddElems()
        {
            for (int row = 0; row < _gridSize; row++)
            {
                for (int col = 0; col < _gridSize; col++)
                {
                    if (_givenNumbers[row, col] % 2 != 0)
                    {
                        double left;
                        double top;
                        CalculateElemInCell(row, col, out left, out top, GridSizeStore.InCellElementSize);
                        _viewModel.GraphicElements.Add(new GreyCircleViewModel(GridSizeStore.InCellElementSize, GridSizeStore.InCellElementSize,
                            left, top, row, col, SudokuElementType.Even, ElementLocationType.Cell));
                    }
                }
            }
        }

        private void CreateNextToNineElems()
        {
            PlaceNumbersLeft();
            PlaceNumbersUp();
        }

        private void PlaceNumbersLeft()
        {
            for (int row = 0; row < _gridSize; row++)
            {
                int nineIndex = FindNineIndexInRow(row);
                if (nineIndex != -1)
                {
                    if (nineIndex == 0)
                    {
                        PlaceNumberLeftAround(row, _givenNumbers[row, 1]);
                    }
                    else if (nineIndex == SudokuStore.Instance.Sudoku.Grid.Size - 1)
                    {
                        PlaceNumberLeftAround(row, _givenNumbers[row, SudokuStore.Instance.Sudoku.Grid.Size - 2]);
                    }
                    else if (_givenNumbers[row, nineIndex + 1] < _givenNumbers[row, nineIndex - 1])
                    {
                        PlaceTwoNumbersLeftAround(row, nineIndex + 1, nineIndex - 1);
                    }
                    else if (_givenNumbers[row, nineIndex + 1] > _givenNumbers[row, nineIndex - 1])
                    {
                        PlaceTwoNumbersLeftAround(row, nineIndex - 1, nineIndex + 1);
                    }
                }
            }
        }

        private int FindNineIndexInRow(int row)
        {
            for (int col = 0; col < _gridSize; col++)
            {
                if (_givenNumbers[row, col] == 9)
                {
                    return col;
                }
            }
            return -1;
        }

        private void PlaceNumberLeftAround(int row, int number)
        {
            _viewModel.LeftNumberCells[row * 3].Number = number;
        }

        private void PlaceTwoNumbersLeftAround(int row, int firstCol, int secondCol)
        {
            _viewModel.LeftNumberCells[row * 3].Number = _givenNumbers[row, secondCol];
            _viewModel.LeftNumberCells[row * 3 + 1].Number = _givenNumbers[row, firstCol];
        }

        private void PlaceNumbersUp()
        {
            for (int col = 0; col < _gridSize; col++)
            {
                int nineIndex = FindNineIndexInCol(col);
                if (nineIndex != -1)
                {
                    if (nineIndex == 0)
                    {
                        PlaceNumberUpAround(col, _givenNumbers[1, col]);
                    }
                    else if (nineIndex == SudokuStore.Instance.Sudoku.Grid.Size - 1)
                    {
                        PlaceNumberUpAround(col, _givenNumbers[SudokuStore.Instance.Sudoku.Grid.Size - 2, col]);
                    }
                    else if (_givenNumbers[nineIndex + 1, col] > _givenNumbers[nineIndex - 1, col])
                    {
                        PlaceTwoNumbersUpAround(nineIndex + 1, nineIndex - 1, col);
                    }
                    else if (_givenNumbers[nineIndex + 1, col] < _givenNumbers[nineIndex - 1, col])
                    {
                        PlaceTwoNumbersUpAround(nineIndex - 1, nineIndex + 1, col);
                    }
                }
            }
        }

        private int FindNineIndexInCol(int col)
        {
            for (int row = 0; row < _gridSize; row++)
            {
                if (_givenNumbers[row, col] == 9)
                {
                    return row;
                }
            }
            return -1;
        }

        private void PlaceNumberUpAround(int col, int number)
        {
            _viewModel.UpNumberCells[col].Number = number;
        }

        private void PlaceTwoNumbersUpAround(int firstRow, int secondRow, int col)
        {
            _viewModel.UpNumberCells[col].Number = _givenNumbers[firstRow, col];
            _viewModel.UpNumberCells[SudokuStore.Instance.Sudoku.Grid.Size + col].Number = _givenNumbers[secondRow, col];
        }

        private void CreateSkyscrapersElems()
        {
            SkyscraperLeftRight();
            SkyscraperUpBottom();
        }

        private void SkyscraperLeftRight()
        {
            for (int row = 0; row < _gridSize; row++)
            {
                int count = 1;
                int actualNumber = _givenNumbers[row, 0];
                for (int col = 1; col < _gridSize; col++)
                {
                    if (actualNumber < _givenNumbers[row, col])
                    {
                        count++;
                        actualNumber = _givenNumbers[row, col];
                    }
                }
                PlaceNumberLeftAround(row, count);

                count = 1;
                actualNumber = _givenNumbers[row, SudokuStore.Instance.Sudoku.Grid.Size - 1];
                for (int col = SudokuStore.Instance.Sudoku.Grid.Size - 1; col > -1; col--)
                {
                    if (actualNumber < _givenNumbers[row, col])
                    {
                        count++;
                        actualNumber = _givenNumbers[row, col];
                    }
                }
                PlaceNumberRightAround(row, count);
            }
        }

        private void PlaceNumberRightAround(int row, int number)
        {
            _viewModel.RightNumberCells[row * 3].Number = number;
        }

        private void SkyscraperUpBottom()
        {
            for (int col = 0; col < _gridSize; col++)
            {
                int count = 1;
                int actualNumber = _givenNumbers[0, col];
                for (int row = 1; row < _gridSize; row++)
                {
                    if (actualNumber < _givenNumbers[row, col])
                    {
                        count++;
                        actualNumber = _givenNumbers[row, col];
                    }
                }
                PlaceNumberUpAround(col, count);

                count = 1;
                actualNumber = _givenNumbers[SudokuStore.Instance.Sudoku.Grid.Size - 1, col];
                for (int row = SudokuStore.Instance.Sudoku.Grid.Size - 1; row > -1; row--)
                {
                    if (actualNumber < _givenNumbers[row, col])
                    {
                        count++;
                        actualNumber = _givenNumbers[row, col];
                    }
                }
                PlaceNumberBottomAround(col, count);
            }
        }

        private void PlaceNumberBottomAround(int col, int number)
        {
            _viewModel.BottomNumberCells[col].Number = number;
        }
    }
}
