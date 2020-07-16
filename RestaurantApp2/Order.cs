using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RestaurantApp2
{
    public partial class Order : INotifyPropertyChanged
    {
        private int orderId;
        private DateTime finish;
        private int foodId;
        public Order()
        {
            Food = new HashSet<Food>();
        }

        public int OrderId 
        {
            get => orderId;
            set
            {
                orderId = value;
                OnPropertyChanged();
            }
        }
        public DateTime Finish 
        {
            get => finish;
            set
            {
                finish = value;
                OnPropertyChanged();
            }
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

        public virtual Food Food { get; set; }
        public virtual ICollection<Food> Foods { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
