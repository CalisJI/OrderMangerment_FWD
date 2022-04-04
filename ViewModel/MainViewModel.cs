using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagerment_WPF.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedview;
        public BaseViewModel SelectedViewModel
        {
            get => _selectedview;
            set => SetProperty(ref _selectedview, value, nameof(SelectedViewModel));
        }
        public MainViewModel mainViewModel;
        public MainViewModel() 
        {
            mainViewModel = this;
        }
    }
}
