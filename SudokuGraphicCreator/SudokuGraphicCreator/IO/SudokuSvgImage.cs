using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using System.Drawing;
using Svg;
using Svg.Pathing;
using System;
using System.Collections.ObjectModel;
using SudokuGraphicCreator.ViewModel;
using System.Drawing.Imaging;

namespace SudokuGraphicCreator.IO
{
    /// <summary>
    /// Export sudoku into image.
    /// </summary>
    public class SudokuSvgImage
    {
        /// <summary>
        /// Size of cell.
        /// </summary>
        public static readonly int CellSize = 50;

        /// <summary>
        /// Size of grid.
        /// </summary>
        public static int GridSize;

        private static int _margin = 5;
        
        /// <summary>
        /// Margin of central grid.
        /// </summary>
        public static int GridMargin
        {
            get => _margin;
            set
            {
                _margin = value;
            }
        }

        private static readonly int _defaultMargin = 5;

        /// <summary>
        /// Size of number in circle on edge.
        /// </summary>
        public static readonly int CircleNumberSize = 10;

        /// <summary>
        /// Size of small circle of edge.
        /// </summary>
        public static readonly int SmallCircleSize = 5;

        /// <summary>
        /// Size of text on edge.
        /// </summary>
        public static readonly int TextOnEdgeSize = 20;

        /// <summary>
        /// Font size of text on edge.
        /// </summary>
        public static readonly int TextOnEdgeFontSize = 20;

        /// <summary>
        /// X size of text in row.
        /// </summary>
        public static readonly int TextRowXSize = 20;

        /// <summary>
        /// X size of text in col.
        /// </summary>
        public static readonly int TextColXSize = 15;

        /// <summary>
        /// Y size of text in row.
        /// </summary>
        public static readonly int TextRowYSize = 15;

        /// <summary>
        /// Y size of text in col.
        /// </summary>
        public static readonly int TextColYSize = 15;

        /// <summary>
        /// Size of element in cell.
        /// </summary>
        public static readonly int ElementInCellSize = 40;

        /// <summary>
        /// Font size of given number.
        /// </summary>
        public static readonly int GivenNumbersFontSize = 35;

        /// <summary>
        /// Font of text.
        /// </summary>
        public static readonly string Font = "Segoe UI";

        /// <summary>
        /// Font size of small number in circle.
        /// </summary>
        public static readonly int NumberInCircleFontSize = 15;

        private static Sudoku _sudoku = SudokuStore.Instance.Sudoku;

        /// <summary>
        /// Export sudoku into image. Base on sufix of <paramref name="name"/> to SVG or PNG.
        /// </summary>
        /// <param name="name">Name of file to export into.</param>
        public static void ExportSaveSvgImage(string name)
        {
            _sudoku = SudokuStore.Instance.Sudoku;
            GridSize = SudokuStore.Instance.Sudoku.Grid.Size;
            double previous = GridSizeStore.XCellSize;
            GridSizeStore.XCellSize = CellSize;
            GridSizeStore.YCellSize = CellSize;
            GridMargin = _defaultMargin;
            SvgDocument doc = new SvgDocument();
            PlaceNumberAroundGrid(doc);
            CreateVariantsWithColorCells(doc);
            CreateExtraRegions(doc);
            CreateSudokuGrid(doc);
            CreateGraphicElements(doc);
            CreateVariantsElems(doc);
            InsertGivenNumbers(doc);
            doc.Height = GridMargin * 2 + _sudoku.Grid.Size * CellSize;
            doc.Width = GridMargin * 2 + _sudoku.Grid.Size * CellSize;
            doc.ViewBox = new SvgViewBox(0, 0, doc.Width, doc.Height);
            ExportToRightFormat(doc, name);
            GridSizeStore.XCellSize = previous;
            GridSizeStore.YCellSize = previous;
        }

        private static void ExportToRightFormat(SvgDocument doc, string name)
        {
            if (name.EndsWith(".svg"))
            {
                doc.Write(name);
                return;
            }
            else if (name.EndsWith(".png"))
            {
                var bitmap = doc.Draw();
                bitmap.Save(name, ImageFormat.Png);
            }
        }

        private static void CreateSudokuGrid(SvgDocument doc)
        {
            SvgGroup gridGroup = new SvgGroup();
            gridGroup.Stroke = new SvgColourServer(Color.Black);
            gridGroup.ID = SvgIDs.Grid.ToString();
            CreateHorizontalGridLine(gridGroup);
            CreateVerticalGridLine(gridGroup);
            doc.Children.Add(gridGroup);

            SvgGroup group = new SvgGroup();
            group.Stroke = new SvgColourServer(Color.Black);
            group.ID = SvgIDs.BoldGrid.ToString();
            CreateIrregularGrid(group);
            doc.Children.Add(group);
        }

        private static void CreateHorizontalGridLine(SvgGroup gridGroup)
        {
            for (int i = 0; i < GridSize + 1; i++)
            {
                SvgPath path = new SvgPath();
                SvgPathSegmentList dAtribute = new SvgPathSegmentList();

                SvgMoveToSegment moveTo = new SvgMoveToSegment(new Point(0 + GridMargin, (CellSize * i) + GridMargin));
                dAtribute.Add(moveTo);
                SvgLineSegment svgLineSegment = new SvgLineSegment(new Point(0 + GridMargin, (CellSize * i) + GridMargin),
                    new Point((CellSize * GridSize) + GridMargin, (CellSize * i) + GridMargin));
                dAtribute.Add(svgLineSegment);

                path.PathData = dAtribute;
                path.StrokeWidth = 1;
                gridGroup.Children.Add(path);
            }
        }

        private static void CreateVerticalGridLine(SvgGroup gridGroup)
        {
            for (int i = 0; i < GridSize + 1; i++)
            {
                SvgPath path = new SvgPath();
                SvgPathSegmentList dAttribute = new SvgPathSegmentList();

                SvgMoveToSegment moveTo = new SvgMoveToSegment(new Point((CellSize * i) + GridMargin, 0 + GridMargin));
                dAttribute.Add(moveTo);
                SvgLineSegment svgLineSegment = new SvgLineSegment(new Point((CellSize * i) + GridMargin, 0 + GridMargin),
                    new Point((CellSize * i) + GridMargin, (CellSize * GridSize) + GridMargin));
                dAttribute.Add(svgLineSegment);

                path.PathData = dAttribute;
                path.StrokeWidth = 1;
                gridGroup.Children.Add(path);
            }
        }

        private static void CreateIrregularGrid(SvgGroup group)
        {
            int count = 0;
            foreach (var box in _sudoku.Grid.Boxes)
            {
                SvgGroup groupBox = new SvgGroup();
                groupBox.ID = count.ToString();
                count++;
                foreach (var cell in box)
                {
                    HasCellNeighbourInBox(groupBox, cell, box);
                }
                group.Children.Add(groupBox);
            }
        }

        private static void HasCellNeighbourInBox(SvgGroup gridGroup, Tuple<int, int> cell, ObservableCollection<Tuple<int, int>> box)
        {
            Tuple<int, int> leftCell = new Tuple<int, int>(cell.Item1, cell.Item2 - 1);
            if (!box.Contains(leftCell))
            {
                CreateLine(gridGroup, cell.Item1, cell.Item2, cell.Item1 + 1, cell.Item2);
            }

            Tuple<int, int> rightCell = new Tuple<int, int>(cell.Item1, cell.Item2 + 1);
            if (!box.Contains(rightCell))
            {
                CreateLine(gridGroup, cell.Item1, cell.Item2 + 1, cell.Item1 + 1, cell.Item2 + 1);
            }

            Tuple<int, int> topCell = new Tuple<int, int>(cell.Item1 - 1, cell.Item2);
            if (!box.Contains(topCell))
            {
                CreateLine(gridGroup, cell.Item1, cell.Item2, cell.Item1, cell.Item2 + 1);
            }

            Tuple<int, int> downCell = new Tuple<int, int>(cell.Item1 + 1, cell.Item2);
            if (!box.Contains(downCell))
            {
                CreateLine(gridGroup, cell.Item1 + 1, cell.Item2, cell.Item1 + 1, cell.Item2 + 1);
            }
        }

