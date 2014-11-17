using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
namespace Messaging_Example
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string buttonContent = "Process requesting input";

        public string ButtonContent
        {
            get { return buttonContent; }
            set 
            {
                buttonContent = value;
                RaisePropertyChanged("ButtonContent");
            }
        }

        public RelayCommand DoProcess { get; set; }

        public MainWindowViewModel()
        {
            DoProcess = new RelayCommand(ProcessExecute);
            Messenger.Default.Register<InputResponseMessage>(this, (action) => ReceiveInputMessage(action));
        }
        private void ReceiveInputMessage(InputResponseMessage inp)
        {
            // Continue with processing
            ButtonContent = inp.UserInput;
        }
       private void ProcessExecute()
       {
           // Piece of processing gets to some point and then requires user input
           var msg = new InputRequestMessage();
           Messenger.Default.Send<InputRequestMessage>(msg);
       }


    }
}
