using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.ViewModel;
using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.Stores
{
    /// <summary>
    /// Store for actual working part of sudoku grid.
    /// </summary>
    public class VariantGraphicElementStore
    {
        private static VariantGraphicElementStore _instance = new VariantGraphicElementStore();

        public static VariantGraphicElementStore Instance => _instance;

        /// <summary>
        /// Collection of graphic elements in corresponding part of grid.
        /// </summary>
        public string VariantGraphicElem { get; set; }

        /// <summary>
        /// Collection of <see cref="GridCellViewModel"/> of corresponding part of grid.
        /// </summary>
        public ObservableCollection<GridCellViewModel> GridCellPartOfGrid { get; set; }

        /// <summary>
        /// Collection of <see cref="SudokuElementViewModel"/> of corresponding part of grid.
        /// </summary>
        public ObservableCollection<SudokuElementViewModel> GraphicCellsPartOfGrid { get; set; }

        /// <summary>
        /// Collection of <see cref="CellNumberViewModel"/> of corresponding part of grid.
        /// </summary>
        public ObservableCollection<CellNumberViewModel> NumberCellPartOfGrid { get; set; }

        /// <summary>
        /// Location of actual working part of grid.
        /// </summary>
        public ElementLocationType LocationType { get; set; }

        /// <summary>
        /// true if user is working with central part of grid, otherwise false.
        /// </summary>
        public bool IsSelectedBasicGrid { get; set; }

        private VariantGraphicElementStore()
        {
            GridCellPartOfGrid = new ObservableCollection<GridCellViewModel>();
            GraphicCellsPartOfGrid = new ObservableCollection<SudokuElementViewModel>();
            NumberCellPartOfGrid = new ObservableCollection<CellNumberViewModel>();
            VariantGraphicElem = "";
        }

        static VariantGraphicElementStore()
        {
        }
    }
}
