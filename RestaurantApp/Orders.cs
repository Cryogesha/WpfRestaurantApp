using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RestaurantApp
{
    public partial class Orders : INotifyPropertyChanged
    {
        private int orderId;
        private DateTime finish;
        private int foodId;
        public Orders()
        {
            Foods = new HashSet<Foods>();
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

        public virtual Foods Food { get; set; }
        public virtual ICollection<Foods> Foods { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
