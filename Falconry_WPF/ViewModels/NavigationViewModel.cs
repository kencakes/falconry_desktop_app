using System.Windows;
using System.Windows.Forms;
using Falconry_WPF.Utilities;
using System.Windows.Input;
using Falconry_WPF.Views;
using Falconry_WPF.Data;
using Falconry_WPF.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Application = System.Windows.Application;

namespace Falconry_WPF.ViewModels
{
    class NavigationViewModel: ViewModelBase
    {
        private object _currentView;
        public User User { get; set; }

        public object CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value; 
                OnPropertyChanged();
            }
        }
        
        // For the button bindings
        public ICommand HomeCommand { get; set; }
        public ICommand BirdCommand { get; set; }
        public ICommand HuntingCommand { get; set; }
        public ICommand LogbookCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        

        private void Home(object obj) => CurrentView = new HomeViewModel();
        private void Bird(object obj) => CurrentView = new BirdViewModel(User);
        private void Hunting(object obj) => CurrentView = new HuntingViewModel();
        private void Logbook(object obj) => CurrentView = new LogbookViewModel();


        public NavigationViewModel(User u)
        {
            this.User = u;
            
            HomeCommand = new RelayCommand(Home);
            BirdCommand = new RelayCommand(Bird);
            HuntingCommand = new RelayCommand(Hunting);
            LogbookCommand = new RelayCommand(Logbook);
            LogoutCommand = new RelayCommand(Logout);

            // Startup page
            CurrentView = new HomeView();
        }

        public void Logout(object obj)
        {
            // Sets the user to null, user gets logged out
            this.User = null;
            
            // Opens login window
            LoginViewModel vm = new LoginViewModel();
            MainWindow view = new MainWindow();
            view.DataContext = vm;
            view.Show();
            
            // Closes the homeview
            var window = Application.Current.Windows[1];
            window.Hide();
        }
    }
}