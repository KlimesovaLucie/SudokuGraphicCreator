using SudokuGraphicCreator.Model;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Command for showing <see cref="BookletInformations"/> dialog.
    /// </summary>
    public class CreateNewBookletCommand : BaseCommand
    {
        /// <summary>
        /// Show <see cref="BookletInformations"/> dialog window.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            var previousBooklet = GetPreviousBooklet();
            bool? result = App.DialogService.ShowDialog(new BookletInformationsViewModel());

            if (result.HasValue)
            {
                if (!result.Value)
                {
                    BookletStore.Instance.Booklet = previousBooklet;
                }
            }
        }

        private Booklet GetPreviousBooklet()
        {
            Booklet result = new Booklet();
            result.Location = BookletStore.Instance.Booklet.Location;
            result.LogoOne = BookletStore.Instance.Booklet.LogoOne;
            result.LogoOneFullPath = BookletStore.Instance.Booklet.LogoOneFullPath;
            result.LogoOneImage = BookletStore.Instance.Booklet.LogoOneImage;
            result.LogoThree = BookletStore.Instance.Booklet.LogoThree;
            result.LogoThreeFullPath = BookletStore.Instance.Booklet.LogoThreeFullPath;
            result.LogoThreeImage = BookletStore.Instance.Booklet.LogoThreeImage;
            result.LogoTwo = BookletStore.Instance.Booklet.LogoTwo;
            result.LogoTwoFullPath = BookletStore.Instance.Booklet.LogoTwoFullPath;
            result.LogoTwoImage = BookletStore.Instance.Booklet.LogoTwoImage;
            result.RoundName = BookletStore.Instance.Booklet.RoundName;
            result.RoundNumber = BookletStore.Instance.Booklet.RoundNumber;
            result.TimeForSolving = BookletStore.Instance.Booklet.TimeForSolving;
            result.TournamentDate = BookletStore.Instance.Booklet.TournamentDate;
            result.TournamentName = BookletStore.Instance.Booklet.TournamentName;
            return result;
        }
    }
}
