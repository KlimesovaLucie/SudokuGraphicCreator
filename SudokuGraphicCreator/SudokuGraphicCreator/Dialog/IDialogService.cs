namespace SudokuGraphicCreator.Dialog
{
    /// <summary>
    /// Register viewModels an threis corresponding views and provides ability for showing views windows.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Register viewModel and hid corresponding view.
        /// </summary>
        /// <typeparam name="TViewModel">Type of viewModel.</typeparam>
        /// <typeparam name="TView">Type of view.</typeparam>
        void Register<TViewModel, TView>() where TViewModel : IDialogRequestClose
            where TView : IDialog;

        /// <summary>
        /// Show view corresponding to given viewModel. For one instance of viewModel can be show dialog only once.
        /// </summary>
        /// <typeparam name="TViewModel">Type of viewModel of showing view.</typeparam>
        /// <param name="viewModel">ViewModel of showing view.</param>
        /// <returns>true if window was closed by OK button, false by cancel button or closed window.</returns>
        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose;
    }
}
