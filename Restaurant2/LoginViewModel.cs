using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Restaurant2.Models;
using System.ComponentModel;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Windows;

namespace Restaurant2
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private ApplicationContext _ctx = new ApplicationContext();

        private TextDecorationCollection _decoration = null;

        public TextDecorationCollection Decoration
        {
            get => _decoration;
            set
            {
                _decoration = value;
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
        private RelayCommand _runRegisterWindowCommand;

        public RelayCommand RunRegisterWindowCommand
        {
            get
            {
                return _runRegisterWindowCommand ??= new RelayCommand(obj =>
                {
                    RegisterWindow registerWindow = new RegisterWindow();
                    registerWindow.ShowDialog();
                });
            }
        }

        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??= new RelayCommand(obj =>
                {
                    User user = (from u in _ctx.Users where u.Login == _user.Login && u.Password == _user.Password select u).FirstOrDefault();
                    LoginWindow loginWindow = obj as LoginWindow;
                    if (user != null)
                    {
                        MainWindow mainWindow = new MainWindow
                        {
                            DataContext = new ApplicationViewModel(user)
                        };

                        mainWindow.Show();
                        loginWindow.Close();
                    }
                });
            }
        }

        private RelayCommand _underlineAddCommand;

        public RelayCommand UnderlineAddCommand
        {
            get
            {
                return _underlineAddCommand ??= new RelayCommand(obj =>
                {
                    Decoration = TextDecorations.Underline;
                });
            }
        }
        private RelayCommand _underlineRemoveCommand;
        public RelayCommand UnderlineRemoveCommand
        {
            get
            {
                return _underlineRemoveCommand ??= new RelayCommand(obj =>
                {
                    Decoration = null;
                });
            }
        }
        //INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
