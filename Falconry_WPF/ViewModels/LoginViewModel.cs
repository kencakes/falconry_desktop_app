using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Falconry_WPF.Data;
using Falconry_WPF.Models;
using GalaSoft.MvvmLight.Command;
using MessageBox = System.Windows.MessageBox;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

namespace Falconry_WPF.ViewModels
{
    public class LoginViewModel
    {
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand ResetPasswordCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        
        public User User { get; set; }


        public LoginViewModel()
        {
            SubmitCommand = new RelayCommand(Login);
            ResetPasswordCommand = new RelayCommand(ResetPassword);
            RegisterCommand = new RelayCommand(Register);
            
            // Initialise database on startup if it doens't exist
            DatabaseFacade facade = new DatabaseFacade(new DataContext());
            facade.EnsureCreated();
        }

        private void Login()
        {
            User u = new User();
            using (DataContext context = new DataContext())
            {
                bool userfound = context.Users.Any(user => user.Username == Username && user.Password == Password);

                if (userfound)
                {
                    u = context.Users.SingleOrDefault(user => user.Username == Username);
                    
                    NavigationViewModel vm = new NavigationViewModel(u);
                    Views.MainView view = new Views.MainView();
                    view.DataContext = vm;
                    view.Show();
                    
                    // Close login window
                    var window = Application.Current.Windows[0];
                    window.Hide();
                }
                else
                {
                    MessageBox.Show("User not found", "Error", MessageBoxButton.OK);
                }
            }
        }

        private void ResetPassword()
        {
            ResetPasswordViewModel vm = new ResetPasswordViewModel();
            Views.ResetPasswordView view = new Views.ResetPasswordView();
            view.DataContext = vm;
            view.Show();
        }

        private void Register()
        {
            RegisterViewModel vm = new RegisterViewModel();
            Views.RegisterView view = new Views.RegisterView();
            view.DataContext = vm;
            view.Show();
        }
    }
}
