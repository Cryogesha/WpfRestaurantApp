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
    public class RegisterViewModel : INotifyPropertyChanged
    {

        private ApplicationContext _ctx = new ApplicationContext();

        private User _newUser = new User();

        public User NewUser
        {
            get => _newUser;
            set
            {
                _newUser = value;
                OnPropertyChanged();
            }
        }

        private string _resultText;
        public string ResultText
        {
            get => _resultText;
            set
            {
                _resultText = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _registerCommand;

        public RelayCommand RegisterCommand
        {
            get
            {
                return _registerCommand ??= new RelayCommand(obj =>
                {
                    string passwordConf = (string)obj;

                    if (!_ctx.Users.Contains(NewUser) && passwordConf.Equals(NewUser.Password))
                    {
                        _ctx.Users.Add(NewUser);
                        _ctx.SaveChanges();
                        ResultText = "You have registered successfully";
                    }
                    else
                    {
                        ResultText = "Something went wrong. Try again";
                    }
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