        private static void CreateLine(SvgGroup gridGroup, int row, int col, int secondRow, int secondCol)
        {
            SvgPath path = new SvgPath();
            SvgPathSegmentList dAtribute = new SvgPathSegmentList();

            SvgMoveToSegment moveTo = new SvgMoveToSegment(new Point((col * CellSize) + GridMargin, (row * CellSize) + GridMargin));
            dAtribute.Add(moveTo);
            SvgLineSegment svgLineSegment = new SvgLineSegment(new Point((col * CellSize) + GridMargin, (row * CellSize) + GridMargin),
                new Point((secondCol * CellSize) + GridMargin, (secondRow * CellSize) + GridMargin));
            dAtribute.Add(svgLineSegment);

            path.PathData = dAtribute;
            path.StrokeWidth = 3;
            gridGroup.Children.Add(path);
        }

        private static void InsertGivenNumbers(SvgDocument doc)
        {
            SvgGroup givenNumbersGroup = new SvgGroup();
            givenNumbersGroup.FontFamily = Font;
            PlaceGivenNumberFromCollection(givenNumbersGroup, SudokuStore.Instance.Sudoku.GivenNumbers, GridSize, GridSize);
            givenNumbersGroup.FontSize = GivenNumbersFontSize;
            givenNumbersGroup.ID = SvgIDs.GivenNumbers.ToString();
            doc.Children.Add(givenNumbersGroup);
        }

        private static void PlaceGivenNumberFromCollection(SvgGroup givenNumbersGroup, int[,] collection, int rowSize, int colSize)
        {
            for (int row = 0; row < rowSize; row++)
            {
                for (int col = 0; col < colSize; col++)
                {
                    int actualNumber = collection[row, col];
                    if (actualNumber != 0)
                    {
                        SvgText svgNumber = new SvgText(actualNumber.ToString());
                        SvgUnitCollection unitX = new SvgUnitCollection();
                        SvgUnitCollection unitY = new SvgUnitCollection();
                        if (actualNumber < 10)
                        {
                            unitX.Add(col * CellSize + GridMargin + 15);
                        }
                        else
                        {
                            unitX.Add(col * CellSize + GridMargin + 7);
                        }
                        unitY.Add((row + 1) * CellSize + GridMargin - 12);
                        svgNumber.X = unitX;
                        svgNumber.Y = unitY;
                        svgNumber.FontSize = GivenNumbersFontSize;
                        givenNumbersGroup.Children.Add(svgNumber);
                    }
                }
            }
        }

        private static void CreateExtraRegions(SvgDocument doc)
        {
            SvgGroup group = new SvgGroup();
            group.ID = SvgIDs.ExtraRegions.ToString();
            int count = 1;
            foreach (var box in _sudoku.Grid.ExtraRegions)
            {
                SvgGroup boxGroup = new SvgGroup();
                boxGroup.ID = count.ToString();
                group.Children.Add(boxGroup);

                foreach (var cell in box)
                {
                    CreateGreyCell(boxGroup, 0, 0, cell.Item1, cell.Item2);
                }

                count++;
            }

            if (group.Children.Count != 0)
            {
                doc.Children.Add(group);
            }
        }

        private static void CreateVariantsElems(SvgDocument doc)
        {
            foreach (var variant in SudokuStore.Instance.Sudoku.Variants)
            {
                if (variant == SudokuType.Diagonal)
                {
                    CreateDiagonalElems(doc);
                }
                else if (variant == SudokuType.Antiknight)
                {
                    CreateAntiknightElems(doc);
                }
                else if (variant == SudokuType.Nonconsecutive)
                {
                    CreateNonconsecutiveElems(doc);
                }
                else if (variant == SudokuType.Untouchable)
                {
                    CreateSvgEmptyGroup(doc, SvgIDs.Untouchable);
                }
                else if (variant == SudokuType.DisjointGroups)
                {
                    CreateSvgEmptyGroup(doc, SvgIDs.DisjointGroup);
                }
            }

            foreach (var variant in SudokuStore.Instance.Sudoku.Variants)
            {
                if (variant == SudokuType.Sum)
                {
                    CreateCircleWithNumberElems(doc, SvgIDs.Sum, SudokuElementType.Sum);
                }
                else if (variant == SudokuType.Difference)
                {
                    CreateCircleWithNumberElems(doc, SvgIDs.Difference, SudokuElementType.Difference);
                }
                else if (variant == SudokuType.Consecutive)
                {
                    CreateWhiteCircle(doc, SvgIDs.Consecutive, SudokuElementType.Consecutive);
                }
                else if (variant == SudokuType.Kropki)
                {
                    CreateWhiteCircle(doc, SvgIDs.KropkiWhite, SudokuElementType.WhiteKropki);
                    CreateBlackCircle(doc, SvgIDs.KropkiBlack, SudokuElementType.BlackKropki);
                }
                else if (variant == SudokuType.GreaterThan)
                {
                    CreateTextOnEdge(doc, SvgIDs.GreaterThanLeft, SudokuElementType.GreaterThanLeft, Char.ConvertFromUtf32(0x0000FF1C).ToString(), TextRowXSize, TextRowYSize);
                    CreateTextOnEdge(doc, SvgIDs.GreaterThanRight, SudokuElementType.GreaterThanRight, Char.ConvertFromUtf32(0x0000FF1E).ToString(), TextRowXSize, TextRowYSize);
                    CreateTextOnEdge(doc, SvgIDs.GreaterThanUp, SudokuElementType.GreaterThanUp, Char.ConvertFromUtf32(0x00002227).ToString(), TextRowYSize, TextColYSize);
                    CreateTextOnEdge(doc, SvgIDs.GreaterThanDown, SudokuElementType.GreaterThanDown, Char.ConvertFromUtf32(0x00002228).ToString(), TextRowYSize, TextColYSize);
                }
                else if (variant == SudokuType.XV)
                {
                    CreateTextOnEdge(doc, SvgIDs.XvX, SudokuElementType.XvX, "X", TextRowYSize, TextColYSize);
                    CreateTextOnEdge(doc, SvgIDs.XvV, SudokuElementType.XvV, "V", TextRowYSize, TextColYSize);
                }
                else if (variant == SudokuType.Odd)
                {
                    PlaceCircleInCell(doc, SvgIDs.Odd, SudokuElementType.Odd);
                }
                else if (variant == SudokuType.Even)
                {
                    PlaceSquareInCell(doc, SvgIDs.Even, SudokuElementType.Even);
                }
                else if (variant == SudokuType.SearchNine)
                {
                    PlaceBoldArrow(doc, SvgIDs.SearchNineLeft, SudokuElementType.SearchNineLeft);
                    PlaceBoldArrow(doc, SvgIDs.SearchNineRight, SudokuElementType.SearchNineRight);
                    PlaceBoldArrow(doc, SvgIDs.SearchNineUp, SudokuElementType.SearchNineUp);
                    PlaceBoldArrow(doc, SvgIDs.SearchNineDown, SudokuElementType.SearchNineDown);
                }
                else if (variant == SudokuType.Palindrome)
                {
                    PlaceLine(doc, SvgIDs.Palindromes, SudokuElementType.Palindromes);
                }
                else if (variant == SudokuType.Sequence)
                {
                    PlaceLine(doc, SvgIDs.Sequences, SudokuElementType.Sequences);
                }
                else if (variant == SudokuType.Arrow)
                {
                    PlaceLongArrowWithCircle(doc, SvgIDs.Arrows, SudokuElementType.Arrows);
                }
                else if (variant == SudokuType.Thermometer)
                {
                    PlaceLine(doc, SvgIDs.Thermometers, SudokuElementType.Thermometers);
                }
                else if (variant == SudokuType.Killer)
                {
                    PlaceCage(doc, SvgIDs.Killer, SudokuElementType.Killer);
                }
            }
        }

