using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;

namespace PhotoMapApp.ViewModels
{
    public class AddPostPageViewModel : ViewModelBase
    {
        public AddPostPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
