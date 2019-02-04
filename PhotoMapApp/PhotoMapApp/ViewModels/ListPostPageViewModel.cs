using PhotoMapApp.Models;
using PhotoMapApp.Services.Definitions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoMapApp.ViewModels
{
	public class ListPostPageViewModel : ViewModelBase
	{
        private IPostService _postService;
        private List<Post> _posts = new List<Post>();
        public List<Post> Posts
        {
            get { return _posts; }
            set { SetProperty(ref _posts, value); }
        }
        
        public DelegateCommand<Post> OpenPostCommand { get; private set; } 

        public ListPostPageViewModel(INavigationService navigationService, IPostService postService): base (navigationService)
        {
            this._postService = postService;
            this.Posts = postService.GetPosts();
            OpenPostCommand = new DelegateCommand<Post>(OpenPostDetail);
        }

        private void OpenPostDetail(Post post)
        {
            var navigationParam = new NavigationParameters();
            navigationParam.Add("post", post);
            base.NavigationService.NavigateAsync("PostPage", navigationParam);
        }
	}
}
