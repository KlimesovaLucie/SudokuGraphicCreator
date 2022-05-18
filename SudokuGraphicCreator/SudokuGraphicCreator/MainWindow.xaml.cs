using System.Windows;
using System.Windows.Input;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainWindowViewModel viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.ResizeSudokuGrid(e.PreviousSize, e.NewSize);
            }
        }
    }
}
