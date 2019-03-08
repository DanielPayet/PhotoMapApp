using PhotoMapApp.Models;
using PhotoMapApp.Services.Definitions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace PhotoMapApp.ViewModels
{
	public class NewPostViewModel : ViewModelBase
    {
        private ITagService _tagService;
        private IPostService _postService;
        private IImageService _imageService;
        private IPageDialogService _dialogService;

        private string _name;
        public string Name
        {
            get {
                return _name;
            }
            set { SetProperty(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get {
                return _description;
            }
            set { SetProperty(ref _description, value); }
        }

        private ObservableCollection<Tag> _tags = new ObservableCollection<Tag>();
        public ObservableCollection<Tag> Tags
        {
            get {
                return _tags;
            }
            set { SetProperty(ref _tags, value); }
        }

        private List<Tag> _selectedTags = new List<Tag>();
        public List<Tag> SelectedTags
        {
            get {
                return _selectedTags;
            }
            set { SetProperty(ref _selectedTags, value);}
        }

        private Tag _selectedTag;
        public Tag SelectedTag
        {
            get {
                return _selectedTag;
            }
            set { SetProperty(ref _selectedTag, value); AddToSelectedTags(value); }
        }

        private const string EMPTY_TAG_LIST = "Aucun tag";
        private string _selectedTagsAsString = EMPTY_TAG_LIST;
        public string SelectedTagsAsString
        {
            get { return _selectedTagsAsString; }
            set { SetProperty(ref _selectedTagsAsString, value); }

        }

        private bool _isClearTagSelectedVisible = false;
        public bool IsClearTagSelectedVisible
        {
            get { return _isClearTagSelectedVisible; }
            set { SetProperty(ref _isClearTagSelectedVisible, value); }
        }

        public ImageSource SaveButtonImageSource { get; private set; }
        public ImageSource PictureImageSource { get; private set; }

        private string ImagePath;
        private ImageSource _imagePost;
        public ImageSource ImagePost
        {
            get { return _imagePost; }
            set { SetProperty(ref _imagePost, value); }
        }

        public DelegateCommand ClearTagCommand { get; private set; }
        public DelegateCommand SavePostCommand { get; private set; }
        public DelegateCommand OpenPhotoCommand { get; private set; }

        public NewPostViewModel(INavigationService navigationService, ITagService tagService, IPostService postService, IImageService imageService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Nouveau";
            _tagService = tagService;
            _postService = postService;
            _imageService = imageService;
            Tags = new ObservableCollection<Tag>(_tagService.GetTags());
            SavePostCommand = new DelegateCommand(SavePost);
            OpenPhotoCommand = new DelegateCommand(openPhotoAsync, ()=> ImagePost == null).ObservesProperty(()=> ImagePost);
            this.SaveButtonImageSource = this._imageService.GetSource("Icons.arrowUp.png");
            _dialogService = dialogService;
        }

        private void AddToSelectedTags(Tag value)
        {
            if (value != null) {
                if (!SelectedTags.Contains(value)) {
                    SelectedTags.Add(value);
                    if (SelectedTagsAsString == EMPTY_TAG_LIST) {
                        SelectedTagsAsString = value.Name;
                        IsClearTagSelectedVisible = true;
                    } else {
                        SelectedTagsAsString += ", " + value.Name;
                    }
                }
                SelectedTag = null;
            }
        }

        private void ClearFilter()
        {
            SelectedTags.Clear();
            SelectedTagsAsString = EMPTY_TAG_LIST;
            IsClearTagSelectedVisible = false;
        }

        private void SavePost()
        {
            var post = new Post(Name, Description, SelectedTags, ImagePath, 1.2948848, 43.39494, "Rue du gros prout de Daniel", DateTime.Now);
            _postService.CreatePost(post);
            var navigationParam = new NavigationParameters {{ "post", post }};
            base.NavigationService.NavigateAsync("/MenuNavigation/NavigationPage/ListPostPage/PostPage", navigationParam);
        }

        private async void openPhotoAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
                await _dialogService.DisplayAlertAsync("Aucun appareil photo", "Aucun appareil photo n'est disponible", "OK");
                return;
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
                return;
            ImagePath = file.Path;
            ImagePost = ImageSource.FromFile(ImagePath);
        }
    }
}
