namespace WpfMyGridLesson
{
    /// <summary>
    /// MyGridView.xaml の相互作用ロジック
    /// </summary>
    public partial class MyGridView : UserControl
    {
        private const int GRID_SIZE = 20;
 
        public MyGridView()
        {
            InitializeComponent();
        }
 
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BuildView();
        }
 
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BuildView();
        }
 
        // キャンバスに線を描画する
        private void BuildView()
        {
            myCanvas.Children.Clear();
 
 
            Style lineStyle = this.FindResource("GridLineStyle") as Style;
 
            for (int i = 0; i < this.ActualWidth; i += GRID_SIZE)
            {
                Line line = new Line()
                {
                    X1 = i,
                    Y1 = 0,
                    X2 = i,
                    Y2 = this.ActualHeight,
                    Style = lineStyle
                };
 
                myCanvas.Children.Add(line);
            }
 
            for (int i = 0; i < this.ActualHeight; i += GRID_SIZE)
            {
                Line line = new Line()
                {
                    X1 = 0,
                    Y1 = i,
                    X2 = this.ActualWidth,
                    Y2 = i,
                    Style = lineStyle
                };
 
                myCanvas.Children.Add(line);
            }
        }
    }
}