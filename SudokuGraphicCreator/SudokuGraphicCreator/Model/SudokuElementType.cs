namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// Represents type of sudoku graphic element based on coupled variant and orientation.
    /// </summary>
    public enum SudokuElementType
    {
        NoMeaning,
        Sum,
        Difference,
        Consecutive,
        WhiteKropki,
        BlackKropki,
        GreaterThanLeft,
        GreaterThanRight,
        GreaterThanUp,
        GreaterThanDown,
        XvX,
        XvV,
        Odd,
        Even,
        StarProduct,
        SearchNineLeft,
        SearchNineRight,
        SearchNineUp,
        SearchNineDown,
        Palindromes,
        Sequences,
        Arrows,
        Thermometers,
        ExtraRegions,
        Killer,
        LittleKillerLeftDown,
        LittleKillerLeftUp,
        LittleKillerRightDown,
        LittleKillerRightUp,
        Skyscrapers,
        NextToNine,
        Outside
    }
}
