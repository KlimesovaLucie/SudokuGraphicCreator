using System.Windows;
using SudokuGraphicCreator.Dialog;
using SudokuGraphicCreator.Stores;
using SudokuGraphicCreator.View;
using SudokuGraphicCreator.ViewModel;

namespace SudokuGraphicCreator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IDialogService DialogService { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // switching languages
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("cs-CZ");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            DialogService = new DialogService(MainWindow);
            DialogService.Register<CreateSudokuSizeViewModel, CreateSudokuSize>();
            DialogService.Register<CountOfSolutionsViewModel, CountOfSolutions>();
            DialogService.Register<InsertGivenNumbersViewModel, InsertGivenNumbers>();
            DialogService.Register<InsertSolutionViewModel, InsertSolution>();
            DialogService.Register<BookletInformationsViewModel, BookletInformations>();
            DialogService.Register<InsertNewSudokuTableViewModel, InsertNewSudokuTable>();
            DialogService.Register<DeleteSudokuPageViewModel, DeleteSudokuPage>();
            DialogService.Register<DeleteSudokuTableViewModel, DeleteSudokuTable>();
            DialogService.Register<EditBookletViewModel, EditBooklet>();
            DialogService.Register<EditSudokuTableViewModel, EditSudokuTable>();
            DialogService.Register<AboutAppViewModel, AboutApp>();
            DialogService.Register<ExportGivenNumbersViewModel, ExportGivenNumbers>();

            NavigationStore.Instance.CurrentViewModel = new StartScreenViewModel();

            var viewModel = new MainWindowViewModel();
            var view = new MainWindow { DataContext = viewModel };

            view.ShowDialog();

            base.OnStartup(e);
        }
    }
}
