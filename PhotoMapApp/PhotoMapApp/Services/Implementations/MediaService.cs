using PhotoMapApp.Services.Definitions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Services;
using System.Threading.Tasks;

namespace PhotoMapApp.Services.Implementations
{
    public class MediaService: IMediaService
    {
        private IPageDialogService _dialogService;

        public MediaService(IPageDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task<string> getImage()
        {
            var action = await _dialogService.DisplayActionSheetAsync("Que voulez-vous faire ?", "Annuler", null, "Appareil photo", "Galerie");
           
            if(action == "Galerie") {
                return  await pickPhoto();
            } else if ( action == "Appareil photo") {
                return await takePhoto();
            }
            return null;
        }

        private async Task<string> takePhoto()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
                await _dialogService.DisplayAlertAsync("Aucun appareil photo", "Aucun appareil photo n'est disponible", "OK");
                return null;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions {
                Directory = "Pictures",
                SaveToAlbum = true,
                CompressionQuality = 50,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Rear
            });

            if (file == null)
                return null;
           return file.Path;
        }

        private async Task<string> pickPhoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported) {
                await _dialogService.DisplayAlertAsync("Galerie inacessible", "Merci d'activer les permissions", "OK");
                return null; ;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions {
                PhotoSize = PhotoSize.Medium,

            });

            if (file == null)
                return null;

            return file.Path;
        }
    }
}
