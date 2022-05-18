using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SudokuGraphicCreator.View
{
    /// <summary>
    /// Interaction logic for CreatingSudoku.xaml
    /// </summary>
    public partial class CreatingSudoku : UserControl
    {
        public ICommand CellCommand
        {
            get => (ICommand)GetValue(CellCommandProperty);
            set => SetValue(CellCommandProperty, value);
        }

        public static readonly DependencyProperty CellCommandProperty =
            DependencyProperty.Register("CellCommand", typeof(ICommand), typeof(CreatingSudoku), new PropertyMetadata(null));

        public ICommand KeyPressCommand
        {
            get => (ICommand)GetValue(KeyPressCommandProperty);
            set => SetValue(KeyPressCommandProperty, value);
        }

        public static readonly DependencyProperty KeyPressCommandProperty =
            DependencyProperty.Register("KeyPressCommand", typeof(ICommand), typeof(CreatingSudoku), new PropertyMetadata(null));

        public CreatingSudoku()
        {
            InitializeComponent();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CellCommand?.Execute(new Tuple<object, Point>(sender, e.GetPosition(canvasGrid)));
        }

        private void UserControl_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            KeyPressCommand?.Execute(e);
        }
    }
}
