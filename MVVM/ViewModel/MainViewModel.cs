using System;
using EstateManager.Core;

namespace EstateManager.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CheckinViewCommand { get; set; }
        public RelayCommand ReserveViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public CheckinViewModel CheckinVM { get; set; }
        public ReserveViewModel ReserveVM { get; set; }

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
            ReserveVM = new ReserveViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand((o =>
            {
                CurrentView = HomeVM;
            }));
            CheckinViewCommand = new RelayCommand((o =>
            {
                CurrentView = CheckinVM;
            }));
            ReserveViewCommand = new RelayCommand((o =>
            {
                CurrentView = ReserveVM;
            }));
        }
    }
}
