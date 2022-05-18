using System.Collections.ObjectModel;

namespace SudokuGraphicCreator.ViewModel
{
    public class ElementButtonGraphicOptions : ElementButton
    {
        public ObservableCollection<ElementButtonOption> ElementButtonOptions { get; set; }

        public string ElementName { get; private set; }

        public ElementButtonGraphicOptions(string text, string elementName, CreatingSudokuViewModel viewModel, string imageSource, ObservableCollection<ElementButtonOption> elementButtonOptions, double imageWidth = 30, double imageHeight = 30)
            : base(text, imageSource, viewModel, imageWidth, imageHeight)
        {
            ElementButtonOptions = elementButtonOptions;
            ElementName = elementName;
            NameOfElement = elementName;
        }

        public override string ToString()
        {
            return ElementName;
        }
    }
}
