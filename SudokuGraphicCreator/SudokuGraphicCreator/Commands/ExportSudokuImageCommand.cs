using SudokuGraphicCreator.Dialog;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command is responsible for opening SaveFileDialog and export image.
    /// </summary>
    public class ExportSudokuImageCommand : BaseCommand
    {
        /// <summary>
        /// Opens SaveFileDialog and exports image.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            IIOService service = new IOService();
            string name = service.SaveImage();
            if (name != "" && name != null)
            {
                IO.SudokuSvgImage.ExportSaveSvgImage(name);
            }
        }
    }
}
