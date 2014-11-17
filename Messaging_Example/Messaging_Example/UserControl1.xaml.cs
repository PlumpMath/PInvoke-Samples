using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;

namespace Messaging_Example
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            this.WPFUCTextBlock.Text = "Textblock in UserControl";
            Messenger.Default.Register<Amessage>(this, (action) => ReceiveAMessage(action));
        }

        private object ReceiveAMessage(Amessage msg)
        {
            this.WPFUCTextBlock.Text = msg.TheText;
            return null;
        }
    }
}
