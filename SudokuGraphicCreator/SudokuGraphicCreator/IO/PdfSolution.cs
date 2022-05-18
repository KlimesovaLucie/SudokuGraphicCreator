using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.IO;
using iText.Svg.Converter;
using System;

namespace SudokuGraphicCreator.IO
{
    /// <summary>
    /// Creates file with solution of booklet in PDF format and save on disk.
    /// </summary>
    public class PdfSolution
    {
        private Booklet _booklet;

        private PdfFont _boldFont;

        private const float _oneUnitIText = 2.83F;

        private const float _roundNameBottom = 277 * _oneUnitIText;

        private const float _roundNameLeft = 20 * _oneUnitIText;

        private const float _roundNameWidth = 200 * _oneUnitIText;

        private const float _sudokuTableSize = 55 * _oneUnitIText;

        private const float _sudokuTableLeft = 10 * _oneUnitIText;

        /// <summary>
        /// Creates file with solution of booklet in PDF format and save on disk.
        /// </summary>
        /// <param name="name">Name of file.</param>
        public void CreatePdfWithSolutions(string name)
        {
            _booklet = BookletStore.Instance.Booklet;
            try
            {
                _boldFont = PdfFontFactory.CreateFont("freesansbold.ttf", PdfEncodings.IDENTITY_H);
            }
            catch
            {
                throw new Exception("Error in exporting solution.");
            }
            PdfWriter writer = new PdfWriter(name);
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.A4);
            Document document = new Document(pdf);
            FillSolutions(document, pdf, _booklet.RoundNumber, _booklet.RoundName);
            document.Close();
            writer.Close();
        }

        private void FillSolutions(Document document, PdfDocument pdfDocument, string roundNumber, string roundName)
        {
            document.Add(new Paragraph(roundNumber + ". " + roundName)
                .SetFont(_boldFont)
                .SetFontSize(12)
                .SetFixedPosition(1, _roundNameLeft, _roundNameBottom, _roundNameWidth));

            int row = 0;
            int col = 0;
            int pageNumber = 1;
            foreach (var page in _booklet.Pages)
            {
                foreach (var sudoku in page.SudokuOnPage)
                {
                    PlaceSudokuInfo(document, _booklet.RoundNumber, sudoku, pageNumber, ((col + 1) * _sudokuTableLeft) + (col * _sudokuTableSize),
                        (10 * _oneUnitIText + _sudokuTableSize) * (4 - row), _sudokuTableSize);

                    PlaceSudoku(document, pdfDocument, sudoku, pageNumber, ((col + 1) * _sudokuTableLeft) + (col * _sudokuTableSize),
                        (10 * _oneUnitIText + _sudokuTableSize) * (4 - row) - _sudokuTableSize, _sudokuTableSize);

                    col++;
                    if (col == 3)
                    {
                        col = 0;
                        row++;
                    }
                    if (row == 4)
                    {
                        row = 0;
                        pageNumber++;
                    }
                }
            }
        }

        private void PlaceSudokuInfo(Document document, string roundOrder, SudokuInBooklet sudoku, int pageNumber, float left, float bottom, float width)
        {
            document.Add(new Paragraph(roundOrder + "." + sudoku.Order + " " + sudoku.Name)
                .SetFont(_boldFont)
                .SetFontSize(9)
                .SetFixedPosition(pageNumber, left, bottom, width));
        }

        private void PlaceSudoku(Document document, PdfDocument pdfDocument, SudokuInBooklet sudoku, int pageNumber, float left, float bottom, float width)
        {
            if (sudoku.SolutionFullPath.EndsWith(".svg"))
            {
                try
                {
                    using (Stream stream = File.Open(sudoku.SolutionFullPath, FileMode.Open))
                    {
                        Image image = SvgConverter.ConvertToImage(stream, pdfDocument);
                        image.SetFixedPosition(pageNumber, left, bottom)
                            .ScaleAbsolute(_sudokuTableSize, _sudokuTableSize);
                        document.Add(image);
                    }
                }
                catch
                {
                    throw new Exception("Cannot export image with path: " + sudoku.SolutionFullPath);
                }
                return;
            }

            try
            {
                ImageData imageData = ImageDataFactory.Create(sudoku.SolutionFullPath);
                Image sudokuImage = new Image(imageData)
                    .SetHeight(_sudokuTableSize)
                    .SetWidth(_sudokuTableSize)
                    .SetFixedPosition(pageNumber, left, bottom);
                Paragraph imageParagraph = new Paragraph()
                    .Add(sudokuImage);
                document.Add(imageParagraph);
            }
            catch
            {
                throw new Exception("Cannot export image with path: " + sudoku.SolutionFullPath);
            }
        }
    }
}
