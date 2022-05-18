using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.ViewModel;
using System;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// This command change view and corresponding viewModel when this command is invoked.
    /// </summary>
    public class NavigateCommand : BaseCommand
    {
        private readonly Func<BaseViewModel> _createViewModel;

        /// <summary>
        /// Initializes a new instance of <see cref="NavigateCommand"/> class.
        /// </summary>
        /// <param name="createViewModel">Function for creating new instance of viewModel class and its corresponding view will be showed on screen.</param>
        public NavigateCommand(Func<BaseViewModel> createViewModel)
        {
            _createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            NavigationStore.Instance.CurrentViewModel = _createViewModel();
        }
    }
}
