using PhotoMapApp.Models;
using PhotoMapApp.Services.Definitions;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace PhotoMapApp.ViewModels
{
	public class ListPostPageViewModel : ViewModelBase
	{
        private IPostService _postService;
        private IImageService _imageService;
        private static List<TagView> _tags = null;
        public List<TagView> Tags
        {
            get {
                return _tags;
            }
            set { SetProperty(ref _tags, value); }
        }

        private static ObservableCollection<Post> _posts = null;
        public ObservableCollection<Post> Posts
        {
            get {
                if (_posts == null) {
                    _posts = new ObservableCollection<Post>();
                }
                return _posts;
            }
            set { SetProperty(ref _posts, value); }
        }

        private TagView _selectedTag;
        public TagView SelectedTag
        {
            get {
                return _selectedTag;
            }
            set { SetProperty(ref _selectedTag, value); AddListFiltre(value);}
        }

        private const string EMPTY_FILTRE = "Aucun filtre";
        private string _tagsSelectedList = EMPTY_FILTRE;
        public string TagsSelectedList
        {
            get {
                return _tagsSelectedList;
            }
            set { SetProperty(ref _tagsSelectedList, value); }
        }

        private bool _isClearFiltreVisible = false;
        public bool IsClearFiltreVisible
        {
            get {
                return _isClearFiltreVisible;
            }
            set { SetProperty(ref _isClearFiltreVisible, value); }
        }

        private bool _isListAscendant = true;
        public bool IsListAscendant
        {
            get {
                return _isListAscendant;
            }
            set { SetProperty(ref _isListAscendant, value); }
        }

        public DelegateCommand<Post> OpenPostCommand { get; private set; } 
        public DelegateCommand ASCListCommand { get; private set; }
        public DelegateCommand DESCListCommand { get; private set; }
        public DelegateCommand ClearFilterCommand { get; private set; }

        public ImageSource ASCButtonImageSource { get; private set; }
        public ImageSource DESCButtonImageSource { get; private set; }

        private bool _isEmptyPost = true;
        public bool IsEmptyPost
        {
            get { return _isEmptyPost; }
            set { SetProperty(ref _isEmptyPost, value); }
        }

        private bool _isActionVisible = false;
        public bool IsActionVisible
        {
            get { return _isActionVisible; }
            set { SetProperty(ref _isActionVisible, value); }
        }

        public ListPostPageViewModel(INavigationService navigationService, IPostService postService, IImageService imageService, ITagService tagService): base (navigationService)
        {
            Title = "Enregistrements";
            _postService = postService;
            _imageService = imageService;
            Tags = tagService.GetTags().ConvertAll((tag)=> new TagView(tag));
            OpenPostCommand = new DelegateCommand<Post>(OpenPostDetail);
            ASCListCommand = new DelegateCommand(OrderByASC, ()=> !IsListAscendant).ObservesProperty(() => IsListAscendant);
            DESCListCommand = new DelegateCommand(OrderByDESC, () => IsListAscendant).ObservesProperty(() => IsListAscendant);
            ClearFilterCommand = new DelegateCommand(ClearFilter);
            ASCButtonImageSource = _imageService.GetSource("Icons.arrowDown.png");
            DESCButtonImageSource = _imageService.GetSource("Icons.arrowUp.png");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            OrderedList(_postService.GetPosts());
            checkIfPostsIsEmpty();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            OrderedList(_postService.GetPosts());
            checkIfPostsIsEmpty();
        }

        private void OpenPostDetail(Post post)
        {
            var navigationParam = new NavigationParameters { { "post", post } };
            NavigationService.NavigateAsync("PostPage", navigationParam);
        }

        private void OrderedList(List<Post> posts)
        {
            if (IsListAscendant) {
                Posts = new ObservableCollection<Post>(posts.OrderBy((x) => x.DateTime.TimeOfDay));
            } else {
                Posts = new ObservableCollection<Post>(posts.OrderByDescending((x) => x.DateTime.TimeOfDay));
            }
        }

        private void OrderedList()
        {
            if (IsListAscendant) {
                Posts = new ObservableCollection<Post>(Posts.OrderBy((x) => x.DateTime.TimeOfDay));
            } else {
                Posts = new ObservableCollection<Post>(Posts.OrderByDescending(x => x.DateTime.TimeOfDay));
            }
        }

        private void OrderByASC()
        {
            IsListAscendant = true;
            OrderedList();
        }

        private void OrderByDESC()
        {
            IsListAscendant = false;
            OrderedList();
        }

        private void checkIfPostsIsEmpty()
        {
            if (Posts.Count > 0) {
                IsEmptyPost = false;
                IsActionVisible = true;
            } else {
                IsEmptyPost = true;
                IsActionVisible = false;
            }
        }

        private void AddListFiltre(TagView tag)
        {
            if (tag != null && !tag.IsSelected) {
                tag.IsSelected = true;
                if (TagsSelectedList != EMPTY_FILTRE) {
                    TagsSelectedList += ", ";
                } else {
                    TagsSelectedList = "";
                    IsClearFiltreVisible = true;
                }
                TagsSelectedList += tag.Tag.Name;
            }
            UpdatePost();
        }

        private void UpdatePost()
        {
            List<Tag> tagsSelected = Tags.FindAll(tag => tag.IsSelected).ConvertAll(tagview => tagview.Tag);
            if (!tagsSelected.Any()) {
                OrderedList(_postService.GetPosts());
            } else {
                OrderedList(
                    _postService.GetPosts()
                    .Where(post => ContainsAll(post.Tags, tagsSelected)).ToList());
            }
        }

        private static bool ContainsAll(List<Tag> tagsFromPost, List<Tag> tags)
        {
            return tags.TrueForAll(tag => tagsFromPost.Contains(tag));
        }

        private void ClearFilter()
        {
            IsClearFiltreVisible = false;
            Tags.FindAll(tag => tag.IsSelected).ForEach((tag) => tag.IsSelected = false);
            SelectedTag = null;
            TagsSelectedList = EMPTY_FILTRE;
            UpdatePost();
        }
    }

    public class TagView
    {
        public Tag Tag { get; set; }
        public bool IsSelected { get; set; }

        public TagView(Tag tag)
        {
            this.Tag = tag;
            this.IsSelected = false;
        }
    }
}
