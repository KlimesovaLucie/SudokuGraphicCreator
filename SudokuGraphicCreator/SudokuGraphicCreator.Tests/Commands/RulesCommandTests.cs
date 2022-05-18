using NUnit.Framework;
using SudokuGraphicCreator.Commands;
using SudokuGraphicCreator.Properties.Resources;
using SudokuGraphicCreator.ViewModel;
using System.Text;

namespace SudokuGraphicCreator.Tests.Commands
{
    public class RulesCommandTests
    {
        private RulesCommand _command;

        private IInsertedSudokuTable _sudokuInserted;

        private SudokuInBookletViewModel _sudokuInBooklet;

        private ICreatingBookletViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new CreatingBookletViewModel();
            _sudokuInBooklet = new SudokuInBookletViewModel();
            _sudokuInserted = new InsertNewSudokuTableViewModel(_sudokuInBooklet, _sudokuInBooklet.OrderNumber, _viewModel);
            _command = new RulesCommand(_sudokuInserted);
        }

        [Test]
        public void Execute_Classic()
        {
            _command.Execute(Resources.SudokuClassic);
            StringBuilder strBuilder = new StringBuilder(Resources.ClassicSudokuRules)
                .AppendLine();
            Assert.That(_sudokuInserted.Rules, Is.EqualTo(strBuilder.ToString()));
        }

        [Test]
        public void Execute_Consecutive()
        {
            _command.Execute(Resources.SudokuConsecutive);
            StringBuilder strBuilder = new StringBuilder(Resources.ClassicRulesShort)
                .AppendLine()
                .AppendLine(Resources.RulesConsecutive);
            Assert.That(_sudokuInserted.Rules, Is.EqualTo(strBuilder.ToString()));
        }

        [Test]
        public void Execute_SkyscrapersKiller()
        {
            _command.Execute(Resources.SudokuSkyscrapers);
            _command.Execute(Resources.SudokuKiller);
            StringBuilder strBuilder = new StringBuilder(Resources.ClassicRulesShort)
                .AppendLine()
                .AppendLine(Resources.RulesSkyscrapers)
                .AppendLine(Resources.RulesKiller);
            Assert.That(_sudokuInserted.Rules, Is.EqualTo(strBuilder.ToString()));
        }

        [Test]
        public void Execute_SkyscrapersKillerClassic()
        {
            _command.Execute(Resources.SudokuSkyscrapers);
            _command.Execute(Resources.SudokuKiller);
            _command.Execute(Resources.SudokuClassic);
            StringBuilder strBuilder = new StringBuilder(Resources.ClassicSudokuRules)
                .AppendLine()
                .AppendLine(Resources.RulesSkyscrapers)
                .AppendLine(Resources.RulesKiller);
            Assert.That(_sudokuInserted.Rules, Is.EqualTo(strBuilder.ToString()));
        }
    }
}
