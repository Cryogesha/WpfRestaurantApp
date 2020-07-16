using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Restaurant2.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Restaurant2
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        //Database context
        private ApplicationContext _ctx = new ApplicationContext();

        //Observable collections
        public ObservableCollection<Food> Foods { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Food> _tempFoods;
        public ObservableCollection<Food> TempFoods
        {
            get => _tempFoods;
            set
            {
                _tempFoods = value;
                OnPropertyChanged();
            }
        }

        //Properties
        private Category _selectedCategory;
        private Food _selectedFood;
        private Order _selectedOrder;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public Food SelectedFood
        {
            get => _selectedFood;
            set
            {
                _selectedFood = value;
                OnPropertyChanged();
            }
        }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
            }
        }

        private bool _removeButtonEnabled = false;
        public bool RemoveButtonEnabled
        {
            get => _removeButtonEnabled;
            set
            {
                _removeButtonEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _foodCBEnabled = false;

        public bool FoodCBEnabled
        {
            get => _foodCBEnabled;
            set
            {
                _foodCBEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _orderButtonEnabled = false;
        public bool OrderButtonEnabled
        {
            get => _orderButtonEnabled;
            set
            {
                _orderButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private User _user = new User();

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        //Commands
        private RelayCommand _changeFoodList;

        public RelayCommand ChangeFoodList
        {
            get
            {
                return _changeFoodList ??= new RelayCommand(obj =>
                {
                    var result = from f in Foods where f.Category == SelectedCategory select f;
                    TempFoods = new ObservableCollection<Food>(result);
                    FoodCBEnabled = true;
                });
            }
        }

        private RelayCommand _addOrder;

        public RelayCommand AddOrder
        {
            get
            {
                return _addOrder ??= new RelayCommand(obj =>
                {
                    Order newOrder = new Order() { Finish = DateTime.Now.AddHours(1), FoodId = SelectedFood.Id, UserId = User.Id };
                    _ctx.Orders.Add(newOrder);
                    _ctx.SaveChanges();
                    var orders = from o in _ctx.Orders where o.UserId == User.Id select o;
                    Orders = new ObservableCollection<Order>(orders);
                });
            }
        }

        private RelayCommand _orderSelectChange;

        public RelayCommand OrderSelectChange
        {
            get
            {
                return _orderSelectChange ??= new RelayCommand(obj =>
                {
                    RemoveButtonEnabled = true;
                });
            }
        }

        private RelayCommand _foodCbSelectionChanged;

        public RelayCommand FoodCBSelectionChanged
        {
            get
            {
                return _foodCbSelectionChanged ??= new RelayCommand(obj =>
                {
                    OrderButtonEnabled = true;
                });
            }
        }
        private RelayCommand _removeOrder;

        public RelayCommand RemoveOrder
        {
            get
            {
                return _removeOrder ??= new RelayCommand(obj =>
                {
                    if (SelectedOrder != null)
                    {
                        _ctx.Orders.Remove(SelectedOrder);
                        _ctx.SaveChanges();
                        var orders = from o in _ctx.Orders where o.UserId == User.Id select o;
                        Orders = new ObservableCollection<Order>(orders);
                        RemoveButtonEnabled = false;
                    }
                });
            }
        }

        private RelayCommand _windowClosingCommand;
        public RelayCommand WindowClosingCommand
        {
            get
            {
                return _windowClosingCommand ??= new RelayCommand(obj =>
                {
                    _ctx.Dispose();
                });
            }
        }
        private RelayCommand _exitAccountCommand;

        public RelayCommand ExitAccountCommand
        {
            get
            {
                return _exitAccountCommand ??= new RelayCommand(obj =>
                {
                    MainWindow window = obj as MainWindow;
                    if (window != null)
                    {
                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.Show();
                        window.Close();
                    }
                });
            }
        }

        //Constructor
        public ApplicationViewModel(User user)
        {
            //Initializing
            User = user;
            List<Food> foods = _ctx.Foods.ToList();
            List<Category> categories = _ctx.Categories.ToList();
            Foods = new ObservableCollection<Food>(foods);//_ctx.Foods.Local.ToObservableCollection();
            Categories = new ObservableCollection<Category>(categories); // _ctx.Categories.Local.ToObservableCollection();
            TempFoods = new ObservableCollection<Food>();


            //Initial database data
            if (_ctx.Categories.Count() == 0)
            {
                Category vegetable = new Category() { Name = "Vegetable" };
                Category meat = new Category() { Name = "Meat" };
                _ctx.Categories.AddRange(vegetable, meat);
                _ctx.Foods.Add(new Food() { Name = "Cabbage", Category = vegetable, Price = 10 });
                _ctx.Foods.Add(new Food() { Name = "Carrot", Category = vegetable, Price = 20 });
                _ctx.Foods.Add(new Food() { Name = "Chicken", Category = meat, Price = 30 });
                _ctx.Foods.Add(new Food() { Name = "Pork", Category = meat, Price = 30 });
                _ctx.SaveChanges();
            }
            var orders = from o in _ctx.Orders where o.UserId == User.Id select o;
            Orders = new ObservableCollection<Order>(orders);
        }

        //INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
