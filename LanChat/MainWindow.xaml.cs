using System.Windows;

namespace LanChat
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new LanChatViewModel();

            InitializeComponent();
        }
    }
}
