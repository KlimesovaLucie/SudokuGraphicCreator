using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System.IO;
using System.Xml.Serialization;

namespace SudokuGraphicCreator.IO
{
    /// <summary>
    /// This class is responsible for serialization of booklet into XML file.
    /// </summary>
    public class SaveBooklet
    {
        /// <summary>
        /// Serializate booklet into XML format and save on disk with <paramref name="name"/>.
        /// </summary>
        /// <param name="viewModel">ViewModel for <see cref="CreatingBooklet"/> view.</param>
        /// <param name="name">Name of file.</param>
        /// <returns>serializated object</returns>
        public static XmlSerializer Save(CreatingBookletViewModel viewModel, string name, bool save=true)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CreatingBookletViewModel));
            if (save)
            {
                using (var file = new StreamWriter(name))
                {
                    serializer.Serialize(file, viewModel);
                }
            }
            return serializer;
        }
    }
}
