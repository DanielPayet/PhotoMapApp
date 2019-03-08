using PhotoMapApp.Models;
using PhotoMapApp.Services.Definitions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PhotoMapApp.ViewModels
{
	public class NewPostViewModel : ViewModelBase
    {
        private ITagService _tagService;
        private IPostService _postService;
        private IImageService _imageService;
        private IMediaService _mediaService;
        private IGeolocationService _geolocationService;
        private IDatabaseService _databaseService;

        private bool _isNewPostMode;
        public bool IsNewPostMode
        {
            get {
                return _isNewPostMode;
            }
            set { SetProperty(ref _isNewPostMode, value); }
        }
        private Post postEdition { get; set; }

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

        private string ImagePath { get; set; }
        private ImageSource _imagePost;
        public ImageSource ImagePost
        {
            get { return _imagePost; }
            set { SetProperty(ref _imagePost, value); }
        }

        public DelegateCommand ClearTagCommand { get; private set; }
        public DelegateCommand SavePostCommand { get; private set; }
        public DelegateCommand OpenPhotoCommand { get; private set; }

        public NewPostViewModel(
            INavigationService navigationService,
            ITagService tagService,
            IPostService postService,
            IImageService imageService,
            IMediaService mediaService,
            IGeolocationService geolocationService,
            IDatabaseService databaseService
            ) : base(navigationService)
        {
            Title = "Nouveau";
            _tagService = tagService;
            _postService = postService;
            _imageService = imageService;
            _mediaService = mediaService;
            _databaseService = databaseService;
            _geolocationService = geolocationService;
            Tags = new ObservableCollection<Tag>(_tagService.GetTags());
            ClearTagCommand = new DelegateCommand(ClearFilter);
            SavePostCommand = new DelegateCommand(SavePost);
            OpenPhotoCommand = new DelegateCommand(GetPhoto);
            SaveButtonImageSource = _imageService.GetSource("Icons.save.png");
            IsNewPostMode = true;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Post post = (Post) parameters["post"];
            if (post != null) {
                ImagePath = post.Image;
                ImagePost = ImageSource.FromFile(ImagePath);
                Name = post.Name;
                Description = post.Description;
                post.Tags.ForEach(tag => AddToSelectedTags(tag));
                IsNewPostMode = false;
                postEdition = post;
            }
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

        private async void SavePost()
        {
            Post post;
            if (IsNewPostMode) {
                var position = await _geolocationService.GetCurrentPosition();
                post = new Post(Name, Description, SelectedTags, ImagePath, position.Latitude, position.Longitude, "Rue du gros prout de Daniel", DateTime.Now);
                _postService.CreatePost(post);
            } else {
                post = postEdition;
                post.Name = Name;
                post.Description = Description;
                post.Tags = SelectedTags;
                _databaseService.UpdateOrSave(post);
            }
            var navigationParam = new NavigationParameters { { "post", post } };
            await NavigationService.NavigateAsync("/MenuNavigation/NavigationPage/ListPostPage/PostPage", navigationParam);
        }

        private async void GetPhoto()
        {
            var imagePath = await  _mediaService.getImage();
            if(imagePath != null) {
                ImagePath = imagePath;
                ImagePost = ImageSource.FromFile(imagePath);
            }
        }
    }
}
