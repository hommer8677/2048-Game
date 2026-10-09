using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _2048.VM;

namespace _2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button[,] buttons = new Button[4, 4];
        GameViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            Model m = new Model();
            vm = new GameViewModel(m);
            this.DataContext = vm;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            vm.OnKeyPress(e);
        }
    }
}