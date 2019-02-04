using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoMapApp.ViewModels
{
    public class MainPageViewModel: ViewModelBase
    {

        public DelegateCommand NavigateToPostPageDelegate { get; private set; }

        public MainPageViewModel(INavigationService navigationService) : base(navigationService) {
            Title = "Accueil";
            this.NavigateToPostPageDelegate = new DelegateCommand(NavigateToPostPageAction);
        }

        private void NavigateToPostPageAction()
        {
            NavigationService.NavigateAsync("PostPage");
        }
    }
}
