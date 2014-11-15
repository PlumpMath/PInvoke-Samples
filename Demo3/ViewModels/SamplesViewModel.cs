using SuiteValue.UI.MVVM;

namespace Demo3.ViewModels
{
    public class SamplesViewModel : ViewModelBase
    {
        public SamplesViewModel()
        { 
            GetSetTextViewModel = new GetSetTextViewModel();
            
        }
        public GetSetTextViewModel GetSetTextViewModel { get; set; }

        public void Init()
        {
            GetSetTextViewModel.Init();
        }
    }
}