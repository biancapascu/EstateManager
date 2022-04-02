using System;
using EstateManager.Core;

namespace EstateManager.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CheckinViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public CheckinViewModel CheckinVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CheckinVM = new CheckinViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand((o =>
            {
                CurrentView = HomeVM;
            }));
            CheckinViewCommand = new RelayCommand((o =>
            {
                CurrentView = CheckinVM;
            }));
        }
    }
}
