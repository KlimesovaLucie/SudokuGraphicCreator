using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Properties.Resources;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System.Collections.Generic;
using System.Windows.Media;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command is responsible for logic based on checking / unchecking <see cref="ElementCheckBox"/> button.
    /// </summary>
    public class VariantCheckBoxCommand : BaseCommand
    {
        private readonly ICreatingSudokuViewModel _viewModel;

        private readonly List<ClassicLineViewModel> _lines = new List<ClassicLineViewModel>();

        private readonly SudokuGrid _grid = SudokuStore.Instance.Sudoku.Grid;

        private readonly List<SudokuType> _variants = SudokuStore.Instance.Sudoku.Variants;

        /// <summary>
        /// Initializes a new instance of <see cref="VariantCheckBoxCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel class for <see cref="CreatingSudoku"/> view.</param>
        public VariantCheckBoxCommand(ICreatingSudokuViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        /// <summary>
        /// If button in <paramref name="parameter"/> is checked, show corresponding graphic elements and add variant to sudoku.
        /// If button in <paramref name="parameter"/> is unchecked, delete corresponding graphic elements and remove variant to sudoku.
        /// </summary>
        /// <param name="parameter">Name of button.</param>
        public override void Execute(object parameter)
        {
            string nameOfElement = parameter as string;
            if (parameter == null)
            {
                return;
            }

            foreach (var elem in _viewModel.SudokuVariantElementButton)
            {
                if (elem.ToString() == nameOfElement)
                {
                    if (elem.ToString() == Resources.SudokuDiagonal)
                    {
                        DiagonalVariant(elem.Checked);
                    }
                    else if (elem.ToString() == Resources.SudokuWindoku)
                    {
                        WindokuVariant(elem.Checked);
                    }
                    else if (elem.ToString() == Resources.SudokuAntiknight)
                    {
                        DiagonalAntiknight(elem.Checked);
                    }
                    else if (elem.ToString() == Resources.SudokuNonconsecutive)
                    {
                        NonconsecutiveVariant(elem.Checked);
                    }
                    else if (elem.ToString() == Resources.SudokuUntouchable)
                    {
                        UntouchableVariant(elem.Checked);
                    }
                    else if (elem.ToString() == Resources.SudokuDisjointGroups)
                    {
                        DisjointGroupsVariant(elem.Checked);
                    }
                }
            }
        }

        #region DIAGONAL
        private void DiagonalVariant(bool selected)
        {
            if (selected)
            {
                _viewModel.SelectedVariantsName.Add(Resources.SudokuDiagonal);
                _variants.Add(SudokuType.Diagonal);
                ShowDiagonalLines();
            }
            else if (_viewModel.SelectedVariantsName.Remove(Resources.SudokuDiagonal))
            {
                _variants.Remove(SudokuType.Diagonal);
                HideDiagonalLines();
            }
        }

        private void ShowDiagonalLines()
        {
            ClassicLineViewModel leftLine = new ClassicLineViewModel(0, 0,
                GridSizeStore.XCellSize * _grid.Size,
                GridSizeStore.YCellSize * _grid.Size,
                2.0, Brushes.Gray);
            _viewModel.GraphicElements.Add(leftLine);
            _lines.Add(leftLine);

            ClassicLineViewModel rightLine = new ClassicLineViewModel(GridSizeStore.XCellSize * _grid.Size, 0,
                0, GridSizeStore.YCellSize * _grid.Size,
                2.0, Brushes.Gray);
            _viewModel.GraphicElements.Add(rightLine);
            _lines.Add(rightLine);
        }

        private void HideDiagonalLines()
        {
            foreach (var elem in _lines)
            {
                _viewModel.GraphicElements.Remove(elem);
            }
            _lines.Clear();
        }
        #endregion

        #region WINDOKU
        private void WindokuVariant(bool selected)
        {
            if (selected)
            {
                _viewModel.SelectedVariantsName.Add(Resources.SudokuWindoku);
                _variants.Add(SudokuType.Windoku);
                ShowWindokuBoxes();
            }
            else if (_viewModel.SelectedVariantsName.Remove(Resources.SudokuWindoku))
            {
                _variants.Remove(SudokuType.Windoku);
                HideWindokuBoxes();
            }
        }

        private void ShowWindokuBoxes()
        {
            SetColorWindokuBoxes(1, 1, Brushes.DarkGray);
            SetColorWindokuBoxes(1, 5, Brushes.DarkGray);
            SetColorWindokuBoxes(5, 1, Brushes.DarkGray);
            SetColorWindokuBoxes(5, 5, Brushes.DarkGray);
        }

        private void HideWindokuBoxes()
        {
            SetColorWindokuBoxes(1, 1, Brushes.Transparent);
            SetColorWindokuBoxes(1, 5, Brushes.Transparent);
            SetColorWindokuBoxes(5, 1, Brushes.Transparent);
            SetColorWindokuBoxes(5, 5, Brushes.Transparent);
        }

        private void SetColorWindokuBoxes(int rowStart, int colStart, Brush brush)
        {
            for (int row = rowStart; row < rowStart + 3; row++)
            {
                for (int col = colStart; col < colStart + 3; col++)
                {
                    int index = row * _grid.Size + col;
                    _viewModel.GridCells[index].Background = brush;
                    _viewModel.GridCells[index].DefaultBrush = brush;
                }
            }
        }
        #endregion

        #region ANTIKNIGHT
        private void DiagonalAntiknight(bool selected)
        {
            if (selected)
            {
                _viewModel.SelectedVariantsName.Add(Resources.SudokuAntiknight);
                _variants.Add(SudokuType.Antiknight);
            }
            else
            {
                _viewModel.SelectedVariantsName.Remove(Resources.SudokuAntiknight);
                _variants.Remove(SudokuType.Antiknight);
            }
        }
        #endregion

        #region NONCONSECUTIVE
        private void NonconsecutiveVariant(bool selected)
        {
            if (selected)
            {
                _viewModel.SelectedVariantsName.Add(Resources.SudokuNonconsecutive);
                _variants.Add(SudokuType.Nonconsecutive);
            }
            else
            {
                _viewModel.SelectedVariantsName.Remove(Resources.SudokuNonconsecutive);
                _variants.Remove(SudokuType.Nonconsecutive);
            }
        }
        #endregion

        #region UNTOUCHABLE
        private void UntouchableVariant(bool selected)
        {
            if (selected)
            {
                _viewModel.SelectedVariantsName.Add(Resources.SudokuUntouchable);
                _variants.Add(SudokuType.Untouchable);
            }
            else
            {
                _viewModel.SelectedVariantsName.Remove(Resources.SudokuUntouchable);
                _variants.Remove(SudokuType.Untouchable);
            }
        }
        #endregion

        #region DISJOINGROUPS
        private void DisjointGroupsVariant(bool selected)
        {
            if (selected)
            {
                _viewModel.SelectedVariantsName.Add(Resources.SudokuDisjointGroups);
                _variants.Add(SudokuType.DisjointGroups);
            }
            else
            {
                _viewModel.SelectedVariantsName.Remove(Resources.SudokuDisjointGroups);
                _variants.Remove(SudokuType.DisjointGroups);
            }
        }
        #endregion
    }
}