        private static void GraphicNumbers(SvgGroup group, int[,] collection, SudokuElementType[,] types, int rowSize, int colSize, int rowShift, int colShift)
        {
            if (ContainsNumbers(collection))
            {
                GridMargin = 3 * CellSize + _defaultMargin;
                PlaceNumberFromCollectionOutside(group, collection, types, rowSize, colSize, rowShift, colShift);
            }
        }

        private static void PlaceNumberFromCollectionOutside(SvgGroup group, int[,] collection, SudokuElementType[,] types, int rowSize, int colSize, int rowShift, int colShift)
        {
            for (int row = 0; row < rowSize; row++)
            {
                for (int col = 0; col < colSize; col++)
                {
                    if (collection[row, col] != 0 && types[row, col] == SudokuElementType.NoMeaning)
                    {
                        PlaceNumberOutside(group, collection[row, col].ToString(), row, col, rowShift + _defaultMargin, colShift + _defaultMargin);
                    }
                }
            }
        }

        private static bool ContainsNumbers(int[,] collection)
        {
            foreach (var number in collection)
            {
                if (number != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private static void CreateGraphicElements(SvgDocument doc)
        {
            SvgGroup group = new SvgGroup();
            group.ID = SvgIDs.GraphicElements.ToString();

            foreach (var elem in _sudoku.SudokuVariants)
            {
                if (elem.SudokuElemType == SudokuElementType.NoMeaning)
                {
                    CircleWithNumber circleWithNumber = elem as CircleWithNumber;
                    if (circleWithNumber != null)
                    {
                        PlaceSumElem(group, circleWithNumber);
                        continue;
                    }
                    
                    WhiteCircle whiteCircle = elem as WhiteCircle;
                    if (whiteCircle != null)
                    {
                        PlaceWhiteCircle(group, whiteCircle);
                        continue;
                    }

                    BlackCircle blackCircle = elem as BlackCircle;
                    if (blackCircle != null)
                    {
                        PlaceCircleOnEdge(group, blackCircle, Color.Black, Color.Black);
                        continue;
                    }

                    GreyCircle greyCircle = elem as GreyCircle;
                    if (greyCircle != null)
                    {
                        PlaceGreyCircleInCell(group, greyCircle);
                        continue;
                    }

                    CircleWithGreyEdge circleWithEdge = elem as CircleWithGreyEdge;
                    if (circleWithEdge != null)
                    {
                        CreateCircleWithGreyEdge(group, circleWithEdge.RowIndex, circleWithEdge.ColIndex);
                        continue;
                    }

                    LongArrowWithCircle arrowWithCircle = elem as LongArrowWithCircle;
                    if (arrowWithCircle != null)
                    {
                        CreateLongArrowWithCircle(group, arrowWithCircle);
                        continue;
                    }

                    BoldArrow boldArrow = elem as BoldArrow;
                    if (boldArrow != null)
                    {
                        CreateBoldArrow(group, boldArrow);
                        continue;
                    }

                    LongArrow longArrow = elem as LongArrow;
                    if (longArrow != null)
                    {
                        CreateLongArrow(group, longArrow.Positions);
                        continue;
                    }

                    SmallArrow smallArrow = elem as SmallArrow;
                    if (smallArrow != null)
                    {
                        CreateSmallArrow(group, smallArrow.ColIndex * CellSize, smallArrow.RowIndex * CellSize, smallArrow.GraphicType, GridMargin, GridMargin);
                        continue;
                    }

                    GreySquare greySquare = elem as GreySquare;
                    if (greySquare != null)
                    {
                        PlaceGreySquareInCell(group, greySquare);
                        continue;
                    }

                    Star star = elem as Star;
                    if (star != null)
                    {
                        double inCell = GridSizeStore.InCellElementSize;
                        double radius = GridSizeStore.InnerRadiusStar;
                        double cell = GridSizeStore.XCellSize;

                        GridSizeStore.InCellElementSize = ElementInCellSize;
                        GridSizeStore.XCellSize = CellSize;
                        CreateStar(group, star);
                        GridSizeStore.InCellElementSize = inCell;
                        GridSizeStore.InnerRadiusStar = radius;
                        GridSizeStore.XCellSize = cell;
                        continue;
                    }

                    Line line = elem as Line;
                    if (line != null)
                    {
                        CreateLine(group, SudokuElementType.NoMeaning, line.Positions);
                        continue;
                    }

                    Cage cage = elem as Cage;
                    if (cage != null)
                    {
                        CreateCage(group, cage);
                        continue;
                    }

                    GreyCell greyCell = elem as GreyCell;
                    if (greyCell != null)
                    {
                        CreateGreyCell(group, 0, 0, greyCell.RowIndex, greyCell.ColIndex);
                        continue;
                    }
                }
            }

            if (group.Children.Count != 0)
            {
                doc.Children.Add(group);
            }
        }

        private static void CreateDiagonalElems(SvgDocument doc)
        {
            SvgGroup diagonalGroup = new SvgGroup();
            diagonalGroup.ID = SvgIDs.Diagonal.ToString();
            diagonalGroup.StrokeWidth = 3;
            diagonalGroup.Stroke = new SvgColourServer(Color.Gray);

            diagonalGroup.Children.Add(CreatePath(GridMargin, GridMargin, (CellSize * GridSize) + GridMargin, (CellSize * GridSize) + GridMargin));
            diagonalGroup.Children.Add(CreatePath((CellSize * GridSize) + GridMargin, GridMargin, GridMargin, (CellSize * GridSize) + GridMargin));

            doc.Children.Add(diagonalGroup);
        }

        private static SvgPath CreatePath(int firstX, int firstY, int secondX, int secondY)
        {
            SvgPath path = new SvgPath();
            SvgPathSegmentList dAttribute = new SvgPathSegmentList();
            SvgMoveToSegment moveTo = new SvgMoveToSegment(new Point(firstX, firstY));
            dAttribute.Add(moveTo);
            SvgLineSegment svgLine = new SvgLineSegment(new Point(GridMargin, GridMargin),
                new Point(secondX, secondY));
            dAttribute.Add(svgLine);
            path.PathData = dAttribute;
            return path;
        }

        private static void CreateWindokuElems(SvgDocument doc)
        {
            SvgGroup group = new SvgGroup();
            group.ID = SvgIDs.Windoku.ToString();
            CreateWindokuRegions(group);
            doc.Children.Add(group);
        }

        private static void CreateWindokuRegions(SvgGroup group)
        {
            CreateWindokuRegion(group, 1, 1);
            CreateWindokuRegion(group, 1, 5);
            CreateWindokuRegion(group, 5, 1);
            CreateWindokuRegion(group, 5, 5);
        }

        private static void CreateWindokuRegion(SvgGroup group, int startRow, int startCol)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    CreateGreyCell(group, startRow, startCol, row, col);
                }
            }
        }

        private static void CreateGreyCell(SvgGroup group, int startRow, int startCol, int row, int col)
        {
            SvgRectangle rect = new SvgRectangle();
            group.Fill = new SvgColourServer(Color.Gray);
            rect.X = (col + startCol) * CellSize + GridMargin;
            rect.Y = (row + startRow) * CellSize + GridMargin;
            rect.Width = CellSize;
            rect.Height = CellSize;
            group.Children.Add(rect);
        }

