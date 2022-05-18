using SudokuGraphicCreator.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SudokuGraphicCreator.ViewModel
{
    public interface ICreatingSudokuViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Selected cells in sudoku grid.
        /// </summary>
        ObservableCollection<GridCellViewModel> SelectedCells { get; }

        /// <summary>
        /// Buttons in <see cref="CreatingSudoku"/> view for creating graphic elements of variants.
        /// </summary>
        ObservableCollection<ElementControl> SudokuVariantElementButton { get; }

        /// <summary>
        /// Visible variant buttons.
        /// </summary>
        ObservableCollection<ElementControl> VisibleSudokuVariantButton { get; }

        /// <summary>
        /// Buttons in <see cref="CreatingSudoku"/> view for creating graphic elements of variants.
        /// </summary>
        ObservableCollection<ElementControl> SudokuGraphicElementButton { get; }

        /// <summary>
        /// Collection of viewModels of graphic elemnets of sudoku.
        /// </summary>
        ObservableCollection<SudokuElementViewModel> GraphicElements { get; }

        /// <summary>
        /// Collection of cells of boxes in grid.
        /// </summary>
        ObservableCollection<ObservableCollection<Tuple<int, int>>> Boxes { get; }

        /// <summary>
        /// Name of selected variant.
        /// </summary>
        List<string> SelectedVariantsName { get; }

        /// <summary>
        /// Collection with cells with number.
        /// </summary>
        ObservableCollection<CellNumberViewModel> CellNumbers { get; }

        /// <summary>
        /// Collection with cells that create grid.
        /// </summary>
        ObservableCollection<GridCellViewModel> GridCells { get; }

        /// <summary>
        /// Button for inserting given numbers.
        /// </summary>
        ElementButton GivenNumberButton { get; }

        /// <summary>
        /// Button for modifying grid.
        /// </summary>
        ElementButtonConfirm GridButton { get; }

        /// <summary>
        /// Cells of grid placed in left of central part.
        /// </summary>
        ObservableCollection<GridCellViewModel> LeftGridCells { get; }

        /// <summary>
        /// Collection of graphic elements placed in left part of central grid.
        /// </summary>
        ObservableCollection<SudokuElementViewModel> LeftGraphicCells { get; }

        /// <summary>
        /// Collection of given number placed in left part of central grid.
        /// </summary>
        ObservableCollection<CellNumberViewModel> LeftNumberCells { get; }

        /// <summary>
        /// Cells of grid placed in up of central part.
        /// </summary>
        ObservableCollection<GridCellViewModel> UpGridCells { get; }

        /// <summary>
        /// Collection of graphic elements placed in up part of central grid.
        /// </summary>
        ObservableCollection<SudokuElementViewModel> UpGraphicCells { get; }

        /// <summary>
        /// Collection of given number placed in up part of central grid.
        /// </summary>
        ObservableCollection<CellNumberViewModel> UpNumberCells { get; }

        /// <summary>
        /// Cells of grid placed in right of central part.
        /// </summary>
        ObservableCollection<GridCellViewModel> RightGridCells { get; }

        /// <summary>
        /// Collection of graphic elements placed in right part of central grid.
        /// </summary>
        ObservableCollection<SudokuElementViewModel> RightGraphicCells { get; }

        /// <summary>
        /// Collection of given number placed in right part of central grid.
        /// </summary>
        ObservableCollection<CellNumberViewModel> RightNumberCells { get; }

        /// <summary>
        /// Cells of grid placed in bottom of central part.
        /// </summary>
        ObservableCollection<GridCellViewModel> BottomGridCells { get; }

        /// <summary>
        /// Collection of graphic elements placed in bottom part of central grid.
        /// </summary>
        ObservableCollection<SudokuElementViewModel> BottomGraphicCells { get; }

        /// <summary>
        /// Collection of given number placed in bottom part of central grid.
        /// </summary>
        ObservableCollection<CellNumberViewModel> BottomNumberCells { get; }
    }
}
