using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.IO;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command open save file dialog and create and save booklet into PDF.
    /// </summary>
    public class BookletExportCommand : BaseCommand
    {
        private readonly PdfBooklet _exportBooklet;

        public BookletExportCommand()
        {
            _exportBooklet = new PdfBooklet();
        }

        /// <summary>
        /// Open save file dialog and create and save booklet with selected file name.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            IIOService service = new IOService();
            string name = service.SaveBooklet();
            if (name != null && name != "")
            {
                _exportBooklet.CreatedPdf(name);
            }
        }
    }
}