        private static void CreateAntiknightElems(SvgDocument doc)
        {
            SvgGroup group = new SvgGroup();
            group.ID = SvgIDs.Antiknight.ToString();
            doc.Children.Add(group);
        }

        private static void CreateVariantsWithColorCells(SvgDocument doc)
        {
            if (SudokuStore.Instance.Sudoku.Variants.Contains(SudokuType.Windoku))
            {
                CreateWindokuElems(doc);
            }
        }

        private static void CreateNonconsecutiveElems(SvgDocument doc)
        {
            SvgGroup group = new SvgGroup();
            group.ID = SvgIDs.Nonconsecutive.ToString();
            doc.Children.Add(group);
        }

        private static void CreateSvgEmptyGroup(SvgDocument doc, SvgIDs id)
        {
            SvgGroup group = new SvgGroup();
            group.ID = id.ToString();
            doc.Children.Add(group);
        }

        private static void CreateCircleWithNumberElems(SvgDocument doc, SvgIDs id, SudokuElementType elemType)
        {
            SvgGroup group = new SvgGroup();
            group.ID = id.ToString();
            doc.Children.Add(group);

            foreach (var elem in _sudoku.SudokuVariants)
            {
                if (elem.SudokuElemType == elemType)
                {
                    CircleWithNumber circle = elem as CircleWithNumber;
                    if (circle != null)
                    {
                        PlaceSumElem(group, circle);
                    }
                }
            }
        }

        private static void PlaceSumElem(SvgGroup group, CircleWithNumber elem)
        {
            SvgCircle circle = new SvgCircle();
            group.Children.Add(circle);
            circle.Radius = CircleNumberSize;
            int x;
            int y;
            CalculateEdgeXYCentral(elem.RowIndex, elem.ColIndex, CircleNumberSize, elem.Location, out x, out y);
            circle.CenterX = x;
            circle.CenterY = y;
            circle.Fill = new SvgColourServer(Color.White);
            circle.Stroke = new SvgColourServer(Color.Black);
            circle.StrokeWidth = 1;

            SvgText number = new SvgText(elem.Value.ToString());
            number.FontFamily = Font;
            number.FontSize = NumberInCircleFontSize;
            group.Children.Add(number);

            SvgUnitCollection unitX = new SvgUnitCollection();
            if (elem.Value < 10)
            {
                unitX.Add(x - 4);
            }
            else
            {
                unitX.Add(x - 8);
            }
            number.X = unitX;

            SvgUnitCollection unitY = new SvgUnitCollection();
            unitY.Add(y + 5);
            number.Y = unitY;
        }

        private static void CalculateEdgeXYCentral(int row, int col, int elemSize, ElementLocationType location, out int x, out int y)
        {
            x = col * CellSize + GridMargin;
            y = row * CellSize + GridMargin;
            if (location == ElementLocationType.Row)
            {
                y += CellSize / 2;
            }
            else if (location == ElementLocationType.Column)
            {
                x += CellSize / 2;
            }
        }

        private static void CreateWhiteCircle(SvgDocument doc, SvgIDs id, SudokuElementType elemType)
        {
            SvgGroup group = new SvgGroup();
            group.ID = id.ToString();
            doc.Children.Add(group);

            foreach (var elem in _sudoku.SudokuVariants)
            {
                if (elem.SudokuElemType == elemType)
                {
                    WhiteCircle circle = elem as WhiteCircle;
                    if (circle != null)
                    {
                        PlaceWhiteCircle(group, circle);
                    }
                }
            }
        }

        private static void PlaceWhiteCircle(SvgGroup group, WhiteCircle elem)
        {
            SvgCircle circle = new SvgCircle();
            group.Children.Add(circle);
            circle.Radius = SmallCircleSize;
            int x;
            int y;
            CalculateEdgeXYCentral(elem.RowIndex, elem.ColIndex, SmallCircleSize, elem.Location, out x, out y);
            circle.CenterX = x;
            circle.CenterY = y;
            circle.Fill = new SvgColourServer(Color.White);
            circle.Stroke = new SvgColourServer(Color.Black);
            circle.StrokeWidth = 1;
        }

        private static void CreateBlackCircle(SvgDocument doc, SvgIDs id, SudokuElementType elemType)
        {
            SvgGroup group = new SvgGroup();
            group.ID = id.ToString();
            doc.Children.Add(group);

            foreach (var elem in _sudoku.SudokuVariants)
            {
                if (elem.SudokuElemType == elemType)
                {
                    BlackCircle circle = elem as BlackCircle;
                    if (circle != null)
                    {
                        PlaceCircleOnEdge(group, circle, Color.Black, Color.Black);
                    }
                }
            }
        }

        private static void PlaceCircleOnEdge(SvgGroup group, Circle elem, Color fill, Color stroke)
        {
            SvgCircle circle = new SvgCircle();
            group.Children.Add(circle);
            circle.Radius = SmallCircleSize;
            int x;
            int y;
            CalculateEdgeXYCentral(elem.RowIndex, elem.ColIndex, SmallCircleSize, elem.Location, out x, out y);
            circle.CenterX = x;
            circle.CenterY = y;
            circle.Fill = new SvgColourServer(fill);
            circle.Stroke = new SvgColourServer(stroke);
            circle.StrokeWidth = 1;
        }

        private static void CreateTextOnEdge(SvgDocument doc, SvgIDs id, SudokuElementType elemType, string value, int xSize, int ySize)
        {
            SvgGroup group = new SvgGroup();
            group.ID = id.ToString();
            doc.Children.Add(group);

            foreach (var elem in _sudoku.SudokuVariants)
            {
                Character character = elem as Character;
                if (character == null)
                {
                    continue;
                }
                if (elem.SudokuElemType == elemType)
                {
                    PlaceTextOnEdge(group, character, value, character.Location, xSize, ySize);
                }
            }
        }

        private static void PlaceTextOnEdge(SvgGroup group, Character elem, string value, ElementLocationType location, int xSize, int ySize)
        {
            SvgText text = new SvgText(value);
            group.Children.Add(text);

            int x;
            int y;
            CalculateXYTextOnEdge(elem.RowIndex, elem.ColIndex, xSize, ySize, location, out x, out y);

            SvgUnitCollection unitX = new SvgUnitCollection();
            unitX.Add(x);
            text.X = unitX;

            SvgUnitCollection unitY = new SvgUnitCollection();
            unitY.Add(y);
            text.Y = unitY;
            text.FontSize = TextOnEdgeFontSize;
            text.Font = Font;
        }

        private static void CalculateXYTextOnEdge(int row, int col, int xSize, int ySize, ElementLocationType location, out int x, out int y)
        {
            x = col * CellSize + GridMargin;
            y = row * CellSize + GridMargin;

            if (location == ElementLocationType.Row)
            {
                x -= xSize / 2;
                y = y + (CellSize / 2) + (ySize / 2);
            }
            else if (location == ElementLocationType.Column)
            {
                x = x + (CellSize / 2) - (xSize / 2);
                y += ySize / 2;
            }
        }

        private static void PlaceCircleInCell(SvgDocument doc, SvgIDs id, SudokuElementType elemType)
        {
            SvgGroup group = new SvgGroup();
            group.ID = id.ToString();
            doc.Children.Add(group);

            foreach (var elem in _sudoku.SudokuVariants)
            {
                GreyCircle circle = elem as GreyCircle;
                if (circle != null)
                {
                    if (elem.SudokuElemType == elemType)
                    {
                        PlaceGreyCircleInCell(group, circle);
                    }
                }
            }
        }

