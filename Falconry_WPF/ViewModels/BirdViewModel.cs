using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Interactivity;
using Falconry_WPF.Data;
using Falconry_WPF.Models;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Helpers;

namespace Falconry_WPF.ViewModels
{
    class BirdViewModel : INotifyPropertyChanged
    {
        public RelayCommand AddBirdCommand { get; set; }
        public RelayCommand DeleteBirdCommand { get; set; }
        public RelayCommand UpdateBirdCommand { get; set; }
        public RelayCommand SelectionChangedCommand { get; set; }
        public User User { get; set; }
        public Bird SelectedBird { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Latin { get; set; }
        public List<Bird> Birds { get; set; }
        public ObservableCollection<Logbook> Logbooks { get; set; }

        private int _weight;
        public int Weight
        {
            get { return this._weight; }
            set
            {
                this._weight = value;
                OnPropertyChanged("Weight");
            }
        }
        
        private int _weightafter;
        public int WeightAfter
        {
            get { return this._weightafter; }
            set
            {
                this._weightafter = value;
                OnPropertyChanged("WeightAfter");
            }
        }
        
        private string _foodamount;
        public string FoodAmount
        {
            get { return this._foodamount; }
            set
            {
                this._foodamount = value;
                OnPropertyChanged("FoodAmount");
            }
        }
       

        public BirdViewModel(User user)
        {
            this.User = user;

            AddBirdCommand = new RelayCommand(CreateBird);
            DeleteBirdCommand = new RelayCommand(DeleteBird);
            UpdateBirdCommand = new RelayCommand(UpdateBird);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);

            using (DataContext context = new DataContext())
            {
                // Get users  birds
                Birds = context.Birds.Where(birds => birds.UserId == User.Id).ToList();
                SelectedBird = Birds[0];
                
                // TODO: Only run this if the birds list isn't empty + Get the alst 8 records
                if (Birds != null)
                {
                    // Get the selected birds logbook records
                    Logbooks = new ObservableCollection<Logbook>(context.Logbooks.Where(logbooks => logbooks.BirdId == SelectedBird.Id).ToList());
                
                    // Gets the newest values for the selected bird
                    Weight = Logbooks[Logbooks.Count - 1].Weight;
                    WeightAfter = Logbooks[Logbooks.Count - 1].WeightAfter;
                    FoodAmount = Logbooks[Logbooks.Count - 1].FoodAmount;
                }
            }
        }

        private void SelectionChanged()
        {
            using (DataContext context = new DataContext())
            {
                // TODO: Only run this if the birds list isn't empty
                // Get the selected birds logbooks
                Logbooks = new ObservableCollection<Logbook>(context.Logbooks.Where(logbooks => logbooks.BirdId == SelectedBird.Id).ToList());
                
                // TODO: Only run this if the birds list isn't empty
                // Get the selected birds logbooks
                Logbooks = new ObservableCollection<Logbook>(context.Logbooks.Where(logbooks => logbooks.BirdId == SelectedBird.Id).ToList());
                
                // if the logbooks list isn't empty
                if (Logbooks.Count != 0)
                {
                    Weight = Logbooks[Logbooks.Count - 1].Weight;
                    WeightAfter = Logbooks[Logbooks.Count -  1].WeightAfter;
                    FoodAmount = Logbooks[Logbooks.Count - 1].FoodAmount;
                }
                else
                {
                    Weight = 0;
                    WeightAfter = 0;
                    FoodAmount = "0";
                }
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
