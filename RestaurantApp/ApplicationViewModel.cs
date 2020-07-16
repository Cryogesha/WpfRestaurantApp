using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Linq;

namespace RestaurantApp
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Foods selectedFood;

       // public List<string> Categories { get; set; }

        private RestaurantContext context = new RestaurantContext();
        public List<Foods> Foods { get; set; }

        public List<Orders> Orders { get; set; }

        private RelayCommand newOrderCommand;
        public RelayCommand NewOrderCommand
        {
            get
            {
                return newOrderCommand ??= new RelayCommand(obj =>
                {
                    context.Orders.Add(new Orders() { Finish = DateTime.Now.AddHours(1), Food = selectedFood});
                    context.SaveChanges();
                });
            }
        }
        public Foods SelectedFood
        {
            get => selectedFood;

            set
            {
                selectedFood = value;
                OnPropertyChanged();
            }
        }
        public ApplicationViewModel()
        {
            //Categories = context.Foods.Select(f => f.Category).Distinct().ToList();
            Foods = context.Foods.ToList();
            Orders = context.Orders.ToList();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
