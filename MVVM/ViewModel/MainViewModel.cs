using System;
using EstateManager.Core;

namespace EstateManager.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CheckinViewCommand { get; set; }
        public RelayCommand CheckoutViewCommand { get; set; }
        public RelayCommand ReserveViewCommand { get; set; }
        public RelayCommand UsersViewCommand { get; set; }
        public RelayCommand RecordsViewCommand { get; set; }
        public RelayCommand CatalogViewCommand { get; set; }
        public RelayCommand HistoryViewCommand { get; set; }
        public RelayCommand AccomodationViewCommand { get; set; }
        public RelayCommand ExtrasViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public CheckinViewModel CheckinVM { get; set; }
        public CheckoutViewModel CheckoutVM { get; set; }
        public ReserveViewModel ReserveVM { get; set; }
        public UsersViewModel UsersVM { get; set; }
        public RecordsViewModel RecordsVM { get; set; }
        public CatalogViewModel CatalogVM { get; set; }
        public HistoryViewModel HistoryVM { get; set; }
        public AccomodationViewModel AccomodationVM { get; set; }
        public ExtrasViewModel ExtrasVM { get; set; }


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
            CheckoutVM = new CheckoutViewModel();
            ReserveVM = new ReserveViewModel();
            UsersVM = new UsersViewModel();
            RecordsVM = new RecordsViewModel();
            CatalogVM = new CatalogViewModel();
            HistoryVM = new HistoryViewModel();
            AccomodationVM = new AccomodationViewModel();
            ExtrasVM = new ExtrasViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand((o =>
            {
                CurrentView = HomeVM;
            }));
            CheckinViewCommand = new RelayCommand((o =>
            {
                CurrentView = CheckinVM;
            }));
            CheckoutViewCommand = new RelayCommand((o =>
            {
                CurrentView = CheckoutVM;
            }));
            ReserveViewCommand = new RelayCommand((o =>
            {
                CurrentView = ReserveVM;
            }));
            UsersViewCommand = new RelayCommand((o =>
            {
                CurrentView = UsersVM;
            }));
            RecordsViewCommand = new RelayCommand((o =>
            {
                CurrentView = RecordsVM;
            }));
            CatalogViewCommand = new RelayCommand((o =>
            {
                CurrentView = CatalogVM;
            }));
            HistoryViewCommand = new RelayCommand((o =>
            {
                CurrentView = HistoryVM;
            }));
            AccomodationViewCommand = new RelayCommand((o =>
            {
                CurrentView = AccomodationVM;
            }));
            ExtrasViewCommand = new RelayCommand((o =>
            {
                CurrentView = ExtrasVM;
            }));
        }
    }
}
