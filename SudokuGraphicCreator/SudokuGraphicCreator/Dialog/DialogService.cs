using System;
using System.Collections.Generic;
using System.Windows;

namespace SudokuGraphicCreator.Dialog
{
    /// <summary>
    /// Register viewModels an threis corresponding views and provides ability for showing views windows.
    /// </summary>
    public class DialogService : IDialogService
    {
        private readonly Window _owner;

        private readonly IDictionary<Type, Type> _mappings;

        /// <summary>
        /// Initializes a new instance of <see cref="DialogService"/> class.
        /// </summary>
        /// <param name="owner">Owner window.</param>
        public DialogService(Window owner)
        {
            _owner = owner;
            _mappings = new Dictionary<Type, Type>();
        }

        public void Register<TViewModel, TView>()
            where TViewModel : IDialogRequestClose
            where TView : IDialog
        {
            if (_mappings.ContainsKey(typeof(TViewModel)))
            {
                throw new ArgumentException("This type is already registered.");
            }

            _mappings.Add(typeof(TViewModel), typeof(TView));
        }

        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose
        {
            Type viewType = _mappings[typeof(TViewModel)];
            IDialog dialog = (IDialog)Activator.CreateInstance(viewType);
            EventHandler<DialogCloseRequestedEventArgs> handler = null;
            handler = (sender, e) =>
            {
                viewModel.CloseRequested -= handler;

                if (e.DialogResult.HasValue)
                {
                    dialog.DialogResult = e.DialogResult;
                }
                else
                {
                    dialog.Close();
                }
            };
            viewModel.CloseRequested += handler;
            dialog.DataContext = viewModel;
            dialog.Owner = _owner;
            return dialog.ShowDialog();
        }
    }
}
