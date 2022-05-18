using NUnit.Framework;
using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Properties.Resources;

namespace SudokuGraphicCreator.Tests.Models
{
    public class SudokuInBookletTests
    {
        private SudokuInBooklet _sudoku;

        [SetUp]
        public void SetUp()
        {
            _sudoku = new SudokuInBooklet();
        }

        [Test]
        public void TestStringRepresentation_ZeroPoints()
        {
            string name = "Classic sudoku";
            _sudoku.Name = name;

            int order = 1;
            _sudoku.Order = order;

            int points = 0;
            _sudoku.Points = points;

            Assert.That(_sudoku.ToString(),
                Is.EqualTo(CreateString(name, order, points, Resources.Points)));
        }

        [Test]
        public void TestStringRepresentation_SingularPoints()
        {
            string name = "Classic sudoku";
            _sudoku.Name = name;

            int order = 1;
            _sudoku.Order = order;

            int points = 1;
            _sudoku.Points = points;

            Assert.That(_sudoku.ToString(),
                Is.EqualTo(CreateString(name, order, points, Resources.Point)));
        }

        [Test]
        public void TestStringRepresentation_MiddlePoints()
        {
            string name = "Classic sudoku";
            _sudoku.Name = name;

            int order = 1;
            _sudoku.Order = order;

            int points = 2;
            _sudoku.Points = points;

            Assert.That(_sudoku.ToString(),
                Is.EqualTo(CreateString(name, order, points, Resources.PointsMiddle)));
        }

        [Test]
        public void TestStringRepresentation_ManyPoints()
        {
            string name = "Classic sudoku";
            _sudoku.Name = name;

            int order = 1;
            _sudoku.Order = order;

            int points = 40;
            _sudoku.Points = points;

            Assert.That(_sudoku.ToString(),
                Is.EqualTo(CreateString(name, order, points, Resources.Points)));
        }

        private string CreateString(string name, int order, int points, string pointFormat)
        {
            return order + ". " + name + " (" + points + " " + pointFormat + ")";
        }
    }
}
