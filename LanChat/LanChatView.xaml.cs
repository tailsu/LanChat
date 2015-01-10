using System;
using System.Collections.Generic;
using System.Linq;
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

namespace LanChat
{
    /// <summary>
    /// Interaction logic for LanChatView.xaml
    /// </summary>
    public partial class LanChatView : UserControl
    {
        public LanChatView()
        {
            InitializeComponent();
        }

        private void MessageBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var message = messageBox.Text;
                var viewModel = (LanChatViewModel) this.DataContext;
                viewModel.Say(message);

                messageBox.Text = "";
            }
        }
    }
}
