using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font;
using iText.Layout.Properties;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.Properties.Resources;
using System.Text;
using System.IO;
using iText.Svg.Converter;
using System;

namespace SudokuGraphicCreator.IO
{
    /// <summary>
    /// Creates booklet in PDF format and save on disk.
    /// </summary>
    public class PdfBooklet
    {
        private Booklet _booklet;

        private PdfFont _boldFont;

        private PdfFont _font;

        private const float OneUnitIText = 2.83F;

        #region SUDOKU_TABLE_CONSTS
        private const float FirstGridLeftMargin = 20;

        private const float HalfA4Width = 148.5F;

        private const float SecondGridLeftmargin = HalfA4Width + 5.5F;

        private const float CellSizePaper = 12 * 9;

        private static float SudokuTableSizeIText = OneUnitIText * CellSizePaper;

        private const float SudokuNameBottom = 185 * OneUnitIText;

        private const float TextLeftWithoutMargin = 0;

        private const float TextWidth = 123 * OneUnitIText;

        private const float RulesBottom = 133 * OneUnitIText;

        private const float RulesHeight = 52 * OneUnitIText;

        private const float ImageLeftWithoutMargin = 7.5F;

        private const float ImageBottom = 25 * OneUnitIText;
        #endregion

        #region INTRO_PAGE_CONSTS
        private const float FirstLogoLeft = 10 * OneUnitIText;

        private const float FirstLogoBottom = 257 * OneUnitIText;

        private const float LogoSize = 30 * OneUnitIText;

        private const float SecondLogoLeft = 170 * OneUnitIText;

        private const float SecondLogoBottom = 257 * OneUnitIText;

        private const float ThirdLogoLeft = 10 * OneUnitIText;

        private const float ThirdLogoBottom = 10 * OneUnitIText;

        private const float TournamentNameLeft = 45 * OneUnitIText;

        private const float TournamentNameBottom = 257 * OneUnitIText;

        private const float TournamentNameWidth = 120 * OneUnitIText;

        private const float InfoLeft = 10 * OneUnitIText;

        private const float InfoBottom = 207 * OneUnitIText;

        private const float InfoWidth = 190 * OneUnitIText;
        #endregion

        /// <summary>
        /// Export booklet into PDF and save on disk.
        /// </summary>
        /// <param name="name">Name of file.</param>
        public void CreatedPdf(string name)
        {
            _booklet = BookletStore.Instance.Booklet;
            try
            {
                _boldFont = PdfFontFactory.CreateFont("freesansbold.ttf", PdfEncodings.IDENTITY_H);
                _font = PdfFontFactory.CreateFont("freesans.ttf", PdfEncodings.IDENTITY_H);
            }
            catch
            {
                throw new Exception("Error in export of booklet.");
            }
            ExportPdf(name);
        }

        private void ExportPdf(string fileName)
        {
            PdfWriter writer = new PdfWriter(fileName);
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.A4.Rotate());
            Document document = new Document(pdf);
            AddInformationsIntoBooklet(pdf, document);

            document.Close();
            writer.Close();
        }

        private void AddInformationsIntoBooklet(PdfDocument pdf, Document document)
        {
            CreateIntroPage(pdf, document);

            for (int i = 0; i < _booklet.Pages.Count; i++)
            {
                FillPage(document, pdf, i + 2);
            }
        }

        private void CreateIntroPage(PdfDocument pdf, Document document)
        {
            pdf.AddNewPage(new PageSize(PageSize.A4));
            PlaceLogo(document, _booklet.LogoOneFullPath, FirstLogoLeft, FirstLogoBottom, LogoSize);
            PlaceLogo(document, _booklet.LogoTwoFullPath, SecondLogoLeft, SecondLogoBottom, LogoSize);
            PlaceLogo(document, _booklet.LogoThreeFullPath, ThirdLogoLeft, ThirdLogoBottom, LogoSize);
            PlaceParagraph(document, _booklet.TournamentName, _boldFont, 18, 1, TournamentNameLeft, TournamentNameBottom, TournamentNameWidth, TextAlignment.CENTER);

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(_booklet.TournamentDate.ToShortDateString())
                .Append(", ")
                .Append(_booklet.Location)
                .AppendLine()
                .AppendLine()
                .Append(_booklet.RoundNumber)
                .Append(". ")
                .AppendLine(Properties.Resources.Resources.Round)
                .AppendLine(_booklet.RoundName)
                .AppendLine()
                .Append(Properties.Resources.Resources.TimeForSolving)
                .Append(" ")
                .Append(_booklet.TimeForSolving)
                .Append("        ")
                .Append(Properties.Resources.Resources.TotalPoints)
                .AppendLine(_booklet.TotalPoints.ToString());

            document.Add(new Paragraph(strBuilder.ToString())
                    .SetFont(_font)
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFixedPosition(1, InfoLeft, InfoBottom, InfoWidth));

            StringBuilder sudokuBuilder = new StringBuilder();
            int sudokuCount = 0;
            foreach (var page in _booklet.Pages)
            {
                foreach (var sudoku in page.SudokuOnPage)
                {
                    sudokuBuilder.AppendLine(sudoku.ToString());
                    sudokuCount++;
                }
            }

            document.Add(new Paragraph(sudokuBuilder.ToString())
                    .SetFont(_font)
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFixedPosition(1, 10 * OneUnitIText, InfoBottom - (sudokuCount * 10 * OneUnitIText), 190 * OneUnitIText));

            StringBuilder nameBuilder = new StringBuilder(Properties.Resources.Resources.NameOfPlayer);
            nameBuilder.Append("........................................................................");

            document.Add(new Paragraph(nameBuilder.ToString())
                    .SetFont(_font)
                    .SetFontSize(10)
                    .SetFixedPosition(1, 50 * OneUnitIText, 30 * OneUnitIText, 150 * OneUnitIText));

            StringBuilder pointsBuilder = new StringBuilder(Properties.Resources.Resources.PointsOfPlayer);
            pointsBuilder.Append(".......................................................................");

            document.Add(new Paragraph(pointsBuilder.ToString())
                    .SetFont(_font)
                    .SetFontSize(10)
                    .SetFixedPosition(1, 50 * OneUnitIText, 20 * OneUnitIText, 150 * OneUnitIText));
        }

