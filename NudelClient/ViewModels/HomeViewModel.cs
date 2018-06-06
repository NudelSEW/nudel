﻿using Nudel.Client.Views;
using System.ComponentModel;
using System.Windows.Controls;

namespace Nudel.Client.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private UserControl currentView;
        public UserControl CurrentView {
            get
            {
                return currentView;
            }
            set
            {
                currentView = value;
                NotifyPropertyChanged("CurrentView");
            }
        }

        public HomeViewModel()
        {
            CurrentView = new EventView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}