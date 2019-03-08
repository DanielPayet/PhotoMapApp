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

        public DelegateCommand NavigateToNewPostPageDelegate { get; private set; }

        public MainPageViewModel(INavigationService navigationService) : base(navigationService) {
            Title = "Accueil";
            this.NavigateToNewPostPageDelegate = new DelegateCommand(NavigateToNewPostPageAction);
        }

        private void NavigateToNewPostPageAction()
        {
            NavigationService.NavigateAsync("NewPost");
        }
    }
}
