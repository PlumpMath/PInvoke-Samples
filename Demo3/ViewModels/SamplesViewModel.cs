using SuiteValue.UI.MVVM;

namespace Demo3.ViewModels
{
    public class SamplesViewModel : ViewModelBase
    {
        public SamplesViewModel()
        { 
            GetSetTextViewModel = new GetSetTextViewModel();
            KeyLoggerViewModel = new KeyLoggerViewModel();
            SystemInfoViewModel = new SystemInfoViewModel();

        }

        public SystemInfoViewModel SystemInfoViewModel { get; set; }

        public GetSetTextViewModel GetSetTextViewModel { get; set; }
        public KeyLoggerViewModel KeyLoggerViewModel { get; set; }
        public void Init()
        {
            GetSetTextViewModel.Init();
            SystemInfoViewModel.Init();
        }
    }
}