using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
namespace Messaging_Example
{
    /// <summary>
    /// Interaction logic for InputChildWIndow.xaml
    /// </summary>
    public partial class InputChildWindow : Window
    {
        public InputChildWindow()
        {
            InitializeComponent();
            this.DataContext = new InputChildWindowViewModel();
            Messenger.Default.Register<InputResponseMessage>(this, (action) => ReceiveInputMessage(action));
        }
        private void ReceiveInputMessage(InputResponseMessage inp)
        {
            this.Close();
        }
    }
}
