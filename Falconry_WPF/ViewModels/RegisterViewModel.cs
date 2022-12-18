using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Falconry_WPF.Data;
using Falconry_WPF.Models;
using GalaSoft.MvvmLight.Command;

namespace Falconry_WPF.ViewModels
{
    internal class RegisterViewModel
    {
        public RelayCommand RegisterCommand { get; set; }
        public  string Username { get; set; }
        public  string Password { get; set; }
        public  string RepeatedPassword { get; set; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
        }

        public void Register()
        {
            using (DataContext context = new DataContext())
            {
                if (Username != null && Password != null)
                {
                    if (Password == RepeatedPassword)
                    {
                        // TODO: Unique username, fix an issue where u can register duplicate accounts
                        context.Users.Add(new User() { Username = this.Username, Password = this.Password });
                        context.SaveChanges();
                        var w = Application.Current.Windows[1];
                        w.Hide();
                        // TODO: Close Window
                    }
                    else
                    {
                        MessageBox.Show("Passwords do not match", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Not every field has been filled in", "Error");
                }
            }
        }
    }
}