        private void PlaceParagraph(Document document, string text, PdfFont font, float fontSize, int page, float left, float bottom, float width, TextAlignment alignment)
        {
            document.Add(new Paragraph(text)
                .SetFont(font)
                .SetFontSize(fontSize)
                .SetFixedPosition(page, left, bottom, width)
                .SetTextAlignment(alignment));
        }

        private void PlaceLogo(Document document, string path, float left, float bottom, float size)
        {
            if (path == null || path == "")
            {
                return;
            }
            PlaceImage(document, path, 1, left, bottom, size);
        }

        private static void PlaceImage(Document document, string path, int pageNumber, float left, float bottom, float size)
        {
            try
            {
                ImageData imageData = ImageDataFactory.Create(path);
                Image sudokuImage = new Image(imageData)
                    .SetHeight(size)
                    .SetWidth(size)
                    .SetFixedPosition(pageNumber, left, bottom);
                Paragraph imageParagraph = new Paragraph()
                    .Add(sudokuImage)
                    .SetMargin(10 * OneUnitIText);
                document.Add(imageParagraph);
            }
            catch
            {
                throw new Exception("Cant export image on path: " + path);
            }
        }

        private void FillPage(Document document, PdfDocument pdfDocument, int pageNumber)
        {
            InsertFirstSudoku(document, pdfDocument, pageNumber);

            if (_booklet.Pages[pageNumber - 2].SudokuOnPage.Count > 1)
            {
                InsertSecondSudoku(document, pdfDocument, pageNumber);
            }
        }

        private void InsertFirstSudoku(Document document, PdfDocument pdfDocument, int pageNumber)
        {
            if (_booklet.Pages[pageNumber - 2].SudokuOnPage.Count == 0)
            {
                return;
            }
            SudokuInBooklet firstSudoku = _booklet.Pages[pageNumber - 2].SudokuOnPage[0];

            float previous = SudokuTableSizeIText;
            
            InsertRules(document, firstSudoku, pageNumber, (TextLeftWithoutMargin + FirstGridLeftMargin) * OneUnitIText, RulesBottom, TextWidth);
            InsertSudokuName(document, firstSudoku, pageNumber, (TextLeftWithoutMargin + FirstGridLeftMargin) * OneUnitIText, SudokuNameBottom, TextWidth);
            //InsertSudokuGridIntoDocument(document, pdfDocument, firstSudoku, pageNumber, (ImageLeftWithoutMargin + FirstGridLeftMargin) * OneUnitIText, ImageBottom);
            if (firstSudoku.Name.Contains(Resources.SudokuOutside) || Resources.SudokuOutside.Contains(firstSudoku.Name) ||
                firstSudoku.Name.Contains(Resources.SudokuSkyscrapers) || Resources.SudokuSkyscrapers.Contains(firstSudoku.Name) ||
                firstSudoku.Name.Contains(Resources.SudokuNextToNine) || Resources.SudokuNextToNine.Contains(firstSudoku.Name) ||
                firstSudoku.Name.Contains(Resources.SudokuLittleKiller) || Resources.SudokuLittleKiller.Contains(firstSudoku.Name))
            {
                SudokuTableSizeIText += (20 * OneUnitIText);
                InsertSudokuGridIntoDocument(document, pdfDocument, firstSudoku, pageNumber, (ImageLeftWithoutMargin + FirstGridLeftMargin - 10) * OneUnitIText,
                    ImageBottom - 10);
            }
            else
            {
                InsertSudokuGridIntoDocument(document, pdfDocument, firstSudoku, pageNumber, (ImageLeftWithoutMargin + FirstGridLeftMargin) * OneUnitIText, ImageBottom);
            }

            SudokuTableSizeIText = previous;
        }

