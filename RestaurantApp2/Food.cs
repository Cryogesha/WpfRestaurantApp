using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RestaurantApp2
{
    public partial class Food : INotifyPropertyChanged
    {
        private int foodId;
        private string name;
        private int price;
        private int? orderId;
        private string category;
        public Food()
        {
            Orders = new HashSet<Order>();
        }

        public int FoodId
        {
            get => foodId;
            set
            {
                foodId = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public int Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }
        public int? OrderId
        {
            get => orderId;
            set
            {
                orderId = value;
                OnPropertyChanged();
            }
        }
        public string Category
        {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged();
            }
        }

        public virtual Order Order { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public static implicit operator Food(HashSet<Food> v)
        {
            throw new NotImplementedException();
        }
    }
}
