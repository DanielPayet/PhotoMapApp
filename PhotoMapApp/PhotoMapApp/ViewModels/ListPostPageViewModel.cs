using PhotoMapApp.Models;
using PhotoMapApp.Services.Definitions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
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

        public DelegateCommand<Post> OpenPostCommand { get; private set; } 
        public DelegateCommand InverseListCommand { get; private set; }
        public DelegateCommand ClearFilterCommand { get; private set; }

        public ListPostPageViewModel(INavigationService navigationService, IPostService postService, IImageService imageService, ITagService tagService): base (navigationService)
        {
            this._postService = postService;
            this._imageService = imageService;
            this.Posts = new ObservableCollection<Post>(OrderedList(postService.GetPosts()));
            this.Tags = tagService.GetTags().ConvertAll((tag)=> new TagView(tag));
            OpenPostCommand = new DelegateCommand<Post>(OpenPostDetail);
            InverseListCommand = new DelegateCommand(ReverseList);
            ClearFilterCommand = new DelegateCommand(ClearFilter);
        }

        private void OpenPostDetail(Post post)
        {
            var navigationParam = new NavigationParameters();
            navigationParam.Add("post", post);
            base.NavigationService.NavigateAsync("PostPage", navigationParam);
        }

        private List<Post> OrderedList(List<Post> posts)
        {
            posts.OrderBy((x) => x.DateTime);
            return posts;
        }

        private void ReverseList()
        {
            this.Posts = new ObservableCollection<Post>(Posts.Reverse());
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
                this.Posts = new ObservableCollection<Post>(_postService.GetPosts());
            } else {
                this.Posts = new ObservableCollection<Post>(
                    _postService.GetPosts()
                    .Where(post => ContainsAll(post.Tags, tagsSelected)));
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
