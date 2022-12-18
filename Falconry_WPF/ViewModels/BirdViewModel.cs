using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Falconry_WPF.Data;
using Falconry_WPF.Models;
using GalaSoft.MvvmLight.CommandWpf;

namespace Falconry_WPF.ViewModels
{
    public class BirdViewModel
    {
        public RelayCommand AddBirdCommand { get; set; }
        public RelayCommand DeleteBirdCommand { get; set; }
        public RelayCommand UpdateBirdCommand { get; set; }
        public User User { get; set; }
        public Bird SelectedBird { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Latin { get; set; }
        public List<Bird> Birds { get; set; }

        public BirdViewModel(User user)
        {
            this.User = user;

            AddBirdCommand = new RelayCommand(Create);
            DeleteBirdCommand = new RelayCommand(Delete);
            UpdateBirdCommand = new RelayCommand(Update);

            using (DataContext context = new DataContext())
            {
                // TODO: Search by user Id
                Birds = context.Birds.ToList();
            }
        }

        public void Create()
        {
            using (DataContext context = new DataContext())
            {
                string name = this.Name;
                string breed = this.Breed;
                string latin = this.Latin;

                if (name != null && breed != null && latin != null)
                {
                    // TODO: Write to user id
                    context.Birds.Add(new Bird() { Name = name, Breed = breed, Latin = latin});
                    context.SaveChanges();
                    Refresh();
                }
                else
                {
                    MessageBox.Show("You need to fill in all the fields.");
                }
            }
        }

        public void Update()
        {
            using (DataContext context = new DataContext())
            {
                Bird selectedBird = SelectedBird as Bird;
                string name = this.Name;
                string breed = this.Breed;
                string latin = this.Latin;

                if (name != null && breed != null && latin != null)
                {
                    Bird bird = context.Birds.Find(selectedBird.Id);
                    bird.Name = name;
                    bird.Breed = breed;
                    bird.Latin = latin;
                    context.SaveChanges();
                    Refresh();
                }
            }
        }

        public void Delete()
        {
            using (DataContext context = new DataContext())
            {
                Bird selectedBird = SelectedBird as Bird;

                if (selectedBird != null)
                {
                    Bird bird = context.Birds.Single(birds => birds.Id == selectedBird.Id);
                    context.Remove(bird);
                    context.SaveChanges();
                    Refresh();
                }
            }
        }

        public void Refresh()
        {
            using (DataContext context = new DataContext())
            {
                // TODO: Search by user Id
                Birds = context.Birds.ToList();
            }
        }
        
    }
}
