using System.Windows.Forms;
using Falconry_WPF.Utilities;
using System.Windows.Input;
using Falconry_WPF.Views;
using Falconry_WPF.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Falconry_WPF.ViewModels
{
    class NavigationViewModel: ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        
        // For the button bindings
        public ICommand HomeCommand { get; set; }
        public ICommand BirdCommand { get; set; }
        public ICommand HuntingCommand { get; set; }
        public ICommand LogbookCommand { get; set; }
        // TODO: Register, Login, Reset password

        private void Home(object obj) => CurrentView = new HomeViewModel();
        private void Bird(object obj) => CurrentView = new BirdViewModel();
        private void Hunting(object obj) => CurrentView = new HuntingViewModel();
        private void Logbook(object obj) => CurrentView = new LogbookViewModel();
        

        public NavigationViewModel()
        {
            HomeCommand = new RelayCommand(Home);
            BirdCommand = new RelayCommand(Bird);
            HuntingCommand = new RelayCommand(Hunting);
            LogbookCommand = new RelayCommand(Logbook);
            
            // Startup page
            CurrentView = new HomeView();

            // Initialise database on startup if it doens't exist
            DatabaseFacade facade = new DatabaseFacade(new DataContext());
            facade.EnsureCreated();
        }
    }
}