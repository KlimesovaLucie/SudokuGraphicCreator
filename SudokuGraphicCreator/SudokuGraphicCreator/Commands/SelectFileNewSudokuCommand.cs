using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.ViewModel;
using System;
using System.Windows.Media.Imaging;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Command for opening open file dialog and saving selected image as sudoku table or sudoku solution.
    /// </summary>
    public class SelectFileNewSudokuCommand : BaseCommand
    {
        private readonly ISudokuImageBooklet _viewModel;

        private readonly IIOService _ioService;

        /// <summary>
        /// Initializes a new instance of <see cref="SelectFileNewSudokuCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel of sudoku for which is selecting image.</param>
        public SelectFileNewSudokuCommand(ISudokuImageBooklet viewModel)
        {
            _viewModel = viewModel;
            _ioService = new IOService();
        }

        /// <summary>
        /// Open file dialog for selecting file and save name and full path of this file and open this file as image.
        /// </summary>
        /// <param name="parameter">Table or Solution, based on this, save selected file to sudoku as table or solution.</param>
        public override void Execute(object parameter)
        {
            string sudokuTable = (string)parameter;
            string fileName = _ioService.OpenImage();
            if (fileName == "")
            {
                return;
            }
            try
            {
                if (sudokuTable == "Table")
                {
                    var elems = fileName.Split("\\");
                    _viewModel.SudokuTableName = elems[elems.Length - 1];
                    _viewModel.SudokuTableFullPath = fileName;
                    if (!fileName.EndsWith(".svg"))
                    {
                        _viewModel.SudokuTable = new BitmapImage(new Uri(fileName));
                    }
                }
                else if (sudokuTable == "Solution")
                {
                    var elems = fileName.Split("\\");
                    _viewModel.SudokuSolutionName = elems[elems.Length - 1];
                    _viewModel.SudokuSolutionFullPath = fileName;
                    if (!fileName.EndsWith(".svg"))
                    {
                        _viewModel.SudokuSolution = new BitmapImage(new Uri(fileName));
                    }
                }
            }
            catch
            { }
        }
    }
}
