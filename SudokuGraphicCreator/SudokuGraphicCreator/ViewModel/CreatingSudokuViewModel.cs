using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.Properties.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SudokuGraphicCreator.ViewModel
{
    public class CreatingSudokuViewModel : BaseViewModel, ICreatingSudokuViewModel
    {
        private SudokuGrid _grid;

        private ObservableCollection<ElementControl> _sudokuVariantElementButton = new ObservableCollection<ElementControl>();

        public ObservableCollection<ElementControl> SudokuVariantElementButton
        {
            get => _sudokuVariantElementButton;
            set
            {
                _sudokuVariantElementButton = value;
                OnPropertyChanged(nameof(SudokuVariantElementButton));
            }
        }

        private ObservableCollection<ElementControl> _sudokuGraphicElementButton = new ObservableCollection<ElementControl>();

        public ObservableCollection<ElementControl> SudokuGraphicElementButton
        {
            get => _sudokuGraphicElementButton;
            set
            {
                _sudokuGraphicElementButton = value;
                OnPropertyChanged(nameof(SudokuGraphicElementButton));
            }
        }

        private ObservableCollection<ElementButtonConfirm> _buttonsConfirm = new ObservableCollection<ElementButtonConfirm>();

        /// <summary>
        /// Collection of <see cref="ElementButtonConfirm"/> buttons.
        /// </summary>
        public ObservableCollection<ElementButtonConfirm> ButtonsConfirm
        {
            get => _buttonsConfirm;
            set
            {
                _buttonsConfirm = value;
                OnPropertyChanged(nameof(ButtonsConfirm));
            }
        }

        private ElementButton _givenNumberButton;
        
        public ElementButton GivenNumberButton
        {
            get => _givenNumberButton;
            set
            {
                _givenNumberButton = value;
                OnPropertyChanged(nameof(GivenNumberButton));
            }
        }

        private ElementButtonConfirm _gridButton;

        public ElementButtonConfirm GridButton
        {
            get => _gridButton;
            set
            {
                _gridButton = value;
                OnPropertyChanged(nameof(GridButton));
            }
        }

        private ObservableCollection<SudokuElementViewModel> _tableElements = new ObservableCollection<SudokuElementViewModel>();

        public ObservableCollection<SudokuElementViewModel> GraphicElements
        {
            get => _tableElements;
            set
            {
                _tableElements = value;
                OnPropertyChanged(nameof(GraphicElements));
            }
        }

        private ObservableCollection<GridCellViewModel> _selectedCells = new ObservableCollection<GridCellViewModel>();

        public ObservableCollection<GridCellViewModel> SelectedCells
        {
            get => _selectedCells;
            set
            {
                _selectedCells = value;
                OnPropertyChanged(nameof(SelectedCells));
            }
        }

        public ObservableCollection<ObservableCollection<Tuple<int, int>>> Boxes
        {
            get => SudokuStore.Instance.Sudoku.Grid.Boxes;
            set
            {
                SudokuStore.Instance.Sudoku.Grid.Boxes = value;
                OnPropertyChanged(nameof(Boxes));
            }
        }

        public List<string> SelectedVariantsName { get; }

        private ObservableCollection<GridCellViewModel> _gridCells = new ObservableCollection<GridCellViewModel>();

        public ObservableCollection<GridCellViewModel> GridCells
        {
            get => _gridCells;
            set
            {
                _gridCells = value;
                OnPropertyChanged(nameof(GridCells));
            }
        }

        private ObservableCollection<CellNumberViewModel> _cellNumbers = new ObservableCollection<CellNumberViewModel>();

        public ObservableCollection<CellNumberViewModel> CellNumbers
        {
            get => _cellNumbers;
            set
            {
                _cellNumbers = value;
                OnPropertyChanged(nameof(CellNumbers));
            }
        }

        public ObservableCollection<ElementControl> _visibleVariantButtons = new ObservableCollection<ElementControl>();

        public ObservableCollection<ElementControl> VisibleSudokuVariantButton
        {
            get => _visibleVariantButtons;
        }

        #region LEFTGRID
        private ObservableCollection<GridCellViewModel> _leftGridCells = new ObservableCollection<GridCellViewModel>();

        public ObservableCollection<GridCellViewModel> LeftGridCells
        {
            get => _leftGridCells;
            set
            {
                _leftGridCells = value;
                OnPropertyChanged(nameof(LeftGridCells));
            }
        }

        private ObservableCollection<SudokuElementViewModel> _leftGraphicCells = new ObservableCollection<SudokuElementViewModel>();

        public ObservableCollection<SudokuElementViewModel> LeftGraphicCells
        {
            get => _leftGraphicCells;
            set
            {
                _leftGraphicCells = value;
                OnPropertyChanged(nameof(LeftGraphicCells));
            }
        }

        private ObservableCollection<CellNumberViewModel> _leftNumberCells = new ObservableCollection<CellNumberViewModel>();

        public ObservableCollection<CellNumberViewModel> LeftNumberCells
        {
            get => _leftNumberCells;
            set
            {
                _leftNumberCells = value;
                OnPropertyChanged(nameof(LeftNumberCells));
            }
        }
        #endregion

        #region RIGHTGRID
        private ObservableCollection<GridCellViewModel> _rightGridCells = new ObservableCollection<GridCellViewModel>();

        public ObservableCollection<GridCellViewModel> RightGridCells
        {
            get => _rightGridCells;
            set
            {
                _rightGridCells = value;
                OnPropertyChanged(nameof(_rightGridCells));
            }
        }

        private ObservableCollection<SudokuElementViewModel> _rightGraphicCells = new ObservableCollection<SudokuElementViewModel>();

        public ObservableCollection<SudokuElementViewModel> RightGraphicCells
        {
            get => _rightGraphicCells;
            set
            {
                _rightGraphicCells = value;
                OnPropertyChanged(nameof(RightGraphicCells));
            }
        }

        private ObservableCollection<CellNumberViewModel> _rightNumberCells = new ObservableCollection<CellNumberViewModel>();

        public ObservableCollection<CellNumberViewModel> RightNumberCells
        {
            get => _rightNumberCells;
            set
            {
                _rightNumberCells = value;
                OnPropertyChanged(nameof(RightNumberCells));
            }
        }
        #endregion

        #region UPGRID
        private ObservableCollection<GridCellViewModel> _upGridCells = new ObservableCollection<GridCellViewModel>();

        public ObservableCollection<GridCellViewModel> UpGridCells
        {
            get => _upGridCells;
            set
            {
                _upGridCells = value;
                OnPropertyChanged(nameof(UpGridCells));
            }
        }

        private ObservableCollection<SudokuElementViewModel> _upGraphicCells = new ObservableCollection<SudokuElementViewModel>();

        public ObservableCollection<SudokuElementViewModel> UpGraphicCells
        {
            get => _upGraphicCells;
            set
            {
                _upGraphicCells = value;
                OnPropertyChanged(nameof(UpGraphicCells));
            }
        }

        private ObservableCollection<CellNumberViewModel> _upNumberCells = new ObservableCollection<CellNumberViewModel>();

        public ObservableCollection<CellNumberViewModel> UpNumberCells
        {
            get => _upNumberCells;
            set
            {
                _upNumberCells = value;
                OnPropertyChanged(nameof(UpNumberCells));
            }
        }
        #endregion

        #region BOTTOMGRID
        private ObservableCollection<GridCellViewModel> _bottomGridCells = new ObservableCollection<GridCellViewModel>();

        public ObservableCollection<GridCellViewModel> BottomGridCells
        {
            get => _bottomGridCells;
            set
            {
                _bottomGridCells = value;
                OnPropertyChanged(nameof(BottomGridCells));
            }
        }

        private ObservableCollection<SudokuElementViewModel> _bottomGraphicCells = new ObservableCollection<SudokuElementViewModel>();

        public ObservableCollection<SudokuElementViewModel> BottomGraphicCells
        {
            get => _bottomGraphicCells;
            set
            {
                _bottomGraphicCells = value;
                OnPropertyChanged(nameof(BottomGraphicCells));
            }
        }

        private ObservableCollection<CellNumberViewModel> _bottomNumberCells = new ObservableCollection<CellNumberViewModel>();

        public ObservableCollection<CellNumberViewModel> BottomNumberCells
        {
            get => _bottomNumberCells;
            set
            {
                _bottomNumberCells = value;
                OnPropertyChanged(nameof(BottomNumberCells));
            }
        }

        private ObservableCollection<ElementControl> _graphicButtons = new ObservableCollection<ElementControl>();

        /// <summary>
        /// Collection with all buttons for creating sudoku graphic elements.
        /// </summary>
        public ObservableCollection<ElementControl> GraphicButtons
        {
            get => _graphicButtons;
            set
            {
                _graphicButtons = value;
                OnPropertyChanged(nameof(GraphicButtons));
            }
        }

        private ObservableCollection<ElementCheckBox> _checkboxButtons = new ObservableCollection<ElementCheckBox>();

        /// <summary>
        /// Collection with <see cref="ElementCheckBox"/> in which graphic element are everywhere.
        /// </summary>
        public ObservableCollection<ElementCheckBox> CheckboxButtons
        {
            get => _checkboxButtons;
            set
            {
                _checkboxButtons = value;
                OnPropertyChanged(nameof(CheckboxButtons));
            }
        }
        #endregion

        #region GRIDSIZE
        /// <summary>
        /// Size of central grid in <see cref="CreatingSudoku"/> view.
        /// </summary>
        public double CentralSize
        {
            get => GridSizeStore.XCellSize * SudokuStore.Instance.Sudoku.Grid.Size;
            set
            {
                OnPropertyChanged(nameof(CentralSize));
            }
        }

        /// <summary>
        /// Size of outside part of grid in <see cref="CreatingSudoku"/> view.
        /// </summary>
        public double OutsideSize
        {
            get => GridSizeStore.XCellSize * 3;
            set
            {
                OnPropertyChanged(nameof(OutsideSize));
            }
        }

        /// <summary>
        /// Size of grid in <see cref="CreatingSudoku"/> view.
        /// </summary>
        public double TotalSize
        {
            get => CentralSize + 2 * OutsideSize;
            set
            {
                OnPropertyChanged(nameof(TotalSize));
            }
        }
        #endregion

        #region COMMANDS
        /// <summary>
        /// Command for insert numbers and characters from keyboard.
        /// </summary>
        public ICommand InputFromKeyboardCommand { get; }

        /// <summary>
        /// Command for changing view to <see cref="StartScreen"/>.
        /// </summary>
        public ICommand GoStartScreenCommand { get; }

        /// <summary>
        /// Show <see cref="CreateSudokuSize"/> view.
        /// </summary>
        public ICommand NewSudokuSizeCommand { get; }

        /// <summary>
        /// Starts computation of count of solution.
        /// </summary>
        public ICommand CountOfSolutionsCommand { get; }

        /// <summary>
        /// Show <see cref="InsertGivenNumbers"/> view.
        /// </summary>
        public ICommand InsertGivenNumberCommand { get; }

        /// <summary>
        /// Show <see cref="InsertSolution"/> view.
        /// </summary>
        public ICommand InsertSolutionCommand { get; }

        /// <summary>
        /// Action for selecting cells or placing elements.
        /// </summary>
        public ICommand CellActionCommand { get; }

        /// <summary>
        /// Command for checked only one button.
        /// </summary>
        public ICommand ElementButtonSelectedCommand { get; }

        /// <summary>
        /// Export sudoku.
        /// </summary>
        public ICommand ExportCommand { get; }

        /// <summary>
        /// End of application.
        /// </summary>
        public ICommand EndCommand { get; }

        /// <summary>
        /// Show <see cref="AboutApp"/> view.
        /// </summary>
        public ICommand ShowAboutAppWindow { get; }

        /// <summary>
        /// Show <see cref="ExportGivenNumbers"/> view.
        /// </summary>
        public ICommand ExportGivenNumbersCommand { get; }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="CreatingSudokuViewModel"/> class.
        /// </summary>
        public CreatingSudokuViewModel()
        {
            _grid = SudokuStore.Instance.Sudoku.Grid;
            SelectedVariantsName = new List<string>();
            GoStartScreenCommand = new NavigateCommand(CreateStartScreenViewModel);
            NewSudokuSizeCommand = new ActionCommand(_ => DisplayCreateSudokuSizeWindow(), _ => true);
            CountOfSolutionsCommand = new CountSolutionCommand();
            InsertGivenNumberCommand = new ActionCommand(_ => DisplayInsertGivenNumbersWindow(), _ => true);
            InsertSolutionCommand = new ActionCommand(_ => DisplayInsertSolutionWindow(), _ => true);
            CellActionCommand = new SelectedCellCommand(this);
            ElementButtonSelectedCommand = new ElementButtonCommand(this);
            InputFromKeyboardCommand = new KeyboardCommand(this);
            ExportCommand = new ExportSudokuImageCommand();
            CreateViewElements();
            EndCommand = new ActionCommand(_ => { Application.Current.Shutdown(); }, _ => true);
            ShowAboutAppWindow = new ActionCommand(_ => { App.DialogService.ShowDialog(new AboutAppViewModel()); }, _ => true);
            ExportGivenNumbersCommand = new ActionCommand(_ => { App.DialogService.ShowDialog(new ExportGivenNumbersViewModel()); }, _ => true);
        }

        public void NotifyResizeGrid()
        {
            OnPropertyChanged(nameof(CentralSize));
            OnPropertyChanged(nameof(OutsideSize));
            OnPropertyChanged(nameof(TotalSize));
        }

        private StartScreenViewModel CreateStartScreenViewModel()
        {
            return new StartScreenViewModel();
        }

        private void DisplayCreateSudokuSizeWindow()
        {
            App.DialogService.ShowDialog(new CreateSudokuSizeViewModel());
        }

        private void DisplayInsertGivenNumbersWindow()
        {
            App.DialogService.ShowDialog(new InsertGivenNumbersViewModel(this));
        }

        private void DisplayInsertSolutionWindow()
        {
            App.DialogService.ShowDialog(new InsertSolutionViewModel(this));
        }

        private void CreateViewElements()
        {
            CreateGivenNumberButton();
            CreateGridNumber();
            CreateVariantButtons();
            CreateGraphicElementButtons();
            CreateGridCells();
            CreateCellNumbers();
            AllCheckButtons();
            CreateCellsAroundGrid();
        }

        private void CreateGivenNumberButton()
        {
            GivenNumberButton = new ElementButton(Resources.MenuInsertNumber, "/Icons/Number.svg", this);
        }

        private void CreateGridNumber()
        {
            GridButton = new ElementButtonConfirm(Resources.SudokuIrregular, "/Icons/IrregularGrid.svg", this);
            ButtonsConfirm.Add(GridButton);
        }

        private void CreateVariantButtons()
        {
            var diagonal = new ElementCheckBox(Resources.SudokuDiagonal, this);
            CheckboxButtons.Add(diagonal);
            SudokuVariantElementButton.Add(diagonal);

            var windoku = new ElementCheckBox(Resources.SudokuWindoku, this, SudokuStore.Instance.Sudoku.Grid.Size == 9);
            SudokuVariantElementButton.Add(windoku);
            CheckboxButtons.Add(windoku);

            var antiknight = new ElementCheckBox(Resources.SudokuAntiknight, this);
            SudokuVariantElementButton.Add(antiknight);
            CheckboxButtons.Add(antiknight);

            var nonconsecutive = new ElementCheckBox(Resources.SudokuNonconsecutive, this);
            SudokuVariantElementButton.Add(nonconsecutive);
            CheckboxButtons.Add(nonconsecutive);

            var untouchable = new ElementCheckBox(Resources.SudokuUntouchable, this);
            SudokuVariantElementButton.Add(untouchable);
            CheckboxButtons.Add(untouchable);

            var disjointGroups = new ElementCheckBox(Resources.SudokuDisjointGroups, this);
            SudokuVariantElementButton.Add(disjointGroups);
            CheckboxButtons.Add(disjointGroups);

            SudokuVariantElementButton.Add(new ElementButton(Resources.SudokuSum, "/Icons/Sum.svg", this));
            SudokuVariantElementButton.Add(new ElementButton(Resources.SudokuDifference, "/Icons/Difference.svg", this));
            SudokuVariantElementButton.Add(new ElementButton(Resources.SudokuConsecutive, "/Icons/WhiteCircle.svg", this, 15, 15));

            ObservableCollection<ElementButtonOption> graphicOptionsKropki = new ObservableCollection<ElementButtonOption>();
            var blackKropki = new ElementButtonOption("/Icons/BlackCircle.svg",
                Resources.SudokuKropki + Resources.ElemBlackCircle, this);
            graphicOptionsKropki.Add(blackKropki);
            GraphicButtons.Add(blackKropki);
            SudokuVariantElementButton.Add(new ElementButtonGraphicOptions(Resources.SudokuKropki,
                Resources.SudokuKropki + Resources.ElemWhiteCircle, this,
                "/Icons/WhiteCircle.svg", graphicOptionsKropki, 15, 15));

            ObservableCollection<ElementButtonOption> graphicOptionsGreaterThan = new ObservableCollection<ElementButtonOption>();
            var greaterRight = new ElementButtonOption("/Icons/GreaterThanRight.svg",
                Resources.SudokuGreaterThan + Resources.MenuRight, this);
            graphicOptionsGreaterThan.Add(greaterRight);
            GraphicButtons.Add(greaterRight);
            var greaterUp = new ElementButtonOption("/Icons/GreaterThanUp.svg",
                Resources.SudokuGreaterThan + Resources.MenuUp, this);
            graphicOptionsGreaterThan.Add(greaterUp);
            GraphicButtons.Add(greaterUp);
            var greaterDown = new ElementButtonOption("/Icons/GreaterThanDown.svg",
                Resources.SudokuGreaterThan + Resources.MenuDown, this);
            graphicOptionsGreaterThan.Add(greaterDown);
            GraphicButtons.Add(greaterDown);
            SudokuVariantElementButton.Add(new ElementButtonGraphicOptions(Resources.SudokuGreaterThan,
                Resources.SudokuGreaterThan + Resources.MenuLeft, this,
                "/Icons/GreaterThanLeft.svg", graphicOptionsGreaterThan, 20, 20));

            ObservableCollection<ElementButtonOption> graphicOptionsXV = new ObservableCollection<ElementButtonOption>();
            var v = new ElementButtonOption("/Icons/V.svg", Resources.SudokuXV + Resources.MenuV, this);
            graphicOptionsXV.Add(v);
            GraphicButtons.Add(v);
            SudokuVariantElementButton.Add(new ElementButtonGraphicOptions(Resources.SudokuXV,
                Resources.SudokuXV + Resources.MenuX, this, "/Icons/X.svg", graphicOptionsXV, 20, 20));
            SudokuVariantElementButton.Add(new ElementButton(Resources.SudokuOdd, "/Icons/GreyCircle.svg", this));
            SudokuVariantElementButton.Add(new ElementButton(Resources.SudokuEven, "/Icons/GreySquare.svg", this));
            SudokuVariantElementButton.Add(new ElementButton(Resources.SudokuStarProducts, "/Icons/Star.svg", this));

            ObservableCollection<ElementButtonOption> graphicOptionsSearchNine = new ObservableCollection<ElementButtonOption>();
            var arrowRight = new ElementButtonOption("/Icons/ArrowRight.svg", Resources.SudokuSearchNine + Resources.MenuRight, this);
            graphicOptionsSearchNine.Add(arrowRight);
            GraphicButtons.Add(arrowRight);
            var arrowUp = new ElementButtonOption("/Icons/ArrowUp.svg", Resources.SudokuSearchNine + Resources.MenuUp, this);
            graphicOptionsSearchNine.Add(arrowUp);
            GraphicButtons.Add(arrowUp);
            var arrowDown = new ElementButtonOption("/Icons/ArrowDown.svg", Resources.SudokuSearchNine + Resources.MenuDown, this);
            graphicOptionsSearchNine.Add(arrowDown);
            GraphicButtons.Add(arrowDown);
            SudokuVariantElementButton.Add(new ElementButtonGraphicOptions(Resources.SudokuSearchNine,
                Resources.SudokuSearchNine + Resources.MenuLeft, this,
                "/Icons/ArrowLeft.svg", graphicOptionsSearchNine));

            var palindrome = new ElementButtonConfirm(Resources.SudokuPalindrome, "/Icons/GreyLine.svg", this);
            SudokuVariantElementButton.Add(palindrome);
            ButtonsConfirm.Add(palindrome);

            var sequence = new ElementButtonConfirm(Resources.SudokuSequence, "/Icons/GreyLine.svg", this);
            SudokuVariantElementButton.Add(sequence);
            ButtonsConfirm.Add(sequence);

            var arrow = new ElementButtonConfirm(Resources.SudokuArrow, "/Icons/CircleWithNumber.svg", this);
            SudokuVariantElementButton.Add(arrow);
            ButtonsConfirm.Add(arrow);

            var thermometer = new ElementButtonConfirm(Resources.SudokuThermometer, "/Icons/Thermometer.svg", this);
            SudokuVariantElementButton.Add(thermometer);
            ButtonsConfirm.Add(thermometer);

            var extraRegion = new ElementButtonConfirm(Resources.SudokuExtraRegions, "/Icons/GreyCell.svg", this);
            SudokuVariantElementButton.Add(extraRegion);
            ButtonsConfirm.Add(extraRegion);

            var killer = new ElementButtonConfirm(Resources.SudokuKiller, "/Icons/Killer.svg", this);
            SudokuVariantElementButton.Add(killer);
            ButtonsConfirm.Add(killer);

            ObservableCollection<ElementButtonOption> graphicOptionsLittleKiller = new ObservableCollection<ElementButtonOption>();
            var littleLeftUp = new ElementButtonOption("/Icons/LittleKillerLeftUp.svg",
                Resources.SudokuLittleKiller + Resources.MenuLeftUp, this);
            GraphicButtons.Add(littleLeftUp); ;
            graphicOptionsLittleKiller.Add(littleLeftUp);
            
            var littleRightDown = new ElementButtonOption("/Icons/LittleKillerDownRight.svg",
                Resources.SudokuLittleKiller + Resources.MenuRightDown, this);
            graphicOptionsLittleKiller.Add(littleRightDown);
            GraphicButtons.Add(littleRightDown);

            var littleRightUp = new ElementButtonOption("/Icons/LittleKillerRightUp.svg",
                Resources.SudokuLittleKiller + Resources.MenuRightUp, this);
            graphicOptionsLittleKiller.Add(littleRightUp);
            GraphicButtons.Add(littleRightUp);
            SudokuVariantElementButton.Add(new ElementButtonGraphicOptions(Resources.SudokuLittleKiller,
                Resources.SudokuLittleKiller + Resources.MenuLeftDown, this,
                "/Icons/LittleKillerleftdown.svg", graphicOptionsLittleKiller));
            SudokuVariantElementButton.Add(new ElementButton(Resources.SudokuSkyscrapers, "/Icons/Skyscrapers.svg", this));
            SudokuVariantElementButton.Add(new ElementButton(Resources.SudokuNextToNine, "/Icons/NextToNine.svg", this));
            SudokuVariantElementButton.Add(new ElementButton(Resources.SudokuOutside, "/Icons/Outside.svg", this));
        }

        private void CreateGraphicElementButtons()
        {
            SudokuGraphicElementButton.Add(new ElementButton(Resources.Number, "/Icons/Number.svg", this));
            SudokuGraphicElementButton.Add(new ElementButton(Resources.ElemCircleWithNumber, "/Icons/Sum.svg", this));
            SudokuGraphicElementButton.Add(new ElementButton(Resources.ElemWhiteCircle, "/Icons/WhiteCircle.svg", this, 15, 15));
            SudokuGraphicElementButton.Add(new ElementButton(Resources.ElemBlackCircle, "/Icons/BlackCircle.svg", this, 15, 15));
            SudokuGraphicElementButton.Add(new ElementButton(Resources.ElemGreyCircle, "/Icons/GreyCircle.svg", this));
            SudokuGraphicElementButton.Add(new ElementButton(Resources.ElemCircleWithGreyEdge, "/Icons/CircleWithGreyEdge.svg", this));

            var arrowWithCircle = new ElementButtonConfirm(Resources.ElemArrowWithCircle, "/Icons/CircleWithArrow.svg", this);
            SudokuGraphicElementButton.Add(arrowWithCircle);
            ButtonsConfirm.Add(arrowWithCircle);

            ObservableCollection<ElementButtonOption> graphicOptionsBoldArrow = new ObservableCollection<ElementButtonOption>();
            var arrowRight = new ElementButtonOption("/Icons/ArrowRight.svg", Resources.ElemBoldArrow + Resources.MenuRight, this);
            graphicOptionsBoldArrow.Add(arrowRight);
            GraphicButtons.Add(arrowRight);

            var arrowUp = new ElementButtonOption("/Icons/ArrowUp.svg", Resources.ElemBoldArrow + Resources.MenuUp, this);
            graphicOptionsBoldArrow.Add(arrowUp);
            GraphicButtons.Add(arrowUp);

            var arrowDown = new ElementButtonOption("/Icons/ArrowDown.svg", Resources.ElemBoldArrow + Resources.MenuDown, this);
            graphicOptionsBoldArrow.Add(arrowDown);
            GraphicButtons.Add(arrowDown);

            SudokuGraphicElementButton.Add(new ElementButtonGraphicOptions(Resources.ElemBoldArrow,
                Resources.ElemBoldArrow + Resources.MenuLeft, this,
                "/Icons/ArrowLeft.svg", graphicOptionsBoldArrow));

            var longArrow = new ElementButtonConfirm(Resources.ElemLongArrow, "/Icons/LongArrow.svg", this);
            SudokuGraphicElementButton.Add(longArrow);
            ButtonsConfirm.Add(longArrow);

            ObservableCollection<ElementButtonOption> graphicOptionsSmallArrow = new ObservableCollection<ElementButtonOption>();
            var arrowLeftUp = new ElementButtonOption("/Icons/SmallArrowLeftUp.svg",
                Resources.ElemSmallArrow + Resources.MenuLeftUp, this);
            graphicOptionsSmallArrow.Add(arrowLeftUp);
            GraphicButtons.Add(arrowLeftUp);

            var arrowRightDown = new ElementButtonOption("/Icons/SmallArrowRightDown.svg",
                Resources.ElemSmallArrow + Resources.MenuRightDown, this);
            graphicOptionsSmallArrow.Add(arrowRightDown);
            GraphicButtons.Add(arrowRightDown);

            var arrowRightUp = new ElementButtonOption("/Icons/SmallArrowRightUp.svg",
                Resources.ElemSmallArrow + Resources.MenuRightUp, this);
            graphicOptionsSmallArrow.Add(arrowRightUp);
            GraphicButtons.Add(arrowRightUp);

            SudokuGraphicElementButton.Add(new ElementButtonGraphicOptions(Resources.ElemSmallArrow,
                Resources.ElemSmallArrow + Resources.MenuLeftDown, this,
                "/Icons/SmallArrowLeftDown.svg", graphicOptionsSmallArrow, 15, 15));

            SudokuGraphicElementButton.Add(new ElementButton(Resources.ElemCharacters, "/Icons/Character.svg", this));
            SudokuGraphicElementButton.Add(new ElementButton(Resources.ElemSquare, "/Icons/GreySquare.svg", this));
            SudokuGraphicElementButton.Add(new ElementButton(Resources.ElemStar, "/Icons/Star.svg", this));

            var line = new ElementButtonConfirm(Resources.ElemLine, "/Icons/GreyLine.svg", this);
            SudokuGraphicElementButton.Add(line);
            ButtonsConfirm.Add(line);

            var cage = new ElementButtonConfirm(Resources.ElemCage, "/Icons/Cage.svg", this);
            SudokuGraphicElementButton.Add(cage);
            ButtonsConfirm.Add(cage);

            SudokuGraphicElementButton.Add(new ElementButton(Resources.ElemGreyCell, "/Icons/GreyCell.svg", this));
        }

        private void CreateGridCells()
        {
            for (int row = 0; row < _grid.Size; row++)
            {
                for (int col = 0; col < _grid.Size; col++)
                {
                    GridCellViewModel cell = new GridCellViewModel(row, col, row * SudokuStore.Instance.Sudoku.Grid.Size + col, col * GridSizeStore.XCellSize, row * GridSizeStore.YCellSize,
                        Brushes.Transparent, GridSizeStore.YCellSize, GridSizeStore.XCellSize, CalculateThickness(row, col));
                    GridCells.Add(cell);
                }
            }
        }

        private Thickness CalculateThickness(int row, int col)
        {
            Thickness result = new Thickness();
            double actualBoldLineRow = GridSizeStore.BoldLine;
            double actualBoldLineCol = GridSizeStore.BoldLine;
            if (row == 0 || row + 1 == _grid.Size)
            {
                actualBoldLineRow *= 2;
            }
            if (col == 0 || col + 1 == _grid.Size)
            {
                actualBoldLineCol *= 2;
            }
            result.Left = (col % _grid.XBoxCells == 0) ? actualBoldLineCol : GridSizeStore.NormalLine;
            result.Top = (row % _grid.YBoxCells == 0) ? actualBoldLineRow : GridSizeStore.NormalLine;
            result.Right = ((col + 1) % _grid.XBoxCells == 0) ? actualBoldLineCol : GridSizeStore.NormalLine;
            result.Bottom = ((row + 1) % _grid.YBoxCells == 0) ? actualBoldLineRow : GridSizeStore.NormalLine;
            return result;
        }

        private void CreateCellNumbers()
        {
            for (int row = 0; row < _grid.Size; row++)
            {
                for (int col = 0; col < _grid.Size; col++)
                {
                    CellNumberViewModel cell = new CellNumberViewModel(col * GridSizeStore.XCellSize, row * GridSizeStore.YCellSize, GridSizeStore.YCellSize, GridSizeStore.XCellSize,
                        SudokuStore.Instance.Sudoku.GivenNumbers, SudokuStore.Instance.Sudoku.GridNumbersType);
                    CellNumbers.Add(cell);
                    int x = col / SudokuStore.Instance.Sudoku.Grid.XBoxCells;
                    int y = row / SudokuStore.Instance.Sudoku.Grid.YBoxCells;
                    int index = x * SudokuStore.Instance.Sudoku.Grid.XBoxCells + y;
                }
            }
        }

        private void AllCheckButtons()
        {
            foreach (var button in SudokuVariantElementButton)
            {
                VisibleSudokuVariantButton.Add(button);
            }
        }

        private void CreateCellsAroundGrid()
        {
            CreateLeftGrid();
            CreateRightGrid();
            CreateUpGrid();
            CreateBottomGrid();
        }

        private void CreateLeftGrid()
        {
            double gridSizeX = 2 * GridSizeStore.XCellSize;
            for (int row = 0; row < SudokuStore.Instance.Sudoku.Grid.Size; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    LeftNumberCells.Add(new CellNumberViewModel(gridSizeX - (col * GridSizeStore.XCellSize), row * GridSizeStore.YCellSize, GridSizeStore.YCellSize,
                        GridSizeStore.XCellSize, SudokuStore.Instance.Sudoku.LeftNumbers, SudokuStore.Instance.Sudoku.LeftNumbersType));
                    LeftGridCells.Add(new GridCellViewModel(row, col, row * 3 + col, gridSizeX - (col * GridSizeStore.XCellSize), row * GridSizeStore.YCellSize, Brushes.Transparent,
                        GridSizeStore.YCellSize, GridSizeStore.XCellSize, new Thickness(0)));
                }
            }
        }

        private void CreateRightGrid()
        {
            for (int row = 0; row < SudokuStore.Instance.Sudoku.Grid.Size; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    RightNumberCells.Add(new CellNumberViewModel(col * GridSizeStore.XCellSize, row * GridSizeStore.YCellSize, GridSizeStore.YCellSize,
                        GridSizeStore.XCellSize, SudokuStore.Instance.Sudoku.RightNumbers, SudokuStore.Instance.Sudoku.RightNumbersType));
                    RightGridCells.Add(new GridCellViewModel(row, col, row * 3 + col, col * GridSizeStore.XCellSize, row * GridSizeStore.YCellSize, Brushes.Transparent,
                        GridSizeStore.YCellSize, GridSizeStore.XCellSize, new Thickness(0)));
                }
            }
        }

        private void CreateUpGrid()
        {
            double gridSizeY = 2 * GridSizeStore.YCellSize;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < SudokuStore.Instance.Sudoku.Grid.Size; col++)
                {
                    UpNumberCells.Add(new CellNumberViewModel(col * GridSizeStore.XCellSize, gridSizeY - (row * GridSizeStore.YCellSize), GridSizeStore.YCellSize,
                        GridSizeStore.XCellSize, SudokuStore.Instance.Sudoku.UpNumbers, SudokuStore.Instance.Sudoku.UpNumbersType));
                    UpGridCells.Add(new GridCellViewModel(row, col, row * SudokuStore.Instance.Sudoku.Grid.Size + col, col * GridSizeStore.XCellSize, gridSizeY - (row * GridSizeStore.YCellSize),
                        Brushes.Transparent, GridSizeStore.YCellSize, GridSizeStore.XCellSize, new Thickness(0)));
                }
            }
        }

        private void CreateBottomGrid()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < SudokuStore.Instance.Sudoku.Grid.Size; col++)
                {
                    BottomNumberCells.Add(new CellNumberViewModel(col * GridSizeStore.XCellSize, row * GridSizeStore.YCellSize, GridSizeStore.YCellSize, GridSizeStore.XCellSize,
                        SudokuStore.Instance.Sudoku.BottomNumbers, SudokuStore.Instance.Sudoku.BottomNumbersType));
                    BottomGridCells.Add(new GridCellViewModel(row, col, row * SudokuStore.Instance.Sudoku.Grid.Size + col, col * GridSizeStore.XCellSize, row * GridSizeStore.YCellSize,
                        Brushes.Transparent, GridSizeStore.YCellSize, GridSizeStore.XCellSize, new Thickness(0)));
                }
            }
        }
    }
}
