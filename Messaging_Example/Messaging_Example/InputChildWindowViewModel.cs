using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
namespace Messaging_Example
{
    class InputChildWindowViewModel
    {
        public string UserInput { get; set; }
        public RelayCommand ReturnInput { get; set; }

        public InputChildWindowViewModel()
        {
            ReturnInput = new RelayCommand(ReturnInputExecute);
        }
        private void ReturnInputExecute()
        {
            // Response will invoke the method which requires input
            var msg = new InputResponseMessage { UserInput = UserInput };
            Messenger.Default.Send<InputResponseMessage>(msg);
        }
    }
}
