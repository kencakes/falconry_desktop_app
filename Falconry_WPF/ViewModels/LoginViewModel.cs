using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Falconry_WPF.Data;
using Falconry_WPF.Models;
using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore.Internal;

namespace Falconry_WPF.ViewModels
{
    public class LoginViewModel
    {
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            SubmitCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }
        
        private void Login()
        {
            using (DataContext context = new DataContext())
            {
                bool userfound = context.Users.Any(user => user.Username == Username && user.Password == Password);

                // TODO: Fix mass login
                if (userfound)
                {
                    MessageBox.Show("You have successfully been logged in!", "Success", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("User not found", "Error", MessageBoxButton.OK);
                }
            }
        }
        
        private void Register()
        {
            using (DataContext context = new DataContext())
            {
                if (Username != null && Password != null)
                {
                    // TODOl Unique username, u can make multiple accs with same name now
                    context.Users.Add(new User() { Username = this.Username, Password = this.Password });
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Not every field has been filled in", "Error");
                }
            }
        }
    }
}
