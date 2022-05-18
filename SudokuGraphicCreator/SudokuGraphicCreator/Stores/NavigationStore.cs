using SudokuGraphicCreator.ViewModel;
using System;

namespace SudokuGraphicCreator.Stores
{
    /// <summary>
    /// This class has reference of actual showing viewModel.
    /// </summary>
    public class NavigationStore
    {
        private static NavigationStore _instance = new NavigationStore();

        /// <summary>
        /// Get instance of this class.
        /// </summary>
        public static NavigationStore Instance
        {
            get => _instance;
        }

        private BaseViewModel _currentViewModel;
        
        /// <summary>
        /// The viewModel class of actual showing view class.
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        static NavigationStore() { }

        private NavigationStore()
        {

        }

        /// <summary>
        /// Event for notifying that <see cref="CurrentViewModel"/> changed.
        /// </summary>
        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
