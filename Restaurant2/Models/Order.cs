using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Restaurant2.Models
{
    public class Order : INotifyPropertyChanged
    {
        private DateTime _finish;

        public DateTime Finish
        {
            get => _finish;
            set
            {
                _finish = value;
                OnPropertyChanged();
            }
        }
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        private int _foodId;
        private Food _food;
        public int FoodId
        {
            get => _foodId;
            set
            {
                _foodId = value;
                OnPropertyChanged();
            }
        }

        public Food Food
        {
            get => _food;
            set
            {
                _food = value;
                OnPropertyChanged();
            }
        }
        private int _userId;
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }
        public User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
