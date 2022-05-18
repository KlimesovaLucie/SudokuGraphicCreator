using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGraphicCreator.Commands
{
    public class ExportSolutionCount : BaseCommand
    {
        private readonly CountOfSolutionsViewModel _viewModel;

        public ExportSolutionCount(CountOfSolutionsViewModel viewModel)
        {
            _viewModel = viewModel;
            //_viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SolutionCount))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            //return _viewModel.SolutionCount == 1;
            return true;
        }

        public override void Execute(object parameter)
        {
            int[,] givenNumbers = new int[Stores.SudokuStore.Instance.Sudoku.Grid.Size, Stores.SudokuStore.Instance.Sudoku.Grid.Size];

            for (int i = 0; i < Stores.SudokuStore.Instance.Sudoku.Grid.Size; i++)
            {
                for (int j = 0; j < Stores.SudokuStore.Instance.Sudoku.Grid.Size; j++)
                {
                    givenNumbers[i, j] = Stores.SudokuStore.Instance.Sudoku.GivenNumbers[i, j];
                }
            }


            for (int i = 0; i < Stores.SudokuStore.Instance.Sudoku.Grid.Size; i++)
            {
                for (int j = 0; j < Stores.SudokuStore.Instance.Sudoku.Grid.Size; j++)
                {
                    Stores.SudokuStore.Instance.Sudoku.GivenNumbers[i, j] = Stores.SudokuStore.Instance.Solution.GivenNumbers[i, j];
                }
            }

            IIOService service = new IOService();
            string name = service.SaveImage();
            if (name != "" && name != null)
            {
                IO.SudokuSvgImage.ExportSaveSvgImage(name);
            }

            for (int i = 0; i < Stores.SudokuStore.Instance.Sudoku.Grid.Size; i++)
            {
                for (int j = 0; j < Stores.SudokuStore.Instance.Sudoku.Grid.Size; j++)
                {
                    Stores.SudokuStore.Instance.Sudoku.GivenNumbers[i, j] = givenNumbers[i, j];
                }
            }
        }
    }
}
