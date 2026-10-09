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

namespace _2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button[,] buttons = new Button[4, 4];
        public MainWindow()
        {
            InitializeComponent();
            CreateGameField();
        }
        private void CreateGameField()
        {
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}