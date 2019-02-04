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
    public class PostPageViewModel : ViewModelBase
    {
        public string test { get; private set;}
        public DelegateCommand DeletePostDelegate { get; private set; }
        public DelegateCommand EditPostDelegate { get; private set; }

        private IPageDialogService _dialogService;
        public PostPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            this.test = "coucou";
            this._dialogService = dialogService;
            this.DeletePostDelegate = new DelegateCommand(DeletePostCommand);
            this.EditPostDelegate = new DelegateCommand(EditPostCommand);
        }

        private async void DeletePostCommand()
        {
            var answer = await _dialogService.DisplayAlertAsync("Suppression de post", "Voulez-vous vraiment supprimer ce post ?", "Supprimer", "Annuler");
            if (answer == true)
            {
                // TODO : SUPPRIMER LE POST
                await _dialogService.DisplayAlertAsync("Alert", "OUI", "ok");
            }
        }

        private void EditPostCommand()
        {
           // TODO : Ouvrir page de modif
        }
    }
}
