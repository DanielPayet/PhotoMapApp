using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoMapApp.ViewModels
{
	public class MenuNavigationViewModel : ViewModelBase
	{
        private INavigationService _navigationService;
        public DelegateCommand<string> NaviagtionCommand { get; private set; }
        public MenuNavigationViewModel(INavigationService navigationService): base(navigationService)
        {
            this._navigationService = navigationService;
            this.NaviagtionCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string route)
        {
            _navigationService.NavigateAsync("NavigationPage/" + route);
        }
	}
}
