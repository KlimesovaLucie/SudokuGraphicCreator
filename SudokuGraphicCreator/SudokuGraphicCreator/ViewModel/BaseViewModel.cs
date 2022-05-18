using System.ComponentModel;

namespace SudokuGraphicCreator.ViewModel
{
    /// <summary>
    /// Provides event for notifications when some property changed.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invoke <see cref="PropertyChanged"/> for <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Name of changed property.</param>
        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
