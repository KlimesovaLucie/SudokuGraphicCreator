using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace SudokuGraphicCreator.Model
{
    /// <summary>
    /// This class represents booklet.
    /// </summary>
    public class Booklet
    {
        /// <summary>
        /// Name of competition.
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// Name of the city where the competition takes place.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Date of competition.
        /// </summary>
        public DateTime TournamentDate { get; set; }

        /// <summary>
        /// Name of the actual round of the competition.
        /// </summary>
        public string RoundName { get; set; }

        /// <summary>
        /// Number of the round of the competition.
        /// </summary>
        public string RoundNumber { get; set; }

        /// <summary>
        /// Time for solving the round.
        /// </summary>
        public string TimeForSolving { get; set; }

        /// <summary>
        /// First name of logo, which is placed in the top left corner of first booklet page.
        /// </summary>
        public string LogoOne { get; set; }

        /// <summary>
        /// Full file path in file system for first logo, which is placed in the top left corner of first booklet page.
        /// </summary>
        public string LogoOneFullPath { get; set; }

        /// <summary>
        /// First logo bitmap image, which is placed in the top left corner of first booklet page.
        /// </summary>
        public BitmapImage LogoOneImage { get; set; }

        /// <summary>
        /// Second name of logo, which is placed in the top right corner of first booklet page.
        /// </summary>
        public string LogoTwo { get; set; }

        /// <summary>
        /// Full file path in file system for second logo, which is placed in the top right corner of first booklet page.
        /// </summary>
        public string LogoTwoFullPath { get; set; }

        /// <summary>
        /// Second logo bitmap image, which is placed in the top right corner of first booklet page.
        /// </summary>
        public BitmapImage LogoTwoImage { get; set; }

        /// <summary>
        /// Third name of logo, which is placed in the left bottom corner of first booklet page.
        /// </summary>
        public string LogoThree { get; set; }

        /// <summary>
        /// Full file path in file system for third logo, which is placed in the left bottom corner of first booklet page.
        /// </summary>
        public string LogoThreeFullPath { get; set; }

        /// <summary>
        /// Third logo bitmap image, which is placed in the left bottom corner of first booklet page.
        /// </summary>
        public BitmapImage LogoThreeImage { get; set; }

        /// <summary>
        /// Pages of booklet.
        /// </summary>
        public List<BookletPage> Pages { get; set; }

        /// <summary>
        /// Sum of all points of all sudoku in booklet.
        /// </summary>
        public int TotalPoints
        {
            get
            {
                if (Pages == null)
                {
                    return 0;
                }
                else
                {
                    int result = 0;
                    foreach (var page in Pages)
                    {
                        foreach (var sudoku in page.SudokuOnPage)
                        {
                            result += sudoku.Points;
                        }
                    }
                    return result;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Booklet"/> class.
        /// </summary>
        /// <param name="tournamentName">Name of tournament.</param>
        /// <param name="location">Name of the city where the competition takes place.</param>
        /// <param name="tournamentDate">Date of competition.</param>
        /// <param name="roundName">Name of the actual round of the competition.</param>
        /// <param name="roundNumber">Number of the round of the competition.</param>
        /// <param name="timeForSolving">Time for solving the round.</param>
        /// <param name="logoOne">First name of logo, which is placed in the top left corner of first booklet page.</param>
        /// <param name="logoTwo">Second name of logo, which is placed in the top right corner of first booklet page.</param>
        /// <param name="logoThree">Third name of logo, which is placed in the left bottom corner of first booklet page.</param>
        /// <param name="logoOneFullPath">Full file path in file system for first logo, which is placed in the top left corner of first booklet page.</param>
        /// <param name="logoTwoFullPath">Full file path in file system for second logo, which is placed in the top right corner of first booklet page.</param>
        /// <param name="logoThreeFullPath">Full file path in file system for third logo, which is placed in the left bottom corner of first booklet page.</param>
        public Booklet(string tournamentName, string location,
            DateTime tournamentDate, string roundName, string roundNumber,
            string timeForSolving, string logoOne = "", string logoTwo = "", string logoThree = "", string logoOneFullPath = null, string logoTwoFullPath = null, string logoThreeFullPath = null)
        {
            TournamentName = tournamentName;
            Location = location;
            TournamentDate = tournamentDate;
            RoundName = roundName;
            RoundNumber = roundNumber;
            TimeForSolving = timeForSolving;
            LogoOne = logoOne;
            LogoTwo = logoTwo;
            LogoThree = logoThree;
            Pages = new List<BookletPage>();
            LogoOneFullPath = logoOneFullPath;
            LogoTwoFullPath = logoTwoFullPath;
            LogoThreeFullPath = logoThreeFullPath;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Booklet"/> class.
        /// </summary>
        public Booklet()
        {
            TournamentName = "";
            Location = "";
            TournamentDate = DateTime.Today;
            RoundName = "";
            RoundNumber = "";
            TimeForSolving = "";
            LogoOne = "";
            LogoTwo = "";
            LogoThree = "";
            Pages = new List<BookletPage>();
            LogoOneFullPath = "";
            LogoTwoFullPath = "";
            LogoThreeFullPath = "";
        }

        /// <summary>
        /// Add <paramref name="page"/> into <see cref="Pages"/>.
        /// </summary>
        /// <param name="page">Page in booklet.</param>
        public void AddPage(BookletPage page)
        {
            Pages.Add(page);
        }

        /// <summary>
        /// Remove <paramref name="page"/> from <see cref="Pages"/>.
        /// </summary>
        /// <param name="page">Page in booklet.</param>
        public void RemovePage(BookletPage page)
        {
            Pages.Remove(page);
        }
    }
}
