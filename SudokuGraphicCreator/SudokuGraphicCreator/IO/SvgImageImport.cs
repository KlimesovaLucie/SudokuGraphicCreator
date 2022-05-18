using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using Svg;
using Svg.Pathing;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SudokuGraphicCreator.IO
{
    /// <summary>
    /// Import SVG from file and create <see cref="Sudoku"/> from data and export solution.
    /// </summary>
    public class SvgImageImport
    {
        private static Sudoku _sudoku = SudokuStore.Instance.Sudoku;
        
        /// <summary>
        /// Open <paramref name="name"/> and export solution into <paramref name="nameOfFile"/>.
        /// </summary>
        /// <param name="name">File with SVG.</param>
        /// <param name="nameOfFile">Name of file to export solution.</param>
        public static void OpenSvg(string name, string nameOfFile)
        {
            try
            {
                SvgDocument doc = SvgDocument.Open(name);

                SudokuStore.Instance.Sudoku = new Sudoku(9, 3, 3);
                _sudoku = SudokuStore.Instance.Sudoku;
                // first numbers around the grid
                foreach (var group in doc.Children)
                {
                    if (group.ID == SvgIDs.Skyscrapers.ToString())
                    {
                        ImportNumbersAroundGrid(group, SudokuType.Skyscraper, SudokuElementType.Skyscrapers, GraphicElementType.None);
                    }
                    else if (group.ID == SvgIDs.NextToNine.ToString())
                    {
                        ImportNumbersAroundGrid(group, SudokuType.NextToNine, SudokuElementType.NextToNine, GraphicElementType.None);
                    }
                    else if (group.ID == SvgIDs.Outside.ToString())
                    {
                        ImportNumbersAroundGrid(group, SudokuType.Outside, SudokuElementType.Outside, GraphicElementType.None);
                    }
                    else if (group.ID == SvgIDs.StarProduct.ToString())
                    {
                        ImportNumbersAroundGrid(group, SudokuType.StarProduct, SudokuElementType.StarProduct, GraphicElementType.None);
                    }
                    else if (group.ID == SvgIDs.LittleKillerLeftDown.ToString())
                    {
                        ImportNumbersAroundGrid(group, SudokuType.LittleKiller, SudokuElementType.LittleKillerLeftDown, GraphicElementType.LeftDown);
                    }
                    else if (group.ID == SvgIDs.LittleKillerLeftUp.ToString())
                    {
                        ImportNumbersAroundGrid(group, SudokuType.LittleKiller, SudokuElementType.LittleKillerLeftUp, GraphicElementType.LeftUp);
                    }
                    else if (group.ID == SvgIDs.LittleKillerRightDown.ToString())
                    {
                        ImportNumbersAroundGrid(group, SudokuType.LittleKiller, SudokuElementType.LittleKillerRightDown, GraphicElementType.RightDown);
                    }
                    else if (group.ID == SvgIDs.LittleKillerRightUp.ToString())
                    {
                        ImportNumbersAroundGrid(group, SudokuType.LittleKiller, SudokuElementType.LittleKillerRightUp, GraphicElementType.RightUp);
                    }
                    else if (group.ID == SvgIDs.BoldGrid.ToString())
                    {
                        ImportBoxes(group);
                    }
                }

                foreach (var group in doc.Children)
                {
                    if (group.ID == SvgIDs.GivenNumbers.ToString())
                    {
                        ImportGivenNumbers(group, SudokuStore.Instance.Sudoku);
                    }
                    else if (group.ID == SvgIDs.Diagonal.ToString())
                    {
                        AddVariant(SudokuType.Diagonal);
                    }
                    else if (group.ID == SvgIDs.Windoku.ToString())
                    {
                        AddVariant(SudokuType.Windoku);
                    }
                    else if (group.ID == SvgIDs.Antiknight.ToString())
                    {
                        AddVariant(SudokuType.Antiknight);
                    }
                    else if (group.ID == SvgIDs.Nonconsecutive.ToString())
                    {
                        AddVariant(SudokuType.Nonconsecutive);
                    }
                    else if (group.ID == SvgIDs.Untouchable.ToString())
                    {
                        AddVariant(SudokuType.Untouchable);
                    }
                    else if (group.ID == SvgIDs.DisjointGroup.ToString())
                    {
                        AddVariant(SudokuType.DisjointGroups);
                    }
                    else if (group.ID == SvgIDs.Sum.ToString())
                    {
                        AddCircleWithNumberElems(group, SudokuType.Sum, SudokuElementType.Sum);
                    }
                    else if (group.ID == SvgIDs.Difference.ToString())
                    {
                        AddCircleWithNumberElems(group, SudokuType.Difference, SudokuElementType.Difference);
                    }
                    else if (group.ID == SvgIDs.Consecutive.ToString())
                    {
                        AddWhiteCircleElems(group, SudokuType.Consecutive, SudokuElementType.Consecutive);
                    }
                    else if (group.ID == SvgIDs.KropkiWhite.ToString())
                    {
                        AddWhiteCircleElems(group, SudokuType.Kropki, SudokuElementType.WhiteKropki);
                    }
                    else if (group.ID == SvgIDs.KropkiBlack.ToString())
                    {
                        AddBlackCircleElems(group, SudokuType.Kropki, SudokuElementType.BlackKropki);
                    }
                    else if (group.ID == SvgIDs.GreaterThanLeft.ToString())
                    {
                        ImportGTTextOnEdge(group, SudokuType.GreaterThan, SudokuElementType.GreaterThanLeft, ElementLocationType.Row);
                    }
                    else if (group.ID == SvgIDs.GreaterThanRight.ToString())
                    {
                        ImportGTTextOnEdge(group, SudokuType.GreaterThan, SudokuElementType.GreaterThanRight, ElementLocationType.Row);
                    }
                    else if (group.ID == SvgIDs.GreaterThanUp.ToString())
                    {
                        ImportGTTextOnEdge(group, SudokuType.GreaterThan, SudokuElementType.GreaterThanUp, ElementLocationType.Column);
                    }
                    else if (group.ID == SvgIDs.GreaterThanDown.ToString())
                    {
                        ImportGTTextOnEdge(group, SudokuType.GreaterThan, SudokuElementType.GreaterThanDown, ElementLocationType.Column);
                    }
                    else if (group.ID == SvgIDs.XvX.ToString())
                    {
                        ImportTextOnEdge(group, SudokuType.XV, SudokuElementType.XvX, "X");
                    }
                    else if (group.ID == SvgIDs.XvV.ToString())
                    {
                        ImportTextOnEdge(group, SudokuType.XV, SudokuElementType.XvV, "V");
                    }
                    else if (group.ID == SvgIDs.Odd.ToString())
                    {
                        ImportGreyCircleInCell(group, SudokuType.Odd, SudokuElementType.Odd);
                    }
                    else if (group.ID == SvgIDs.Even.ToString())
                    {
                        ImportGreySquareInCell(group, SudokuType.Even, SudokuElementType.Even);
                    }
                    else if (group.ID == SvgIDs.SearchNineLeft.ToString())
                    {
                        ImportBoldArrow(group, SudokuType.SearchNine, SudokuElementType.SearchNineLeft);
                    }
                    else if (group.ID == SvgIDs.SearchNineRight.ToString())
                    {
                        ImportBoldArrow(group, SudokuType.SearchNine, SudokuElementType.SearchNineRight);
                    }
                    else if (group.ID == SvgIDs.SearchNineUp.ToString())
                    {
                        ImportBoldArrow(group, SudokuType.SearchNine, SudokuElementType.SearchNineUp);
                    }
                    else if (group.ID == SvgIDs.SearchNineDown.ToString())
                    {
                        ImportBoldArrow(group, SudokuType.SearchNine, SudokuElementType.SearchNineDown);
                    }
                    else if (group.ID == SvgIDs.Palindromes.ToString())
                    {
                        ImportLines(group, SudokuType.Palindrome, SudokuElementType.Palindromes);
                    }
                    else if (group.ID == SvgIDs.Sequences.ToString())
                    {
                        ImportLines(group, SudokuType.Sequence, SudokuElementType.Sequences);
                    }
                    else if (group.ID == SvgIDs.Arrows.ToString())
                    {
                        ImportLongArrowWithCircle(group, SudokuType.Arrow, SudokuElementType.Arrows);
                    }
                    else if (group.ID == SvgIDs.Thermometers.ToString())
                    {
                        ImportLines(group, SudokuType.Thermometer, SudokuElementType.Thermometers);
                    }
                    else if (group.ID == SvgIDs.ExtraRegions.ToString())
                    {
                        ImportExtraRegions(group);
                    }
                    else if (group.ID == SvgIDs.Killer.ToString())
                    {
                        ImportCage(group, SudokuType.Killer, SudokuElementType.Killer);
                    }
                }

                using var ctSource = new CancellationTokenSource();
                int count = 0;
                int[,] finalSolution = new int[9, 9];
                //Rules.SolveSudoku.Solve(SudokuStore.Instance.Sudoku.GivenNumbers, SudokuStore.Instance.Sudoku.Grid.Size, 0, 0, ref count, finalSolution, ctSource.Token);

                var solveTask = new TaskFactory().StartNew(
                    () => Rules.SolveSudoku.Solve(SudokuStore.Instance.Sudoku.GivenNumbers, SudokuStore.Instance.Sudoku.Grid.Size, 0, 0, ref count, finalSolution, ctSource.Token),
                    ctSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                Thread.Sleep(3000);
                ctSource.Cancel();
                solveTask.Wait();


                for (int i = 0; i < SudokuStore.Instance.Sudoku.Grid.Size; i++)
                {
                    for (int j = 0; j < SudokuStore.Instance.Sudoku.Grid.Size; j++)
                    {
                        SudokuStore.Instance.Sudoku.GivenNumbers[i, j] = finalSolution[i, j];
                    }
                }
                SudokuSvgImage.ExportSaveSvgImage(Path.GetFullPath("./GeneratedSolutions/" + "sol_" + nameOfFile));
            }
            catch
            { }
        }

        private static bool ImportBoxes(SvgElement group)
        {
            _sudoku.Grid.Boxes = new ObservableCollection<ObservableCollection<Tuple<int, int>>>();
            foreach (var svgBox in group.Children)
            {
                SvgGroup svgGroup = svgBox as SvgGroup;
                if (svgGroup == null)
                {
                    return false;
                }
                ObservableCollection<Tuple<int, int>> box = new ObservableCollection<Tuple<int, int>>();

                for (int row = 0; row < _sudoku.Grid.Size; row++)
                {
                    for (int col = 0; col < _sudoku.Grid.Size; col++)
                    {
                        if (IsInThisBox(svgGroup, row, col))
                        {
                            box.Add(new Tuple<int, int>(row, col));
                        }
                    }
                }
                _sudoku.Grid.Boxes.Add(box);
            }
            return true;
        }

        private static bool IsInThisBox(SvgGroup svgGroup, int row, int col)
        {
            int foundedLeft = 0;
            int foundedRight = 0;
            int foundedTop = 0;
            int foundedDown = 0;
            if (row == 1 && col == 4)
            {

            }
            foreach (var svgLine in svgGroup.Children)
            {
                SvgPath line = svgLine as SvgPath;
                if (line != null)
                {
                    bool isVertical = true;
                    if (line.PathData[0].End.Y == line.PathData[1].End.Y &&
                        line.PathData[0].End.Y == line.PathData[0].Start.Y &&
                        line.PathData[0].End.Y == line.PathData[1].Start.Y)
                    {
                        isVertical = false;
                    }
                    float x = line.PathData[0].End.X;
                    float y = line.PathData[0].End.Y;

                    int rowIndex = (int)((y - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize);
                    int colIndex = (int)((x - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize);

                    if (rowIndex <= row && col == colIndex && !isVertical)
                    {
                        foundedTop++;
                    }

                    if (row < rowIndex && col == colIndex && !isVertical)
                    {
                        foundedDown++;
                    }

                    if (colIndex <= col && row == rowIndex && isVertical)
                    {
                        foundedLeft++;
                    }

                    if (col < colIndex && row == rowIndex && isVertical)
                    {
                        foundedRight++;
                    }
                }
            }

            return foundedLeft > 0 && foundedRight > 0 && foundedTop > 0 && foundedDown > 0;
        }

        private static bool ImportGivenNumbers(SvgElement group, Sudoku sudoku)
        {
            foreach (var givenNumber in group.Children)
            {
                SvgText svgNumber = givenNumber as SvgText;
                if (svgNumber != null)
                {
                    var x = svgNumber.X;
                    int colX;
                    if (!IsParseNumber(svgNumber.X.ToString(), out colX))
                    {
                        return false;
                    }
                    int rowY;
                    if (!IsParseNumber(svgNumber.Y.ToString(), out rowY))
                    {
                        return false;
                    }
                    int number;
                    if (!IsParseNumber(svgNumber.Text, out number))
                    {
                        return false;
                    }
                    sudoku.GivenNumbers[GetRowGivenNumber(rowY), GetColGivenNumber(colX, number)] = number;
                }
            }
            return true;
        }

        private static bool IsParseNumber(string number, out int result)
        {
            try
            {
                result = Int32.Parse(number);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }

        private static int GetColGivenNumber(int x, int number)
        {
            if (number >= 10)
            {
                return (x - 7 - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize;
            }
            return (x - 15 - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize;
        }

        private static int GetRowGivenNumber(int y)
        {
            return (y + 10 - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize - 1;
        }

        private static void AddVariant(SudokuType type)
        {
            _sudoku.Variants.Add(type);
        }

        private static bool AddCircleWithNumberElems(SvgElement group, SudokuType sudokutype, SudokuElementType elemType)
        {
            _sudoku.Variants.Add(sudokutype);
            if (group.Children.Count % 2 != 0)
            {
                return false;
            }
            for (int i = 0; i < group.Children.Count; i += 2)
            {
                SvgCircle circle = group.Children[i] as SvgCircle;
                SvgText number = group.Children[i + 1] as SvgText;
                if (circle == null || number == null)
                {
                    return false;
                }
                int x;
                if (!IsParseNumber(circle.CenterX.ToString(), out x))
                {
                    return false;
                }

                int y;
                if (!IsParseNumber(circle.CenterY.ToString(), out y))
                {
                    return false;
                }

                ElementLocationType location = ElementLocationType.Column;
                if ((x - SudokuSvgImage.GridMargin) % SudokuSvgImage.CellSize == 0)
                {
                    location = ElementLocationType.Row;
                    y -= SudokuSvgImage.CellSize / 2;
                }
                else
                {
                    x -= SudokuSvgImage.CellSize / 2;
                }
                x -= SudokuSvgImage.GridMargin / SudokuSvgImage.CellSize;
                y -= SudokuSvgImage.GridMargin / SudokuSvgImage.CellSize;

                CircleWithNumber elem = new CircleWithNumber(0, 0, y, x, elemType, location);
                int value;
                if (!IsParseNumber(number.Text, out value))
                {
                    return false;
                }
                elem.Value = value;
                _sudoku.SudokuVariants.Add(elem);
            }
            return true;
        }

        private static bool AddWhiteCircleElems(SvgElement group, SudokuType sudokutype, SudokuElementType elemType)
        {
            _sudoku.Variants.Add(sudokutype);
            for (int i = 0; i < group.Children.Count; i++)
            {
                SvgCircle circle = group.Children[i] as SvgCircle;
                if (circle == null)
                {
                    return false;
                }
                int x;
                if (!IsParseNumber(circle.CenterX.ToString(), out x))
                {
                    return false;
                }

                int y;
                if (!IsParseNumber(circle.CenterY.ToString(), out y))
                {
                    return false;
                }

                ElementLocationType location = ElementLocationType.Column;
                if ((x - SudokuSvgImage.GridMargin) % SudokuSvgImage.CellSize == 0)
                {
                    location = ElementLocationType.Row;
                    y -= SudokuSvgImage.CellSize / 2;
                }
                else
                {
                    x -= SudokuSvgImage.CellSize / 2;
                }
                x = (x - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize;
                y = (y - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize;

                WhiteCircle elem = new WhiteCircle(0, 0, y, x, elemType, location);
                _sudoku.SudokuVariants.Add(elem);
            }
            return true;
        }

        private static bool AddBlackCircleElems(SvgElement group, SudokuType sudokutype, SudokuElementType elemType)
        {
            if (!_sudoku.Variants.Contains(sudokutype))
            {
                _sudoku.Variants.Add(sudokutype);
            }
            for (int i = 0; i < group.Children.Count; i++)
            {
                SvgCircle circle = group.Children[i] as SvgCircle;
                if (circle == null)
                {
                    return false;
                }
                int x;
                if (!IsParseNumber(circle.CenterX.ToString(), out x))
                {
                    return false;
                }

                int y;
                if (!IsParseNumber(circle.CenterY.ToString(), out y))
                {
                    return false;
                }

                ElementLocationType location = ElementLocationType.Column;
                if ((x - SudokuSvgImage.GridMargin) % SudokuSvgImage.CellSize == 0)
                {
                    location = ElementLocationType.Row;
                    y -= SudokuSvgImage.CellSize / 2;
                }
                else
                {
                    x -= SudokuSvgImage.CellSize / 2;
                }
                x = (x - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize;
                y = (y - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize;

                BlackCircle elem = new BlackCircle(0, 0, y, x, elemType, location);
                _sudoku.SudokuVariants.Add(elem);
            }
            return true;
        }

        private static bool ImportGTTextOnEdge(SvgElement group, SudokuType sudokutype, SudokuElementType elemType, ElementLocationType location)
        {
            if (!_sudoku.Variants.Contains(sudokutype))
            {
                _sudoku.Variants.Add(sudokutype);
            }
            foreach (var gtSign in group.Children)
            {
                SvgText text = gtSign as SvgText;
                if (text != null)
                {
                    int colX;
                    if (!IsParseNumber(text.X.ToString(), out colX))
                    {
                        return false;
                    }
                    int rowY;
                    if (!IsParseNumber(text.Y.ToString(), out rowY))
                    {
                        return false;
                    }
                    int row = rowY;
                    int col = colX;
                    if (location == ElementLocationType.Row)
                    {
                        col += SudokuSvgImage.TextRowXSize / 2;
                        row = row - (SudokuSvgImage.CellSize / 2) - (SudokuSvgImage.TextRowYSize / 2);
                    }
                    else if (location == ElementLocationType.Column)
                    {
                        col = col - (SudokuSvgImage.CellSize / 2) - (SudokuSvgImage.TextColXSize / 2);
                        row -= SudokuSvgImage.TextColYSize / 2;
                    }
                    row -= SudokuSvgImage.GridMargin;
                    col -= SudokuSvgImage.GridMargin;
                    row /= SudokuSvgImage.CellSize;
                    col /= SudokuSvgImage.CellSize;
                    _sudoku.SudokuVariants.Add(new Character(0, 0, row, col, "", elemType, location));
                }
            }
            return true;
        }

        private static bool ImportTextOnEdge(SvgElement group, SudokuType sudokutype, SudokuElementType elemType, string value)
        {
            if (!_sudoku.Variants.Contains(sudokutype))
            {
                _sudoku.Variants.Add(sudokutype);
            }
            foreach (var gtSign in group.Children)
            {
                SvgText text = gtSign as SvgText;
                if (text != null)
                {
                    int colX;
                    if (!IsParseNumber(text.X.ToString(), out colX))
                    {
                        return false;
                    }
                    int rowY;
                    if (!IsParseNumber(text.Y.ToString(), out rowY))
                    {
                        return false;
                    }
                    ElementLocationType location = ElementLocationType.Row;
                    if ((rowY - (SudokuSvgImage.TextColYSize / 2) - SudokuSvgImage.GridMargin) % SudokuSvgImage.CellSize == 0)
                    {
                        location = ElementLocationType.Column;
                    }
                    int row = rowY;
                    int col = colX;
                    if (location == ElementLocationType.Row)
                    {
                        col += SudokuSvgImage.TextRowXSize / 2;
                        row = row - (SudokuSvgImage.CellSize / 2) - (SudokuSvgImage.TextRowYSize / 2);
                    }
                    else if (location == ElementLocationType.Column)
                    {
                        col = col - (SudokuSvgImage.CellSize / 2) - (SudokuSvgImage.TextColXSize / 2);
                        row -= SudokuSvgImage.TextColYSize / 2;
                    }
                    row -= SudokuSvgImage.GridMargin;
                    col -= SudokuSvgImage.GridMargin;
                    row /= SudokuSvgImage.CellSize;
                    col /= SudokuSvgImage.CellSize;
                    _sudoku.SudokuVariants.Add(new Character(0, 0, row, col, value, elemType, location));
                }
            }
            return true;
        }

        private static bool ImportGreyCircleInCell(SvgElement group, SudokuType sudokutype, SudokuElementType elemType)
        {
            _sudoku.Variants.Add(sudokutype);
            foreach (var svgCircle in group.Children)
            {
                SvgCircle circle = svgCircle as SvgCircle;
                if (circle == null)
                {
                    return false;
                }
                int x;
                if (!IsParseNumber(circle.CenterX.ToString(), out x))
                {
                    return false;
                }

                int y;
                if (!IsParseNumber(circle.CenterY.ToString(), out y))
                {
                    return false;
                }
                x = (x - ((SudokuSvgImage.CellSize - SudokuSvgImage.ElementInCellSize) / 2) -
                    (SudokuSvgImage.CellSize / 2)) / SudokuSvgImage.CellSize;
                y = (y - ((SudokuSvgImage.CellSize - SudokuSvgImage.ElementInCellSize) / 2) -
                    (SudokuSvgImage.CellSize / 2)) / SudokuSvgImage.CellSize;
                _sudoku.SudokuVariants.Add(new GreyCircle(0, 0, y, x, elemType, ElementLocationType.Cell));
            }
            return true;
        }

        private static bool ImportGreySquareInCell(SvgElement group, SudokuType sudokutype, SudokuElementType elemType)
        {
            _sudoku.Variants.Add(sudokutype);
            foreach (var svgSquare in group.Children)
            {
                SvgRectangle square = svgSquare as SvgRectangle;
                if (square == null)
                {
                    return false;
                }
                int x;
                if (!IsParseNumber(square.X.ToString(), out x))
                {
                    return false;
                }

                int y;
                if (!IsParseNumber(square.Y.ToString(), out y))
                {
                    return false;
                }
                x = (x - SudokuSvgImage.GridMargin - ((SudokuSvgImage.CellSize - SudokuSvgImage.ElementInCellSize) / 2)) / SudokuSvgImage.CellSize;
                y = (y - SudokuSvgImage.GridMargin - ((SudokuSvgImage.CellSize - SudokuSvgImage.ElementInCellSize) / 2)) / SudokuSvgImage.CellSize;
                _sudoku.SudokuVariants.Add(new GreySquare(0, 0, y, x, elemType, ElementLocationType.Cell));
            }
            return true;
        }

        private static bool ImportLines(SvgElement group, SudokuType sudokutype, SudokuElementType elemType)
        {
            _sudoku.Variants.Add(sudokutype);
            foreach (var svgLine in group.Children)
            {
                SvgPath line = svgLine as SvgPath;
                if (line == null && elemType != SudokuElementType.Thermometers)
                {
                    return false;
                }
                if (line != null)
                {
                    ObservableCollection<Tuple<int, int>> positions = new ObservableCollection<Tuple<int, int>>();

                    var dAttribute = line.PathData;
                    foreach (var segment in dAttribute)
                    {
                        positions.Add(new Tuple<int, int>((int)(segment.End.Y - (SudokuSvgImage.CellSize / 2) - SudokuSvgImage.GridMargin)
                            / SudokuSvgImage.CellSize, (int)(segment.End.X - (SudokuSvgImage.CellSize / 2) - SudokuSvgImage.GridMargin)
                            / SudokuSvgImage.CellSize));

                    }
                    Line actualLine = new Line(elemType, positions);
                    _sudoku.SudokuVariants.Add(actualLine);
                }
                else
                {
                    SvgCircle circle = svgLine as SvgCircle;
                    if (circle == null)
                    {
                        return false;
                    }
                    int x;
                    if (!IsParseNumber(circle.CenterX.ToString(), out x))
                    {
                        return false;
                    }

                    int y;
                    if (!IsParseNumber(circle.CenterY.ToString(), out y))
                    {
                        return false;
                    }
                    x = (x - ((SudokuSvgImage.CellSize - SudokuSvgImage.ElementInCellSize) / 2) -
                        (SudokuSvgImage.CellSize / 2)) / SudokuSvgImage.CellSize;
                    y = (y - ((SudokuSvgImage.CellSize - SudokuSvgImage.ElementInCellSize) / 2) -
                        (SudokuSvgImage.CellSize / 2)) / SudokuSvgImage.CellSize;
                    _sudoku.SudokuVariants.Add(new GreyCircle(0, 0, y, x, elemType, ElementLocationType.Cell));
                }
            }
            return true;
        }

        private static bool ImportCage(SvgElement group, SudokuType sudokutype, SudokuElementType elemType)
        {
            _sudoku.Variants.Add(sudokutype);
            foreach (var svgCage in group.Children)
            {
                SvgPath cage = svgCage as SvgPath;
                if (cage != null && SudokuType.Killer == sudokutype)
                {
                    ObservableCollection<Tuple<int, int>> positions = new ObservableCollection<Tuple<int, int>>();

                    var dAttribute = cage.PathData;
                    foreach (var segment in dAttribute)
                    {
                        int pointX = (int)(segment.End.X - SudokuSvgImage.GridMargin);
                        int pointY = (int)(segment.End.Y - SudokuSvgImage.GridMargin);

                        int rowIndex = pointY / (int)GridSizeStore.XCellSize;
                        int colIndex = pointX / (int)GridSizeStore.XCellSize;

                        Tuple<int, int> cell = new Tuple<int, int>(rowIndex, colIndex);
                        if (!positions.Contains(cell))
                        {
                            positions.Add(cell);
                        }
                    }

                    Cage modelCage = new Cage(elemType, positions);
                    _sudoku.SudokuVariants.Add(modelCage);
                }
                SvgText text = svgCage as SvgText;
                if (text != null)
                {
                    Cage actualCage = _sudoku.SudokuVariants[_sudoku.SudokuVariants.Count - 1] as Cage;
                    if (actualCage != null)
                    {
                        int value;
                        if (!IsParseNumber(text.Text, out value))
                        {
                            return false;
                        }
                        actualCage.Number = value;
                    }
                }
            }
            return true;
        }

        private static bool ImportNumbersAroundGrid(SvgElement group, SudokuType sudokuType, SudokuElementType elemType, GraphicElementType graphicType)
        {
            if (!_sudoku.Variants.Contains(sudokuType))
            {
                _sudoku.Variants.Add(sudokuType);
            }
            SmallArrow previousArrow = new SmallArrow(0, 0, 0, 0, 0, elemType, ElementLocationType.Cell, graphicType);
            SudokuSvgImage.GridMargin = 3 * SudokuSvgImage.CellSize + 5;
            foreach (var svgNumber in group.Children)
            {
                SvgText text = svgNumber as SvgText;
                if (text == null && elemType != SudokuElementType.StarProduct && elemType != SudokuElementType.LittleKillerLeftDown &&
                    elemType != SudokuElementType.LittleKillerLeftUp && elemType != SudokuElementType.LittleKillerRightDown &&
                    elemType != SudokuElementType.LittleKillerRightUp)
                {
                    return false;
                }
                if (text != null)
                {
                    int colX;
                    if (!IsParseNumber(text.X.ToString(), out colX))
                    {
                        return false;
                    }
                    int rowY;
                    if (!IsParseNumber(text.Y.ToString(), out rowY))
                    {
                        return false;
                    }
                    int number;
                    if (!IsParseNumber(text.Text, out number))
                    {
                        return false;
                    }
                    previousArrow.Value = number;
                    SaveNumberAroundGridInCorrectCollection(colX, rowY, number, elemType);
                }
                else
                {
                    SvgPath svgStar = svgNumber as SvgPath;
                    if (svgStar == null)
                    {
                        return false;
                    }
                    int col = (int)((svgStar.PathData[0].End.X - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize);
                    int row = (int)((svgStar.PathData[0].End.Y - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize);
                    if (elemType == SudokuElementType.StarProduct)
                    {
                        _sudoku.SudokuVariants.Add(new Star(col * SudokuSvgImage.CellSize, row * SudokuSvgImage.CellSize, row, col, elemType, ElementLocationType.Cell));
                    }
                    else
                    {
                        ElementLocationType location = ElementLocationType.GridDown;
                        col = (int)((svgStar.PathData[0].End.X - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize);
                        row = (int)((svgStar.PathData[0].End.Y - SudokuSvgImage.GridMargin - 9 * SudokuSvgImage.CellSize) /
                                SudokuSvgImage.CellSize);
                        int order = row * 9 + col;
                        if (svgStar.PathData[0].End.X <= SudokuSvgImage.GridMargin)
                        {
                            location = ElementLocationType.GridLeft;
                            col = (int)((svgStar.PathData[0].End.X - 5) / SudokuSvgImage.CellSize);
                            row = (int)((svgStar.PathData[0].End.Y - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize);
                            order = row * 3 + col;
                        }
                        else if ((3 * SudokuSvgImage.CellSize) <= svgStar.PathData[0].End.X &&
                            svgStar.PathData[0].End.X <= (9 * SudokuSvgImage.CellSize + SudokuSvgImage.GridMargin) &&
                            (svgStar.PathData[0].End.Y <= SudokuSvgImage.GridMargin))
                        {
                            col = (int)((svgStar.PathData[0].End.X - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize);
                            row = (int)((svgStar.PathData[0].End.Y - 5) / SudokuSvgImage.CellSize);
                            location = ElementLocationType.GridUp;
                            order = row * 9 + col;
                        }
                        else if (svgStar.PathData[0].End.X >= (9 * SudokuSvgImage.CellSize + SudokuSvgImage.GridMargin))
                        {
                            col = (int)((svgStar.PathData[0].End.X - SudokuSvgImage.GridMargin - 9 * SudokuSvgImage.CellSize) /
                                SudokuSvgImage.CellSize);
                            row = (int)((svgStar.PathData[0].End.Y - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize);
                            location = ElementLocationType.GridRight;
                            order = row * 3 + col;
                        }
                        SmallArrow arrow = new SmallArrow(col * SudokuSvgImage.CellSize, row * SudokuSvgImage.CellSize, row, col, order, elemType, location,
                            FindTypeForSmallArrow(svgStar.PathData[0].End.X, svgStar.PathData[0].End.Y));
                        previousArrow = arrow;
                        _sudoku.SudokuVariants.Add(arrow);
                    }
                }
            }
            return true;
        }

        private static GraphicElementType FindTypeForSmallArrow(double x, double y)
        {
            x = (x - SudokuSvgImage.GridMargin) % SudokuSvgImage.CellSize;
            y = (y - SudokuSvgImage.GridMargin) % SudokuSvgImage.CellSize;
            if (x <= (SudokuSvgImage.CellSize / 2) && y <= (SudokuSvgImage.CellSize / 2))
            {
                return GraphicElementType.LeftUp;
            }
            else if (x >= (SudokuSvgImage.CellSize / 2) && y <= (SudokuSvgImage.CellSize / 2))
            {
                return GraphicElementType.RightUp;
            }
            else if (x <= (SudokuSvgImage.CellSize / 2) && y >= (SudokuSvgImage.CellSize / 2))
            {
                return GraphicElementType.LeftDown;
            }
            return GraphicElementType.RightDown;
        }

        private static void SaveNumberAroundGridInCorrectCollection(int x, int y, int number, SudokuElementType elemType)
        {
            int indexX = 0;
            int indexY = 0;
            if (IsLeftOutsideGrid(x, y, out indexX, out indexY, number))
            {
                _sudoku.LeftNumbers[indexY, indexX] = number;
                _sudoku.LeftNumbersType[indexY, indexX] = elemType;
            }
            else if (IsUpOutsideGrid(x, y, out indexX, out indexY, number))
            {
                _sudoku.UpNumbers[indexY, indexX] = number;
                _sudoku.UpNumbersType[indexY, indexX] = elemType;
            }
            else if (IsRightOutsideGrid(x, y, out indexX, out indexY, number))
            {
                _sudoku.RightNumbers[indexY, indexX] = number;
                _sudoku.RightNumbersType[indexY, indexX] = elemType;
            }
            else if (IsBottomOutsideGrid(x, y, out indexX, out indexY, number))
            {
                _sudoku.BottomNumbers[indexY, indexX] = number;
                _sudoku.BottomNumbersType[indexY, indexX] = elemType;
            }
        }

        private static bool IsLeftOutsideGrid(int x, int y, out int indexX, out int indexY, int number)
        {
            if (number < 10)
            {
                indexX = (x - 15) / SudokuSvgImage.CellSize;
            }
            else
            {
                indexX = (x - 7) / SudokuSvgImage.CellSize;
            }
            indexY = ((y - (3 * SudokuSvgImage.CellSize) + 10) / SudokuSvgImage.CellSize) - 1;
            return x <= SudokuSvgImage.GridMargin;
        }

        private static bool IsUpOutsideGrid(int x, int y, out int indexX, out int indexY, int number)
        {
            if (number < 10)
            {
                indexX = (x - 15 - (3 * SudokuSvgImage.CellSize)) / SudokuSvgImage.CellSize;
            }
            else
            {
                indexX = (x - 7 - (3 * SudokuSvgImage.CellSize)) / SudokuSvgImage.CellSize;
            }
            indexY = ((y + 10) / SudokuSvgImage.CellSize) - 1;
            return (3 * SudokuSvgImage.CellSize) <= x && x <= (12 * SudokuSvgImage.CellSize) &&
                (y <= SudokuSvgImage.GridMargin);
        }

        private static bool IsRightOutsideGrid(int x, int y, out int indexX, out int indexY, int number)
        {
            if (number < 10)
            {
                indexX = (x - 15 - (12 * SudokuSvgImage.CellSize)) / SudokuSvgImage.CellSize;
            }
            else
            {
                indexX = (x - 7 - (12 * SudokuSvgImage.CellSize)) / SudokuSvgImage.CellSize;
            }
            indexY = ((y - (3 * SudokuSvgImage.CellSize) + 10) / SudokuSvgImage.CellSize) - 1;
            return x >= (12 * SudokuSvgImage.CellSize);
        }

        private static bool IsBottomOutsideGrid(int x, int y, out int indexX, out int indexY, int number)
        {
            if (number < 10)
            {
                indexX = (x - 15 - (3 * SudokuSvgImage.CellSize)) / SudokuSvgImage.CellSize;
            }
            else
            {
                indexX = (x - 7 - (3 * SudokuSvgImage.CellSize)) / SudokuSvgImage.CellSize;
            }
            indexY = ((y - (12 * SudokuSvgImage.CellSize) + 10) / SudokuSvgImage.CellSize) - 1;
            return (3 * SudokuSvgImage.CellSize) <= x && x <= (12 * SudokuSvgImage.CellSize) &&
                (y >= (12 * SudokuSvgImage.CellSize));
        }

        private static bool ImportBoldArrow(SvgElement group, SudokuType sudokutype, SudokuElementType elemType)
        {
            if (!_sudoku.Variants.Contains(sudokutype))
            {
                _sudoku.Variants.Add(sudokutype);
            }
            
            foreach (var childArrow in group.Children)
            {
                SvgPath svgArrow = childArrow as SvgPath;
                if (svgArrow == null)
                {
                    return false;
                }
                SvgPathSegmentList dAttribute = svgArrow.PathData;
                float svgY = dAttribute[0].End.Y;
                float svgX = dAttribute[0].End.X;
                int row;
                int col;
                GraphicElementType graphicType = BoldArrowLocation(svgX, svgY, out row, out col, elemType);
                _sudoku.SudokuVariants.Add(new BoldArrow(svgX - SudokuSvgImage.GridMargin, svgY - SudokuSvgImage.GridSize,
                    row, col, elemType, graphicType));
            }
            return true;
        }

        private static GraphicElementType BoldArrowLocation(float svgX, float svgY, out int row, out int col, SudokuElementType elemType)
        {
            svgY -= SudokuSvgImage.GridSize;
            svgX -= SudokuSvgImage.GridSize;
            row = (int)(svgY / SudokuSvgImage.CellSize);
            col = (int)(svgX / SudokuSvgImage.CellSize);
            if (elemType == SudokuElementType.SearchNineLeft)
            {
                return GraphicElementType.Left;
            }
            else if (elemType == SudokuElementType.SearchNineRight)
            {
                return GraphicElementType.Right;
            }
            else if (elemType == SudokuElementType.SearchNineUp)
            {
                return GraphicElementType.Up;
            }
            return GraphicElementType.Down;
        }

        private static bool ImportExtraRegions(SvgElement group)
        {
            if (!_sudoku.Variants.Contains(SudokuType.Extraregion))
            {
                _sudoku.Variants.Add(SudokuType.Extraregion);
            }

            foreach (var extraGroup in group.Children)
            {
                SvgGroup svgBox = extraGroup as SvgGroup;
                if (svgBox == null)
                {
                    return false;
                }
                ObservableCollection<Tuple<int, int>> box = new ObservableCollection<Tuple<int, int>>();

                foreach (var childCell in svgBox.Children)
                {
                    SvgRectangle cell = childCell as SvgRectangle;
                    if (cell == null)
                    {
                        return false;
                    }

                    int rowIndex;
                    if (!IsParseNumber(cell.Y.ToString(), out rowIndex))
                    {
                        return false;
                    }
                    rowIndex = (rowIndex - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize;
                    int colIndex;
                    if (!IsParseNumber(cell.X.ToString(), out colIndex))
                    {
                        return false;
                    }
                    colIndex = (colIndex - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize;
                    box.Add(new Tuple<int, int>(rowIndex, colIndex));
                }

                _sudoku.Grid.ExtraRegions.Add(box);
            }
            return true;
        }

        private static bool ImportLongArrowWithCircle(SvgElement group, SudokuType sudokuType, SudokuElementType elemType)
        {
            if (!_sudoku.Variants.Contains(sudokuType))
            {
                _sudoku.Variants.Add(sudokuType);
            }

            LongArrow actualArrow = new LongArrow(elemType, new ObservableCollection<Tuple<int, int>>());
            foreach (var svgElem in group.Children)
            {
                SvgPath svgArrow = svgElem as SvgPath;
                if (svgArrow != null)
                {
                    ObservableCollection<Tuple<int, int>> positions = new ObservableCollection<Tuple<int, int>>();

                    var dAttribute = svgArrow.PathData;
                    for (int i = 0; i < dAttribute.Count - 3; i++)
                    {
                        SvgPathSegment segment = dAttribute[i];
                        positions.Add(new Tuple<int, int>((int)(segment.End.Y - (SudokuSvgImage.CellSize / 2) - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize,
                            (int)(segment.End.X - (SudokuSvgImage.CellSize / 2) - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize));
                    }

                    LongArrow arrow = new LongArrow(elemType, positions);
                    actualArrow = arrow;
                }
                SvgCircle svgCircle = svgElem as SvgCircle;
                if (svgCircle != null)
                {
                    int x;
                    if (!IsParseNumber(svgCircle.CenterX.ToString(), out x))
                    {
                        return false;
                    }

                    int y;
                    if (!IsParseNumber(svgCircle.CenterY.ToString(), out y))
                    {
                        return false;
                    }
                    x = (x - (SudokuSvgImage.CellSize / 2) - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize;
                    y = (y - (SudokuSvgImage.CellSize / 2) - SudokuSvgImage.GridMargin) / SudokuSvgImage.CellSize;
                    CircleWithGreyEdge circle = new CircleWithGreyEdge(x * GridSizeStore.XCellSize, y * GridSizeStore.XCellSize, y, x, elemType);

                    LongArrowWithCircle arrow = new LongArrowWithCircle(elemType);
                    arrow.Arrow = actualArrow;
                    arrow.Circle = circle;
                    _sudoku.SudokuVariants.Add(arrow);
                }
            }
            return true;
        }
    }
}
