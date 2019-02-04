using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using PhotoMapApp.Models;

namespace PhotoMapApp.ViewModels
{
    public class PostPageViewModel : ViewModelBase
    {
        private Post _post;
        public Post Post { get { return this._post; } set { SetProperty(ref this._post, value); }}
        public DelegateCommand DeletePostDelegate { get; private set; }
        public DelegateCommand EditPostDelegate { get; private set; }

        private IPageDialogService _dialogService;
        public PostPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            this._dialogService = dialogService;
            this.DeletePostDelegate = new DelegateCommand(DeletePostCommand);
            this.EditPostDelegate = new DelegateCommand(EditPostCommand);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            this.Post = (Post)parameters["post"];

            this.Post = new Post("Post de test", "Ceci est une description", new List<Tag>(), "null", 1.2948848, 43.39494, "Rue du gros prout de Daniel", new DateTime())
;
            this.Title = Post.Name;
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
