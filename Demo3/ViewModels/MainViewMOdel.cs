using System.Diagnostics;
using Demo3.Services;
using SuiteValue.UI.MVVM;

namespace Demo3.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private WinApiService _apiService;

        public MainViewModel()
        {
            _apiService = new WinApiService();
        }
        public void Init()
        {
            Notepads = Process.GetProcessesByName("notepad");
        }

        private Process[] _notepads;

        public Process[] Notepads
        {
            get { return _notepads; }
            set
            {
                if (value != _notepads)
                {
                    _notepads = value;
                    OnPropertyChanged(() => Notepads);
                }
            }
        }

        private Process _selectedProcess;

        public Process SelectedProcess
        {
            get { return _selectedProcess; }
            set
            {
                if (value != _selectedProcess)
                {
                    _selectedProcess = value;
                    OnPropertyChanged(() => SelectedProcess);
                    SendCommand.Refresh();

                    if (SelectedProcess != null)
                    {
                        GetText(SelectedProcess);

                    }
                }
            }
        }

        private void GetText(Process selectedProcess)
        {
            Text = _apiService.GetTextOfProcess(selectedProcess);

        }

        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                if (value != _text)
                {
                    _text = value;
                    OnPropertyChanged(() => Text);
                }
            }
        }

        private string _newMessage;

        public string NewMessage
        {
            get { return _newMessage; }
            set
            {
                if (value != _newMessage)
                {
                    _newMessage = value;
                    OnPropertyChanged(() => NewMessage);
                    SendCommand.Refresh();
                }
            }
        }

        private DelegateCommand _sendCommand;

        public DelegateCommand SendCommand
        {
            get
            {
                return _sendCommand ?? (_sendCommand = new DelegateCommand(
                    () => _apiService.SendNewMessage(SelectedProcess, NewMessage),
                    () => !string.IsNullOrEmpty(NewMessage) && SelectedProcess != null));
            }
        }

        private DelegateCommand _refreshCommand;

        public DelegateCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new DelegateCommand(
                    () =>
                    {
                        Text = _apiService.GetTextOfProcess(SelectedProcess);
                    }));
            }
        }






    }
}