        private static void PlaceGreyCircleInCell(SvgGroup group, GreyCircle elem)
        {
            SvgCircle circle = new SvgCircle();
            group.Children.Add(circle);
            circle.Radius = ElementInCellSize / 2;
            circle.CenterX = (elem.ColIndex * CellSize) + (CellSize / 2) + ((CellSize - ElementInCellSize) / 2) + GridMargin - _defaultMargin;
            circle.CenterY = (elem.RowIndex * CellSize) + (CellSize / 2) + ((CellSize - ElementInCellSize) / 2) + GridMargin - _defaultMargin;
            circle.Fill = new SvgColourServer(Color.Gray);
            circle.Stroke = new SvgColourServer(Color.Gray);
            circle.StrokeWidth = 1;
        }

        private static void PlaceSquareInCell(SvgDocument doc, SvgIDs id, SudokuElementType elemType)
        {
            SvgGroup group = new SvgGroup();
            group.ID = id.ToString();
            doc.Children.Add(group);

            foreach (var elem in _sudoku.SudokuVariants)
            {
                GreySquare circle = elem as GreySquare;
                if (circle != null)
                {
                    if (elem.SudokuElemType == elemType)
                    {
                        PlaceGreySquareInCell(group, circle);
                    }
                }
            }
        }

        private static void PlaceGreySquareInCell(SvgGroup group, GreySquare elem)
        {
            SvgRectangle square = new SvgRectangle();
            group.Children.Add(square);
            square.Height = ElementInCellSize;
            square.Width = ElementInCellSize;
            square.Fill = new SvgColourServer(Color.Gray);
            square.X = elem.ColIndex * CellSize + ((CellSize - ElementInCellSize) / 2) + GridMargin;
            square.Y = (elem.RowIndex * CellSize) + ((CellSize - ElementInCellSize) / 2) + GridMargin;
        }

        private static void PlaceLine(SvgDocument doc, SvgIDs id, SudokuElementType elemType)
        {
            SvgGroup group = new SvgGroup();
            group.ID = id.ToString();
            doc.Children.Add(group);

            foreach (var elem in _sudoku.SudokuVariants)
            {
                Line line = elem as Line;
                if (line != null)
                {
                    if (elem.SudokuElemType == elemType)
                    {
                        CreateLine(group, elemType, line.Positions);
                    }
                }
                else if (elemType == SudokuElementType.Thermometers)
                {
                    GreyCircle circle = elem as GreyCircle;
                    if (circle != null)
                    {
                        if (elem.SudokuElemType == elemType)
                        {
                            PlaceGreyCircleInCell(group, circle);
                        }
                    }
                }
            }
        }

        private static void CreateLine(SvgGroup group, SudokuElementType type, ObservableCollection<Tuple<int, int>> positions)
        {
            if (positions.Count < 2)
            {
                return;
            }
            SvgPath line = new SvgPath();
            group.Children.Add(line);
            line.Stroke = new SvgColourServer(Color.Gray);
            line.StrokeWidth = 8;
            if (type == SudokuElementType.Thermometers)
            {
                line.StrokeWidth = 15;
            }
            line.Fill = new SvgColourServer(Color.Transparent);

            Point previous = new Point(positions[0].Item2 * CellSize + GridMargin + (CellSize / 2),
                positions[0].Item1 * CellSize + GridMargin + (CellSize / 2));
            SvgPathSegmentList dAttribute = new SvgPathSegmentList();
            SvgMoveToSegment moveTo = new SvgMoveToSegment(previous);
            dAttribute.Add(moveTo);

            for (int i = 1; i < positions.Count; i++)
            {
                Tuple<int, int> position = positions[i];
                Point actual = new Point(position.Item2 * CellSize + GridMargin + (CellSize / 2),
                    position.Item1 * CellSize + GridMargin + (CellSize / 2));
                SvgLineSegment lineSegment = new SvgLineSegment(previous, actual);
                dAttribute.Add(lineSegment);
                previous = actual;
            }

            line.PathData = dAttribute;
        }

        private static void PlaceCage(SvgDocument doc, SvgIDs id, SudokuElementType elemType)
        {
            SvgGroup group = new SvgGroup();
            group.ID = id.ToString();
            doc.Children.Add(group);

            foreach (var elem in _sudoku.SudokuVariants)
            {
                Cage cage = elem as Cage;
                if (cage != null)
                {
                    if (cage.SudokuElemType == elemType)
                    {
                        CreateCage(group, cage);
                    }
                }
            }
        }

        private static void CreateCage(SvgGroup group, Cage cage)
        {
            SvgPath line = new SvgPath();
            group.Children.Add(line);
            line.StrokeWidth = 1;
            line.Stroke = new SvgColourServer(Color.Black);
            SvgUnitCollection unit = new SvgUnitCollection();
            unit.Add(3);
            unit.Add(3);
            unit.Add(3);
            line.StrokeDashArray = unit;
            line.Fill = new SvgColourServer(Color.Transparent);

            System.Windows.Media.PointCollection points = new System.Windows.Media.PointCollection();
            double x = 0;
            double y = 0;
            ObservableCollection<Tuple<double, double>> cells = new ObservableCollection<Tuple<double, double>>();
            foreach (var position in cage.Positions)
            {
                cells.Add(new Tuple<double, double>(position.Item2 * GridSizeStore.XCellSize, position.Item1 * GridSizeStore.XCellSize));
            }
            CageViewModel.FindPoints(cells, ref x, ref y, points);

            Point previous = new Point((int)(points[0].X + GridMargin), (int)(points[0].Y + GridMargin));
            SvgPathSegmentList dAttribute = new SvgPathSegmentList();
            SvgMoveToSegment moveTo = new SvgMoveToSegment(previous);
            dAttribute.Add(moveTo);

            for (int i = 1; i < points.Count; i++)
            {
                Point actual = new Point((int)(points[i].X + GridMargin), (int)(points[i].Y + GridMargin));
                SvgLineSegment lineSegment = new SvgLineSegment(previous, actual);
                dAttribute.Add(lineSegment);
                previous = actual;
            }

            line.PathData = dAttribute;

            SvgText text = new SvgText();
            text.FontFamily = Font;
            text.Text = cage.Number.ToString();
            if (cage.Text != null && cage.Text != "")
            {
                text.Text = cage.Text;
            }
            SvgUnitCollection unitX = new SvgUnitCollection();
            unitX.Add((int)x + 10 + GridMargin);
            text.X = unitX;

            SvgUnitCollection unitY = new SvgUnitCollection();
            unitY.Add((int)y + 15 + GridMargin);
            text.Y = unitY;
            text.FontSize = 10;
            group.Children.Add(text);
        }

        private static void PlaceNumberAroundGrid(SvgDocument doc)
        {
            TransformGridMarginByVariant(doc, SvgIDs.Skyscrapers, SudokuElementType.Skyscrapers);
            TransformGridMarginByVariant(doc, SvgIDs.NextToNine, SudokuElementType.NextToNine);
            TransformGridMarginByVariant(doc, SvgIDs.Outside, SudokuElementType.Outside);
            TransformGridMarginByVariant(doc, SvgIDs.StarProduct, SudokuElementType.StarProduct);
            LittleKillerTransform(doc);

            SvgGroup group = new SvgGroup();
            group.ID = SvgIDs.GraphicElements.ToString();
            int gridSize = SudokuStore.Instance.Sudoku.Grid.Size;
            GraphicNumbers(group, _sudoku.LeftNumbers, _sudoku.LeftNumbersType, gridSize, 3, 3 * CellSize, 0);
            GraphicNumbers(group, _sudoku.RightNumbers, _sudoku.RightNumbersType, gridSize, 3, 3 * CellSize, (gridSize + 3) * CellSize);
            GraphicNumbers(group, _sudoku.UpNumbers, _sudoku.UpNumbersType, 3, gridSize, 0, 3 * CellSize);
            GraphicNumbers(group, _sudoku.BottomNumbers, _sudoku.BottomNumbersType, 3, gridSize, (gridSize + 3) * CellSize, 3 * CellSize);
            PlaceCharacters(group);

            if (group.Children.Count != 0)
            {
                doc.Children.Add(group);
            }
        }

