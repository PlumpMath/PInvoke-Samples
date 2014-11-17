using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Interop;

namespace Messaging_Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();

            Form1 winform = new Form1();
            WindowInteropHelper wih = new WindowInteropHelper(this);
            wih.Owner = winform.Handle;
            winform.Location = new System.Drawing.Point(500, 0);
            winform.Show();
            Messenger.Default.Register<InputRequestMessage>(this, (action) => InputRequestMessageExecute(action));
        }

        private object InputRequestMessageExecute(InputRequestMessage msg)
        {
            InputChildWindow input = new InputChildWindow();
            input.ShowDialog();

            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtEntry.Text.Length > 0)
            {
                var msg = new Amessage { TheText = this.txtEntry.Text };
                Messenger.Default.Send<Amessage>(msg);
            }
            else
            {
                System.Windows.MessageBox.Show("Please enter some text in the box");
            }
        }
    }
}
