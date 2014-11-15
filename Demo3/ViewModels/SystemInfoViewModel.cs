using System.Collections.Generic;
using System.Linq;
using Demo3.Services;
using SuiteValue.UI.MVVM;

namespace Demo3.ViewModels
{
    public class SystemInfoViewModel : ViewModelBase
    {
        private WinApiService _apiService;

        public SystemInfoViewModel()
        {
            _apiService = new WinApiService();
        }

        public void Init()
        {
            var list = new List<KeyValuePair<string, string>>();
            Info = _apiService.GetSystemInfo();
            var type = Info.GetType();

            foreach (var field in type.GetFields())
            {
                list.Add(new KeyValuePair<string, string>(field.Name, field.GetValue(Info).ToString()));
            }
            Values = list.ToArray();
        }

        private KeyValuePair<string,string>[] _values;

        public KeyValuePair<string,string>[] Values
        {
            get { return _values; }
            set
            {
                if (value != _values)
                {
                    _values = value;
                    OnPropertyChanged(() => Values);
                }
            }
        }

        private SystemInfo _info;

        public SystemInfo Info
        {
            get { return _info; }
            set
            {
                _info = value;
                OnPropertyChanged(() => Info);
            }
        }
    }
}