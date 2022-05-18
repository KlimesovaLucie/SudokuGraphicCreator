using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Provides basic informations of tournament which have to be validate.
    /// </summary>
    public interface IBookletInformationsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Name of competition.
        /// </summary>
        string TournamentName { get; }

        /// <summary>
        /// Date of competition.
        /// </summary>
        DateTime TournamentDate { get; }

        /// <summary>
        /// Name of the city where the competition takes place.
        /// </summary>
        string Location { get; }

        /// <summary>
        /// Number of the round of the competition.
        /// </summary>
        string RoundNumber { get; }

        /// <summary>
        /// Name of the actual round of the competition.
        /// </summary>
        string RoundName { get; }

        /// <summary>
        /// Time for solving the round.
        /// </summary>
        string TimeForSolving { get; }

        /// <summary>
        /// First name of logo, which is placed in the top left corner of first booklet page.
        /// </summary>
        string LogoOne { get; set; }

        /// <summary>
        /// Full file path in file system for first logo, which is placed in the top left corner of first booklet page.
        /// </summary>
        string LogoOneFullPath { get; set; }

        /// <summary>
        /// First logo bitmap image, which is placed in the top left corner of first booklet page.
        /// </summary>
        BitmapImage LogoOneImage { get; set; }

        /// <summary>
        /// Second name of logo, which is placed in the top right corner of first booklet page.
        /// </summary>
        string LogoTwo { get; set; }

        /// <summary>
        /// Full file path in file system for second logo, which is placed in the top right corner of first booklet page.
        /// </summary>
        string LogoTwoFullPath { get; set; }

        /// <summary>
        /// Second logo bitmap image, which is placed in the top right corner of first booklet page.
        /// </summary>
        BitmapImage LogoTwoImage { get; set; }

        /// <summary>
        /// Third name of logo, which is placed in the left bottom corner of first booklet page.
        /// </summary>
        string LogoThree { get; set; }

        /// <summary>
        /// Full file path in file system for third logo, which is placed in the left bottom corner of first booklet page.
        /// </summary>
        string LogoThreeFullPath { get; set; }

        /// <summary>
        /// Third logo bitmap image, which is placed in the left bottom corner of first booklet page.
        /// </summary>
        BitmapImage LogoThreeImage { get; set; }

        /// <summary>
        /// Close window with true.
        /// </summary>
        void CloseWindowWithOk();
    }
}
