using SudokuGraphicCreator.Stores;
using System;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// ViewModel class for <see cref="MainWindow"/> view.
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// Actual view on screen.
        /// </summary>
        public BaseViewModel SelectedViewModel => NavigationStore.Instance.CurrentViewModel;

        /// <summary>
        /// Initializes a new instance of <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            NavigationStore.Instance.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(SelectedViewModel));
        }

        /// <summary>
        /// When window size change, resize grid and all elements.
        /// </summary>
        /// <param name="previous">Previous size.</param>
        /// <param name="actual">New size.</param>
        public void ResizeSudokuGrid(System.Windows.Size previous, System.Windows.Size actual)
        {
            if (previous.Width == 0 && previous.Height == 0)
            {
                // inicial state
                return;
            }

            double ratio = Math.Round(actual.Width / previous.Width, 1);
            ResizeGridSizeStore(ratio);
            CreatingSudokuViewModel actualViewModel = SelectedViewModel as CreatingSudokuViewModel;
            if (actualViewModel != null)
            {
                ResizeGridCells(actualViewModel, ratio);
                ResizeCellNumbers(actualViewModel, ratio);
                ResizeGraphicElement(actualViewModel, ratio);
                actualViewModel.NotifyResizeGrid();
            }
        }

        private void ResizeGridSizeStore(double ratio)
        {
            GridSizeStore.XCellSize *= ratio;
            GridSizeStore.YCellSize *= ratio;
            GridSizeStore.InCellElementSize *= ratio;
            GridSizeStore.OnEdgeTextSize *= ratio;
            GridSizeStore.CageOffset *= ratio;
            GridSizeStore.KillerNumberTextSize *= ratio;
            GridSizeStore.KillerNumberSize *= ratio;
            GridSizeStore.SmallCircleSize *= ratio;
            GridSizeStore.InCellTextSize *= ratio;
            GridSizeStore.CircleSize *= ratio;
            GridSizeStore.ThermometerThickness *= ratio;
        }

        private void ResizeGridCells(CreatingSudokuViewModel actualViewModel, double ratio)
        {
            foreach (var elem in actualViewModel.GridCells)
            {
                elem.Height *= ratio;
                elem.Width *= ratio;
                elem.Left *= ratio;
                elem.Top *= ratio;
            }

            foreach (var elem in actualViewModel.LeftGridCells)
            {
                elem.Height *= ratio;
                elem.Width *= ratio;
                elem.Left *= ratio;
                elem.Top *= ratio;
            }

            foreach (var elem in actualViewModel.RightGridCells)
            {
                elem.Height *= ratio;
                elem.Width *= ratio;
                elem.Left *= ratio;
                elem.Top *= ratio;
            }

            foreach (var elem in actualViewModel.UpGridCells)
            {
                elem.Height *= ratio;
                elem.Width *= ratio;
                elem.Left *= ratio;
                elem.Top *= ratio;
            }

            foreach (var elem in actualViewModel.BottomGridCells)
            {
                elem.Height *= ratio;
                elem.Width *= ratio;
                elem.Left *= ratio;
                elem.Top *= ratio;
            }
        }

        private void ResizeCellNumbers(CreatingSudokuViewModel actualViewModel, double ratio)
        {
            foreach (var elem in actualViewModel.CellNumbers)
            {
                elem.Width *= ratio;
                elem.Height *= ratio;
                elem.Left *= ratio;
                elem.Top *= ratio;
                elem.TextSize *= ratio;
            }

            foreach (var elem in actualViewModel.LeftNumberCells)
            {
                elem.Width *= ratio;
                elem.Height *= ratio;
                elem.Left *= ratio;
                elem.Top *= ratio;
                elem.TextSize *= ratio;
            }

            foreach (var elem in actualViewModel.RightNumberCells)
            {
                elem.Width *= ratio;
                elem.Height *= ratio;
                elem.Left *= ratio;
                elem.Top *= ratio;
                elem.TextSize *= ratio;
            }

            foreach (var elem in actualViewModel.UpNumberCells)
            {
                elem.Width *= ratio;
                elem.Height *= ratio;
                elem.Left *= ratio;
                elem.Top *= ratio;
                elem.TextSize *= ratio;
            }

            foreach (var elem in actualViewModel.BottomNumberCells)
            {
                elem.Width *= ratio;
                elem.Height *= ratio;
                elem.Left *= ratio;
                elem.Top *= ratio;
                elem.TextSize *= ratio;
            }
        }

        private void ResizeGraphicElement(CreatingSudokuViewModel actualViewModel, double ratio)
        {
            foreach (var elem in actualViewModel.GraphicElements)
            {
                ResizeElement(elem, ratio);
            }

            foreach (var elem in actualViewModel.LeftGraphicCells)
            {
                ResizeElement(elem, ratio);
            }

            foreach (var elem in actualViewModel.RightGraphicCells)
            {
                ResizeElement(elem, ratio);
            }

            foreach (var elem in actualViewModel.UpGraphicCells)
            {
                ResizeElement(elem, ratio);
            }

            foreach (var elem in actualViewModel.BottomGraphicCells)
            {
                ResizeElement(elem, ratio);
            }
        }

        private void ResizeElement(SudokuElementViewModel elem, double ratio)
        {
            ClassicLineViewModel classicLine = elem as ClassicLineViewModel;
            if (classicLine != null)
            {
                classicLine.Resize(ratio);
                return;
            }

            CircleWithNumberViewModel circleWithNumber = elem as CircleWithNumberViewModel;
            if (circleWithNumber != null)
            {
                circleWithNumber.Resize(ratio);
                return;
            }

            CircleViewModel circle = elem as CircleViewModel;
            if (circle != null)
            {
                circle.Resize(ratio);
                return;
            }

            CharacterViewModel character = elem as CharacterViewModel;
            if (character != null)
            {
                character.Resize(ratio);
                return;
            }

            GreySquareViewModel square = elem as GreySquareViewModel;
            if (square != null)
            {
                square.Resize(ratio);
                return;
            }

            StarViewModel star = elem as StarViewModel;
            if (star != null)
            {
                star.Resize(ratio);
                return;
            }

            BoldArrowViewModel boldArrow = elem as BoldArrowViewModel;
            if (boldArrow != null)
            {
                boldArrow.Resize(ratio);
                return;
            }

            LineViewModel line = elem as LineViewModel;
            if (line != null)
            {
                line.Resize(ratio);
                return;
            }

            LongArrowViewModel longArrow = elem as LongArrowViewModel;
            if (longArrow != null)
            {
                longArrow.Resize();
                return;
            }

            LongArrowWithCircleViewModel arrowWithCircle = elem as LongArrowWithCircleViewModel;
            if (arrowWithCircle != null)
            {
                arrowWithCircle.Resize(ratio);
                return;
            }

            CageViewModel cage = elem as CageViewModel;
            if (cage != null)
            {
                cage.Resize(ratio);
                return;
            }

            SmallArrowViewModel smallArrow = elem as SmallArrowViewModel;
            if (smallArrow != null)
            {
                smallArrow.Resize(ratio);
                return;
            }
        }
    }
}
