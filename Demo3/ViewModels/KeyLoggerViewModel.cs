using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo3.Services;
using SuiteValue.UI.MVVM;

namespace Demo3.ViewModels
{
    public class KeyLoggerViewModel : ViewModelBase
    {
        private WinApiService _apiService;

        public KeyLoggerViewModel()
        {
            _apiService = new WinApiService();
        }

        private DelegateCommand _startCommand;

        public DelegateCommand StartCommand
        {
            get
            {
                return _startCommand ?? (_startCommand = new DelegateCommand(
                    () =>
                    {
                        _apiService.KeyDown += _apiService_KeyDown;
                        _apiService.StartKeyboardHook();

                    }));
            }
        }

        private DelegateCommand _stopCommand;

        public DelegateCommand StopCommand
        {
            get
            {
                return _stopCommand ?? (_stopCommand = new DelegateCommand(
                    () =>
                    {
                        _apiService.KeyDown -= _apiService_KeyDown;
                        _apiService.StopKeyboardHook();
                    }));
            }
        }

        void _apiService_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            Text += e.KeyData;
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



    }
}
