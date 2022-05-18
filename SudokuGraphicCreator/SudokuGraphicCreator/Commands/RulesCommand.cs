using SudokuGraphicCreator.Properties.Resources;
using SudokuGraphicCreator.ViewModel;
using System.Collections.Generic;
using System.Text;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Command for creating default version of sudoku rules based on selected types of variants.
    /// </summary>
    public class RulesCommand : BaseCommand
    {
        private readonly IInsertedSudokuTable _sudoku;

        private readonly Dictionary<string, string> _rules = new Dictionary<string, string>()
        {
            { Resources.SudokuClassic, Resources.ClassicSudokuRules },
            { Resources.SudokuDiagonal, Resources.DiagonalRules },
            { Resources.SudokuWindoku, Resources.WindokuRules },
            { Resources.SudokuAntiknight, Resources.RulesAntiknight },
            { Resources.SudokuNonconsecutive, Resources.RulesNonconsecutive },
            { Resources.SudokuUntouchable, Resources.RulesUntouchable },
            { Resources.SudokuDisjointGroups, Resources.RulesDisjointGroups },
            { Resources.SudokuIrregular, Resources.RulesIrregular },
            { Resources.SudokuSum, Resources.RulesSum },
            { Resources.SudokuDifference, Resources.RulesDifference },
            { Resources.SudokuConsecutive, Resources.RulesConsecutive },
            { Resources.SudokuKropki, Resources.RulesKropki },
            { Resources.SudokuGreaterThan, Resources.RulesGreaterThan },
            { Resources.SudokuXV, Resources.RulesXVSudoku },
            { Resources.SudokuOdd, Resources.RulesOdd },
            { Resources.SudokuEven, Resources.RulesEven },
            { Resources.SudokuStarProducts, Resources.RulesStarProduct },
            { Resources.SudokuSearchNine, Resources.RulesSearchNine },
            { Resources.SudokuPalindrome, Resources.RulesPalindromes },
            { Resources.SudokuSequence, Resources.RulesSequences },
            { Resources.SudokuArrow, Resources.RulesArrows },
            { Resources.SudokuThermometer, Resources.RulesThermometers },
            { Resources.SudokuExtraRegions, Resources.RulesExtraRegions },
            { Resources.SudokuKiller, Resources.RulesKiller },
            { Resources.SudokuLittleKiller, Resources.RulesLittleKiller },
            { Resources.SudokuSkyscrapers, Resources.RulesSkyscrapers },
            { Resources.SudokuNextToNine, Resources.RulesNextToNine },
            { Resources.SudokuOutside, Resources.RulesOutside },
        };

        private readonly List<string> _selectedVariants = new List<string>();

        /// <summary>
        /// Initializes a new instance of <see cref="RulesCommand"/> class.
        /// </summary>
        /// <param name="sudoku">Sudoku for which are rules created.</param>
        public RulesCommand(IInsertedSudokuTable sudoku)
        {
            _sudoku = sudoku;
        }

        /// <summary>
        /// Method responsible for creating default version of sudoku rules.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            string sudokuName = parameter as string;
            if (sudokuName == null)
            {
                return;
            }

            if (_selectedVariants.Remove(sudokuName))
            {
                CreateNewRules();
            }
            else
            {
                AddNewRules(sudokuName);
            }
        }

        private void AddNewRules(string sudokuName)
        {
            _selectedVariants.Add(sudokuName);
            if (_selectedVariants.Count == 1)
            {
                if (sudokuName == Resources.SudokuClassic)
                {
                    StringBuilder sudokuRules = new StringBuilder();
                    sudokuRules.AppendLine(_rules[sudokuName]);
                    _sudoku.Rules = sudokuRules.ToString();
                }
                else
                {
                    StringBuilder sudokuRules = new StringBuilder();
                    sudokuRules.AppendLine(Resources.ClassicRulesShort);
                    sudokuRules.AppendLine(_rules[sudokuName]);
                    _sudoku.Rules = sudokuRules.ToString();
                }
            }
            else
            {
                if (sudokuName == Resources.SudokuClassic)
                {
                    CreateNewRules();
                }
                else
                {
                    StringBuilder sudokuRules = new StringBuilder(_sudoku.Rules);
                    sudokuRules.AppendLine(_rules[sudokuName]);
                    _sudoku.Rules = sudokuRules.ToString();
                }
            }
        }

        private void CreateNewRules()
        {
            if (_selectedVariants.Count == 0)
            {
                _sudoku.Rules = "";
                return;
            }

            StringBuilder sudokuRules = new StringBuilder();

            if (_selectedVariants.Contains(Resources.SudokuClassic))
            {
                sudokuRules.AppendLine(Resources.ClassicSudokuRules);
            }
            else
            {
                sudokuRules.AppendLine(Resources.ClassicRulesShort);
            }

            foreach (var variant in _selectedVariants)
            {
                if (variant != Resources.SudokuClassic)
                {
                    sudokuRules.AppendLine(_rules[variant]);
                }
            }
            _sudoku.Rules = sudokuRules.ToString();
        }
    }
}
