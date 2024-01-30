using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand GameViewCommand { get; set; }

        HomeViewModel HomeView {  get; set; }
        GameViewModel GameView { get; set; }

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
            GameView = new GameViewModel();

            CurrentView = HomeView;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeView;
            });

            GameViewCommand = new RelayCommand(o =>
            {
                CurrentView = GameView;
            });

        }

        public void test()
        {
            CurrentView = GameView;
        }



    }
}