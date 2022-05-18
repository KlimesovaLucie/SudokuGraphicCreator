using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;
using System;
using System.ComponentModel;

namespace SudokuGraphicCreator.Commands
{
    /// <summary>
    /// Ok action for <see cref="BookletInformationsViewModel"/> class. Changes actual view to <see cref="CreatingBooklet"/>.
    /// </summary>
    public class OkBookletInfoCommand : BaseCommand
    {
        private readonly IBookletInformationsViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of <see cref="OkBookletInfoCommand"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel of correspond <see cref="BookletInformations"/> window.</param>
        public OkBookletInfoCommand(IBookletInformationsViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked. Closes window and changes ViewModel <see cref="NavigationStore.CurrentViewModel"/> to <see cref="CreatingBookletViewModel"/>.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public override void Execute(object parameter)
        {
            _viewModel.CloseWindowWithOk();
            CreatingBookletViewModel viewModel = NavigationStore.Instance.CurrentViewModel as CreatingBookletViewModel;
            if (viewModel == null)
            {
                NavigationStore.Instance.CurrentViewModel = new CreatingBookletViewModel();
            }
            else
            {
                NavigationStore.Instance.CurrentViewModel = viewModel;
            }
            BookletStore.Instance.CreatingBookletViewModel.BasicInfoChanged();
        }

        /// <summary>
        /// Determines whether <see cref="OkBookletInfoCommand"/> can be execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns>true if this command can be executed, otherwise false.</returns>
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.TournamentName) &&
                !string.IsNullOrEmpty(_viewModel.Location) &&
                CorrectRoundNumber() &&
                DateTime.Today <= _viewModel.TournamentDate &&
                !string.IsNullOrEmpty(_viewModel.RoundName) &&
                !string.IsNullOrEmpty(_viewModel.TimeForSolving) &&
                base.CanExecute(parameter);
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsChangedPropertyInViewModel(e))
            {
                OnCanExecutedChanged();
            }
        }

        private bool IsChangedPropertyInViewModel(PropertyChangedEventArgs e)
        {
            return e.PropertyName == nameof(_viewModel.TournamentName) || e.PropertyName == nameof(_viewModel.Location) ||
                e.PropertyName == nameof(_viewModel.RoundNumber) || e.PropertyName == nameof(_viewModel.RoundName) ||
                e.PropertyName == nameof(_viewModel.TournamentDate) || e.PropertyName == nameof(_viewModel.TimeForSolving);
        }

        private bool CorrectRoundNumber()
        {
            try
            {
                return Int32.Parse(_viewModel.RoundNumber) > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
