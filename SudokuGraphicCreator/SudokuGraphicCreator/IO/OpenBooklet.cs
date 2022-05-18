using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System.IO;
using System.Xml.Serialization;

namespace SudokuGraphicCreator.IO
{
    /// <summary>
    /// This class is responsible for deserialization of booklet from XML file.
    /// </summary>
    public class OpenBooklet
    {
        /// <summary>
        /// Open OpenFileDialog for chooseing file with serialized booklet and do deseralization.
        /// </summary>
        public static void Open()
        {
            IIOService openService = new IOService();
            string fileName = openService.OpenBooklet();
            Deserializate(fileName);
        }

        /// <summary>
        /// Deserializes a <paramref name="previousSerializer"/> if is not null or from file <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">Name of file.</param>
        /// <param name="previousSerializer">Previous serializer.</param>
        public static void Deserializate(string fileName, XmlSerializer previousSerializer = null)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CreatingBookletViewModel));
            try
            {
                if (previousSerializer == null)
                {
                    using (var file = File.OpenRead(fileName))
                    {
                        serializer.Deserialize(file);
                    }
                }
                else
                {
                    serializer = previousSerializer;
                }

                NavigationStore.Instance.CurrentViewModel = BookletStore.Instance.CreatingBookletViewModel;

                foreach (var page in BookletStore.Instance.CreatingBookletViewModel.Pages)
                {
                    foreach (var sudoku in page.Sudoku)
                    {
                        page.AddSudokuInCollection(sudoku);
                    }
                }
            }
            catch
            { }
        }
    }
}