        private void InsertSecondSudoku(Document document, PdfDocument pdfDocument, int pageNumber)
        {
            SudokuInBooklet secondSudoku = _booklet.Pages[pageNumber - 2].SudokuOnPage[1];

            float previous = SudokuTableSizeIText;

            InsertRules(document, secondSudoku, pageNumber, (TextLeftWithoutMargin + SecondGridLeftmargin) * OneUnitIText, RulesBottom, TextWidth);
            InsertSudokuName(document, secondSudoku, pageNumber, (TextLeftWithoutMargin + SecondGridLeftmargin) * OneUnitIText, SudokuNameBottom, TextWidth);
            //InsertSudokuGridIntoDocument(document, pdfDocument, secondSudoku, pageNumber, (ImageLeftWithoutMargin + SecondGridLeftmargin) * OneUnitIText, ImageBottom);

            if (secondSudoku.Name.Contains(Resources.SudokuOutside) || Resources.SudokuOutside.Contains(secondSudoku.Name) ||
                secondSudoku.Name.Contains(Resources.SudokuSkyscrapers) || Resources.SudokuSkyscrapers.Contains(secondSudoku.Name) ||
                secondSudoku.Name.Contains(Resources.SudokuNextToNine) || Resources.SudokuNextToNine.Contains(secondSudoku.Name) ||
                secondSudoku.Name.Contains(Resources.SudokuLittleKiller) || Resources.SudokuLittleKiller.Contains(secondSudoku.Name))
            {
                SudokuTableSizeIText += (20 * OneUnitIText);
                InsertSudokuGridIntoDocument(document, pdfDocument, secondSudoku, pageNumber, (ImageLeftWithoutMargin + SecondGridLeftmargin - 10) * OneUnitIText,
                    ImageBottom - 10);
            }
            else
            {
                InsertSudokuGridIntoDocument(document, pdfDocument, secondSudoku, pageNumber, (ImageLeftWithoutMargin + SecondGridLeftmargin) * OneUnitIText, ImageBottom);
            }
            SudokuTableSizeIText = previous;
        }

        private void InsertSudokuName(Document document, SudokuInBooklet sudoku, int pageNumber, float left, float bottom, float width)
        {
            document.Add(new Paragraph(sudoku.GetNameWithPoints())
                .SetFont(_boldFont)
                .SetFontSize(12)
                .SetFixedPosition(pageNumber, left, bottom, width));
        }

        private void InsertRules(Document document, SudokuInBooklet sudoku, int pageNumber, float left, float bottom, float width)
        {
            document.Add(new Paragraph(sudoku.Rules)
                    .SetFont(_font)
                    .SetFontSize(10)
                    .SetHeight(RulesHeight)
                    .SetWidth(width)
                    .SetFixedPosition(pageNumber, left, bottom, width)
                    .SetTextAlignment(TextAlignment.JUSTIFIED));
        }

        private void InsertSudokuGridIntoDocument(Document document, PdfDocument pdfDocument, SudokuInBooklet sudoku, int pageNumber, float left, float bottom)
        {
            try
            {
                if (sudoku.TableFullPath.EndsWith(".svg"))
                {
                    PlaceSvgImage(document, pdfDocument, sudoku.TableFullPath, pageNumber, left, bottom);
                    return;
                }

                PlaceImage(document, sudoku, pageNumber, left, bottom);
            }
            catch
            {
                throw new Exception("Cannot export image with path: " + sudoku.TableFullPath);
            }
        }

        private static void PlaceSvgImage(Document document, PdfDocument pdfDoc, string path, int pageNumber, float left, float bottom)
        {
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                Image img = SvgConverter.ConvertToImage(stream, pdfDoc);
                img.SetFixedPosition(pageNumber, left, bottom)
                    .ScaleAbsolute(SudokuTableSizeIText, SudokuTableSizeIText);
                document.Add(img);
            }
        }

        private static void PlaceImage(Document document, SudokuInBooklet sudoku, int pageNumber, float left, float bottom)
        {
            try
            {
                ImageData imageData = ImageDataFactory.Create(sudoku.TableFullPath);
                Image sudokuImage = new Image(imageData)
                    .SetHeight(SudokuTableSizeIText)
                    .SetWidth(SudokuTableSizeIText)
                    .SetFixedPosition(pageNumber, left, bottom);
                Paragraph imageParagraph = new Paragraph()
                    .Add(sudokuImage);
                document.Add(imageParagraph);
            }
            catch
            {
                throw new Exception("Cannot export image with path: " + sudoku.TableFullPath);
            }
        }
    }
}