        private static void PlaceCharacters(SvgGroup group)
        {
            foreach (var elem in _sudoku.SudokuVariants)
            {
                Character character = elem as Character;
                if (character != null && character.SudokuElemType != SudokuElementType.Killer && character.SudokuElemType != SudokuElementType.GreaterThanDown
                    && character.SudokuElemType != SudokuElementType.GreaterThanLeft && character.SudokuElemType != SudokuElementType.GreaterThanRight
                    && character.SudokuElemType != SudokuElementType.GreaterThanUp && character.SudokuElemType != SudokuElementType.XvX
                    && character.SudokuElemType != SudokuElementType.XvV)
                {
                    int gridSize = SudokuStore.Instance.Sudoku.Grid.Size;
                    GridMargin = 3 * CellSize + _defaultMargin;
                    if (character.Location == ElementLocationType.GridLeft)
                    {
                        PlaceNumberOutside(group, character.Text, character.RowIndex, character.ColIndex, 3 * CellSize + _defaultMargin, _defaultMargin);
                    }
                    else if (character.Location == ElementLocationType.GridRight)
                    {
                        PlaceNumberOutside(group, character.Text, character.RowIndex, character.ColIndex, 3 * CellSize + _defaultMargin, (gridSize + 3) * CellSize +_defaultMargin);
                    }
                    else if (character.Location == ElementLocationType.GridUp)
                    {
                        PlaceNumberOutside(group, character.Text, character.RowIndex, character.ColIndex, _defaultMargin, 3 * CellSize + _defaultMargin);
                    }
                    else if (character.Location == ElementLocationType.GridDown)
                    {
                        PlaceNumberOutside(group, character.Text, character.RowIndex, character.ColIndex, (gridSize + 3) * CellSize + _defaultMargin, 3 * CellSize + _defaultMargin);
                    }
                    else
                    {
                        PlaceNumberOutside(group, character.Text, character.RowIndex, character.ColIndex, GridMargin, GridMargin);
                    }
                }
            }
        }

        private static void TransformGridMarginByVariant(SvgDocument doc, SvgIDs id, SudokuElementType elemType)
        {
            if (TransformGridSize(elemType, SudokuStore.Instance.Sudoku.RightNumbersType) ||
                TransformGridSize(elemType, SudokuStore.Instance.Sudoku.LeftNumbersType) ||
                TransformGridSize(elemType, SudokuStore.Instance.Sudoku.BottomNumbersType) ||
                TransformGridSize(elemType, SudokuStore.Instance.Sudoku.UpNumbersType))
            {
                SvgGroup group = PlaceNumbersAround(doc, id, elemType);
                if (elemType == SudokuElementType.StarProduct)
                {
                    PlaceStar(doc, SvgIDs.StarProduct, elemType, group);
                }
            }
        }

        private static bool TransformGridSize(SudokuElementType elemType, SudokuElementType[,] collectionType)
        {
            foreach (var elem in collectionType)
            {
                if (elem == elemType)
                {
                    GridMargin = _defaultMargin + 3 * CellSize;
                    return true;
                }
            }
            return false;
        }

        private static SvgGroup PlaceNumbersAround(SvgDocument doc, SvgIDs groupID, SudokuElementType elemType)
        {
            SvgGroup givenNumbersGroup = new SvgGroup();
            int gridSize = SudokuStore.Instance.Sudoku.Grid.Size;
            CreateSkyscraperFromCollection(givenNumbersGroup, SudokuStore.Instance.Sudoku.LeftNumbers, gridSize, 3,
                SudokuStore.Instance.Sudoku.LeftNumbersType, elemType, 3 * CellSize, 0);
            CreateSkyscraperFromCollection(givenNumbersGroup, SudokuStore.Instance.Sudoku.RightNumbers, gridSize, 3,
                SudokuStore.Instance.Sudoku.RightNumbersType, elemType, 3 * CellSize, (gridSize + 3) * CellSize);
            CreateSkyscraperFromCollection(givenNumbersGroup, SudokuStore.Instance.Sudoku.BottomNumbers, 3, gridSize,
                SudokuStore.Instance.Sudoku.BottomNumbersType, elemType, (gridSize + 3) * CellSize, 3 * CellSize);
            CreateSkyscraperFromCollection(givenNumbersGroup, SudokuStore.Instance.Sudoku.UpNumbers, 3, gridSize,
                SudokuStore.Instance.Sudoku.UpNumbersType, elemType, 0, 3 * CellSize);
            givenNumbersGroup.FontSize = GivenNumbersFontSize;
            givenNumbersGroup.ID = groupID.ToString();
            doc.Children.Add(givenNumbersGroup);
            return givenNumbersGroup;
        }

        private static void CreateSkyscraperFromCollection(SvgGroup givenNumbersGroup, int[,] collection, int rowSize, int colSize,
            SudokuElementType[,] typeCollection, SudokuElementType type, int rowShift, int colShift)
        {
            for (int row = 0; row < rowSize; row++)
            {
                for (int col = 0; col < colSize; col++)
                {
                    int actualNumber = collection[row, col];
                    if (actualNumber != 0 && typeCollection[row, col] == type)
                    {
                        SvgText svgNumber = new SvgText(actualNumber.ToString());
                        svgNumber.FontFamily = Font;
                        SvgUnitCollection unitX = new SvgUnitCollection();
                        if (actualNumber < 10)
                        {
                            unitX.Add(col * CellSize + 15 + colShift + _defaultMargin);
                        }
                        else
                        {
                            unitX.Add(col * CellSize + 7 + colShift + _defaultMargin);
                        }
                        
                        svgNumber.X = unitX;
                        SvgUnitCollection unitY = new SvgUnitCollection();
                        unitY.Add((row + 1) * CellSize - 10 + rowShift + _defaultMargin);
                        svgNumber.Y = unitY;

                        givenNumbersGroup.Children.Add(svgNumber);
                    }
                }
            }
        }

        private static void PlaceBoldArrow(SvgDocument doc, SvgIDs id, SudokuElementType elemType)
        {
            SvgGroup group = new SvgGroup();
            group.ID = id.ToString();
            doc.Children.Add(group);

            foreach (var elem in _sudoku.SudokuVariants)
            {
                BoldArrow arrow = elem as BoldArrow;
                if (arrow != null)
                {
                    if (arrow.SudokuElemType == elemType)
                    {
                        CreateBoldArrow(group, arrow);
                    }
                }
            }
        }

        private static void CreateBoldArrow(SvgGroup group, BoldArrow arrow)
        {
            SvgPath polygon = new SvgPath();
            group.Children.Add(polygon);
            polygon.Fill = new SvgColourServer(Color.Gray);
            System.Windows.Media.PointCollection points = BoldArrowViewModel.CreatePoints(arrow.ColIndex * CellSize, arrow.RowIndex * CellSize, arrow.ElementType);
            SvgPathSegmentList dAttribute = new SvgPathSegmentList();

            Point previous = new Point((int)(points[0].X + GridMargin), (int)(points[0].Y + GridMargin));
            SvgMoveToSegment moveTo = new SvgMoveToSegment(previous);
            dAttribute.Add(moveTo);

            for (int i = 1; i < points.Count; i++)
            {
                Point actual = new Point((int)(points[i].X + GridMargin), (int)(points[i].Y + GridMargin));
                SvgLineSegment lineSegment = new SvgLineSegment(previous, actual);
                dAttribute.Add(lineSegment);
                previous = actual;
            }

            SvgClosePathSegment close = new SvgClosePathSegment();
            dAttribute.Add(close);
            polygon.PathData = dAttribute;
        }


