﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand View2Command { get; set; }

        HomeViewModel HomeView { get; set; }
        ViewModel2 View2 { get; set; }

        private object m_currentView;

        public object CurrentView
        {
            get { return m_currentView; }
            set 
            { 
                m_currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeView = new HomeViewModel();
            View2 = new ViewModel2();

            CurrentView = HomeView;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeView;
            });

            
            View2Command = new RelayCommand(o =>
            {
                CurrentView = View2;
            });

        }
    }
}
