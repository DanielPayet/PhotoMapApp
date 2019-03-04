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
        
        public DelegateCommand<Post> OpenPostCommand { get; private set; } 
        public DelegateCommand inverseListCommand { get; private set; }

        public ListPostPageViewModel(INavigationService navigationService, IPostService postService, IImageService imageService): base (navigationService)
        {
            this._postService = postService;
            this._imageService = imageService;
            this.Posts = new ObservableCollection<Post>(OrderedList(postService.GetPosts()));
            OpenPostCommand = new DelegateCommand<Post>(OpenPostDetail);
            inverseListCommand = new DelegateCommand(ReverseList);
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
    }
}