        private static void PlaceStar(SvgDocument doc, SvgIDs id, SudokuElementType elemType, SvgGroup group = null)
        {
            if (group == null)
            {
                group = new SvgGroup();
                group.ID = id.ToString();
                doc.Children.Add(group);
            }

            double inCell = GridSizeStore.InCellElementSize;
            double radius = GridSizeStore.InnerRadiusStar;
            double cell = GridSizeStore.XCellSize;

            GridSizeStore.InCellElementSize = ElementInCellSize;
            GridSizeStore.XCellSize = CellSize;

            foreach (var elem in _sudoku.SudokuVariants)
            {
                Star star = elem as Star;
                if (star != null)
                {
                    if (star.SudokuElemType == elemType)
                    {
                        CreateStar(group, star);
                    }
                }
            }

            GridSizeStore.InCellElementSize = inCell;
            GridSizeStore.InnerRadiusStar = radius;
            GridSizeStore.XCellSize = cell;
        }

        private static void CreateStar(SvgGroup group, Star star)
        {
            SvgPath polygon = new SvgPath();
            group.Children.Add(polygon);
            polygon.Fill = new SvgColourServer(Color.Gray);
            System.Windows.Media.PointCollection points = StarViewModel.CreatePoints(star.ColIndex * CellSize, star.RowIndex * CellSize);
            SvgPathSegmentList dAttribute = new SvgPathSegmentList();

            Point previous = new Point((int)(points[0].X + GridMargin), (int)(points[0].Y + GridMargin));
            SvgMoveToSegment moveTo = new SvgMoveToSegment(previous);
            dAttribute.Add(moveTo);

            for (int i = 1; i < points.Count; i++)
            {
                Point actual = new Point((int)(points[i].X + GridMargin), (int)(points[i].Y + GridMargin));
                SvgLineSegment lineSegment = new SvgLineSegment(previous, actual);
                dAttribute.Add(lineSegment);
                previous = actual;
            }

            SvgClosePathSegment close = new SvgClosePathSegment();
            dAttribute.Add(close);
            polygon.PathData = dAttribute;
        }

        private static void CreateSmallArrow(SvgGroup group, double left, double top, GraphicElementType graphicType, double rowShift, double colShift)
        {
            SvgPath polygon = new SvgPath();
            group.Children.Add(polygon);
            polygon.Fill = new SvgColourServer(Color.Black);
            System.Windows.Media.PointCollection points = SmallArrowViewModel.CreatePoints(graphicType);
            SvgPathSegmentList dAttribute = new SvgPathSegmentList();

            Point previous = new Point((int)(points[0].X + colShift + left),
                (int)(points[0].Y + rowShift + top));
            SvgMoveToSegment moveTo = new SvgMoveToSegment(previous);
            dAttribute.Add(moveTo);

            for (int i = 1; i < points.Count; i++)
            {
                Point actual = new Point((int)(points[i].X + colShift + left),
                    (int)(points[i].Y + rowShift + top));
                SvgLineSegment lineSegment = new SvgLineSegment(previous, actual);
                dAttribute.Add(lineSegment);
                previous = actual;
            }

            SvgClosePathSegment close = new SvgClosePathSegment();
            dAttribute.Add(close);
            polygon.PathData = dAttribute;
        }

        private static void PlaceNumberOutside(SvgGroup group, string number, int row, int col, int rowShift, int colShift)
        {
            SvgText svgNumber = new SvgText(number.ToString());
            svgNumber.FontFamily = Font;
            SvgUnitCollection unitX = new SvgUnitCollection();

            if (number.Length < 2)
            {
                unitX.Add(col * CellSize + 15 + colShift);
            }
            else
            {
                unitX.Add(col * CellSize + 7 + colShift);
            }

            svgNumber.X = unitX;

            SvgUnitCollection unitY = new SvgUnitCollection();
            unitY.Add((row + 1) * CellSize - 10 + rowShift);
            svgNumber.Y = unitY;
            svgNumber.FontSize = GivenNumbersFontSize;

            group.Children.Add(svgNumber);
        }

        private static void LittleKillerTransform(SvgDocument doc)
        {
            if (_sudoku.Variants.Contains(SudokuType.LittleKiller))
            {
                GridMargin = _defaultMargin + 3 * CellSize;
                SvgPath path = CreatePath(GridMargin, GridMargin, (CellSize * GridSize) + GridMargin, (CellSize * GridSize) + GridMargin);
                path.StrokeWidth = 3;
                path.Stroke = new SvgColourServer(Color.Gray);
                SvgPath secondPath = CreatePath((CellSize * GridSize) + GridMargin, GridMargin, GridMargin, (CellSize * GridSize) + GridMargin);
                secondPath.StrokeWidth = 3;
                secondPath.Stroke = new SvgColourServer(Color.Gray);
                doc.Children.Add(path);
                doc.Children.Add(secondPath);

                LittleKillerType(doc, SvgIDs.LittleKillerLeftDown, SudokuElementType.LittleKillerLeftDown, GraphicElementType.LeftDown);
                LittleKillerType(doc, SvgIDs.LittleKillerLeftUp, SudokuElementType.LittleKillerLeftUp, GraphicElementType.LeftUp);
                LittleKillerType(doc, SvgIDs.LittleKillerRightUp, SudokuElementType.LittleKillerRightUp, GraphicElementType.RightUp);
                LittleKillerType(doc, SvgIDs.LittleKillerRightDown, SudokuElementType.LittleKillerRightDown, GraphicElementType.RightDown);
            }

        }

        private static void LittleKillerType(SvgDocument doc, SvgIDs id, SudokuElementType elemType, GraphicElementType graphicElem)
        {
            SvgGroup group = new SvgGroup();
            group.ID = id.ToString();
            int gridSize = SudokuStore.Instance.Sudoku.Grid.Size;
            LittleKillerNumberAround(group, _sudoku.LeftNumbers, ElementLocationType.GridLeft, gridSize, 3, 3 * CellSize, 0,
                _sudoku.LeftNumbersType, elemType, graphicElem);
            LittleKillerNumberAround(group, _sudoku.RightNumbers, ElementLocationType.GridRight, gridSize, 3, 3 * CellSize, (gridSize + 3) * CellSize,
                _sudoku.RightNumbersType, elemType, graphicElem);
            LittleKillerNumberAround(group, _sudoku.UpNumbers, ElementLocationType.GridUp, 3, gridSize, 0, 3 * CellSize,
                _sudoku.UpNumbersType, elemType, graphicElem);
            LittleKillerNumberAround(group, _sudoku.BottomNumbers, ElementLocationType.GridDown, 3, gridSize, (gridSize + 3) * CellSize, 3 * CellSize,
                _sudoku.BottomNumbersType, elemType, graphicElem);
            if (group.Children.Count != 0)
            {
                doc.Children.Add(group);
            }
        }

        private static void LittleKillerNumberAround(SvgGroup group, int[,] collection, ElementLocationType location, int rowSize, int colSize,
            int rowShift, int colShift, SudokuElementType[,] sudokuElementTypes, SudokuElementType elemType, GraphicElementType graphicType)
        {
            for (int row = 0; row < rowSize; row++)
            {
                for (int col = 0; col < colSize; col++)
                {
                    if (collection[row, col] != 0 && sudokuElementTypes[row, col] == elemType)
                    {
                        SmallArrow arrow = FindSmallArrow(location, row, col);
                        CreateSmallArrow(group, col * CellSize, row * CellSize,
                            graphicType, rowShift + _defaultMargin, colShift + _defaultMargin);
                        PlaceNumberOutside(group, collection[row, col].ToString(), row, col, rowShift, colShift);
                    }
                }
            }
        }

        private static SmallArrow FindSmallArrow(ElementLocationType location, int row, int col)
        {
            foreach (var elem in _sudoku.SudokuVariants)
            {
                SmallArrow arrow = elem as SmallArrow;
                if (arrow != null && (arrow.Left / GridSizeStore.XCellSize) == col && (arrow.Top / GridSizeStore.XCellSize) == row &&
                    arrow.Location == location)
                {
                    return arrow;
                }
            }
            return null;
        }

