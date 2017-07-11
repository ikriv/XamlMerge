using System.Windows;
using MyLibrary;

namespace TestWpfProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new RedSquareViewModel {Text = "This is yellow text on red background"};
        }
    }
}
