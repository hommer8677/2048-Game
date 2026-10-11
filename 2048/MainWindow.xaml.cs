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
        GameViewModel vm;
        List<Theme> themes = new List<Theme>();
        int idx = -1;
        public MainWindow()
        {
            InitializeComponent();
            AddThemes();
            Model m = new Model();
            vm = new GameViewModel(m);
            this.DataContext = vm;
            vm.GameEnded += OnGameEnded;
        }
        private void OnGameEnded(string message)
        {
            MessageBox.Show(message, "End Game");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.T)
            {
                if ((idx + 1) == themes.Count)
                {
                    idx = -1;
                    MainFrame.BorderBrush = Brushes.Red;
                    ScoreFrame1.ClearValue(Border.BorderBrushProperty);
                    ScoreFrame2.ClearValue(Border.BorderBrushProperty);

                    MainFrame.ClearValue(Border.BackgroundProperty);
                    ScoreFrame1.ClearValue(Border.BackgroundProperty);
                    ScoreFrame2.ClearValue(Border.BackgroundProperty);

                    foreach (var child in FieldGrid.Children)
                    {
                        if(child is Button btn)
                        {
                            btn.ClearValue(Button.ForegroundProperty);
                            btn.ClearValue(Button.BackgroundProperty);
                            btn.ClearValue(Button.BorderBrushProperty);
                        }
                    }
                    ResetStatisticsStyle();

                    this.ClearValue(Window.BackgroundProperty);
                    return;
                }
                idx++;
                Theme th = themes[idx];
                MainFrame.BorderBrush = th.Frame;
                MainFrame.Background = th.Background;

                ScoreFrame1.BorderBrush = th.Frame;
                ScoreFrame1.Background = th.Background;

                ScoreFrame2.BorderBrush = th.Frame;
                ScoreFrame2.Background = th.Background;

                this.Background = th.Background;

                foreach (var child in FieldGrid.Children)
                {
                    if (child is Button btn)
                    {
                        btn.Background = th.Background;
                        btn.Foreground = th.TextColor;
                        btn.BorderBrush = th.Frame;
                    }
                }
                BrushStatistics(th);
                return;
            }
            vm.OnKeyPress(e.Key);
        }
        private void BrushStatistics(Theme th)
        {
            foreach (var child in Statistics1.Children)
            {
                if (child is Label lbl)
                {
                    lbl.Foreground = th.TextColor;
                }
                else if (child is TextBlock textBlock)
                {
                    textBlock.Foreground = th.TextColor;
                }
            }
            foreach (var child in Statistics2.Children)
            {
                if (child is Label lbl)
                {
                    lbl.Foreground = th.TextColor;
                }
                else if (child is TextBlock textBlock)
                {
                    textBlock.Foreground = th.TextColor;
                }
            }
        }
        private void ResetStatisticsStyle()
        {
            foreach (var child in Statistics1.Children)
            {
                if (child is Label lbl)
                {
                    lbl.ClearValue(Label.ForegroundProperty);
                }
                else if (child is TextBlock textBlock)
                {
                    textBlock.ClearValue(TextBlock.ForegroundProperty);
                }
            }
            foreach (var child in Statistics2.Children)
            {
                if (child is Label lbl)
                {
                    lbl.ClearValue(Label.ForegroundProperty);
                }
                else if (child is TextBlock textBlock)
                {
                    textBlock.ClearValue(TextBlock.ForegroundProperty);
                }
            }
        }
        private void AddThemes()
        {
            //theme1
            Brush back = (Brush)new BrushConverter().ConvertFrom("#0A0A0A");
            Brush frame = (Brush)new BrushConverter().ConvertFrom("#B6FF00");
            Brush text = (Brush)new BrushConverter().ConvertFrom("#FFFF00");
            themes.Add(new Theme(back, frame, text));

            //theme2
            back = (Brush)new BrushConverter().ConvertFrom("#245BFF");
            frame = (Brush)new BrushConverter().ConvertFrom("#101A46");
            text = (Brush)new BrushConverter().ConvertFrom("#000080");
            themes.Add(new Theme(back, frame, text));

            //theme3
            back = (Brush)new BrushConverter().ConvertFrom("#DC143C");
            frame = (Brush)new BrushConverter().ConvertFrom("#8B0000");
            text = (Brush)new BrushConverter().ConvertFrom("#FFFFE0");
            themes.Add(new Theme(back, frame, text));

            //theme4
            back = (Brush)new BrushConverter().ConvertFrom("#D96B27");
            frame = (Brush)new BrushConverter().ConvertFrom("#24150F");
            text = (Brush)new BrushConverter().ConvertFrom("#F5DEB3");
            themes.Add(new Theme(back, frame, text));

            //theme5
            back = (Brush)new BrushConverter().ConvertFrom("#292929");
            frame = (Brush)new BrushConverter().ConvertFrom("#C88E9B");
            text = (Brush)new BrushConverter().ConvertFrom("#C0C0C0");
            themes.Add(new Theme(back, frame, text));

            //theme6
            back = (Brush)new BrushConverter().ConvertFrom("#F1E9D8");
            frame = (Brush)new BrushConverter().ConvertFrom("#B22222");
            themes.Add(new Theme(back, frame, frame));

            //theme7
            back = (Brush)new BrushConverter().ConvertFrom("#123B2A");
            frame = (Brush)new BrushConverter().ConvertFrom("#E5FF00");
            themes.Add(new Theme(back, frame, frame));

            //theme8
            back = (Brush)new BrushConverter().ConvertFrom("#FF3CAC");
            frame = (Brush)new BrushConverter().ConvertFrom("#800081");
            themes.Add(new Theme(back, frame, frame));

            //theme9
            back = (Brush)new BrushConverter().ConvertFrom("#A5E7FF");
            frame = (Brush)new BrushConverter().ConvertFrom("#310062");
            text = (Brush)new BrushConverter().ConvertFrom("#4169E1");
            themes.Add(new Theme(back, frame, text));
        }
    }
    struct Theme
    {
        public Brush Background { get; init; }
        public Brush Frame { get; init; }
        public Brush TextColor { get; init; }
        public Theme(Brush back,Brush frame, Brush textColor)
        {
            Background = back;
            Frame = frame;
            TextColor = textColor;
        }
    }
}