        private static void PlaceLongArrowWithCircle(SvgDocument doc, SvgIDs id, SudokuElementType elemType, SvgGroup group = null)
        {
            if (group == null)
            {
                group = new SvgGroup();
                group.ID = id.ToString();
                doc.Children.Add(group);
            }

            foreach (var elem in _sudoku.SudokuVariants)
            {
                LongArrowWithCircle arrow = elem as LongArrowWithCircle;
                if (arrow != null)
                {
                    if (arrow.SudokuElemType == elemType)
                    {
                        CreateLongArrowWithCircle(group, arrow);
                    }
                }
            }
        }

        private static void CreateLongArrowWithCircle(SvgGroup group, LongArrowWithCircle arrow)
        {
            CreateLongArrow(group, arrow.Arrow.Positions);
            CreateCircleWithGreyEdge(group, arrow.Circle.RowIndex, arrow.Circle.ColIndex);
        }

        private static void CreateCircleWithGreyEdge(SvgGroup group, double row, double col)
        {
            SvgCircle circle = new SvgCircle();
            group.Children.Add(circle);
            circle.Radius = ElementInCellSize / 2;
            circle.CenterX = (int)(GridMargin + col * CellSize + (CellSize / 2));
            circle.CenterY = (int)(GridMargin + row * CellSize + (CellSize / 2));
            circle.Fill = new SvgColourServer(Color.White);
            circle.Stroke = new SvgColourServer(Color.Gray);
            circle.StrokeWidth = 3;
        }

        private static void CreateLongArrow(SvgGroup group, ObservableCollection<Tuple<int, int>> positions)
        {
            if (positions.Count < 2)
            {
                return;
            }
            SvgPath line = new SvgPath();
            group.Children.Add(line);
            line.Stroke = new SvgColourServer(Color.Gray);
            line.StrokeWidth = 3;
            line.Fill = new SvgColourServer(Color.Transparent);

            Point previous = new Point(positions[0].Item2 * CellSize + GridMargin + (CellSize / 2),
                positions[0].Item1 * CellSize + GridMargin + (CellSize / 2));
            SvgPathSegmentList dAttribute = new SvgPathSegmentList();
            SvgMoveToSegment moveTo = new SvgMoveToSegment(previous);
            dAttribute.Add(moveTo);

            for (int i = 1; i < positions.Count; i++)
            {
                Tuple<int, int> position = positions[i];
                Point actual = new Point(position.Item2 * CellSize + GridMargin + (CellSize / 2),
                    position.Item1 * CellSize + GridMargin + (CellSize / 2));
                SvgLineSegment lineSegment = new SvgLineSegment(previous, actual);
                dAttribute.Add(lineSegment);
                previous = actual;
            }

            int lastIndex = positions.Count - 1;
            int previousIndex = positions.Count - 2;
            //item1 row, item2 col
            if (positions[lastIndex].Item1 == positions[previousIndex].Item1)
            {
                // in row
                if (positions[previousIndex].Item2 < positions[lastIndex].Item2)
                {
                    Point actual = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 4),
                    positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 4));
                    dAttribute.Add(new SvgLineSegment(previous, actual));

                    Point actual2 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 2),
                    positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 2));
                    dAttribute.Add(new SvgLineSegment(actual2, actual2));

                    Point actual3 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 4),
                    positions[lastIndex].Item1 * CellSize + GridMargin + 3 * (CellSize / 4));
                    dAttribute.Add(new SvgLineSegment(actual2, actual3));
                }
                else
                {
                    Point actual = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + 3 * (CellSize / 4),
                    positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 4));
                    dAttribute.Add(new SvgLineSegment(previous, actual));

                    Point actual2 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 2),
                    positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 2));
                    dAttribute.Add(new SvgLineSegment(actual2, actual2));

                    Point actual3 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + 3 * (CellSize / 4),
                    positions[lastIndex].Item1 * CellSize + GridMargin + 3 * (CellSize / 4));
                    dAttribute.Add(new SvgLineSegment(actual2, actual3));
                }
            }
            else if (positions[lastIndex].Item2 == positions[previousIndex].Item2)
            {
                // in col
                if (positions[lastIndex].Item1 > positions[previousIndex].Item1)
                {
                    Point actual = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 4),
                    positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 4));
                    dAttribute.Add(new SvgLineSegment(previous, actual));

                    Point actual2 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 2),
                    positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 2));
                    dAttribute.Add(new SvgLineSegment(actual2, actual2));

                    Point actual3 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + 3 * (CellSize / 4),
                    positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 4));
                    dAttribute.Add(new SvgLineSegment(actual2, actual3));
                }
                else
                {
                    Point actual = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 4),
                    positions[lastIndex].Item1 * CellSize + GridMargin + 3 * (CellSize / 4));
                    dAttribute.Add(new SvgLineSegment(previous, actual));

                    Point actual2 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 2),
                    positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 2));
                    dAttribute.Add(new SvgLineSegment(actual2, actual2));

                    Point actual3 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + 3 * (CellSize / 4),
                    positions[lastIndex].Item1 * CellSize + GridMargin + 3 * (CellSize / 4));
                    dAttribute.Add(new SvgLineSegment(actual2, actual3));
                }
            }
            else if (positions[lastIndex].Item1 < positions[previousIndex].Item1 && positions[lastIndex].Item2 < positions[previousIndex].Item2)
            {
                Point actual = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 2),
                    positions[lastIndex].Item1 * CellSize + GridMargin + 3 * (CellSize / 4));
                dAttribute.Add(new SvgLineSegment(previous, actual));

                Point actual2 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 2),
                positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 2));
                dAttribute.Add(new SvgLineSegment(actual2, actual2));

                Point actual3 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + 3 * (CellSize / 4),
                positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 2));
                dAttribute.Add(new SvgLineSegment(actual2, actual3));
            }
            else if (positions[lastIndex].Item1 < positions[previousIndex].Item1 && positions[lastIndex].Item2 > positions[previousIndex].Item2)
            {
                Point actual = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 4),
                    positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 2));
                dAttribute.Add(new SvgLineSegment(previous, actual));

                Point actual2 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 2),
                positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 2));
                dAttribute.Add(new SvgLineSegment(actual2, actual2));

                Point actual3 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 2),
                positions[lastIndex].Item1 * CellSize + GridMargin + 3 * (CellSize / 4));
                dAttribute.Add(new SvgLineSegment(actual2, actual3));
            }
            else if (positions[lastIndex].Item1 > positions[previousIndex].Item1 && positions[lastIndex].Item2 > positions[previousIndex].Item2)
            {
                Point actual = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 4),
                    positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 2));
                dAttribute.Add(new SvgLineSegment(previous, actual));

                Point actual2 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 2),
                positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 2));
                dAttribute.Add(new SvgLineSegment(actual2, actual2));

                Point actual3 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 2),
                positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 4));
                dAttribute.Add(new SvgLineSegment(actual2, actual3));
            }
            else
            {
                Point actual = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 2),
                       positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 4));
                dAttribute.Add(new SvgLineSegment(previous, actual));

                Point actual2 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + (CellSize / 2),
                positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 2));
                dAttribute.Add(new SvgLineSegment(actual2, actual2));

                Point actual3 = new Point(positions[lastIndex].Item2 * CellSize + GridMargin + 3 * (CellSize / 4),
                positions[lastIndex].Item1 * CellSize + GridMargin + (CellSize / 2));
                dAttribute.Add(new SvgLineSegment(actual2, actual3));

            }

            line.PathData = dAttribute;
        }
    }
}
