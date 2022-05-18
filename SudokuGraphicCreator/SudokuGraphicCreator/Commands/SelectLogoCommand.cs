using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.ViewModel;
using System;
using System.Windows.Media.Imaging;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Selecting and saving logos for booklet from the file system.
    /// </summary>
    public class SelectLogoCommand : BaseCommand
    {
        private readonly IBookletInformationsViewModel _viewModel;

        private readonly IIOService _ioService;

        /// <summary>
        /// Initializes a new instance of <see cref="SelectLogoCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel of window with button for seleting file.</param>
        public SelectLogoCommand(IBookletInformationsViewModel viewModel)
        {
            _viewModel = viewModel;
            _ioService = new IOService();
        }

        /// <summary>
        /// Open file dialog and then save path to selected file.
        /// </summary>
        /// <param name="parameter">Based on this parameter saves selected file as corresponding logo. Values can be LogoOne, LogoTwo or LogoThree.</param>
        public override void Execute(object parameter)
        {
            string logo = (string)parameter;
            string fileName = _ioService.OpenImage();
            try
            {
                if (logo == "LogoOne")
                {
                    _viewModel.LogoOneFullPath = fileName;
                    _viewModel.LogoOneImage = new BitmapImage(new Uri(fileName));
                }
                else if (logo == "LogoTwo")
                {
                    _viewModel.LogoTwoFullPath = fileName;
                    _viewModel.LogoTwoImage = new BitmapImage(new Uri(fileName));
                }
                else if (logo == "LogoThree")
                {
                    _viewModel.LogoThreeFullPath = fileName;
                    _viewModel.LogoThreeImage = new BitmapImage(new Uri(fileName));
                }
            }
            catch
            { }
        }
    }
}
