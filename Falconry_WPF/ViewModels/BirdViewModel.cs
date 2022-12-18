using System;
using System.Collections.Generic;
using System.Linq;
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
        public int Weight { get; set; }
        public int WeightAfter { get; set; }
        public string FoodAmount { get; set; }
        public List<Bird> Birds { get; set; }
        public List<Logbook> Logbooks { get; set; }
       

        public BirdViewModel(User user)
        {
            this.User = user;

            AddBirdCommand = new RelayCommand(CreateBird);
            DeleteBirdCommand = new RelayCommand(DeleteBird);
            UpdateBirdCommand = new RelayCommand(UpdateBird);

            using (DataContext context = new DataContext())
            {
                // Get users  birds
                Birds = context.Birds.Where(birds => birds.UserId == User.Id).ToList();
                SelectedBird = Birds[0];
                
                // TODO: Only run this if the birds list isn't empty
                // Get the selected birds logbooks
                Logbooks = context.Logbooks.Where(logbooks => logbooks.BirdId == SelectedBird.Id).ToList();
                
                // Gets the newest values for the selected bird
                Weight = Logbooks[Logbooks.Count - 1].Weight;
                WeightAfter = Logbooks[Logbooks.Count - 1].WeightAfter;
                FoodAmount = Logbooks[Logbooks.Count - 1].FoodAmount;
            }
        }
        
        
        public void CreateBird()
        {
            using (DataContext context = new DataContext())
            {
                Bird bird = new Bird();
                bird.Name = Name;
                bird.Breed = Breed;
                bird.Latin = Latin;
                bird.UserId = User.Id;

                if (Name != null && Breed != null && Latin != null)
                {
                    // TODO: Write to user id
                    context.Birds.Add(bird);
                    context.SaveChanges();
                    MessageBox.Show("Bird added");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("You need to fill in all the fields.");
                }
            }
        }

        public void UpdateBird()
        {
            string name = this.Name;
            string breed = this.Breed;
            string latin = this.Latin;
            
            using (DataContext context = new DataContext())
            {
                Bird selectedBird = SelectedBird as Bird;
                if (name != null && breed != null && latin != null)
                {
                    Bird bird = context.Birds.Find(selectedBird.Id);
                    bird.Name = name;
                    bird.Breed = breed;
                    bird.Latin = latin;
                    context.SaveChanges();
                    Refresh();
                    Clear();
                }
            }
        }

        private void DeleteBird()
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
                    Clear();
                }
            }
        }

        private void Refresh()
        {
            using (DataContext context = new DataContext())
            {
                // TODO: Search by user Id
                Birds = context.Birds.ToList();
            }
        }
        // TODO: Clear method to empty fields after create/delete/update
        private void Clear()
        {
            this.Name = null;
            this.Breed = null;
            this.Latin = null;
        }
    }
}
